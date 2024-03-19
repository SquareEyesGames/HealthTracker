namespace Gold.HealthTracker.DBModel;

public class NutritionRecord
{
    public int Id { get; set; }
    // Nullable virtual back-reference to Person, supporting lazy loading
    public virtual Person? Person { get; set; }
    public DateOnly DateOfRecord { get; set; }
    // Calory Intake for the whole day
    public int CalorieIntake { get; set; }
}
