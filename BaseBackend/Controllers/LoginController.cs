using BaseBackend.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

[ApiController]
[Route("api")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        // Validate user credentials (this is just an example)
        if (request.Username == "admin" && request.Password == "admin") // Use a proper user validation method
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, request.Username),
            new Claim(ClaimTypes.Role, "Admin"), // Set user role if needed
        };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return Ok(new { message = "Login successful", role = "Admin" });
        }

        var users = new Dictionary<string, string>
    { { "user", "user" },
        { "user1", "user1" },
        { "user2", "user2" },
        { "user3", "user3" },
        { "user4", "user4" },
        { "user5", "user5" },
        { "user6", "user6" },
        { "user7", "user7" },
        { "user8", "user8" },
        { "user9", "user9" },
        { "user10", "user10" }
    };

        if (users.ContainsKey(request.Username) && users[request.Username] == request.Password)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, request.Username),
            new Claim(ClaimTypes.Role, "user"), // Set user role as "User"
        };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return Ok(new { message = "Login successful", role = "User" });
        }

        return Unauthorized(new { message = "Invalid username or password" });
    }


    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok(new { message = "Logout successful" });
    }

    [HttpGet("UserInformation")]
    [Authorize]
    public async Task<IActionResult> UserInformation()
    {
        var username = User.Identity.Name; // This gets the username
        UserInformation currentUser = new UserInformation(username);
        return Ok(new { currentUser });
    }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}
