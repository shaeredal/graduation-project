using System;
using System.Collections.Generic;
using System.Net.Mail;
using Moq;
using OnlinerNotifier.BLL.Models.OnlinerDataModels;
using OnlinerNotifier.BLL.Services.Interfaces;
using OnlinerNotifier.BLL.SmtpClients;
using OnlinerNotifier.DAL;
using OnlinerNotifier.DAL.Models;
using OnlinerNotifier.DAL.Repositories.Interfaces;

namespace OnlinerNotifier.BLL_Tests.Moq
{
    public class MockStorage
    {
        public MockStorage()
        {
            GenerateProductMock();
            GenerateUserMock();
            GenerateUserRepositoryMock();
            GenerateProductRepositoryMock();
            GenerateOnlinerServiceMock();
            GenerateUnitOfWorkMock();
            GenerateSmtpClientMock();
        }

        public Mock<IUserRepository> UserRepositoryMock { get; private set; }

        public Mock<User> UserMock { get; private set; }

        public Mock<Product> ProductMock { get; private set; }

        public Mock<UserProduct> UserProductMock { get; private set; }

        public Mock<IRepository<Product>> ProductRepositoryMock { get; private set; }

        public Mock<IOnlinerSearchService> OnlinerSearchServiceMock { get; private set; }

        public Mock<IUnitOfWork> UnitOfWorkMock { get; private set; }

        public Mock<ISmtpClient> SmtpClientMock { get; private set; }

        private void GenerateUserRepositoryMock()
        {
            UserRepositoryMock = new Mock<IUserRepository>();
            UserRepositoryMock.Setup(ur => ur.Get(1)).Returns(() => UserMock.Object);
            UserRepositoryMock.Setup(ur => ur.Get(It.Is<int>(i => i != 1))).Returns(() => null);
            var userList = GenerateUserList();
            UserRepositoryMock.Setup(ur => ur.GetAll()).Returns(() => userList);
            var deepUserList = GenerateUserListDeep();
            UserRepositoryMock.Setup(ur => ur.GetAllDeep()).Returns(() => deepUserList);
            UserRepositoryMock.Setup(ur => ur.Create(It.IsAny<User>()));
            UserRepositoryMock.Setup(ur => ur.Create(It.IsAny<User>()));
        }

        private void GenerateUserMock()
        {
            UserMock = new Mock<User>();
            UserMock.Object.Id = 1;
            UserMock.Object.FirstName = "TestName";
            UserMock.Object.LastName = "TestLastName";
            UserMock.Object.Email = "User@email.test";
            UserMock.Object.EnableNotifications = true;
            GenerateUserProductMock(ProductMock.Object, UserMock.Object);
            UserMock.Object.UserProducts.Add(UserProductMock.Object);
        }

        private void GenerateUserProductMock(Product product, User user)
        {
            UserProductMock = new Mock<UserProduct>();
            UserProductMock.Object.Id = 1;
            UserProductMock.Object.Product = product;
            UserProductMock.Object.User = user;
            UserProductMock.Object.IsTracked = true;
        }

        private void GenerateProductMock()
        {
            ProductMock = new Mock<Product>();
            ProductMock.Object.Id = 1;
            ProductMock.Object.Name = "TestProductName";    
        }

        private List<User> GenerateUserList()
        {
            var userList = new List<User>();
            userList.Add(new User() { Id = 1, SocialId = "qwerty" });
            userList.Add(new User() { Id = 2, SocialId = "7788" });
            userList.Add(new User() { Id = 42, SocialId = "not 42" });
            return userList;
        }

        private List<User> GenerateUserListDeep()
        {
            var priceChanges = new List<ProductPriceChange>
            {
                new ProductPriceChange() {NewMinPrice = 100, NewMaxPrice = 200, OldMinPrice = 99, OldMaxPrice = 199, CheckTime = DateTime.Now},
                new ProductPriceChange() {NewMinPrice = 101, NewMaxPrice = 201, OldMinPrice = 100, OldMaxPrice = 200, CheckTime = DateTime.Now},
                new ProductPriceChange() {NewMinPrice = 101, NewMaxPrice = 201, OldMinPrice = 100, OldMaxPrice = 200,
                    CheckTime = DateTime.Now - TimeSpan.FromDays(5)}
            };
            var product = new Product() { PriceChanges = priceChanges };
            var userProducts = new List<UserProduct> {new UserProduct() {IsTracked = true, Product = product}};
            var userList = new List<User> {new User() {Id = 33, UserProducts = userProducts, Email = "valid_address@it.is", EnableNotifications = true}};
            return userList;
        }

        private void GenerateProductRepositoryMock()
        {
            ProductRepositoryMock = new Mock<IRepository<Product>>();
            ProductRepositoryMock.Setup(pr => pr.Get(1)).Returns(() => ProductMock.Object);
            var productList = GenerateProductList();
            ProductRepositoryMock.Setup(pr => pr.GetAll()).Returns(() => productList);
            ProductRepositoryMock.Setup(pr => pr.Get(100)).Returns(() => productList[0]);
            ProductRepositoryMock.Setup(pr => pr.Create(It.IsAny<Product>()));
        }

        private List<Product> GenerateProductList()
        {
            var productList = new List<Product>();
            productList.Add(new Product() { Id = 100, OnlinerId = 12345});
            productList.Add(new Product() { Id = 200, OnlinerId = 33});
            productList.Add(new Product() { Id = 300, OnlinerId = 1122 });
            return productList;
        }

        private void GenerateOnlinerServiceMock()
        {
            OnlinerSearchServiceMock = new Mock<IOnlinerSearchService>();
            var searchResult = GenerateSearcResult();
            OnlinerSearchServiceMock.Setup(m => m.Search(It.IsAny<string>())).Returns(() => searchResult);
        }

        private SearchResultOnliner GenerateSearcResult()
        {
            var productList = new List<ProductOnliner>();
            productList.Add(new ProductOnliner()
            {
                FullName = "TestProduct1",
                Id = 12345,
                Prices = new PriceOnliner()
                {
                    Max = 70000,
                    Min = 50000
                }
            });
            productList.Add(new ProductOnliner()
            {
                FullName = "TestProduct2",
                Id = 12,
                Prices = new PriceOnliner()
                {
                    Max = 80000,
                    Min = 40000
                }
            });
            var searchResult = new SearchResultOnliner()
            {
                Products = productList
            };
            return searchResult;
        }

        private void GenerateUnitOfWorkMock()
        {
            UnitOfWorkMock = new Mock<IUnitOfWork>();
            UnitOfWorkMock.Setup(m => m.Users).Returns(UserRepositoryMock.Object);
            UnitOfWorkMock.Setup(m => m.Products).Returns(ProductRepositoryMock.Object);
            var userProducts = new List<UserProduct>();
            userProducts.Add(UserProductMock.Object);
            UnitOfWorkMock.Setup(m => m.UserProducts.GetAll()).Returns(() => userProducts);
            UnitOfWorkMock.Setup(m => m.UserProducts.Get(1)).Returns(() => UserProductMock.Object);
        }

        private void GenerateSmtpClientMock()
        {
            SmtpClientMock = new Mock<ISmtpClient>();
            SmtpClientMock.Setup(m => m.Send(It.IsAny<MailMessage>()));
        }
    }
}
