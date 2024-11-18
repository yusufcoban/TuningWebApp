using BaseBackend.Models;

using Dapper;

using System.Data.SqlClient;

namespace TuningWebApp.Controllers
{
    public class EcuHandler
    {
        private readonly IConfiguration _configuration;

        public EcuHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<EcuInfo> GetAllEcus()
        {
            using (var con = new SqlConnection(_configuration.GetConnectionString("dbo")))
            {
                // Opening the connection
                con.Open();

                // Dapper query: maps result to IEnumerable<Employee>
                var ecuList = con.Query<EcuInfo>("SELECT * FROM EcuInfo");

                return ecuList;
            }
        }
    }
}
