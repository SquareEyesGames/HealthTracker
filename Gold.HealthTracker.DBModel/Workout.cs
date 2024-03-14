namespace Gold.HealthTracker.DBModel;

public class Workout : GeneralActivity
{
    public virtual Person? Person { get; set; }
    public List<Exercise> Exercises { get; set; } = new();
}
