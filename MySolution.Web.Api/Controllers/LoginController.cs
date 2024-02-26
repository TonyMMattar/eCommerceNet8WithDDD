using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Threading.Tasks;
using System.Text;
using System.Security.Claims;

namespace eCommerce.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {

        #region Variables
        private readonly IUserServices _userServices;
        #endregion

        #region Constructors
        public LoginController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        #endregion

        #region Actions
        [HttpPost]
        public ActionResult Login([FromBody] User user)
        {
            if (_userServices.GetCredentialsAsync(user).Result != null)
            {
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecureKeyRequiredforvalidationAdmin"));

                var authClaims = new[]
                {
                    new Claim("UserId", _userServices.GetCredentialsAsync(user).Result.Id.ToString())
                };

                var token = new JwtSecurityToken(
                    expires: DateTime.Now.AddHours(8),
                    claims: authClaims,
                    signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            else
            {
                return Unauthorized("wrong credentials");
            }
        }
        #endregion
    }
}
