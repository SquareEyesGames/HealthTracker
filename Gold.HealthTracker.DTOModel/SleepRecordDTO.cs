namespace Gold.HealthTracker.DTOModel;

public class SleepRecordDTO
{
    public int Id { get; set; }
    public DateOnly DateOfRecord { get; set; }
    public DateTime Bedtime { get; set; }
    public DateTime WakeUpTime { get; set; }
    public TimeSpan TimeAsleep => WakeUpTime - Bedtime;
    public int? SleepQuality { get; set; }
}
