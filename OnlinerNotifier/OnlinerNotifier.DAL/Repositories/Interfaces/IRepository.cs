using System.Collections.Generic;

namespace OnlinerNotifier.DAL.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T item);
        bool Update(T item);
        bool Delete(int id);
    }
}
