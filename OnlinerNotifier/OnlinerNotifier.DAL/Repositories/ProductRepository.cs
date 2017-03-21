using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OnlinerNotifier.DAL.Models;
using OnlinerNotifier.DAL.Repositories.Interfaces;

namespace OnlinerNotifier.DAL.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly Context db;

        public ProductRepository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return db.Products;
        }

        public Product Get(int id)
        {
            return db.Products.Include(prod => prod.UserProducts.Select(up => up.User)).SingleOrDefault(x => x.Id == id);
        }

        public void Create(Product item)
        {
            db.Products.Add(item);
        }

        public bool Update(Product item)
        {
            if (db.Products.Contains(item))
            {
                db.Entry(item).State = EntityState.Modified;
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            Product product = db.Products.Find(id);
            if (product != null)
            {
                db.Products.Remove(product);
                return true;
            }
            return false;
        }
    }
}
