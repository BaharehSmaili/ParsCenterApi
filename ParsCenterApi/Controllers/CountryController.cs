using Data.Interface;
using Data.Repositories;
using Entities.Models.BasicInformation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ParsCenterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IRepository<Country> repository;
        public CountryController(IRepository<Country> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Country>>> Get(CancellationToken cancellationToken)
        {
            var countreis = await repository.TableNoTracking.ToListAsync(cancellationToken);
            return Ok(countreis);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Country>> Get(int id, CancellationToken cancellationToken)
        {
            var country = await repository.GetByIdAsync(cancellationToken, id);
            if (country == null)
                return NotFound();
            return country;
        }

        [HttpPost]
        public async Task Create(Country country, CancellationToken cancellationToken)
        {
            await repository.AddAsync(country, cancellationToken);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, Country country, CancellationToken cancellationToken)
        {
            var updateCountry = await repository.GetByIdAsync(cancellationToken, id);

            updateCountry.Title = country.Title;
            updateCountry.TitleEn = country.TitleEn;

            await repository.UpdateAsync(updateCountry, cancellationToken);

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var country = await repository.GetByIdAsync(cancellationToken, id);
            await repository.DeleteAsync(country, cancellationToken);

            return Ok();
        }
    }
}
