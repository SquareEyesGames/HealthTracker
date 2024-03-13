namespace Gold.HealthTracker.DBModel;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<BodyRecord> BodyDataList { get; set; } = new();
    public List<Run> RunDataList { get; set; } = new();
    public List<Workout> StrengthDataList { get; set; } = new();
    public List<SportSession> VersatileTrainingDataList { get; set; } = new();
    public List<SleepRecord> SleepDataList { get; set; } = new();
    public List<NutritionRecord> NutritionDataList { get; set; } = new();
    public List<MentalRecord> MentalDataList { get; set; } = new();
}
