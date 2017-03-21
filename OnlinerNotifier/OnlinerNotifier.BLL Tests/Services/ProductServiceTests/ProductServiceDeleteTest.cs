using Moq;
using NUnit.Framework;

namespace OnlinerNotifier.BLL_Tests.Services.ProductServiceTests
{
    public class ProductServiceDeleteTest : ProductServiceTestBase
    {
        [SetUp]
        public void SetUp()
        {
            unitOfWorkMock.Setup(m => m.Users.Get(1)).Returns(() => userMock.Object);
            unitOfWorkMock.Setup(m => m.Products.Get(1)).Returns(() => productMock.Object);
            unitOfWorkMock.Setup(m => m.Users.Delete(1));
            unitOfWorkMock.Setup(m => m.Products.Delete(1)).Returns(() => true);
        }

        [Test]
        public void Delete_DeletingProduct_ProductIsDeleted()
        {
            productService.RemoveUserProduct(1, 1);

            unitOfWorkMock.Verify(m => m.Users.Get(1));
            unitOfWorkMock.Verify(m => m.Products.Get(1));
            unitOfWorkMock.Verify(m => m.UserProducts.Delete(1), Times.Once);
            unitOfWorkMock.Verify(m => m.Products.Delete(1), Times.Once);
        }

        [TestCase(1, 1, ExpectedResult = true)]
        [TestCase(1, 2, ExpectedResult = false)]
        [TestCase(2, 1, ExpectedResult = false)]
        [TestCase(2, 2, ExpectedResult = false)]
        public bool Delete_ReturnValue_IsCorrect(int userId, int productId)
        {
            return productService.RemoveUserProduct(userId, productId);
        }
    }
}
