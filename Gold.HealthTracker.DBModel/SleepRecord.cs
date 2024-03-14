namespace Gold.HealthTracker.DBModel;

public class SleepRecord
{
    public int Id { get; set; }
    public virtual Person? Person { get; set; }
    public DateTime DateOfRecord { get; set; }
    public DateTime Bedtime { get; set; }
    public DateTime WakeUpTime { get; set; }
}
