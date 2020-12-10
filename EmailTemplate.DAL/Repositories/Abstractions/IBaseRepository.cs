using EmailTemplate.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplate.DAL.Repositories.Abstractions
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        Task<IEnumerable<T>> Find(Func<T, Boolean> predicate);
        Task<T> FindFirst(Func<T, Boolean> predicate);
        Task Create(T item);
        Task Update(T item);
        Task Delete(int id);
        Task Delete(T item);
    }
}
