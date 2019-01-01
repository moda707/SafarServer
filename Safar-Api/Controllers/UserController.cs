using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SafarApi.Core;
using SafarCore;
using SafarCore.UserClasses;
using SafarObjects.UserClasses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SafarApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly JwtOptions _appSettings;
        private readonly IUsersFunc _usersFunc;

        public UserController(IUsersFunc usersFunc, IOptions<JwtOptions> appSettings)
        {
            _usersFunc = usersFunc;
            _appSettings = appSettings.Value;
        }
        
        // POST api/<controller>
        // Request for Token
        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<IActionResult> Post([FromBody]SignIn value)
        {
            var uu = await _usersFunc.GetUserByEmailPassword(value.UserName, value.Password);
            if (uu == null)
            {
                return BadRequest("Username or password is not correct");
            }

            //generate the token
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, value.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            uu.token = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(uu);
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> Post2([FromBody]SignUp value)
        {
            var u = await _usersFunc.AddUser(value);

            if (u.Result == ResultEnum.Successfull)
            {
                return Ok("New user added successfully");
            }
            else
            {
                return BadRequest(u.Message);
            }
        }
    }
}
