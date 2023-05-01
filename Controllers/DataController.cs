using Lab3.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        public DataController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetDataForAll()
        {
            var user = await _userManager.GetUserAsync(User);

            return Ok(new string[] { "omar", "mohamed",user.UserName,user.Email,user.Department });
        }

        [HttpGet]
        [Authorize(Policy ="Manager")]
        [Route("manager")]
        public ActionResult GetDataManager()
        {
            return Ok (new string[] { "omar", "mohamed", "Data for managers"});
        }


        [HttpGet]
        [Authorize(Policy = "User")]
        [Route("user")]
        public ActionResult GetDataUser()
        {
            return Ok(new string[] { "omar", "mohamed", "Data for users" });
        }
    }
}
