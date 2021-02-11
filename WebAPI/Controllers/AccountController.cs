using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Entities;
using WebAPI.Models;
using WebAPI.UnitOfWorks;

namespace WebAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController: ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public AccountController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
           var userFromRep = await _unitOfWork.UserRepository.Authenticate(loginRequest.userName, 
               loginRequest.password);

           if(userFromRep == null)
            {
               return Unauthorized();
            }

            var loginRes = new LoginResponse();
            loginRes.userName = userFromRep.Username;
            loginRes.Token = CreateJWT(userFromRep);

            return Ok(loginRes);
        }

        private string CreateJWT(User user)
        {
            var secretKey = _configuration.GetSection("AppSettings:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(secretKey));

            var signingCredential = new SigningCredentials(key,
                SecurityAlgorithms.HmacSha256Signature);

            var claim = new Claim[]
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = signingCredential
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token); 
        }
    }
}
