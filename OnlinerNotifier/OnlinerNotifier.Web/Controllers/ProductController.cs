using System.Collections.Generic;
using System.Web.Http;
using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.BLL.Services.Interfaces.UserProductServices;
using OnlinerNotifier.Filters;
using OnlinerNotifier.ToastNotifier;

namespace OnlinerNotifier.Controllers
{
    [Authentication]
    public class ProductController : ApiControllerBase
    {
        private readonly IUserProductService productService;

        private readonly IToastNotifier toastNotifier;

        public ProductController(IUserProductService productService, IToastNotifier toastNotifier)
        {
            this.productService = productService;
            this.toastNotifier = toastNotifier;
        }

        public List<ProductViewModel> Get()
        {
            return productService.GetUserProducts(Principal.Id);
        }

        public IHttpActionResult Post(ProductViewModel product)
        {
            var userId = Principal.Id;
            if (productService.AddUserProduct(product, userId))
            {
                SendNotification(product.Name, "is added.");
                return Ok();
            }
            return Conflict();
        }

        public IHttpActionResult Delete(int id)
        {
            if (productService.RemoveUserProduct(Principal.Id, id))
            {
                SendNotification("Product", "is deleted."); //TODO: get product name
                return Ok();
            }
            return NotFound();
        }

        private void SendNotification(string productName, string message)
        {
            toastNotifier.Send(Request, $"{productName} {message}");
        }
    }
}
