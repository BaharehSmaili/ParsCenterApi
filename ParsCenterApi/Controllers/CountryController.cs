using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Interface;
using Data.Repositories;
using Entities.Models.BasicInformation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParsCenterApi.Models.BasicInformation;
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
        public async Task<ApiResult<List<CountryDto>>> Get(CancellationToken cancellationToken)
        {
            #region old code 
            // برای حالتی بود که خروجی لیستی از مدل کشورها بود
            //var countreis = await _countryRepository.TableNoTracking.ToListAsync(cancellationToken);
            //return Ok(countreis);

            //برای حالتی که لیستی از ویو مدل کشور مقدار برگشتی بود ولی بهینه نیست
            //var countreis = await _countryRepository.TableNoTracking.ToListAsync(cancellationToken);
            //var list = countreis.Select(p =>
            //{
            //    var countreisDto = Mapper.Map<CountryDto>(p);
            //    return countreisDto;
            //}).ToList();

            //برای حالتی که از مپر استفاده نمیکنیم و خروجی مدل ویو از کسورخاست
            //var list = await _countryRepository.TableNoTracking.Select(p => new CountryDto
            //{
            //    Id = p.Id,
            //    Title = p.Title,
            //    TitleEn = p.TitleEn,
            //    state
            //}).ToListAsync(cancellationToken);
            #endregion

            var listCountriesDto = await _countryRepository.TableNoTracking.ProjectTo<CountryDto>()
                .Where(countreisDto => countreisDto.Title.Contains("test") || countreisDto.TitleEn.Contains("test")) // برای اضافه کردن شرط به خروجی
                .ToListAsync(cancellationToken);

            return Ok(listCountriesDto);
        }

        [HttpGet("{id:int}")]
        public async Task<ApiResult<CountryDto>> Get(int id, CancellationToken cancellationToken)
        {
            var country = await _countryRepository.GetByIdAsync(cancellationToken, id);
            if (country == null)
                return NotFound();

            #region Old Code
            //var countryDto = new CountryDto
            //{
            //    Id = country.Id,
            //    Title = country.Title,
            //    TitleEn = country.TitleEn
            //    // States
            //};
            //return countryDto;
            #endregion
            var countryDto = Mapper.Map<CountryDto>(country);
            return countryDto;
        }

        [HttpPost]
        public async Task<ApiResult<Country>> Create(Country country, CancellationToken cancellationToken)
        {
            await _countryRepository.AddAsync(country, cancellationToken);
            return Ok(country);
        }

        [HttpPut]
        public async Task<ApiResult<CountryDto?>> Update(int id, CountryDto countryDto, CancellationToken cancellationToken)
        {
            var updateCountry = await _countryRepository.GetByIdAsync(cancellationToken, id);

            Mapper.Map(countryDto, updateCountry);

            #region Old Code
            //updateCountry.Title = countryDto.Title;
            //updateCountry.TitleEn = countryDto.TitleEn;
            //    // States
            #endregion

            await _countryRepository.UpdateAsync(updateCountry, cancellationToken);

            #region old code
            //var resultCountryDto = await _countryRepository.TableNoTracking.Select(p => new countryDto
            //{
            //    Id = p.Id,
            //    Title = p.Title,
            //    TitleEn = p.TitleEn
            //    // States

            //}).SingleOrDefaultAsync(p => p.Id == model.Id, cancellationToken);
            #endregion

            var resultCountryDto = await _countryRepository.TableNoTracking.ProjectTo<CountryDto>().SingleOrDefaultAsync(p => p.Id == updateCountry.Id, cancellationToken);

            return resultCountryDto;
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
