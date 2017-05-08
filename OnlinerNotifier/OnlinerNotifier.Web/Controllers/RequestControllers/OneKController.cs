using System.Web.Http;
using OnlinerNotifier.BLL.Models.SearchDataModels;
using OnlinerNotifier.BLL.Services.Interfaces.SearchServices;

namespace OnlinerNotifier.Controllers.RequestControllers
{
    public class OneKController : ApiController
    {
        private readonly IOneKSearchService oneKSearchService;

        public OneKController(IOneKSearchService oneKSearchService)
        {
            this.oneKSearchService = oneKSearchService;
        }

        [HttpGet]
        public SearchResult Get([FromUri]string name)
        {
            return oneKSearchService.Search(name);
        }
    }
}