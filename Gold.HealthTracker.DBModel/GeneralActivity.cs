namespace Gold.HealthTracker.DBModel;

public abstract class GeneralActivity
{
    public int Id { get; set; }
    public virtual Person? Person { get; set; }
    public DateTime DateOfRecord { get; set; }
}
