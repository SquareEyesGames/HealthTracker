namespace Gold.HealthTracker.DBModel;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public virtual ICollection<BodyRecord> BodyRecords { get; set; } = new List<BodyRecord>();
    public virtual ICollection<Run> Runs { get; set; } = new List<Run>();
    public virtual ICollection<Workout> Workouts { get; set; } = new List<Workout>();
    public virtual ICollection<SportSession> SportSessions { get; set; } = new List<SportSession>();
    public virtual ICollection<SleepRecord> SleepRecords { get; set; } = new List<SleepRecord>();
    public virtual ICollection<NutritionRecord> NutritionRecords { get; set; } = new List<NutritionRecord>();
    public virtual ICollection<MentalRecord> MentalRecords { get; set; } = new List<MentalRecord>();
}
