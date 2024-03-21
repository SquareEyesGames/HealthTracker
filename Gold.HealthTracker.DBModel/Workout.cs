namespace Gold.HealthTracker.DBModel;

public class Workout : GeneralActivity
{
    public string? TrainingLocation { get; set; } = string.Empty;
    // Virtual ICollection enables EF Core's lazy loading feature, allowing Exercises related to this Workout to be loaded on-demand.
    public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();    
}
