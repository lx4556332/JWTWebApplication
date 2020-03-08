using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JWTWebApplication.Data;
using JWTWebApplication.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JWTWebApplication.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private UserManager<ApplcationUser> userManager;

        public AuthController(UserManager<ApplcationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user != null && await userManager.CheckPasswordAsync(user, model.PassWord))
            {
                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };

                var signingkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecurityKey")); 

                var token = new JwtSecurityToken(
                    issuer:"http://haisichen.com",
                    audience: "http://haisichen.com",
                    expires:DateTime.UtcNow.AddHours(1),
                    claims: claims,
                    signingCredentials:new Microsoft.IdentityModel.Tokens.SigningCredentials(signingkey,SecurityAlgorithms.HmacSha256)
                    );


                return Ok(new
                {
                    token=new JwtSecurityTokenHandler().WriteToken(token),
                    expiration=token.ValidTo
                });
            }
            return Unauthorized();
        }
    }
}