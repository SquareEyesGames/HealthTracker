namespace Gold.HealthTracker.DBModel;

public class BodyRecord
{
    public int Id { get; set; }
    // Nullable virtual back-reference to Person
    public virtual Person? Person { get; set; }
    public DateOnly DateOfRecord { get; set; }
    public float Bodyweight { get; set; }
    public float BMI { get; set; }
    public float BodyFat { get; set; }
    public float FatlessBodyWeight { get; set; }
    public float SubcutaneousBodyFat { get; set; }
    public float VisceralFat { get; set; }
    public float BodyWater { get; set; }
    public float SkeletalMuscle { get; set; }
    public float MuscleMass  { get; set; }
    public float BoneMass { get; set; }
    public int MetabolicRate { get; set; }
    public int MetabolicAge { get; set; }
}
