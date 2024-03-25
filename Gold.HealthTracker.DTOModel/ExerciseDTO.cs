namespace Gold.HealthTracker.DTOModel;

public class ExerciseDTO
{
    public int Id { get; set; }
    public string ExerciseName { get; set; } = string.Empty;
    public List<SetDTO> Sets { get; set; } = new List<SetDTO>();
}
