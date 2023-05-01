using Lab3.Data.Models;
using Lab3.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace Lab3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly UserManager<User> _userManager;
        public UserController(IConfiguration configuration , UserManager<User> userManager) 
        {
            _configuration= configuration;
            _userManager= userManager;
        }

        [HttpPost]
        [Route("Admin")]
        public async Task<ActionResult> Register_Admin(RegisterDto registerDto)
        {
            var newuser = new User
            {

                UserName = registerDto.username,
                Email = registerDto.email,
                Department = registerDto.Department,
            };

            var CreationResult = await _userManager.CreateAsync(newuser,registerDto.password);

            if(!CreationResult.Succeeded)
            {
                return BadRequest(CreationResult.Errors);
            }

            var claims = new List<Claim>
            {
                new Claim ( ClaimTypes.NameIdentifier , newuser.Id ),
                new Claim ( ClaimTypes.Role , "Admin"),
                new Claim ( "Nationality" , "Egyptian")
            };

            await _userManager.AddClaimsAsync(newuser,claims);

            return NoContent();
        }


        [HttpPost]
        [Route("User")]
        public async Task<ActionResult> Register_User(RegisterDto registerDto)
        {
            var newuser = new User
            {
                UserName = registerDto.username,
                Email = registerDto.email,
                Department = registerDto.Department,
            };

            var CreationResult = await _userManager.CreateAsync(newuser, registerDto.password);

            if (!CreationResult.Succeeded)
            {
                return BadRequest(CreationResult.Errors);
            }

            var claims = new List<Claim>
            {
                new Claim ( ClaimTypes.NameIdentifier , newuser.Id ),
                new Claim ( ClaimTypes.Role , "User"),
                new Claim ( "Nationality" , "Egyptian")
            };

            await _userManager.AddClaimsAsync(newuser, claims);

            return NoContent();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<TokenDto>>Login(LoginDto loginDto)
        {
            var newuser = await _userManager.FindByNameAsync(loginDto.Username);

            if ( newuser == null )
            {
                return BadRequest();
            }

            bool Is_Pass_Correct = await _userManager.CheckPasswordAsync(newuser, loginDto.Password);

            if (! Is_Pass_Correct )
            {
                return BadRequest();
            }

            var claims = await _userManager.GetClaimsAsync(newuser);

            return Generate_Token(claims);
        }

        public TokenDto Generate_Token (IList<Claim>claims)
        {
            string keystring = _configuration.GetValue<string>("SecreteKey");
            var KeyInBytes = Encoding.ASCII.GetBytes(keystring);
            var key = new SymmetricSecurityKey(KeyInBytes);

            var signingcredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var expiry = DateTime.Now.AddMinutes(15);
            var JWT = new JwtSecurityToken(
                    claims: claims,
                    signingCredentials: signingcredentials,
                    expires: expiry
                );

            var JwtHandler = new JwtSecurityTokenHandler();
            var JwtString = JwtHandler.WriteToken(JWT);

            return new TokenDto { 
                Token = JwtString ,
                Expiry = expiry};
        }
    }
}
