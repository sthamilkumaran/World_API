using WorldAPI.Models;
using WorldAPI.Data;

namespace WorldAPI.Repository.IRepository
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        //Task<List<Country>> GetAll();
        //Task<Country> GetById(int id);
        //Task Create(Country entity);
        Task Update(Country entity);
        //Task Delete(Country entity);
        //Task Save();

        //bool IsCountryExsits(string name);
    }
}
