namespace Gold.HealthTracker.DTOModel;

public class SportSessionDTO
{
    public int Id { get; set; }
    public int? PersonId { get; set; }
    public DateTime DateOfRecord { get; set; }
    public TimeSpan? TrainingTime { get; set; }
    public int? AverageHeartRate { get; set; }
    public int? MaxHeartRate { get; set; }
    public int? CaloriesBurned { get; set; }
    public int? SessionRating { get; set; }
    public string Sport { get; set; } = string.Empty;
}

public class CreateSportSessionDTO
{
    public int? PersonId { get; set; }
    public DateTime DateOfRecord { get; set; }
    public TimeSpan? TrainingTime { get; set; }
    public int? AverageHeartRate { get; set; }
    public int? MaxHeartRate { get; set; }
    public int? CaloriesBurned { get; set; }
    public int? SessionRating { get; set; }
    public string Sport { get; set; } = string.Empty;
}