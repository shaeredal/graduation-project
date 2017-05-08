using System.Web.Http;
using OnlinerNotifier.BLL.Models.SearchDataModels;
using OnlinerNotifier.BLL.Services.Interfaces.SearchServices;

namespace OnlinerNotifier.Controllers.RequestControllers
{
    public class KupiTutByController : ApiController
    {
        private readonly IKupiTutBySearchService kupiTutBySearchService;

        public KupiTutByController(IKupiTutBySearchService kupiTutBySearchService)
        {
            this.kupiTutBySearchService = kupiTutBySearchService;
        }

        [HttpGet]
        public SearchResult Get([FromUri]string name)
        {
            return kupiTutBySearchService.Search(name);
        }
    }
}
