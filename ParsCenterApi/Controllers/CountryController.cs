using Data.Interface;
using Data.Repositories;
using Entities.Models.BasicInformation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFramework.Api;
using WebFramework.Filters;

namespace ParsCenterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiResultFilter]
    public class CountryController : ControllerBase
    {
        private readonly IRepository<Country> _countryRepository;
        public CountryController(IRepository<Country> countryRepository)
        {
            this._countryRepository = countryRepository;
        }

        [HttpGet]
        public async Task<ApiResult<List<Country>>> Get(CancellationToken cancellationToken)
        {
            var countreis = await _countryRepository.TableNoTracking.ToListAsync(cancellationToken);
            return Ok(countreis);
        }

        [HttpGet("{id:int}")]
        public async Task<ApiResult<Country>> Get(int id, CancellationToken cancellationToken)
        {
            var country = await _countryRepository.GetByIdAsync(cancellationToken, id);
            if (country == null)
                return NotFound();
            return country;
        }

        [HttpPost]
        public async Task<ApiResult<Country>> Create(Country country, CancellationToken cancellationToken)
        {
            await _countryRepository.AddAsync(country, cancellationToken);
            return Ok(country);
        }

        [HttpPut]
        public async Task<ApiResult> Update(int id, Country country, CancellationToken cancellationToken)
        {
            var updateCountry = await _countryRepository.GetByIdAsync(cancellationToken, id);

            updateCountry.Title = country.Title;
            updateCountry.TitleEn = country.TitleEn;

            await _countryRepository.UpdateAsync(updateCountry, cancellationToken);

            return Ok();
        }

        [HttpDelete]
        public async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            var country = await _countryRepository.GetByIdAsync(cancellationToken, id);
            await _countryRepository.DeleteAsync(country, cancellationToken);

            return Ok();
        }
    }
}
