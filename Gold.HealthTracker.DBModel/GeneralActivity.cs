namespace Gold.HealthTracker.DBModel;

public abstract class GeneralActivity
{
    public int Id { get; set; }
    // Nullable virtual back-reference to Person
    public virtual Person? Person { get; set; }
    public DateTime DateOfRecord { get; set; }
}
