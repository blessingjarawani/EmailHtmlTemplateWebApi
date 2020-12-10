using EmailTemplate.DAL.Databases;
using EmailTemplate.DAL.Entities;
using EmailTemplate.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplate.DAL.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected EmailContext db;

        public BaseRepository(EmailContext context)
        {
            this.db = context;
        }

        public async Task Create(T item)
        {
            await db.Set<T>().AddAsync(item);
        }

        public async Task Delete(int id)
        {
            var entity = await db.Set<T>().FindAsync(id);
            if (entity != null)
                db.Set<T>().Remove(entity);
        }

        public async Task Delete(T item)
        {
            await Task.Run(() => db.Set<T>().Remove(item));
        }

        public async Task<IEnumerable<T>> Find(Func<T, bool> predicate)
        {
            return await Task.Run(() => db.Set<T>().Where(predicate).AsParallel().ToList());
        }

        public async Task<T> FindFirst(Func<T, bool> predicate)
        {
            return await Task.Run(() => db.Set<T>().FirstOrDefault(predicate));
        }

        public async Task<T> Get(int id)
        {
            return await db.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Task.Run(() => db.Set<T>());
        }

        public async Task Update(T item)
        {
            await Task.Run(() => db.Entry(item).State = EntityState.Modified);
        }



    }
}
