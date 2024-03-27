namespace Gold.HealthTracker.DTOModel;

public class NutritionRecordDTO
{
    public int Id { get; set; }
    public int? PersonId { get; set; }
    public DateTime DateOfRecord { get; set; }
    public int CalorieIntake { get; set; }
}

public class CreateNutritionRecordDTO
{
    public int? PersonId { get; set; }
    public DateTime DateOfRecord { get; set; }
    public int CalorieIntake { get; set; }
}