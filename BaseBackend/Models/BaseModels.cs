using YourNamespace.Controllers;

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
        public List<CarModel> Models { get; set; }
    }

    public class TuningVariant
    {
        public string TypeName { get; set; }
        public string Year { get; set; }
        public string Engine { get; set; }
        public string Horsepower { get; set; }
        public string Variant { get; set; }
        public string EcuType { get; set; }
        public string TuningId { get; set; }
    }

    public class TuningDatabaseInfo
    {
        public string Id { get; set; }
        public string Brand { get; set; }
        public string Information { get; set; }
        public List<TuningVariant> Variants { get; set; }
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
        public List<EcuConnection> AvailableConnection { get; set; }
    }

    public class ToolInfo
    {
        public List<ConnectionType> AvailableConnection { get; internal set; }
    }

    public class EcuInfo
    {
        public string EcuName { get; set; }
        public TuningToolsInfo TuningToolsInfo { get; set; }
    }

    public class TuningSpecialInfo
    {
        public string Id { get; set; }
        public List<AvailableSolution> AvailableSolutions { get; set; }
        public string AdditionalInformation { get; set; }
        public EcuInfo EcuInfo { get; set; }
    }


    public class ConnectionType
    {
        public string Type { get; set; }
    }

}
