using Common.Exceptions;
using Data.Repositories;
using ElmahCore;
using Entities;
using Entities.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ParsCenterApi.Models;
using Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;
using WebFramework.Filters;
using ElmahCore;

namespace ParsCenterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiResultFilter]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly ILogger<UserController> logger;
        private readonly IJwtService jwtService;

        public UserController(IUserRepository userRepository, ILogger<UserController> logger, IJwtService jwtService)
        {
            this.userRepository = userRepository;
            this.logger = logger;
            this.jwtService = jwtService;
        }

        [HttpGet]
        public async Task<List<User>> Get(CancellationToken cancellationToken)
        {

            logger.LogDebug("This is a debug message");
            logger.LogInformation("This is an info message");
            logger.LogWarning("This is a warning message ");
            logger.LogError(new Exception(), "This is an error message");
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
            //برای ایجاد لاگ در دیتابیس در صورت نیاز 
            logger.LogError("متد Create فراخوانی شد");
            //HttpContext.RiseError(new Exception("متد Create فراخوانی شد"));

            // در متد ادد سینک در یوزر ریپازیتوری چک می شود.
            //var exists = await userRepository.TableNoTracking.AnyAsync(p => p.NationalCode == userDto.NationalCode);
            //if (exists)
            //    return BadRequest("نام کاربری تکراری است");

            var user = new User
            {
                Name = userDto.Name,
                Family = userDto.Family,
                NationalCode = userDto.NationalCode,
                Mobile = userDto.Mobile,
                Email = userDto.Email,
                IsActive = userDto.IsActive,
                LastLoginDate = DateTimeOffset.Now
        };
            await userRepository.AddAsync(user, userDto.Password, cancellationToken);
            return user;
        }

        [HttpPut]
        public async Task<ApiResult> Update(Guid id, User user, CancellationToken cancellationToken)
        {
            var updateUser = await userRepository.GetByIdAsync(cancellationToken, id);

            updateUser.Mobile = user.Mobile;
            updateUser.PasswordHash = user.PasswordHash;
            updateUser.Name = user.Name;
            updateUser.Family = user.Family;
            updateUser.NationalCode = user.NationalCode;
            updateUser.Email = user.Email;
            updateUser.IsActive = user.IsActive;
            updateUser.LastLoginDate = DateTimeOffset.Now;

            //updateUser.Country = user.Country;
            //updateUser.State = user.State;
            //updateUser.City = user.City;

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

        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<string> Token(string nationalCode, string password, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByUserAndPass(nationalCode, password, cancellationToken);
            if (user == null)
                throw new BadRequestException("کد ملی یا رمز عبور اشتباه است");

            var jwt = jwtService.Generate(user);
            return jwt;
        }
    }
}
