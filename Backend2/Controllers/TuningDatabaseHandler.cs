using System.Data.SqlClient;
using BaseBackend.Models;
using Dapper;

using TuningWebApp.Handler;

namespace TuningWebApp.Controllers
{
    public class TuningDatabaseHandler
    {
        private readonly IConfiguration _configuration;
        private readonly StringReplacementHandler _stringReplacementHandler;


        public TuningDatabaseHandler(IConfiguration configuration, StringReplacementHandler stringReplacementHandler)
        {
            _configuration = configuration;
            _stringReplacementHandler = stringReplacementHandler;
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
                var carBrands = con.Query<TuningDatabaseInfo>("SELECT * FROM TuningDatabaseInfo Where Id =@id  ", new { id });
                if (carBrands != null)
                {
                    foreach (var item in carBrands)
                    {
                        item.Variants = con.Query<TuningVariant>("SELECT * FROM TuningVariant Where TuningId LIKE '" + id + "%" + "' and isDeleted  = 0").ToList();
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
                var query = "SELECT * FROM TuningSpecialInfo WHERE Id = @Id and isdeleted=0";

                // Fetching data and mapping to List<TuningSpecialInfo>
                var tuningSpecialInfos = con.Query<TuningSpecialInfo>(query, new { Id = id }).ToList();

                // Load related data, e.g., AvailableSolutions or EcuInfo if needed
                foreach (var tuningSpecialInfo in tuningSpecialInfos)
                {
                    // Assuming AvailableSolutions are in another table
                    tuningSpecialInfo.AvailableSolutions = GetAvailableSolutionsByTuningId(tuningSpecialInfo.Id);
                    tuningSpecialInfo.EcuInfo = GetEcuInfoByTuningId(tuningSpecialInfo.EcuInfoId);
                    if (tuningSpecialInfo.EcuInfo != null)
                    {
                        tuningSpecialInfo.EcuInfo.initi_after();
                    }
                }

                return tuningSpecialInfos;
            }
        }

        public List<TuningSpecialInfo> getTuningSpecialInfoByTuningIdfull(string id)
        {
            List<TuningSpecialInfo> miniList = getTuningSpecialInfoByTuningId(id);
            foreach (var item in miniList)
            {
                foreach (var itemSolution in item.AvailableSolutions)
                {
                    itemSolution.ReplacementsCommands = new List<ReplacementCommand>();
                    List<SolutionMapping> replacementIds = _stringReplacementHandler.GetSolutionMappings(id, new List<string>() { itemSolution.Name });
                    foreach (var replacementId in replacementIds)
                    {
                        itemSolution.ReplacementsCommands.AddRange(_stringReplacementHandler.GetReplacementCommands(replacementId.ReplacementId));
                    }
                }
            }
            return miniList;
        }

        public void DeleteTuningVariant(string tuningVariantId)
        {
            using (var con = new SqlConnection(_configuration.GetConnectionString("dbo")))
            {
                con.Open();

                // Mark the TuningVariant record as deleted
                string deleteTuningVariantQuery = @"
            UPDATE [TuningVariant]
            SET [isDeleted] = 1
            WHERE [TuningId] = @TuningId";
                con.Execute(deleteTuningVariantQuery, new { TuningId = tuningVariantId });

                // Mark associated TuningSpecialInfo records as deleted
                string deleteTuningSpecialInfoQuery = @"
            UPDATE [TuningSpecialInfo]
            SET [isDeleted] = 1
            WHERE [Id] = @TuningId";
                con.Execute(deleteTuningSpecialInfoQuery, new { TuningId = tuningVariantId });

                // Mark related AvailableSolution records as deleted
                string deleteAvailableSolutionQuery = @"
            UPDATE [AvailableSolution]
            SET [isDeleted] = 1
            WHERE [TuningSpecialInfoId] = @TuningId";
                con.Execute(deleteAvailableSolutionQuery, new { TuningId = tuningVariantId });

                // Additional tables can be handled here if needed
            }
        }


        public void UpdateTuningVariant(InputNewVariant inputNewVariant)
        {
            UpdateExistingTuningVariant(inputNewVariant.tuningvariantid, inputNewVariant.TypeName, $"{inputNewVariant.YearStart}-{inputNewVariant.YearEnd}", inputNewVariant.EngineName, inputNewVariant.EnginePowerKw.ToString(), inputNewVariant.FuelVariant);

            // Insert a new record into the TuningSpecialInfo table, associating it with the selected ECU info and special details
            UpdateExistingTuningSpecialInfo(inputNewVariant.tuningvariantid, inputNewVariant.SpecialInfo, inputNewVariant.SelectedEcu.Id);

            // Loop through the list of available solutions and insert each into the AvailableSolution table
            foreach (var solution in inputNewVariant.AvailableSolutions)
            {
                UpdateExistingAvailableSolution(inputNewVariant.tuningvariantid, solution.Name, solution.Information, solution.Value1, solution.Value2);
                string deleteQuery = "DELETE FROM [ReplacementStrings] WHERE ReplacementId in (SELECT [ReplacementId] FROM [SolutionMappings] WHERE TuningSpecialInfoId = @newTuningVariantId)";
                string deleteQuery2 = "DELETE FROM [SolutionMappings] WHERE TuningSpecialInfoId = @newTuningVariantId";

                using (var con = new SqlConnection(_configuration.GetConnectionString("dbo")))
                {
                    // Open the connection
                    con.Open();
                    con.Execute(deleteQuery, new { inputNewVariant.tuningvariantid });
                    con.Execute(deleteQuery2, new { inputNewVariant.tuningvariantid });
                }

                foreach (input_ReplacementStrings item in solution.replacementStrings)
                {
                    _stringReplacementHandler.InsertReplacementStringAsync(inputNewVariant.tuningvariantid, solution.Name, item.searchString, item.replacementString, item.number);
                }
            }
        }

        private void UpdateExistingAvailableSolution(string newTuningVariantId, string name, string information, int value1, int value2)
        {
            string deleteQuery = "DELETE from [AvailableSolution] where [TuningSpecialInfoId] =@newTuningVariantId";

            using (var con = new SqlConnection(_configuration.GetConnectionString("dbo")))
            {
                // Open the connection
                con.Open();
                con.Execute(deleteQuery, new { newTuningVariantId });
            }

            CreateNewAvailableSolution(newTuningVariantId, name, information, value1, value2);
        }

        private void UpdateExistingTuningSpecialInfo(string newTuningVariantId, string specialInfo, int id)
        {
            string deleteQuery = "DELETE from [TuningSpecialInfo] where [Id] =@newTuningVariantId";
            using (var con = new SqlConnection(_configuration.GetConnectionString("dbo")))
            {
                // Open the connection
                con.Open();
                con.Execute(deleteQuery, new { newTuningVariantId });
            }
            CreateNewTuningSpecialInfo(newTuningVariantId, specialInfo, id);
        }

        private void UpdateExistingTuningVariant(string newTuningVariantId, string typeName, string v1, string engineName, string v2, string fuelVariant)
        {
            string deleteQuery = "DELETE from [TuningVariant] where [TuningId] =@newTuningVariantId";
            using (var con = new SqlConnection(_configuration.GetConnectionString("dbo")))
            {
                // Open the connection
                con.Open();
                con.Execute(deleteQuery, new { newTuningVariantId });
            }
            CreateNewTuningVariant(newTuningVariantId,  typeName,  v1,  engineName,  v2,  fuelVariant);
        }

        public string GenerateTuningVariant(InputNewVariant inputNewVariant)
        {
            // Generate a unique identifier for the tuning variant using the car brand's ID, e.g., "12_1"
            string newTuningVariantId = getFreeTuningSpecialName(inputNewVariant.CarBrand.Id); // e.g., "12_1_1"
            VerifyTuningDatabaseInfo(inputNewVariant);
            // Insert a new record into the TuningVariant table with the provided details (e.g., "Golf4", "2000-2005", engine details)
            CreateNewTuningVariant(newTuningVariantId, inputNewVariant.TypeName, $"{inputNewVariant.YearStart}-{inputNewVariant.YearEnd}", inputNewVariant.EngineName, inputNewVariant.EnginePowerKw.ToString(), inputNewVariant.FuelVariant);

            // Insert a new record into the TuningSpecialInfo table, associating it with the selected ECU info and special details
            CreateNewTuningSpecialInfo(newTuningVariantId, inputNewVariant.SpecialInfo, inputNewVariant.SelectedEcu.Id);

            // Loop through the list of available solutions and insert each into the AvailableSolution table
            foreach (var solution in inputNewVariant.AvailableSolutions)
            {
                CreateNewAvailableSolution(newTuningVariantId, solution.Name, solution.Information, solution.Value1, solution.Value2);
                foreach (input_ReplacementStrings item in solution.replacementStrings)
                {
                    _stringReplacementHandler.InsertReplacementStringAsync(newTuningVariantId, solution.Name, item.searchString, item.replacementString, item.number);
                }
            }

            // Return the new TuningVariantId or a success message indicating the tuning variant has been created
            return newTuningVariantId; // or return a confirmation message
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
                if (result != null)
                {
                    var query2 = "SELECT * FROM [ConnectionInfo] WHERE [id] = @resultId"; // Adjust table/column names accordingly
                    var resultList = con.Query<BaseBackend.Models.ConnectionInfo>(query2, new { resultId = result?.ConnectionInfoId });
                    if (resultList != null && resultList.Any())
                    {
                        result.AvailableConnection = resultList.ToList();
                    }
                    else
                    {
                        result.AvailableConnection = new List<BaseBackend.Models.ConnectionInfo>();
                    }
                }


                return result;
            }
        }
        private void VerifyTuningDatabaseInfo(InputNewVariant inputNewVariant)
        {
            string query = @"
                             SELECT  [Id]
                              ,[Brand]
                              ,[Information]
                          FROM [dbo].[TuningDatabaseInfo] Where Id=@id
                        ";
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("dbo")))
            {
                // Open the connection
                conn.Open();

                // Execute the query using Dapper's Execute method, passing in the parameters as an anonymous object
                TuningDatabaseInfo verifyObject = conn.QueryFirstOrDefault<TuningDatabaseInfo>(query, new
                {
                    id = inputNewVariant.CarBrand.Id,
                });
                if (verifyObject == null)
                {
                    string queryInsert = @"
                             INSERT INTO [dbo].[TuningDatabaseInfo] (Id, Brand, Information)
                                VALUES (@Id, @Brand, @Information)";
                    CarBrand fullBrand = fetchCarBrandById(inputNewVariant.CarBrand.Id);
                    conn.Execute(queryInsert, new
                    {
                        Id = inputNewVariant.CarBrand.Id,
                        Brand = fullBrand.Name,
                        Information = inputNewVariant.TypeName
                    });
                }
            }
        }

        private CarBrand fetchCarBrandById(string ID)
        {
            string query = @"
                          SELECT [Id]
                            ,[Name]
                            ,[Icon]
                            ,[Slug]
                          FROM [dbo].[CarBrand] Where Id=@id
                        ";
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("dbo")))
            {
                // Open the connection
                conn.Open();

                // Execute the query using Dapper's Execute method, passing in the parameters as an anonymous object
                return conn.QueryFirstOrDefault<CarBrand>(query, new
                {
                    Id = int.Parse(ID.Split('_').First()),
                });
            }
        }
        private void CreateNewTuningSpecialInfo(string newTuningVariantIdSpecial, string specialInfo, int ecuId)
        {
            // Define the SQL query for inserting a new record
            string query = @"
                             INSERT INTO [dbo].[TuningSpecialInfo] (Id, AdditionalInformation, EcuInfoId, isDeleted)
                                VALUES (@Id, @AdditionalInformation, @EcuInfoId , false)";

            // Use the using statement to ensure proper disposal of the connection
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("dbo")))
            {
                // Open the connection
                conn.Open();

                // Execute the query using Dapper's Execute method, passing in the parameters as an anonymous object
                conn.Execute(query, new
                {
                    Id = newTuningVariantIdSpecial,
                    AdditionalInformation = string.IsNullOrEmpty(specialInfo) ? null : specialInfo,
                    EcuInfoId = ecuId > 0 ? ecuId : (int?)null
                });
            }
        }

        private void CreateNewTuningVariant(string newTuningVariantId, string typeName, string year, string engineName, string horsepower, string fuelVariant)
        {
            // Step 1: Define the SQL query for inserting a new record into the TuningVariant table
            string query = @"
                              INSERT INTO [dbo].[TuningVariant] (TuningId, TypeName, Year, Engine, Horsepower, Variant, isDeleted)
                              VALUES (@TuningId, @TypeName, @Year, @Engine, @Horsepower, @Variant, false)";

            // Step 2: Use the 'using' statement to ensure proper disposal of the connection
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("dbo")))
            {
                // Step 3: Open the connection
                conn.Open();

                // Step 4: Execute the query using Dapper's Execute method, passing the parameters as an anonymous object
                conn.Execute(query, new
                {
                    TuningId = newTuningVariantId,
                    TypeName = typeName,
                    Year = year,
                    Engine = engineName,
                    Horsepower = horsepower,
                    Variant = fuelVariant
                });
            }
        }

        private void CreateNewAvailableSolution(string newTuningVariantIdSpecial, string name, string information, int value1, int value2)
        {
            // Define the SQL query for inserting a new record into the AvailableSolution table
            string query = @"
                             INSERT INTO [dbo].[AvailableSolution] (TuningSpecialInfoId, Name, Information, Value1, Value2, Checked, isDeleted)
                             VALUES (@TuningSpecialInfoId, @Name, @Information, @Value1, @Value2, @Checked, false)";

            // Use the 'using' statement to ensure proper disposal of the connection
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("dbo")))
            {
                // Open the connection
                conn.Open();

                // Execute the query using Dapper's Execute method, passing in the parameters
                conn.Execute(query, new
                {
                    TuningSpecialInfoId = newTuningVariantIdSpecial,
                    Name = name,
                    Information = string.IsNullOrEmpty(information) ? null : information,
                    Value1 = value1,
                    Value2 = value2,
                    Checked = false // default to unchecked, you can change it based on your requirements
                });
            }
        }

        private string getFreeTuningSpecialName(string carBrandId)
        {
            // Fetch like `1_2_X` from [TuningSpecialInfo] if available, if not return `1_2_1`.
            // If available, split the string by `_` and increase the last part by 1 and return.
            using (var con = new SqlConnection(_configuration.GetConnectionString("dbo")))
            {
                con.Open();
                // Modify the query to search for all entries starting with the carBrandId (e.g., "1_2%")
                string query = "SELECT [TuningId] FROM [dbo].[TuningVariant] WHERE [TuningId] LIKE @carBrandId + '%'";
                List<string> result = con.Query<string>(query, new { carBrandId }).ToList();

                // Check if the result contains any entries
                if (result != null && result.Any())
                {
                    // Parse the IDs to get the highest suffix number
                    int highestDigit = 0;
                    foreach (var item in result)
                    {
                        // Split the ID by `_` and take the last part (the numeric suffix)
                        int lastDigit = int.Parse(item.Split('_').Last());

                        // Find the highest suffix number
                        if (lastDigit > highestDigit)
                        {
                            highestDigit = lastDigit;
                        }
                    }

                    // Return the new ID with the incremented suffix
                    return carBrandId + "_" + (highestDigit + 1);
                }

                // If no results, return carBrandId with `_1` as the initial suffix
                return carBrandId + "_1";
            }
        }

        public List<EcuInfo> GetEcuList()
        {
            // Fetch like `1_2_X` from [TuningSpecialInfo] if available, if not return `1_2_1`.
            // If available, split the string by `_` and increase the last part by 1 and return.
            using (var con = new SqlConnection(_configuration.GetConnectionString("dbo")))
            {
                con.Open();
                // Modify the query to search for all entries starting with the carBrandId (e.g., "1_2%")
                string query = "SELECT * FROM [dbo].[EcuInfo]";
                List<EcuInfo> result = con.Query<EcuInfo>(query).ToList();

                return result;
            }
        }

    }

}