using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gold.HealthTracker.DBModel;

public class Set
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [ForeignKey("ExerciseId")]
    public virtual Exercise? Exercise { get; set; }
    
    [Range(0, 10000)]
    public float? Repetitions { get; set; }
    
    [Range(0, 10000)]
    public float? Weight { get; set; }
}
