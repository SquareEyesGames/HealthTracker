namespace Gold.HealthTracker.DBModel;

public class Exercise
{
    public string ExerciseName { get; set; } = string.Empty;
    public int Sets { get; set; }
    public int Repetitions { get; set; }
    public float ResistanceWeight { get; set; }
}
