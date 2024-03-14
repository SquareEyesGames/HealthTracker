namespace Gold.HealthTracker.DBModel;

public class MentalRecord
{
    public int Id { get; set; }
    public virtual Person? Person { get; set; }
    public DateTime DateOfRecord { get; set; }
    public int StressLevel { get; set; }
}
