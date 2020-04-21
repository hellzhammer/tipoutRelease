using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TipoutProject;

namespace TipOutAPI.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class AppLoginController : ControllerBase
    {
        private readonly ILogger<AppLoginController> _logger;
        private IConfiguration _config;
        public AppLoginController(ILogger<AppLoginController> logger, IConfiguration config)
        {
            _config = config;
            _logger = logger;
        }

        [HttpPost]
        public string Login([FromBody] AuthorizationModel auth)
        {
            if (Program.userController.UserExists(auth.username))
            {
                UserModel u = Program.userController.GetUser(auth.username);
                if (u != null)
                {
                    if (auth.username == u.userAuth.username && auth.password == u.userAuth.password)
                    {
                        return BuildToken();
                    }
                }
            }

            return null; 
        }

        private string BuildToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              expires: DateTime.Now.AddMinutes(600),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
