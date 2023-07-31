using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NzWalks.Api.Dtos;

namespace NzWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenRepository tokenRepository;

        public UserManager<IdentityUser> UserManager { get; set; }
        public AuthController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository)
        {
            UserManager = userManager;
            this.tokenRepository = tokenRepository;
        }        

        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterRequestDto registerRequestDto)
        {
            var user = new IdentityUser()
            {
                UserName = registerRequestDto.User,
                Email = registerRequestDto.User
            };

            var identityResult = await UserManager.CreateAsync(user, registerRequestDto.Password);
            if (identityResult.Succeeded)
            {
                if(registerRequestDto.Roles !=null && registerRequestDto.Roles.Any())
                {
                    foreach (var roles in registerRequestDto.Roles)
                        identityResult = await UserManager.AddToRoleAsync(user, roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User Added");
                    }

                }
            }
            return BadRequest("Please Conact to developer");

        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            var UserExist = await UserManager.FindByEmailAsync(loginUserDto.UserName);
            if(UserExist!=null)
            {
                var passwordMatch = await UserManager.CheckPasswordAsync(UserExist, loginUserDto.Password);
                if (passwordMatch)
                {
                    var roles = await UserManager.GetRolesAsync(UserExist);

                    //create tocken
                    if(roles!=null)
                    {
                        var jwtToken = await tokenRepository.CreateJwtToken(UserExist, roles.ToList());
                        
                        return Ok(jwtToken);

                    }

                }
            }
            return NotFound("Username Or Password Not exist");
        }
    }
}
