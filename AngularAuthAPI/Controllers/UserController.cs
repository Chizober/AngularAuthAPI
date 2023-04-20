using AngularAuthAPI.Context;
using AngularAuthAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngularAuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _authContext;
        public UserController(AppDbContext appDbContext)
        {
            _authContext = appDbContext;
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userDbj )
        {
            if(userDbj == null)
            
                return BadRequest();
                var user = await _authContext.Users.FirstOrDefaultAsync(x=>x.UserName==userDbj.UserName && x.Password==userDbj.Password);   
            if(user == null)
                return NotFound(new {Message="User Not Found"});
            return Ok(new
            {
                Message = "Login SuccessFul!"
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userDbj)
        {
            if (userDbj == null)

                return BadRequest();
            await _authContext.Users.AddAsync(userDbj);
            await _authContext.SaveChangesAsync();
            return Ok(new
            {
                Message = "User Registered"
            }); 

        }
    }
}
