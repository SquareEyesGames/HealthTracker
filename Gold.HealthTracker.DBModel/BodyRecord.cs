namespace Gold.HealthTracker.DBModel;

public class BodyRecord
{
    public int Id { get; set; }
    public virtual Person? Person { get; set; }
    public DateTime DateOfRecord { get; set; }
    public float Bodyweight { get; set; }
    public float BodyFat { get; set; }
}
