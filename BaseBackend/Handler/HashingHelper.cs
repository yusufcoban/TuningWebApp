using Dapper;

using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

public class HashingHelper
{
    private readonly IConfiguration _configuration;

    public HashingHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // Method to hash the password using SHA-256
    public static string HashPassword(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // Compute hash from the input string
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Convert byte array to a string
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2")); // Convert to hexadecimal
            }
            return builder.ToString();
        }
    }

    // Method to verify if the provided password matches the hashed password
    public static bool VerifyPassword(string inputPassword, string storedHash)
    {
        // Hash the input password and compare it with the stored hash
        string inputHash = HashPassword(inputPassword);
        return inputHash.Equals(storedHash);
    }

    public bool VerifyUser(string username, string inputPassword)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("dbo").ToString()))
        {
            connection.Open();

            // Get the stored hash for the user
            string sql = "SELECT PasswordHash FROM Users WHERE Username = @Username";
            string storedHash = connection.QuerySingleOrDefault<string>(sql, new { Username = username });

            if (storedHash == null)
            {
                return false; // User not found
            }

            // Verify if the hashed input password matches the stored hash
            return HashingHelper.VerifyPassword(inputPassword, storedHash);
        }
    }
}
