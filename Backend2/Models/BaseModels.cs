using System.Collections.Generic;

using TuningWebApp.Controllers;

namespace BaseBackend.Models
{
    public class CarModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string CarBrandId { get; set; }
    }

    public class CarBrand
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Slug { get; set; }
        public List<CarModel> Models { get; set; } = new List<CarModel>(); // Initialize list
    }

    public class TuningVariant
    {
        public string TypeName { get; set; }
        public string Year { get; set; }
        public string Engine { get; set; }
        public string Horsepower { get; set; }
        public string Variant { get; set; }
        public string TuningId { get; set; }
    }

    public class TuningDatabaseInfo
    {
        public string Id { get; set; }
        public string Brand { get; set; }
        public string Information { get; set; }
        public List<TuningVariant> Variants { get; set; } = new List<TuningVariant>(); // Initialize list
    }

    public class AvailableSolution
    {
        public string Name { get; set; }
        public string Information { get; set; }
        public int Value1 { get; set; }
        public int Value2 { get; set; }
        public bool Checked { get; set; }
    }

    public class EcuConnection
    {
        public string Type { get; set; }
    }

    public class TuningTool
    {
        public List<EcuConnection> AvailableConnection { get; set; } = new List<EcuConnection>(); // Initialize list
    }

    public class ToolInfo
    {
        // Additional properties can be defined here if needed
    }

    public class EcuInfo
    {
        public int Id { get; set; }
        public string EcuName { get; set; }
        public int ConnectionInfoId { get; set; }
        public List<GroupedConnectionInfo> GroupedAvailableConnections { get; set; } = new List<GroupedConnectionInfo>(); // Initialize list

        public List<ConnectionInfo> AvailableConnection { get; set; } = new List<ConnectionInfo>(); // Initialize list

        public void initi_after()
        {
            List<GroupedConnectionInfo> bu = AvailableConnection
        .GroupBy(c => c.ToolInfoId)  // Ensure ToolName is used correctly here
        .Select(g => new GroupedConnectionInfo
        {
            ToolName = ((ToolInfoId)g.Key).ToString(),  // g.Key will be the ToolName
            ConnectionTypes = g.Select(c => c.ConnectionTypeName).ToList()  // Ensure distinct connection types
        })
        .ToList();
            GroupedAvailableConnections = bu;
        }

    }

    public class TuningSpecialInfo
    {
        public string Id { get; set; }
        public List<AvailableSolution> AvailableSolutions { get; set; } = new List<AvailableSolution>(); // Initialize list
        public string AdditionalInformation { get; set; }
        public int EcuInfoId { get; set; }
        public bool isDeleted { get; set; }
        public EcuInfo EcuInfo { get; set; }
    }

    public enum ToolInfoId
    {
        Kess = 1,
        Autotuner = 2,
        Flex = 3
    }

    public enum ConnectionTypeEnum
    {
        OBD = 1,
        Bench = 2,
        Boot = 3
    }

    public class GroupedConnectionInfo
    {
        public string ToolName { get; set; }
        public List<string> ConnectionTypes { get; set; } = new List<string>(); // Initialize list
    }

    public class ConnectionInfo
    {
        public int ToolInfoId { get; set; }
        public string ToolName { get; set; }
        public int Type { get; set; }

        public string ConnectionTypeName => ((ConnectionTypeEnum)Type).ToString(); // Computed property
        public string ToolNameString => ((ToolInfoId)Type).ToString();

    }
}
