namespace Gold.HealthTracker.DBModel;

public abstract class GeneralActivity
{
    public int Id { get; set; }
    // Nullable virtual back-reference to Person
    public virtual Person? Person { get; set; }
    public DateTime DateOfRecord { get; set; }
    public TimeSpan TrainingTime { get; set; }
    public int AverageHeartRate { get; set; }
    public int MaxHeartRate { get; set; }
    public int CaloriesBurned { get; set; }
    public int SessionRating { get; set; }
}
