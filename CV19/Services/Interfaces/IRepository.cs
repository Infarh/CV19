using System.Collections.Generic;
using System.Linq;
using CV19.Models.Interfaces;

namespace CV19.Services.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        void Add(T item);

        IEnumerable<T> GetAll();

        T Get(int id) => GetAll().FirstOrDefault(item => item.Id == id);

        bool Remove(T item);

        void Update(int id, T item);
    }
}
