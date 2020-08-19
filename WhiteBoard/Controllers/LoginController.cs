using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WhiteBoard.Models.Entities;
using WhiteBoard.Models.Repos;
using WhiteBoard.Shared;

namespace WhiteBoard.Controllers
{
    [ApiController]
    [Route("/api/login")]
    public class LoginController : ControllerBase
    {
        private IUserRepository _userRepository;
        private IConfiguration _config;

        public LoginController(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        
        [Authorize]
        [HttpPost("Post")]
        public string Post()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity?.Claims.ToList();
            var fullName = claims[0].Value;
            return "welcome to" + fullName;

        }

        [Authorize]
        [HttpGet("GetValue")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[]
            {
                "Value1", "Value2", "Value3"
            };
        }

        [HttpGet]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userRepository.VerifyUserLogin(username, password);
            IActionResult response = Unauthorized();

            if (user != null)
            {
                var tokenString = GenerateJWT(user);
                response = Ok(new {token = tokenString, user});

            }
            return response;
        }

        private string GenerateJWT(UserDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
                );

            var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodeToken; 

        }
    }
}