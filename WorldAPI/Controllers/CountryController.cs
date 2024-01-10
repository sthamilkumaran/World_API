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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<Country>> GetAll()
        {
            var countries = _dbContext.Countries.ToList();
            if (countries == null)
            {
                return NoContent();
            }
            return Ok(countries);
        }

        [HttpGet("{id:int}")] //get singel data from database with Id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<Country> GetById(int id)
        {
            var countries = _dbContext.Countries.Find(id);
            if (countries == null)
            {
                return NoContent();
            }
            return Ok(countries);
        }

        [HttpPost] //post data from database
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<Country> Create([FromBody]Country country)
        {
            var result = _dbContext.Countries.AsQueryable().Where(x => x.Name.ToLower().Trim() == country.Name.ToLower().Trim()).Any(); // Name Check in database
            if (result)
            {
                return Conflict("Country already exsitx in database");
            }
            _dbContext.Countries.Add(country);
            _dbContext.SaveChanges();
            return CreatedAtAction("GetById", new { id = country.Id }, country);
        }

        [HttpPut] //data base data update with id 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Country> Update(int id,[FromBody]Country country)
        {

            if (country == null || id != country.Id)
            {
                return BadRequest();
            }

            var countryFromDb = _dbContext.Countries.Find(id);

            if (countryFromDb == null)
            {
                return NotFound();
            }

            countryFromDb.Name = country.Name;
            countryFromDb.ShortName = country.ShortName;
            countryFromDb.CountryCode = country.CountryCode;

            _dbContext.Countries.Update(countryFromDb);
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
