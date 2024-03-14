namespace Gold.HealthTracker.DBModel;

public class Exercise
{
    // Primary key
    public int Id { get; set; }
    // Nullable virtual back-reference to Workout, allowing navigation back to the associated workout
    public virtual Workout? Workout { get; set; }
    public string ExerciseName { get; set; } = string.Empty;
    public int Sets { get; set; }
    public int Repetitions { get; set; }
    public float ResistanceWeight { get; set; }
}
