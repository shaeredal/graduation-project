using NUnit.Framework;

namespace OnlinerNotifier.BLL_Tests.Services.ProductServiceTests
{
    public class ProductServiceGetUserProductsTest : ProductServiceTestBase
    {
        [Test]
        public void GetUserProducts_GetData_ExpectedData()
        {
            var result = productService.GetUserProducts(1);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("TestProductName", result[0].Name);
        }
    }
}
