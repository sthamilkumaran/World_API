using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorldAPI.Data;
using WorldAPI.Models;

namespace WorldAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public CountryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]  //get all database datas
        public ActionResult<IEnumerable<Country>> GetAll()
        {
            return _dbContext.Countries.ToList();
        }

        [HttpGet("{id:int}")] //get singel data from database with Id
        public ActionResult<Country> GetById(int id)
        {
            return _dbContext.Countries.Find(id);
        }

        [HttpPost] //post data from database 
        public ActionResult<Country> Create([FromBody]Country country)
        {
            _dbContext.Countries.Add(country);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpPut] //data base data update with id
        public ActionResult<Country> Update([FromBody]Country country)
        {
            _dbContext.Countries.Update(country);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id:int}")] //database data delete with id
        public ActionResult DeleteById(int id)
        {
            var country = _dbContext.Countries.Find(id);
            _dbContext.Countries.Remove(country);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
