using BaseBackend.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TuningWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Ensure only authenticated users can access this controller
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserHandler _userHandler;

        // Constructor injection for both IConfiguration and TuningDatabaseHandler
        public UserController(IConfiguration configuration, UserHandler userHandler)
        {
            _configuration = configuration;
            _userHandler = userHandler;
        }


        // 1st API: Get list of all car brands
        [HttpGet("GetAllUser")]
        public ActionResult<List<CarBrand>> GetAllUser()
        {
            return Ok(_userHandler.GetUsers());
        }


        [HttpPost("CreateUser")]
        [Authorize(Roles = "Admin")]
        public ActionResult<TuningSpecialInfo> CreateUser(User user)
        {
            return Ok(_userHandler.CreateUser(user));
        }

        [HttpPost("EditUserInfo")]
        public ActionResult<TuningSpecialInfo> EditUserInfo(User user)
        {
            var username = User.Identity.Name; // This gets the username
            UserInformation currentUser = new UserInformation(username);
            User requestedUser = _userHandler.GetUserByUsername(username);
            if (requestedUser != null && (currentUser.Username == user.Username || User.IsInRole("Admin")))
            {
                return Ok(_userHandler.CreateUser(user));
            }
            return BadRequest();
        }

        //Todo check for admin
        [HttpPost("ResetPassword")]
        public ActionResult<TuningSpecialInfo> ResetPassword(StringInput stringInput)
        {
            var username = User.Identity.Name; // This gets the username
            UserInformation currentUser = new UserInformation(username);
            User requestedUser = _userHandler.GetUserByUsername(stringInput.Input);
            if (requestedUser != null && (currentUser.Username == requestedUser.Username || User.IsInRole("Admin")))
            {
                _userHandler.ResetPassword(stringInput.Input);

                return Ok();
            }
            return BadRequest();
        }
    }
}
