namespace Gold.HealthTracker.DTOModel;

public class SleepRecordDTO
{
    public int Id { get; set; }
    public int? PersonId { get; set; }
    public DateTime DateOfRecord { get; set; }
    public DateTime Bedtime { get; set; }
    public DateTime WakeUpTime { get; set; }
    public TimeSpan TimeAsleep => WakeUpTime - Bedtime;
    public int? SleepQuality { get; set; }
}

public class CreateSleepRecordDTO
{
    public int? PersonId { get; set; }
    public DateTime DateOfRecord { get; set; }
    public DateTime Bedtime { get; set; }
    public DateTime WakeUpTime { get; set; }
    public TimeSpan TimeAsleep => WakeUpTime - Bedtime;
    public int? SleepQuality { get; set; }
}