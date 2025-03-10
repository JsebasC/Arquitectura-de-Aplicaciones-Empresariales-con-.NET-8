﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface.Features;
using Pacagroup.Ecommerce.Services.WebApi.Helpers;
using Pacagroup.Ecommerce.Transversal.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Asp.Versioning;

namespace Pacagroup.Ecommerce.Services.WebApi.Controllers.v1
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0",Deprecated = true)]
    public class UsersController : Controller
    {
        private readonly IUsersApplication _usersApplication;
        private readonly AppSettings _appSettings;

        public UsersController(IUsersApplication usersApplication, AppSettings appSettings)
        {
            _usersApplication = usersApplication;
            _appSettings = appSettings;
        }

        [AllowAnonymous] //El unico metodo que necesita ser accesible
        [HttpPost]
        public IActionResult Authenticate([FromBody] UserDto usersDto)
        {
            var response = _usersApplication.Authenticate(usersDto.UserName, usersDto.Password);
            if (response.IsSuccess && response.Data != null)
            {
                response.Data.Token = ConstruirToken(response);
                return Ok(response);
            }
            return NotFound(response);
        }

        private string ConstruirToken(Response<UserDto> usersDto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.Name, usersDto.Data.UserId.ToString())
                ]),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
