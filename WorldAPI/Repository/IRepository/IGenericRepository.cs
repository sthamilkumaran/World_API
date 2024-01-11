using System.Linq.Expressions;
using WorldAPI.Models;

namespace WorldAPI.Repository.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> Get(int id);
        Task Create(T entity);
        //Task Update(T entity);
        Task Delete(T entity);
        Task Save();
        bool IsRecordExsits(Expression<Func<T, bool>> condition);
    }
}
