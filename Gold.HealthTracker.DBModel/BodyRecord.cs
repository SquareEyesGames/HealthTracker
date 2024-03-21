using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gold.HealthTracker.DBModel;

public class BodyRecord
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("PersonId")]
    /// <summary>
    /// Nullable virtual back-reference to Person 
    /// </summary>
    public virtual Person? Person { get; set; }

    [Required]
    public DateOnly DateOfRecord { get; set; }
    
    [Required]
    [Range(0,400)]
    public float Bodyweight { get; set; }
    
    [Range(0, 100)]
    public float? BMI { get; set; }
    
    
    public float? BodyFat { get; set; }
    

    public float? FatlessBodyWeight { get; set; }
    

    public float? SubcutaneousBodyFat { get; set; }
    

    public float? VisceralFat { get; set; }
    

    public float? BodyWater { get; set; }
    

    public float? SkeletalMuscle { get; set; }
    

    public float? MuscleMass  { get; set; }
    

    public float? BoneMass { get; set; }
    

    public int? MetabolicRate { get; set; }
    

    public int? MetabolicAge { get; set; }
}
