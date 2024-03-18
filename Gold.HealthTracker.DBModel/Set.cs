namespace Gold.HealthTracker.DBModel;

public class Set
{
    public int Id { get; set; }
    public int ExerciseId { get; set; }
    public virtual Exercise? Exercise { get; set; }
    public int Repetitions { get; set; }
    public float Weight { get; set; }
}
