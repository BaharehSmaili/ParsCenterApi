using Data.IRepositories;
using Entities.Models.BasicInformation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFramework.Api;
using WebFramework.Filters;

namespace ParsCenterApi.Controllers.v1
{
    [ApiVersion("1")]
    public class StateController : BaseController
    {
        private readonly IRepository<State> _stateRepository;
        public StateController(IRepository<State> stateRepository)
        {
            _stateRepository = stateRepository;
        }

        [HttpGet]
        public async Task<ApiResult<List<State>>> Get(CancellationToken cancellationToken)
        {
            var stetes = await _stateRepository.TableNoTracking.ToListAsync(cancellationToken);
            return Ok(stetes);
        }

        [HttpGet("{id:int}")]
        public async Task<ApiResult<State>> Get(int id, CancellationToken cancellationToken)
        {
            var state = await _stateRepository.GetByIdAsync(cancellationToken, id);
            if (state == null)
                return NotFound();
            return state;
        }

        [HttpPost]
        public async Task<ApiResult<State>> Create(State state, CancellationToken cancellationToken)
        {
            await _stateRepository.AddAsync(state, cancellationToken);
            return Ok(state);

        }

        [HttpPut]
        public async Task<ApiResult> Update(int id, State state, CancellationToken cancellationToken)
        {
            var updateState = await _stateRepository.GetByIdAsync(cancellationToken, id);

            updateState.Title = state.Title;
            updateState.CountryId = state.CountryId;

            await _stateRepository.UpdateAsync(updateState, cancellationToken);

            return Ok();
        }

        [HttpDelete]
        public async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            var state = await _stateRepository.GetByIdAsync(cancellationToken, id);
            await _stateRepository.DeleteAsync(state, cancellationToken);

            return Ok();
        }
    }
}
