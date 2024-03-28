namespace Gold.HealthTracker.DTOModel;

public class RunDTO
{
    public int Id { get; set; }
    public int? PersonId { get; set; }
    public DateTime DateOfRecord { get; set; }
    public TimeSpan? TrainingTime { get; set; }
    public int? AverageHeartRate { get; set; }
    public int? MaxHeartRate { get; set; }
    public int? CaloriesBurned { get; set; }
    public int? SessionRating { get; set; }
    public string? EnduranceType { get; set; } = string.Empty;
    public float Distance { get; set; }
    public float? AverageSpeed { get; set; }
    public int? Altitude { get; set; }
    public int? Cadence { get; set; }
    public string? Weather { get; set; }
    public string? RunningShoes { get; set; }
}

public class CreateRunDTO
{
    public int? PersonId { get; set; }
    public DateTime DateOfRecord { get; set; }
    public TimeSpan? TrainingTime { get; set; }
    public int? AverageHeartRate { get; set; }
    public int? MaxHeartRate { get; set; }
    public int? CaloriesBurned { get; set; }
    public int? SessionRating { get; set; }
    public string? EnduranceType { get; set; } = string.Empty;
    public float Distance { get; set; }
    public float? AverageSpeed { get; set; }
    public int? Altitude { get; set; }
    public int? Cadence { get; set; }
    public string? Weather { get; set; }
    public string? RunningShoes { get; set; }
}