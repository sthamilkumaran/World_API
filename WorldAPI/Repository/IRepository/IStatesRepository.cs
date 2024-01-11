using WorldAPI.Models;
using WorldAPI.Data;

namespace WorldAPI.Repository.IRepository
{
    public interface IStatesRepository : IGenericRepository<States>
    {
        //Task<List<States>> GetAll();
        //Task<States> GetById(int id);
        //Task Create(States entity);
        Task Update(States entity);
        //Task Delete(States entity);
        //Task Save();
        //bool IsStatesExsits(String name);
    }
}
