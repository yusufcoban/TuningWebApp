public class InputNewVariant
{
    public input_CarBrandInfo CarBrand { get; set; }
    public string TypeName { get; set; }
    public int YearStart { get; set; }
    public int YearEnd { get; set; }
    public string EngineName { get; set; }
    public int EnginePowerKw { get; set; }
    public string FuelVariant { get; set; }
    public string SpecialInfo { get; set; }
    public input_SelectedEcu SelectedEcu { get; set; }
    public List<input_AvailableSolution> AvailableSolutions { get; set; }
}

public class StringInput
{
    public string Input { get; set; }
}



public class input_CarBrandInfo
{
    public string Id { get; set; }
}

public class input_SelectedEcu
{
    public int Id { get; set; }
}

public class input_ReplacementStrings
{
    public string searchString { get; set; }
    public string replacementString { get; set; }
    public int number { get; set; }
}


public class input_AvailableSolution
{
    public string Name { get; set; }
    public string Information { get; set; }
    public int Value1 { get; set; }
    public int Value2 { get; set; }
    public List<input_ReplacementStrings> replacementStrings { get; set; }
}
