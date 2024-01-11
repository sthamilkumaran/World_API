using Microsoft.EntityFrameworkCore;
using WorldAPI.Data;
using WorldAPI.Models;
using WorldAPI.Repository.IRepository;

namespace WorldAPI.Repository
{
    public class StatesRepository : GenericRepository<States>, IStatesRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StatesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        ////public async Task Create(States entity)
        ////{
        ////    await _dbContext.States.AddAsync(entity);
        ////    await Save();
        ////}

        ////public async Task Delete(States entity)
        ////{
        ////    _dbContext.States.Remove(entity);
        ////    await Save();
        ////}

        ////public async Task<List<States>> GetAll()
        ////{
        ////    List<States> states = await _dbContext.States.ToListAsync();
        ////    return states;
        ////}

        ////public async Task<States> GetById(int id)
        ////{
        ////    States state = await _dbContext.States.FindAsync(id);
        ////    return state;
        ////}

        ////public bool IsStatesExsits(string name)
        ////{
        ////    var result = _dbContext.States.AsQueryable().Where(x => x.Name.ToLower().Trim() == name.ToLower().Trim()).Any();
        ////    return result;
        ////}

        ////public async Task Save()
        ////{
        ////    await _dbContext.SaveChangesAsync();
        ////}

        public async Task Update(States entity)
        {
            _dbContext.States.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
