using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAuth2;
using OAuth2.Client;
using OAuth2.Models;
using OnlinerNotifier.BLL.Redis;
using OnlinerNotifier.BLL.Services.Interfaces.UserServices;

namespace OnlinerNotifier.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IUserManageService userService;

        private readonly AuthorizationRoot authorizationRoot;

        public AuthorizationController(AuthorizationRoot authorizationRoot, IUserManageService userService)
        {
            this.authorizationRoot = authorizationRoot;
            this.userService = userService;
        }

        public ActionResult GetAuthUrl(string providerName)
        {
            return Json( new { Url = GetClient(providerName).GetLoginLinkUri() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Auth(string providerName)
        {
            UserInfo userInfo;
            var client = GetClient(providerName);
            try
            {
                userInfo = client.GetUserInfo(Request.QueryString);
            }
            catch (Exception)
            {
                return Redirect("/auth");
            }
            var userId = userService.GetOrCreate(userInfo);
            var key = GetKey(providerName, client);
            SetAuthParameters(userId.ToString(), key);
            return Redirect("/home");
        }

        private void SetAuthParameters(string id, string key)
        {
            SetStorage(id, key);
            SetCookies(id, key);
        }

        private void SetStorage(string id, string key)
        {
            RedisConnector.Connection.GetDatabase().StringSet($"user:{id}:key", key, TimeSpan.FromHours(1));
        }

        private void SetCookies(string id, string key)
        {
            var idCookie = new HttpCookie("User", id);
            var keyCookie = new HttpCookie("Key", key);
            Response.Cookies.Add(idCookie);
            Response.Cookies.Add(keyCookie);
        }

        private IClient GetClient(string providerName)
        {
            return authorizationRoot.Clients.First(c => c.Name == providerName);
        }

        private string GetKey(string providerName, IClient client)
        {
            switch (providerName)
            {
                case "Vkontakte":
                case "Facebook":
                    return ((OAuth2Client) client).AccessToken;
                case "Twitter":
                    return ((OAuthClient) client).AccessToken;
            }
            throw new Exception("Unsupported provider name.");
        }
    }
}
