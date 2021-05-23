using Chess.Data.Models;
using System.Collections.Generic;

namespace Chess.Data.Services.Interfaces
{
    public interface IBaseService<T> where T : Entity
    {
        List<T> GetAll();
        T Get(int? id);
        void Create(T entity);
        void Update(T entity);
        void Remove(int? id);
    }
}
