namespace Gold.HealthTracker.DTOModel;

public class NutritionRecordDTO
{
    public int Id { get; set; }
    public DateOnly DateOfRecord { get; set; }
    public int CalorieIntake { get; set; }
}
