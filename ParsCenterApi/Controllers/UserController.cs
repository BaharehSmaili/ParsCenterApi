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
using System.Security.Claims;
using Common;

namespace ParsCenterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiResultFilter]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;
        private readonly IJwtService _jwtService;

        public UserController(IUserRepository userRepository, ILogger<UserController> logger, IJwtService jwtService)
        {
            this._userRepository = userRepository;
            this._logger = logger;
            this._jwtService = jwtService;
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
        public async Task<ApiResult<User>> Get(Guid id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(cancellationToken, id);
            if (user == null)
                return NotFound();

            return user;
        }

        //[HttpGet]
        ////[Authorize(Roles = "Admin")]
        //public async Task<ActionResult<List<User>>> GetAll(CancellationToken cancellationToken)
        //{
        //    var userName = HttpContext.User.Identity.GetUserName();
        //    userName = HttpContext.User.Identity.Name;
        //    var userId = HttpContext.User.Identity.GetUserId();
        //    var userIdInt = HttpContext.User.Identity.GetUserId<int>();
        //    var phone = HttpContext.User.Identity.FindFirstValue(ClaimTypes.MobilePhone);
        //    //var role = HttpContext.User.Identity.FindFirstValue(ClaimTypes.Role);

        //    var users = await _userRepository.TableNoTracking.ToListAsync(cancellationToken);
        //    return Ok(users);
        //}

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

            //updateUser.Country = user.Country;
            //updateUser.State = user.State;
            //updateUser.City = user.City;

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
