using Data.Repositories;
using Entities;
using Entities.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParsCenterApi.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;
using WebFramework.Filters;

namespace ParsCenterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiResultFilter]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<List<User>> Get(CancellationToken cancellationToken)
        {
            var users = await userRepository.TableNoTracking.ToListAsync(cancellationToken);
            return users;
        }


        [HttpGet("{id:guid}")]
        public async Task<ApiResult<User>> Get(Guid id, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(cancellationToken, id);
            if (user == null)
                return NotFound();
            return user;
        }

        [HttpPost]
        public async Task<ApiResult<User>> Create(UserDto userDto, CancellationToken cancellationToken)
        {

            //var exists = await userRepository.TableNoTracking.AnyAsync(p => p.UserName == userDto.UserName);
            //if (exists)
            //    return BadRequest("نام کاربری تکراری است");

            var user = new User
            {
                Name = userDto.Name,
                Family = userDto.Family,
                NationalCode = userDto.NationalCode,
                Mobile = userDto.Mobile,
                Email = userDto.Email
            };
            await userRepository.AddAsync(user, userDto.Password, cancellationToken);
            return user;
        }

    [HttpPut]
    public async Task<ApiResult> Update(Guid id, User user, CancellationToken cancellationToken)
    {
        var updateUser = await userRepository.GetByIdAsync(cancellationToken, id);

        updateUser.Mobile = user.Mobile;
        updateUser.Password = user.Password;
        updateUser.Name = user.Name;
        updateUser.Family = user.Family;
        updateUser.NationalCode = user.NationalCode;
        updateUser.Email = user.Email;
        updateUser.Country = user.Country;
        updateUser.State = user.State;
        updateUser.City = user.City;

        await userRepository.UpdateAsync(updateUser, cancellationToken);

        return Ok();
    }

    [HttpDelete]
    public async Task<ApiResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(cancellationToken, id);
        await userRepository.DeleteAsync(user, cancellationToken);

        return Ok();
    }
}
}
