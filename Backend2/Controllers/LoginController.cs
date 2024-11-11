using Dapper;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Data.SqlClient;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

[ApiController]
[Route("api")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // Login Action
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            // Validate user credentials against the database
            var user = ValidateUserCredentials(request.Username, request.Password);
            if (user != null)
            {
                // Create claims for the logged-in user
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, request.Username),
                new Claim(ClaimTypes.Role, user.Role)  // Set user role from DB if needed
            };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Sign in the user with cookie authentication
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                return Ok(new { message = "Login successful", role = user.Role });
            }
            return Unauthorized(new { message = "Invalid username or password"+"\r\n"+_configuration.GetConnectionString("dbo") });

        }
        catch (Exception ex)
        {
            //DEBUG
            
            return BadRequest(ex.Message + "\r\n"+_configuration.GetConnectionString("dbo"));
        } }

    // Logout Action
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok(new { message = "Logout successful" });
    }

    // Fetch user information after login
    [HttpGet("UserInformation")]
    [Authorize]
    public IActionResult UserInformation()
    {
        var username = User.Identity.Name; // Get logged-in username
        UserInformation currentUser = new UserInformation(username);
        return Ok(new { currentUser });
    }

    // Inserts users with hashed passwords into the database
    private void InsertUsers((string username, string password)[] users, string role = "User")
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("dbo")))
        {
            connection.Open();

            foreach (var (username, password) in users)
            {
                // Hash the password
                string hashedPassword = HashPassword(password);

                // Insert the user into the database
                string sql = "INSERT INTO Users (Username, PasswordHash, Role) VALUES (@Username, @PasswordHash, @Role)";
                connection.Execute(sql, new { Username = username, PasswordHash = hashedPassword, Role = role });
            }
        }
    }

    
    // Method to validate user credentials
    private User ValidateUserCredentials(string username, string password)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("dbo")))
        {
            connection.Open();

            // Fetch the hashed password and role for the user from the database
            string sql = "SELECT Username, PasswordHash, Role FROM Users WHERE Username = @Username";
            var user = connection.QuerySingleOrDefault<User>(sql, new { Username = username });

            if (user != null)
            {
                // Check if the provided password matches the stored hashed password
                if (VerifyPassword(password, user.PasswordHash))
                {
                    return user;
                }
            }
        }

        return null;
    }

    // Method to hash the password using SHA-256
    private string HashPassword(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2")); // Convert to hexadecimal
            }
            return builder.ToString();
        }
    }

    // Method to verify if a password matches the stored hash
    private bool VerifyPassword(string inputPassword, string storedHash)
    {
        string inputHash = HashPassword(inputPassword);
        return inputHash.Equals(storedHash);
    }
}

// LoginRequest class representing the login request payload
public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}


// UserInformation class representing user data
public class UserInformation
{
    public string Username { get; set; }

    public UserInformation(string username)
    {
        Username = username;
    }
}
