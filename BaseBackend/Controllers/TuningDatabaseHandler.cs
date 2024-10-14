using BaseBackend.Models;

using Dapper;

using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Configuration;

using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace YourNamespace.Controllers
{
    public class TuningDatabaseHandler
    {
        private readonly IConfiguration _configuration;

        public TuningDatabaseHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public List<CarBrand> getCarBrands()
        {
            using (var con = new SqlConnection(_configuration.GetConnectionString("dbo")))
            {
                con.Open();

                // Step 1: Fetch all CarBrands
                var carBrands = con.Query<CarBrand>("SELECT * FROM CarBrand").ToList();

                // Step 2: Fetch CarModels for each CarBrand
                var carModels = con.Query<CarModel>("SELECT * FROM CarModel").ToList();

                // Step 3: Map CarModels to the corresponding CarBrands
                foreach (var brand in carBrands)
                {
                    brand.Models = carModels
                        .Where(model => model.CarBrandId == brand.Id) // Assuming CarBrandId is an int in CarModel
                        .ToList();
                }

                return carBrands;
            }
        }


        public List<TuningDatabaseInfo> getTuningDatabaseInfoById(string id)
        {
            using (var con = new SqlConnection(_configuration.GetConnectionString("dbo")))
            {
                // Open the connection
                con.Open();

                // SQL query to fetch TuningDatabaseInfo with filtered TuningVariants
                var carBrands = con.Query<TuningDatabaseInfo>("SELECT * FROM TuningDatabaseInfo Where Id =@id", new { id });
                if (carBrands != null)
                {
                    foreach (var item in carBrands)
                    {
                        item.Variants = con.Query<TuningVariant>("SELECT * FROM TuningVariant Where TuningId LIKE '" + id + "%" + "'").ToList();
                    }

                }

                return carBrands.ToList(); // Return the list of TuningDatabaseInfo with variants
            }
        }


        public List<TuningSpecialInfo> getTuningSpecialInfoByTuningId(string id)
        {
            using (var con = new SqlConnection(_configuration.GetConnectionString("dbo")))
            {
                // Open the connection
                con.Open();

                // SQL query to fetch tuning special info by Tuning ID
                var query = "SELECT * FROM TuningSpecialInfo WHERE Id = @Id";

                // Fetching data and mapping to List<TuningSpecialInfo>
                var tuningSpecialInfos = con.Query<TuningSpecialInfo>(query, new { Id = id }).ToList();

                // Load related data, e.g., AvailableSolutions or EcuInfo if needed
                foreach (var tuningSpecialInfo in tuningSpecialInfos)
                {
                    // Assuming AvailableSolutions are in another table
                    tuningSpecialInfo.AvailableSolutions = GetAvailableSolutionsByTuningId(tuningSpecialInfo.Id);
                    tuningSpecialInfo.EcuInfo = GetEcuInfoByTuningId(tuningSpecialInfo.EcuInfoId);
                    tuningSpecialInfo.EcuInfo.initi_after();
                }

                return tuningSpecialInfos;
            }
        }

        // Example of fetching AvailableSolutions (adjust based on your structure)
        private List<AvailableSolution> GetAvailableSolutionsByTuningId(string tuningId)
        {
            using (var con = new SqlConnection(_configuration.GetConnectionString("dbo")))
            {
                con.Open();
                var query = "SELECT * FROM AvailableSolution WHERE [TuningSpecialInfoId] = @TuningId"; // Adjust table/column names accordingly
                return con.Query<AvailableSolution>(query, new { TuningId = tuningId }).ToList();
            }
        }

        // Example of fetching EcuInfo (adjust based on your structure)
        private EcuInfo GetEcuInfoByTuningId(int ecuinfoid)
        {
            using (var con = new SqlConnection(_configuration.GetConnectionString("dbo")))
            {
                con.Open();
                var query = "SELECT * FROM [EcuInfo] WHERE [id] = @ecuinfoid"; // Adjust table/column names accordingly
                EcuInfo result = con.QueryFirstOrDefault<EcuInfo>(query, new { ecuinfoid });
                var query2 = "SELECT * FROM [ConnectionInfo] WHERE [id] = @resultId"; // Adjust table/column names accordingly
                result.AvailableConnection = con.Query<BaseBackend.Models.ConnectionInfo>(query2, new { resultId = result.ConnectionInfoId }).ToList();
                return result;
            }
        }

        private string GenerateTuningVariant(CarBrand carBrand, string typeName, int yearStart, int yearEnd, string engineName, int enginePowerKw, string fuelVariant, string specialInfo, EcuInfo selectedEcu, List<AvailableSolution> availableSolutions)
        {
            //carBrand => Golf
            //typeName Golf4=> will be typeName
            //specialInfo=>TuningSpecialInfo with ecuinfo NEW for link
            //generate carBrandid_NEWNUMEBR for  [TuningVariant] and [TuningSpecialInfo]
            //carBrandid_NEWNUMEBR => availableSolutions add new entries in db

            return "";
        }
        private string GenerateTuningSpecialInfo(TuningSpecialInfo createNewTuningSpecialInfo)
        {
            return "";

        }
        private string getFreeTuningSpecialName(string carBrandId)
        {
            //fetch like 12_X from [TuningVariant] if available, if not return 12_1_1. IF available split string by _ char and increase last one by 1 and return
            return "";
        }

    }

}