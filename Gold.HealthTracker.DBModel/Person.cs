namespace Gold.HealthTracker.DBModel;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<BodyRecord> BodyRecordList { get; set; } = new();
    public List<Run> RunList { get; set; } = new();
    public List<Workout> WorkoutList { get; set; } = new();
    public List<SportSession> SportSessionList { get; set; } = new();
    public List<SleepRecord> SleepRecordList { get; set; } = new();
    public List<NutritionRecord> NutritionRecordList { get; set; } = new();
    public List<MentalRecord> MentalRecordList { get; set; } = new();
}
