using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace netcore3._1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private ILogger<UserController> _logger;
        private IHttpContextAccessor _httpContextAccessor;
        public UserController(ILogger<UserController> logger, IHttpContextAccessor httpContextAccessor)
        {
            this._logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        [Route("GenJwt")]
        [HttpGet]
        [AllowAnonymous]
        public string GetJwt()
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, "张三"),
                new Claim(ClaimTypes.Expired, "123"),
                new Claim(ClaimTypes.Email, "aa@12.com"),
                new Claim("customType", "自定义type"),
                new Claim(JwtRegisteredClaimNames.NameId, "NameId")

            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ceshikeyaaaaaaabac"));
            var token = new JwtSecurityToken(
                issuer: "localhost",
               expires: DateTime.Now.AddHours(1),
               audience: "localhost",
               signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                claims:  claims);
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);


            return jwtToken;

        }
        [Route("GetJwt")]
        [HttpGet]
        public string JwtVal(string token)
        {
            var tokenSer = "";
            if (!string.IsNullOrWhiteSpace(token))
            {
                var jwtHandler = new JwtSecurityTokenHandler();
                var tokenObj = jwtHandler.ReadToken(token);
                tokenSer = JsonConvert.SerializeObject(tokenObj);
            }

           var name = _httpContextAccessor.HttpContext.User.Identity.Name;
            var custom = _httpContextAccessor.HttpContext.User.Claims;
            return $"{tokenSer}-{name}";
        }
    }
}
