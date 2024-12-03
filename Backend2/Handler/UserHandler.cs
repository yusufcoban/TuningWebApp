using BaseBackend.Models;

using Dapper;

using System.Data.SqlClient;
using System.Text;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace TuningWebApp.Controllers
{
    public class UserHandler
    {
        private readonly IConfiguration _configuration;

        public UserHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public User GetUserByUsername(string username)
        {
            using (var con = new SqlConnection(_configuration.GetConnectionString("dbo")))
            {
                con.Open();

                var user = con.QuerySingleOrDefault<User>(@"
            SELECT [UserID]
                  ,[Username]
                  ,[Email]
                  ,[FullName]
                  ,[Address]
                  ,[City]
                  ,[Role]
                  ,[ZipCode]
                  ,[Country]
                  ,[PhoneNumber]
                  ,[DateOfBirth]
                  ,[Credentials]
                  ,[CreatedAt]
                  ,[LastLoginAt]
                  ,[IsActive]
              FROM [Users]
              WHERE [Username] = @Username
        ", new { Username = username });

                return user;
            }
        }

        public IEnumerable<User> GetUsers()
        {
            using (var con = new SqlConnection(_configuration.GetConnectionString("dbo")))
            {
                con.Open();

                var users = con.Query<User>(@"
                    SELECT [UserID]
                          ,[Username]
                          ,[Email]
                          ,[FullName]
                          ,[Address]
                          ,[City]
                          ,[Role]
                          ,[ZipCode]
                          ,[Country]
                          ,[PhoneNumber]
                          ,[DateOfBirth]
                          ,[Credentials]
                          ,[CreatedAt]
                          ,[LastLoginAt]
                          ,[IsActive]
                      FROM [Users]
                ");

                return users;
            }
        }

        public int CreateUser(User user)
        {
            using (var con = new SqlConnection(_configuration.GetConnectionString("dbo")))
            {
                con.Open();

                var result = con.Execute(@"
                    INSERT INTO [Users] 
                        ([Username], [Email], [FullName], [Address], [City], [Role], [ZipCode], 
                         [Country], [PhoneNumber], [DateOfBirth], [Credentials], [PasswordHash], 
                         [CreatedAt], [IsActive]) 
                    VALUES 
                        (@Username, @Email, @FullName, @Address, @City, @Role, @ZipCode, 
                         @Country, @PhoneNumber, @DateOfBirth, @Credentials, @PasswordHash, 
                         GETDATE(), 1)
                ", user);

                return result;
            }
        }

        public int EditUserInfo(User user)
        {
            using (var con = new SqlConnection(_configuration.GetConnectionString("dbo")))
            {
                con.Open();

                var result = con.Execute(@"
                    UPDATE [Users]
                    SET 
                        [Username] = @Username,
                        [Email] = @Email,
                        [FullName] = @FullName,
                        [Address] = @Address,
                        [City] = @City,
                        [Role] = @Role,
                        [ZipCode] = @ZipCode,
                        [Country] = @Country,
                        [PhoneNumber] = @PhoneNumber,
                        [DateOfBirth] = @DateOfBirth
                    WHERE 
                        [UserID] = @UserID
                ", user);

                return result;
            }
        }

        public int ResetPassword(string userName)
        {
            using (var con = new SqlConnection(_configuration.GetConnectionString("dbo")))
            {
                con.Open();

                var result = con.Execute(@"
                    UPDATE [Users]
                    SET [PasswordHash] = @PasswordHash
                    WHERE [Username] = @Username
                ", new { Username = userName, PasswordHash = GenerateRandomPassword(10) });

                return result;
            }
        }

        public int DeleteUser(int userId)
        {
            using (var con = new SqlConnection(_configuration.GetConnectionString("dbo")))
            {
                con.Open();

                var result = con.Execute(@"
                    UPDATE [Users]
                    SET [IsActive] = 0
                    WHERE [UserID] = @UserID
                ", new { UserID = userId });

                return result;
            }
        }


        // Method to validate user credentials
        public User ValidateUserCredentials(string username, string password)
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
        public string HashPassword(string password)
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
        public bool VerifyPassword(string inputPassword, string storedHash)
        {
            string inputHash = HashPassword(inputPassword);
            return inputHash.Equals(storedHash);
        }



        public string GenerateRandomPassword(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
            StringBuilder result = new StringBuilder(length);
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] buffer = new byte[length];
                rng.GetBytes(buffer);
                for (int i = 0; i < length; i++)
                {
                    result.Append(validChars[buffer[i] % validChars.Length]);
                }
            }
            return result.ToString();
        }

    }
}
