namespace Gold.HealthTracker.DTOModel;

public class MentalRecordDTO
{
    public int Id { get; set; }
    public DateOnly DateOfRecord { get; set; }
    public int? StressLevel { get; set; }
    public string? Mood { get; set; }
}
