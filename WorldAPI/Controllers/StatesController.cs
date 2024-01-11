using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorldAPI.DTO.Country;
using WorldAPI.DTO.States;
using WorldAPI.Models;
using WorldAPI.Repository;
using WorldAPI.Repository.IRepository;

namespace WorldAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly IStatesRepository _statesRepository;
        private readonly IMapper _mapper;

        public StatesController(IStatesRepository statesRepository, IMapper mapper)
        {
            _statesRepository = statesRepository;
            _mapper = mapper;
        }

        [HttpGet]  //get all database datas
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<StatesDto>>> GetAll()
        {
            var states = await _statesRepository.GetAll();


            var statesDto = _mapper.Map<List<StatesDto>>(states);
            if (states == null)
            {
                return NoContent();
            }
            return Ok(statesDto);
        }

        [HttpGet("{id:int}")] //get singel data from database with Id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<StatesDto>> GetById(int id)
        {
            var state = await _statesRepository.Get(id);

            

            if (state == null)
            {
                return NoContent();
            }
            var stateDto = _mapper.Map<StatesDto>(state);
            return Ok(stateDto);
        }

        [HttpPost] //post data from database
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<States>> Create([FromBody] CreateStatesDto statesDto)
        {
            var result = _statesRepository.IsRecordExsits(x => x.Name == statesDto.Name); // Name Check in database


            if (result)
            {
                return Conflict("Country already exsitx in database");
            }
            //auto mapper function
            var state = _mapper.Map<States>(statesDto);

            await _statesRepository.Create(state);
            return CreatedAtAction("GetById", new { id = state.Id }, state);
        }


        [HttpPut] //data base data update with id 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<States>> Update(int id, [FromBody] UpdateStatesDto stateDto)
        {

            if (stateDto == null || id != stateDto.Id)
            {
                return BadRequest();
            }

            //var countryFromDb = _dbContext.Countries.Find(id);

            //if (countryFromDb == null)
            //{
            //    return NotFound();
            //}

            var state = _mapper.Map<States>(stateDto);

            await _statesRepository.Update(state);
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

            var state = await _statesRepository.Get(id);

            if (state == null)
            {
                return NotFound();
            }

            await _statesRepository.Delete(state);
            return NoContent();
        }
    }
}
