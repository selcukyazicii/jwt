using jwt.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwt.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IJWTAuthenticationManager _jwtAuthenticationManager;      
        public IActionResult Get()
        {
            return Ok("value,value");
        }

        public ValuesController (IJWTAuthenticationManager jwtAuthenticationManager)
        {
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }
        User user = new User();
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authentication([FromBody] User user)
        {
            var token = _jwtAuthenticationManager.Authenticate(user.UserName, user.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
       public class User
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}
