using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IdentityAndAuthorizationServer.Models;
using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using IdentityAndAuthorizationServer.Filters.ExceptionFilter;
using Microsoft.Extensions.Localization;

namespace IdentityAndAuthorizationServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ExceptionHandlerFilter))]
    public class ApplicationUserController : ControllerBase
    {
        private UserManager<ApplicationUser> userManager { get; set; }
        private  ApplicationSettings applicationSettings { get; set; }

        private readonly IStringLocalizer<ApplicationUserController> localizer;

        public ApplicationUserController(UserManager<ApplicationUser> userManager, IOptions<ApplicationSettings> applicationSettings, IStringLocalizer<ApplicationUserController> localizer)
        {
            this.userManager = userManager;
            this.applicationSettings = applicationSettings.Value;
            this.localizer = localizer;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<Object> PostApplicationUser(ApplicationUserModel applicationUserModel)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = applicationUserModel.UserName,
                Email = applicationUserModel.Email,
                FullName = applicationUserModel.FullName
        };
            try
            {
                var result = await userManager.CreateAsync(applicationUser, applicationUserModel.Password);
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var user = await userManager.FindByNameAsync(loginModel.UserName);
            if (user != null && await userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserID", user.Id.ToString())
                }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(applicationSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
            return BadRequest(new { error = localizer["Username or password is incorrect."].Value });
            }
        }
   }
