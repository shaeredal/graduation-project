using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using OnlinerNotifier.BLL.Redis;

namespace OnlinerNotifier.Filters
{
    public class AuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple => false;

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {

            var userIdCookie = context.Request.Headers.GetCookies("User").FirstOrDefault();
            var userKeyCookie = context.Request.Headers.GetCookies("Key").FirstOrDefault();
            if (userIdCookie != null && userKeyCookie != null)
            {
                int userId;
                if (int.TryParse(userIdCookie["User"].Value, out userId))
                {
                    var key = RedisConnector.Connection.GetDatabase().StringGet($"user:{userId}:key");
                    if (!key.IsNull && key == userKeyCookie["Key"].Value)
                    {
                        context.Principal = new Principal(userId);
                    }
                    else
                    {
                        context.ErrorResult = new AuthenticationFailureResult("Key is not match.", context.Request);
                    }
                }
                else
                {
                    context.ErrorResult = new AuthenticationFailureResult("Incorrect cookie.", context.Request);
                }
            }
            else
            {
                context.ErrorResult = new AuthenticationFailureResult("Cookie is missing.", context.Request);
            }
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public void Challenge(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
        }
    }
}