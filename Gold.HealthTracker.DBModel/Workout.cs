namespace Gold.HealthTracker.DBModel;

public class Workout : GeneralActivity
{
    public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
}
