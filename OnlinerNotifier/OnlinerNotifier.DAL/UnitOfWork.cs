using System;
using OnlinerNotifier.DAL.Models;
using OnlinerNotifier.DAL.Repositories;
using OnlinerNotifier.DAL.Repositories.Interfaces;

namespace OnlinerNotifier.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context db = new Context();
        private UserRepository userRepository;
        private ProductRepository productRepository;
        private ProductPriceChangeRepository productPriceChangeRepository;
        private UserProductRepository userProductRepository;

        public IUserRepository Users => userRepository ?? (userRepository = new UserRepository(db));

        public IRepository<Product> Products => productRepository ?? (productRepository = new ProductRepository(db));

        public IRepository<ProductPriceChange> PriceCanges => productPriceChangeRepository ??
                                                              (productPriceChangeRepository = new ProductPriceChangeRepository(db));

        public IRepository<UserProduct> UserProducts => userProductRepository ?? (userProductRepository = new UserProductRepository(db));

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
