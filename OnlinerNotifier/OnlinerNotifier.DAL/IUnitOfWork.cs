using System;
using OnlinerNotifier.DAL.Models;
using OnlinerNotifier.DAL.Repositories.Interfaces;

namespace OnlinerNotifier.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<ProductPriceChange> PriceCanges { get; }
        IRepository<Product> Products { get; }
        IRepository<UserProduct> UserProducts { get; }
        IUserRepository Users { get; }

        void Dispose(bool disposing);
        void Save();
    }
}