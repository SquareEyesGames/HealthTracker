namespace Gold.HealthTracker.DTOModel;

public class WorkoutDTO
{
    public int Id { get; set; }
    public DateTime DateOfRecord { get; set; }
    public TimeSpan? TrainingTime { get; set; }
    public int? AverageHeartRate { get; set; }
    public int? MaxHeartRate { get; set; }
    public int? CaloriesBurned { get; set; }
    public int? SessionRating { get; set; }
    public string? TrainingLocation { get; set; }
    public List<ExerciseDTO> Exercises { get; set; } = new List<ExerciseDTO>();
}
