namespace Gold.HealthTracker.DBModel;

public class MentalRecord
{
    public int Id { get; set; }
    // Nullable virtual back-reference to Person, supporting lazy loading
    public virtual Person? Person { get; set; }
    public DateTime DateOfRecord { get; set; }
    public int StressLevel { get; set; }
    public string Mood { get; set; } = string.Empty;
}
