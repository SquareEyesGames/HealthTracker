namespace Gold.HealthTracker.DTOModel;

public class MentalRecordDTO
{
    public int Id { get; set; }
    public int? PersonId { get; set; }
    public DateTime DateOfRecord { get; set; }
    public int? StressLevel { get; set; }
    public string? Mood { get; set; }
}

public class CreateMentalRecordDTO
{
    public int? PersonId { get; set; }
    public DateTime DateOfRecord { get; set; }
    public int? StressLevel { get; set; }
    public string? Mood { get; set; }
}