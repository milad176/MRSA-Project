using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Entities;
using WebAPI.Models;
using WebAPI.UnitOfWorks;

namespace WebAPI.Controllers
{
    [Route("api/cities")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CityController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ??  throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/cities
        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            var CitiesFromRepo = await _unitOfWork.CityRepository.GetCitiesAsync();
            var CitiesToReturn = _mapper.Map<IEnumerable<CityDto>>(CitiesFromRepo);
            return Ok(CitiesToReturn);
        }


        // POST: api/cities
        [HttpPost]
        public async Task<IActionResult> AddCity([FromBody] CityDto cityDto)
        {
            var city = _mapper.Map<City>(cityDto);
            _unitOfWork.CityRepository.AddCity(city);
            await _unitOfWork.CompleteAsync();

            var CityToReturn = _mapper.Map<CityDto>(city);
            return Ok(CityToReturn);
        }

        //PUT: api/cities/1
        [HttpPut("{cityId}")]
        public async Task<IActionResult> UpdateCity(int cityId,
            [FromBody] CityForUpdateDto cityForUpdateDto)
        {
            var cityFromRepo = await _unitOfWork.CityRepository.GetCity(cityId);
            if (cityFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(cityForUpdateDto, cityFromRepo);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        //PUT: api/cities/updateCityName/1
        [HttpPut("updateCityName/{cityId}")]
        public async Task<IActionResult> UpdateCity(int cityId,
            [FromBody] CityUpdateNameDto cityForUpdateDto)
        {
            var cityFromRepo = await _unitOfWork.CityRepository.GetCity(cityId);
            if (cityFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(cityForUpdateDto, cityFromRepo);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        //PATCH: api/cities/1
        [HttpPatch("{cityId}")]
        public async Task<IActionResult> PartiallyUpdateCity(int cityId,
            JsonPatchDocument<CityForUpdateDto> patchDocument)
        {
            var cityFromRepo = await _unitOfWork.CityRepository.GetCity(cityId);
            if (cityFromRepo == null)
            {
                return NotFound();
            }

            var cityToPatch = _mapper.Map<CityForUpdateDto>(cityFromRepo);

            patchDocument.ApplyTo(cityToPatch, ModelState);

            if (!TryValidateModel(cityToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(cityToPatch, cityFromRepo);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // DELETE: api/cities/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            _unitOfWork.CityRepository.DeleteCity(id);
            await _unitOfWork.CompleteAsync();
            return Ok(id);
        }
    }
}
