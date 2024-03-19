namespace Gold.HealthTracker.DBModel;

public class SleepRecord
{
    // Primary key of the SleepRecord entity
    public int Id { get; set; }
    // Nullable virtual navigation property back to Person. The '?' and 'virtual' indicate it supports lazy loading and may not always have a value.
    public virtual Person? Person { get; set; }
    public DateOnly DateOfRecord { get; set; }
    public DateTime Bedtime { get; set; }
    public DateTime WakeUpTime { get; set; }
    public TimeSpan TimeAsleep { get; set; }
    public int SleepQuality { get; set; }
}
