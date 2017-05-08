using System.Linq;
using NUnit.Framework;
using OnlinerNotifier.BLL.Services.Implementations;
using OnlinerNotifier.BLL.Services.Implementations.SearchServices;
using OnlinerNotifier.BLL.Services.Interfaces;
using OnlinerNotifier.BLL.Services.Interfaces.SearchServices;

namespace OnlinerNotifier.BLL_Tests.Services
{
    [TestFixture]
    public class OnlinerSearchServiceTest
    {
        private IOnlinerSearchService onlinerSearchService;

        [SetUp]
        public void Setup()
        {
            onlinerSearchService = new OnlinerSearchService();
        }

        [Test]
        public void Search_ProductsSearching_GetProducts()
        {
            var result = onlinerSearchService.Search("Samsung");

            Assert.IsTrue(result.Products.ToList()[0].FullName.Contains("Samsung"));
        }
    }
}
