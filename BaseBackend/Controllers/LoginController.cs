using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

namespace BaseBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        // Simple in-memory user store for demonstration purposes
        private static readonly Dictionary<string, (string password, string role)> Users = new()
        {
            { "admin", ("admin", "Admin") },
            { "user", ("user", "User") }
        };

        // POST: api/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Check if the username exists and the password matches
            if (Users.ContainsKey(request.Username) && Users[request.Username].password == request.Password)
            {
                var userRole = Users[request.Username].role;

                // Create user claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, request.Username),
                    new Claim(ClaimTypes.Role, userRole)
                };

                // Create identity and principal
                var identity = new ClaimsIdentity(claims, "CookieAuth");  // Changed to use "CookieAuth"
                var principal = new ClaimsPrincipal(identity);

                // Sign in the user using the correct scheme
                await HttpContext.SignInAsync("CookieAuth", principal);  // Changed to use "CookieAuth"

                // Return a JSON response with the role
                return Ok(new { Message = "Login successful", Role = userRole });
            }

            // Invalid credentials, return 401 Unauthorized
            return Unauthorized(new { Message = "Invalid username or password" });
        }

        // GET: api/Logout
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            // Sign out the user using the correct scheme
            await HttpContext.SignOutAsync("CookieAuth");  // Changed to use "CookieAuth"
            return Ok(new { Message = "Logout successful" });
        }

        // A sample protected route to test role-based access
        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminPage()
        {
            return Ok(new { Message = "Welcome, Admin!" });
        }

        [HttpGet("user")]
        [Authorize(Roles = "User")]
        public IActionResult UserPage()
        {
            return Ok(new { Message = "Welcome, User!" });
        }
    }

    // Model for Login Request
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
