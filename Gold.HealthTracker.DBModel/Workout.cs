namespace Gold.HealthTracker.DBModel;

public class Workout : GeneralActivity
{
    public List<Exercise> Exercises { get; set; } = new();
}
