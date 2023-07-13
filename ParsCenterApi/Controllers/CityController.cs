﻿using Data.Interface;
using Entities.Models.BasicInformation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ParsCenterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IRepository<City> repository;
        public CityController(IRepository<City> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<City>>> Get(CancellationToken cancellationToken)
        {
            var citeis = await repository.TableNoTracking.ToListAsync(cancellationToken);
            return Ok(citeis);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<City>> Get(int id, CancellationToken cancellationToken)
        {
            var city = await repository.GetByIdAsync(cancellationToken, id);
            if (city == null)
                return NotFound();
            return city;
        }

        [HttpPost]
        public async Task Create(City city, CancellationToken cancellationToken)
        {
            await repository.AddAsync(city, cancellationToken);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, City city, CancellationToken cancellationToken)
        {
            var updateCity = await repository.GetByIdAsync(cancellationToken, id);

            updateCity.Title = city.Title;
            updateCity.StateId = city.StateId;

            await repository.UpdateAsync(updateCity, cancellationToken);

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var city = await repository.GetByIdAsync(cancellationToken, id);
            await repository.DeleteAsync(city, cancellationToken);

            return Ok();
        }
    }
}