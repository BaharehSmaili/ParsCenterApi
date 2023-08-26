using Data.IRepositories;
using Entities.Models.BasicInformation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFramework.Api;
using WebFramework.Filters;

namespace ParsCenterApi.Controllers.v1
{
    [ApiVersion("1")]
    public class CityController : BaseController
    {
        private readonly IRepository<City> _cityRepository;
        public CityController(IRepository<City> cityRepository)
        {
            _cityRepository = cityRepository;
        }

        [HttpGet]
        public async Task<ApiResult<List<City>>> Get(CancellationToken cancellationToken)
        {
            var citeis = await _cityRepository.TableNoTracking.ToListAsync(cancellationToken);
            return Ok(citeis);
        }

        [HttpGet("{id:int}")]
        public async Task<ApiResult<City>> Get(int id, CancellationToken cancellationToken)
        {
            var city = await _cityRepository.GetByIdAsync(cancellationToken, id);
            if (city == null)
                return NotFound();
            return city;
        }

        [HttpPost]
        public async Task<ApiResult<City>> Create(City city, CancellationToken cancellationToken)
        {
            await _cityRepository.AddAsync(city, cancellationToken);
            return Ok(city);

        }

        [HttpPut]
        public async Task<ApiResult> Update(int id, City city, CancellationToken cancellationToken)
        {
            var updateCity = await _cityRepository.GetByIdAsync(cancellationToken, id);

            updateCity.Title = city.Title;
            updateCity.StateId = city.StateId;

            await _cityRepository.UpdateAsync(updateCity, cancellationToken);

            return Ok();
        }

        [HttpDelete]
        public async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            var city = await _cityRepository.GetByIdAsync(cancellationToken, id);
            await _cityRepository.DeleteAsync(city, cancellationToken);

            return Ok();
        }
    }
}