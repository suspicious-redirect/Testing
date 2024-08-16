using Microsoft.AspNetCore.Mvc;
using Apple.Data;
using Apple.Models;
using Microsoft.AspNetCore.Authorization;

namespace Apple.Controllers
{
    ////////////////////////////////////////////////////// DOCUMENT THIS
    [Route("api")]
    [ApiController]
    public class AppleController : Controller
    {
        private IAppleRepo _repo;
        public AppleController(IAppleRepo repo)
        {
            _repo = repo;
        }

        ////////////////////////////////////////////////////// DOCUMENT THIS
        [HttpPost("Register")]
        public ActionResult<string> Register(User user)
        {
            string response = "";
            User u = _repo.GetUser(user.UserName);
            if (u == null)
            {
                _repo.AddUser(user);
                response = "User successfully registered.";
            }
            else
            {
                response = "Username not available.";
            }

            return Ok(response);
        }

        ////////////////////////////////////////////////////// DOCUMENT THIS
        [Authorize(AuthenticationSchemes = "AppleAuthentication")]
        [Authorize(Policy = "UserOnly")]
        [HttpGet("GetVersionA")]
        public ActionResult<string> GetVersionA()
        {
            return Ok("1.0.0 (auth)");
        }
    }
}