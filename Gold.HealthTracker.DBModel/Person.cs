namespace Gold.HealthTracker.DBModel;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<BodyData> BodyDataList { get; set; } = new();
    public List<RunData> RunDataList { get; set; } = new();
    public List<StrengthData> StrengthDataList { get; set; } = new();
    public List<VersatileTrainingData> VersatileTrainingDataList { get; set; } = new();
    public List<SleepData> SleepDataList { get; set; } = new();
    public List<NutritionData> NutritionDataList { get; set; } = new();
    public List<MentalData> MentalDataList { get; set; } = new();
}
