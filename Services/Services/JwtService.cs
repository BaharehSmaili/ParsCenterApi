using Common;
using Common.Interface;
using Entities;
using Entities.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public class JwtService : IJwtService, IScopedDependency
    {
        private readonly SiteSettings _siteSetting;
        public JwtService(IOptionsSnapshot<SiteSettings> settings)
        {
            _siteSetting = settings.Value;
        }

        public string Generate(User user)
        {
            var secretKey = Encoding.UTF8.GetBytes(_siteSetting.JwtSettings.SecretKey); // باید بیشتر از 16 کاراکتر باشد
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var encryptionkey = Encoding.UTF8.GetBytes(_siteSetting.JwtSettings.Encryptkey); // باید فقط 16 کاراکتر باشد
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var claims = _getClaims(user);

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _siteSetting.JwtSettings.Issuer,
                Audience = _siteSetting.JwtSettings.Audience,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(_siteSetting.JwtSettings.NotBeforeMinutes),
                Expires = DateTime.Now.AddMinutes(_siteSetting.JwtSettings.ExpirationMinutes),
                SigningCredentials = signingCredentials,
                EncryptingCredentials = encryptingCredentials,
                Subject = new ClaimsIdentity(claims)
            };

            // میتوانند یک بار در استارت آپ پروژه ست بشن
            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            //JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            //JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(descriptor);

            var jwt = tokenHandler.WriteToken(securityToken);

            return jwt;
        }

        private IEnumerable<Claim> _getClaims(User user)
        {
            var securityStampClaimType = new ClaimsIdentityOptions().SecurityStampClaimType;

            var list = new List<Claim>
            {
                new Claim(ClaimTypes.Name, (user.Name + "_" + user.Family)),
                new Claim(ClaimTypes.NameIdentifier, user.Mobile.ToString()),
                new Claim(ClaimTypes.MobilePhone, user.Mobile),
                new Claim(securityStampClaimType, user.SecurityStamp.ToString())
            };

            //var stafs = new Staf[] { new Staf { Name = "Admin" } };
            //foreach (var staf in stafs)
            //    list.Add(new Claim(ClaimTypes.Role, staf.Name));

            return list;
        }
    }
}
