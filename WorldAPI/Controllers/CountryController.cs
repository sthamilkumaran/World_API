using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorldAPI.Data;
using WorldAPI.DTO.Country;
using WorldAPI.Models;
using WorldAPI.Repository.IRepository;

namespace WorldAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;

        }

        [HttpGet]  //get all database datas
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetAll()
        {
            var countries =await _countryRepository.GetAll();


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
        public async Task<ActionResult<CountryDto>> GetById(int id)
        {
            var countries =await _countryRepository.GetById(id);

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
        public async Task<ActionResult<Country>> Create([FromBody] CreateCountryDto countryDto)
        {
            var result = _countryRepository.IsCountryExsits(countryDto.Name); // Name Check in database


            if (result)
            {
                return Conflict("Country already exsitx in database");
            }
            //auto mapper function
            var country = _mapper.Map<Country>(countryDto);

            await _countryRepository.Create(country);
            return CreatedAtAction("GetById", new { id = country.Id }, country);
        }

        [HttpPut] //data base data update with id 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Country>> Update(int id,[FromBody]UpdateCountryDto countryDto)
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

            await _countryRepository.Update(country);
            return NoContent();
        }

        [HttpDelete("{id:int}")] //database data delete with id 
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var country = await _countryRepository.GetById(id);

            if (country == null)
            {
                return NotFound();
            }

            await _countryRepository.Delete(country);
            return NoContent();
        }
    }
}
