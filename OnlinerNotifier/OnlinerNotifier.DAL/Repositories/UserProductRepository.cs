using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OnlinerNotifier.DAL.Models;
using OnlinerNotifier.DAL.Repositories.Interfaces;

namespace OnlinerNotifier.DAL.Repositories
{
    public class UserProductRepository : IRepository<UserProduct>
    {
        private readonly Context db;

        public UserProductRepository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<UserProduct> GetAll()
        {
            return db.UserProducts;
        }

        public UserProduct Get(int id)
        {
            return db.UserProducts.Find(id);
        }

        public void Create(UserProduct item)
        {
            db.UserProducts.Add(item);
        }

        public bool Update(UserProduct item)
        {
            if (db.UserProducts.Contains(item))
            {
                db.Entry(item).State = EntityState.Modified;
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            UserProduct item = db.UserProducts.Find(id);
            if (item != null)
            {
                db.UserProducts.Remove(item);
                return true;
            }
            return false;
        }
    }
}
