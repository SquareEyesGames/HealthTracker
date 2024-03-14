namespace Gold.HealthTracker.DBModel;

public class NutritionRecord
{
    public int Id { get; set; }
    public virtual Person? Person { get; set; }
    public DateTime DateOfRecord { get; set; }
    public int CalorieIntake { get; set; }
}
