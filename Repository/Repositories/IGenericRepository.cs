using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Model.AppConfig;

namespace Repository.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task Add(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetByGuid(Guid guid);
        Task<T?> GetById(int id);
        void Delete(T entity);
        void Update(T entity);
        Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> expression);
        Task<PaginatedResponseModel<T>> GetByConditionWithPaginationByDesc<TKey>(Expression<Func<T, bool>> expression, int page, int pageSize, Expression<Func<T, TKey>> orderBy);
        Task<PaginatedResponseModel<T>> GetPaginationByDesc<TKey>(int page, int pageSize, Expression<Func<T, TKey>> orderBy);
    }
}
