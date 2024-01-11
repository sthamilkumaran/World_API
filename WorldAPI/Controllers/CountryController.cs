using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorldAPI.Data;
using WorldAPI.DTO.Country;
using WorldAPI.Models;

namespace WorldAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CountryController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }

        [HttpGet]  //get all database datas
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<CountryDto>> GetAll()
        {
            var countries = _dbContext.Countries.ToList();
            
            var countriesDto = _mapper.Map<List<CountryDto>>(countries);
            if (countries == null)
            {
                return NoContent();
            }
            return Ok(countriesDto);
        }

        [HttpGet("{id:int}")] //get singel data from database with Id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<CountryDto> GetById(int id)
        {
            var countries = _dbContext.Countries.Find(id);

            var countriesDto = _mapper.Map<CountryDto>(countries);

            if (countries == null)
            {
                return NoContent();
            }
            return Ok(countriesDto);
        }

        [HttpPost] //post data from database
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<Country> Create([FromBody] CreateCountryDto countryDto)
        {
            var result = _dbContext.Countries.AsQueryable().Where(x => x.Name.ToLower().Trim() == countryDto.Name.ToLower().Trim()).Any(); // Name Check in database
            
            if (result)
            {
                return Conflict("Country already exsitx in database");
            }
            //auto mapper function
            var country = _mapper.Map<Country>(countryDto);

            _dbContext.Countries.Add(country);
            _dbContext.SaveChanges();
            return CreatedAtAction("GetById", new { id = country.Id }, country);
        }

        [HttpPut] //data base data update with id 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Country> Update(int id,[FromBody]UpdateCountryDto countryDto)
        {

            if (countryDto == null || id != countryDto.Id)
            {
                return BadRequest();
            }

            //var countryFromDb = _dbContext.Countries.Find(id);

            //if (countryFromDb == null)
            //{
            //    return NotFound();
            //}

            var country = _mapper.Map<Country>(countryDto);

            _dbContext.Countries.Update(country);
            _dbContext.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id:int}")] //database data delete with id 
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var country = _dbContext.Countries.Find(id);

            if (country == null)
            {
                return NotFound();
            }

            _dbContext.Countries.Remove(country);
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
}
