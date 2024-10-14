using BaseBackend.Models;

using Dapper;

using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Configuration;

using System.Data.SqlClient;

namespace YourNamespace.Controllers
{
    public class TuningDatabaseHandler
    {
        private readonly IConfiguration _configuration;

        public TuningDatabaseHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Generate fake tuning special info data
        /*private static List<TuningSpecialInfo> tuningSpecialInfos = new List<TuningSpecialInfo>
        {
          new TuningSpecialInfo
            {
                Id = "12_1_1",
                AvailableSolutions = new List<AvailableSolution>
                {
                    new AvailableSolution { Name = "egr", Information = "No unplug required", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "dpf", Information = "Unplug all sensors", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "stage1", Information = "+15ps/+40nm", Value1 = 15, Value2 = 40, Checked = false },
                    new AvailableSolution { Name = "stage2", Information = "+25ps/+55nm", Value1 = 25, Value2 = 55, Checked = false },
                    new AvailableSolution { Name = "adblue", Information = "AdBlue system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "flaps", Information = "Flap system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "Dtc", Information = "OBD read enough to handle the errors", Value1 = 0, Value2 = 0, Checked = false }
                },
                AdditionalInformation = "For optimal performance with Stage 2 tuning, we recommend upgrading the intake and exhaust systems.",
                EcuInfo = new EcuInfo
                {
                    EcuName = "ME7.5",
                    TuningToolsInfo = new TuningToolsInfo
                    {
                        Kess = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Flex = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Mpps = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        }
                    }
                }
            },
            new TuningSpecialInfo
            {
                Id = "12_1_2",
                AvailableSolutions = new List<AvailableSolution>
                {
                    new AvailableSolution { Name = "egr", Information = "No unplug required", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "dpf", Information = "Unplug all sensors", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "stage1", Information = "+20ps/+45nm", Value1 = 20, Value2 = 45, Checked = false },
                    new AvailableSolution { Name = "stage2", Information = "+30ps/+65nm", Value1 = 30, Value2 = 65, Checked = false },
                    new AvailableSolution { Name = "adblue", Information = "AdBlue system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "flaps", Information = "Flap system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "Dtc", Information = "OBD read enough to handle the errors", Value1 = 0, Value2 = 0, Checked = false }
                },
                AdditionalInformation = "For optimal performance with Stage 2 tuning, we recommend upgrading the intake and exhaust systems.",
                EcuInfo = new EcuInfo
                {
                    EcuName = "ME7.5",
                    TuningToolsInfo = new TuningToolsInfo
                    {
                        Kess = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Flex = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Mpps = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        }
                    }
                }
            },
            new TuningSpecialInfo
            {
                Id = "12_1_3",
                AvailableSolutions = new List<AvailableSolution>
                {
                    new AvailableSolution { Name = "egr", Information = "No unplug required", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "dpf", Information = "Unplug all sensors", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "stage1", Information = "+15ps/+35nm", Value1 = 15, Value2 = 35, Checked = false },
                    new AvailableSolution { Name = "stage2", Information = "+25ps/+55nm", Value1 = 25, Value2 = 55, Checked = false },
                    new AvailableSolution { Name = "adblue", Information = "AdBlue system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "flaps", Information = "Flap system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "Dtc", Information = "OBD read enough to handle the errors", Value1 = 0, Value2 = 0, Checked = false }
                },
                AdditionalInformation = "For optimal performance with Stage 2 tuning, we recommend upgrading the intake and exhaust systems.",
                EcuInfo = new EcuInfo
                {
                    EcuName = "ME7.5",
                    TuningToolsInfo = new TuningToolsInfo
                    {
                        Kess = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Flex = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Mpps = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        }
                    }
                }
            },
            new TuningSpecialInfo
            {
                Id = "12_1_4",
                AvailableSolutions = new List<AvailableSolution>
                {
                    new AvailableSolution { Name = "egr", Information = "No unplug required", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "dpf", Information = "Unplug all sensors", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "stage1", Information = "+15ps/+40nm", Value1 = 15, Value2 = 40, Checked = false },
                    new AvailableSolution { Name = "stage2", Information = "+25ps/+55nm", Value1 = 25, Value2 = 55, Checked = false },
                    new AvailableSolution { Name = "adblue", Information = "AdBlue system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "flaps", Information = "Flap system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "Dtc", Information = "OBD read enough to handle the errors", Value1 = 0, Value2 = 0, Checked = false }
                },
                AdditionalInformation = "For optimal performance with Stage 2 tuning, we recommend upgrading the intake and exhaust systems.",
                EcuInfo = new EcuInfo
                {
                    EcuName = "ME7.5",
                    TuningToolsInfo = new TuningToolsInfo
                    {
                        Kess = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Flex = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Mpps = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        }
                    }
                }
            },
            new TuningSpecialInfo
            {
                Id = "12_1_5",
                AvailableSolutions = new List<AvailableSolution>
                {
                    new AvailableSolution { Name = "egr", Information = "No unplug required", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "dpf", Information = "Unplug all sensors", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "stage1", Information = "+15ps/+40nm", Value1 = 15, Value2 = 40, Checked = false },
                    new AvailableSolution { Name = "stage2", Information = "+25ps/+55nm", Value1 = 25, Value2 = 55, Checked = false },
                    new AvailableSolution { Name = "adblue", Information = "AdBlue system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "flaps", Information = "Flap system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "Dtc", Information = "OBD read enough to handle the errors", Value1 = 0, Value2 = 0, Checked = false }
                },
                AdditionalInformation = "For optimal performance with Stage 2 tuning, we recommend upgrading the intake and exhaust systems.",
                EcuInfo = new EcuInfo
                {
                    EcuName = "ME7.5",
                    TuningToolsInfo = new TuningToolsInfo
                    {
                        Kess = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Flex = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Mpps = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        }
                    }
                }
            },
            new TuningSpecialInfo
            {
                Id = "12_1_6",
                AvailableSolutions = new List<AvailableSolution>
                {
                    new AvailableSolution { Name = "egr", Information = "No unplug required", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "dpf", Information = "Unplug all sensors", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "stage1", Information = "+15ps/+40nm", Value1 = 15, Value2 = 40, Checked = false },
                    new AvailableSolution { Name = "stage2", Information = "+25ps/+55nm", Value1 = 25, Value2 = 55, Checked = false },
                    new AvailableSolution { Name = "adblue", Information = "AdBlue system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "flaps", Information = "Flap system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "Dtc", Information = "OBD read enough to handle the errors", Value1 = 0, Value2 = 0, Checked = false }
                },
                AdditionalInformation = "For optimal performance with Stage 2 tuning, we recommend upgrading the intake and exhaust systems.",
                EcuInfo = new EcuInfo
                {
                    EcuName = "ME7.5",
                    TuningToolsInfo = new TuningToolsInfo
                    {
                        Kess = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Flex = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Mpps = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        }
                    }
                }
            },
            new TuningSpecialInfo
            {
                Id = "12_1_7",
                AvailableSolutions = new List<AvailableSolution>
                {
                    new AvailableSolution { Name = "egr", Information = "No unplug required", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "dpf", Information = "Unplug all sensors", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "stage1", Information = "+15ps/+40nm", Value1 = 15, Value2 = 40, Checked = false },
                    new AvailableSolution { Name = "stage2", Information = "+25ps/+55nm", Value1 = 25, Value2 = 55, Checked = false },
                    new AvailableSolution { Name = "adblue", Information = "AdBlue system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "flaps", Information = "Flap system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "Dtc", Information = "OBD read enough to handle the errors", Value1 = 0, Value2 = 0, Checked = false }
                },
                AdditionalInformation = "For optimal performance with Stage 2 tuning, we recommend upgrading the intake and exhaust systems.",
                EcuInfo = new EcuInfo
                {
                    EcuName = "ME7.5",
                    TuningToolsInfo = new TuningToolsInfo
                    {
                        Kess = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Flex = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Mpps = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        }
                    }
                }
            },
            new TuningSpecialInfo
            {
                Id = "12_1_8",
                AvailableSolutions = new List<AvailableSolution>
                {
                    new AvailableSolution { Name = "egr", Information = "No unplug required", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "dpf", Information = "Unplug all sensors", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "stage1", Information = "+15ps/+40nm", Value1 = 15, Value2 = 40, Checked = false },
                    new AvailableSolution { Name = "stage2", Information = "+25ps/+55nm", Value1 = 25, Value2 = 55, Checked = false },
                    new AvailableSolution { Name = "adblue", Information = "AdBlue system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "flaps", Information = "Flap system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "Dtc", Information = "OBD read enough to handle the errors", Value1 = 0, Value2 = 0, Checked = false }
                },
                AdditionalInformation = "For optimal performance with Stage 2 tuning, we recommend upgrading the intake and exhaust systems.",
                EcuInfo = new EcuInfo
                {
                    EcuName = "ME7.5",
                    TuningToolsInfo = new TuningToolsInfo
                    {
                        Kess = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Flex = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Mpps = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        }
                    }
                }
            },
            new TuningSpecialInfo
            {
                Id = "12_1_9",
                AvailableSolutions = new List<AvailableSolution>
                {
                    new AvailableSolution { Name = "egr", Information = "No unplug required", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "dpf", Information = "Unplug all sensors", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "stage1", Information = "+15ps/+40nm", Value1 = 15, Value2 = 40, Checked = false },
                    new AvailableSolution { Name = "stage2", Information = "+25ps/+55nm", Value1 = 25, Value2 = 55, Checked = false },
                    new AvailableSolution { Name = "adblue", Information = "AdBlue system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "flaps", Information = "Flap system modification", Value1 = 0, Value2 = 0, Checked = false },
                    new AvailableSolution { Name = "Dtc", Information = "OBD read enough to handle the errors", Value1 = 0, Value2 = 0, Checked = false }
                },
                AdditionalInformation = "For optimal performance with Stage 2 tuning, we recommend upgrading the intake and exhaust systems.",
                EcuInfo = new EcuInfo
                {
                    EcuName = "ME7.5",
                    TuningToolsInfo = new TuningToolsInfo
                    {
                        Kess = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Flex = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        },
                        Mpps = new ToolInfo
                        {
                            AvailableConnection = new List<ConnectionType>
                            {
                                new ConnectionType { Type = "obd" },
                                new ConnectionType { Type = "bench" },
                                new ConnectionType { Type = "boot" }
                            }
                        }
                    }
                }
            }

        };

        */


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

    }

}