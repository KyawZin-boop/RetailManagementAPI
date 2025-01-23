using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.AppConfig;

namespace Repository.Repositories
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _entities;

        public GenericRepository(DataContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public async Task Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await _entities.AddAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAll() => await _entities.ToListAsync();
        
        public async Task<T?> GetById(int id) => await _entities.FindAsync(id);

        public async Task<T?> GetByGuid(Guid guid) => await _entities.FindAsync(guid);

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Update(entity);
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> expression)
        {
            if (expression == null)
            {
                throw new InvalidOperationException(nameof(expression));
            }

            return await _entities.Where(expression).ToListAsync();
        }

        public async Task<PaginatedResponseModel<T>> GetByConditionWithPaginationByDesc<TKey>(Expression<Func<T, bool>> expression, int page, int pageSize, Expression<Func<T, TKey>> orderBy)
        {
            if (expression == null)
            {
                throw new InvalidOperationException(nameof(expression));
            }

            if (page < 1 || pageSize <= 0)
            {
                throw new InvalidOperationException("PageError");
            }

            var totalCount = await _entities.CountAsync(expression);
            if (totalCount == 0)
            {
                return null;
            }
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            if (page > totalPages)
            {
                throw new InvalidOperationException($"Invalid page number. The page number should be between 1 and {totalPages}.");
            }

            var lst = await _entities.OrderByDescending(orderBy).Where(expression).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResponseModel<T>
            {
                Items = lst, 
                TotalCount = totalCount, 
                TotalPages = totalPages
            };
        }

        public async Task<PaginatedResponseModel<T>> GetPaginationByDesc<TKey>(int page, int pageSize, Expression<Func<T, TKey>> orderBy)
        {
            if (page < 1 || pageSize <= 0)
            {
                throw new InvalidOperationException("PageError");
            }

            var totalCount = await _entities.CountAsync();
            if (totalCount == 0)
            {
                return null;
            }
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            if (page > totalPages)
            {
                throw new InvalidOperationException($"Invalid page number. The page number should be between 1 and {totalPages}.");
            }

            var lst = await _entities.OrderByDescending(orderBy).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResponseModel<T>
            {
                Items = lst,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
        }
    }
}
