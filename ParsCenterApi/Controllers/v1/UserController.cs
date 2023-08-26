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
using Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;
using WebFramework.Filters;
using System.Security.Claims;
using Common;
using ParsCenterApi.Models.User;
using Data.IRepositories;

namespace ParsCenterApi.Controllers.v1
{
    [ApiVersion("1")]
    public class UserController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;
        private readonly IJwtService _jwtService;

        public UserController(IUserRepository userRepository, ILogger<UserController> logger, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _logger = logger;
            _jwtService = jwtService;
        }

        [HttpGet]
        //[AllowAnonymous]
        public async Task<List<User>> Get(CancellationToken cancellationToken)
        {

            //_logger.LogDebug("This is a debug message");
            //_logger.LogInformation("This is an info message");
            //_logger.LogWarning("This is a warning message ");
            //_logger.LogError(new Exception(), "This is an error message");
            var users = await _userRepository.TableNoTracking.ToListAsync(cancellationToken);
            return users;
        }


        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        //[Authorize(Roles = "Admin")]
        public async Task<ApiResult<User>> Get(Guid id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(cancellationToken, id);
            if (user == null)
                return NotFound();

            return user;
        }

        [HttpPost]
        public async Task<ApiResult<User>> Create(UserDto userDto, CancellationToken cancellationToken)
        {
            //برای ایجاد لاگ در دیتابیس در صورت نیاز 
            _logger.LogError("متد Create فراخوانی شد");
            //HttpContext.RiseError(new Exception("متد Create فراخوانی شد"));

            // در متد ادد سینک در یوزر ریپازیتوری چک می شود.
            //var exists = await _userRepository.TableNoTracking.AnyAsync(p => p.NationalCode == userDto.NationalCode);
            //if (exists)
            //    return BadRequest("نام کاربری تکراری است");

            var user = new User
            {
                Name = userDto.Name,
                Family = userDto.Family,
                NationalCode = userDto.NationalCode,
                Mobile = userDto.Mobile,
                Email = userDto.Email,
                IsActive = true,//userDto.IsActive,
                LastLoginDate = DateTimeOffset.Now
            };
            await _userRepository.AddAsync(user, userDto.Password, cancellationToken);
            return user;
        }

        [HttpPut]
        public async Task<ApiResult> Update(Guid id, User user, CancellationToken cancellationToken)
        {
            var updateUser = await _userRepository.GetByIdAsync(cancellationToken, id);

            updateUser.Mobile = user.Mobile;
            updateUser.PasswordHash = user.PasswordHash;
            updateUser.Name = user.Name;
            updateUser.Family = user.Family;
            updateUser.NationalCode = user.NationalCode;
            updateUser.Email = user.Email;
            updateUser.IsActive = user.IsActive;
            updateUser.LastLoginDate = DateTimeOffset.Now;

            await _userRepository.UpdateAsync(updateUser, cancellationToken);

            await _userRepository.UpdateSecuirtyStampAsync(user, cancellationToken); //برای امنیت و عدم استفاده از توکن قدیمی

            return Ok();
        }

        [HttpDelete]
        public async Task<ApiResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(cancellationToken, id);
            await _userRepository.DeleteAsync(user, cancellationToken);

            return Ok();
        }

        /// <summary>
        /// This method generate JWT Token
        /// </summary>
        /// <param name="mobile">The Mobile of User</param>
        /// <param name="password">The Password of User</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<string> Token(string mobile, string password, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUserAndPass(mobile, password, cancellationToken);
            if (user == null)
                throw new BadRequestException("کد ملی یا رمز عبور اشتباه است");

            var jwt = _jwtService.Generate(user);
            return jwt;
        }
    }
}
