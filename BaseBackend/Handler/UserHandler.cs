using Dapper;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace YourNamespace.Controllers
{
    public class UserHandler
    {
        private readonly IConfiguration _configuration;

        public UserHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<User> GetUsers()
        {
            using (var con = new SqlConnection(_configuration.GetConnectionString("dbo")))
            {
                // Opening the connection
                con.Open();

                // Dapper query: maps result to IEnumerable<Employee>
                var users = con.Query<User>("SELECT * FROM Users");

                return users;
            }
        }
    }
}
