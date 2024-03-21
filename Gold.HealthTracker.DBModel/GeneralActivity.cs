using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gold.HealthTracker.DBModel;

public abstract class GeneralActivity
{
    [Key]
    public int Id { get; set; }
    
    // Nullable virtual back-reference to Person
    [Required]
    [ForeignKey("PersonId")]
    public virtual Person? Person { get; set; }

    [Required]
    public DateTime DateOfRecord { get; set; }
    
    public TimeSpan? TrainingTime { get; set; }
    
    [Range(0, 250)]
    public int? AverageHeartRate { get; set; }
    
    [Range(0, 250)]
    public int? MaxHeartRate { get; set; }
    
    [Range(0, 100000)]
    public int? CaloriesBurned { get; set; }
    
    [Range(0, 10)]
    public int? SessionRating { get; set; }
}
