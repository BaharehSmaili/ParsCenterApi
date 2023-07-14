﻿using Data.Interface;
using Entities.Models.BasicInformation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFramework.Api;

namespace ParsCenterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IRepository<State> repository;
        public StateController(IRepository<State> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ApiResult<List<State>>> Get(CancellationToken cancellationToken)
        {
            var stetes = await repository.TableNoTracking.ToListAsync(cancellationToken);
            return Ok(stetes);
        }

        [HttpGet("{id:int}")]
        public async Task<ApiResult<State>> Get(int id, CancellationToken cancellationToken)
        {
            var state = await repository.GetByIdAsync(cancellationToken, id);
            if (state == null)
                return NotFound();
            return state;
        }

        [HttpPost]
        public async Task<ApiResult<State>> Create(State state, CancellationToken cancellationToken)
        {
            await repository.AddAsync(state, cancellationToken);
            return Ok(state);

        }

        [HttpPut]
        public async Task<ApiResult> Update(int id, State state, CancellationToken cancellationToken)
        {
            var updateState = await repository.GetByIdAsync(cancellationToken, id);

            updateState.Title = state.Title;
            updateState.CountryId = state.CountryId;

            await repository.UpdateAsync(updateState, cancellationToken);

            return Ok();
        }

        [HttpDelete]
        public async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            var state = await repository.GetByIdAsync(cancellationToken, id);
            await repository.DeleteAsync(state, cancellationToken);

            return Ok();
        }
    }
}
