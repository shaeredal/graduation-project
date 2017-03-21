using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OnlinerNotifier.DAL.Models;
using OnlinerNotifier.DAL.Repositories.Interfaces;

namespace OnlinerNotifier.DAL.Repositories
{
    public class ProductPriceChangeRepository : IRepository<ProductPriceChange>
    {
        private readonly Context db;

        public ProductPriceChangeRepository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<ProductPriceChange> GetAll()
        {
            return db.PriceChanges;
        }

        public ProductPriceChange Get(int id)
        {
            return db.PriceChanges.Include(price => price.Product).SingleOrDefault(x => x.Id == id);
        }

        public void Create(ProductPriceChange item)
        {
            db.PriceChanges.Add(item);
        }

        public bool Update(ProductPriceChange item)
        {
            if (db.PriceChanges.Contains(item))
            {
                db.Entry(item).State = EntityState.Modified;
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            ProductPriceChange price = db.PriceChanges.Find(id);
            if (price != null)
            {
                db.PriceChanges.Remove(price);
                return true;
            }
            return false;
        }
    }
}
