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
    [Required]
    public virtual Person? Person { get; set; }

    [Required]
    public DateOnly DateOfRecord { get; set; }
    
    [Required]
    [Range(0,400f)]
    public float Bodyweight { get; set; }
    
    [Range(0, 100f)]
    public float? BMI { get; set; }
    
    [Range(0, 100f)]
    public float? BodyFat { get; set; }
    
    [Range(0, 100f)]
    public float? FatlessBodyWeight { get; set; }
    
    [Range(0, 100f)]
    public float? SubcutaneousBodyFat { get; set; }
    
    [Range(0, 100f)]
    public float? VisceralFat { get; set; }
    
    [Range(0, 100f)]
    public float? BodyWater { get; set; }
    
    [Range(0, 100f)]
    public float? SkeletalMuscle { get; set; }
    
    [Range(0, 100f)]
    public float? MuscleMass  { get; set; }
    
    [Range(0, 100f)]
    public float? BoneMass { get; set; }
    
    [Range(0, 10000)]
    public int? MetabolicRate { get; set; }
    
    [Range(0, 200)]
    public int? MetabolicAge { get; set; }
}
