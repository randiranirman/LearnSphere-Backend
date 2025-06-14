using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseRegistration.Application.Repositories;
using CourseRegistration.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseRegistration.Infrastructure.Repositories
{

    // base repository implmentation 
    public class   BaseRepository<T> : IRepository<T> where T : class
    {

        protected readonly CourseRegistrationDbcontext _context;
        protected readonly DbSet<T> _set;


        public BaseRepository(CourseRegistrationDbcontext context)
        {
            _context = context;
            _set= context.Set<T>();
        }
        public virtual  async Task<T> AddAsync(T entity)  
        {
            await _set.AddAsync(entity);
            return entity;
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if( entity != null)
            {
                _set.Remove(entity);
            }
        }

        public virtual Task<bool> ExistAsync(int id)
        {
            throw new NotImplementedException();
        }

        public  virtual async Task<IEnumerable<T>> GetAllAsync()
        {
           return await _set.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _set.FindAsync(id);
        }

        public  virtual async Task<T> UpdateAsync(T entity)
        {
            _set.Update(entity);
            return entity;
        }
    }
}
