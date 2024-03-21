using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gold.HealthTracker.DBModel;

public class Exercise
{
    // Primary key
    [Key]
    public int Id { get; set; }
    
    // Nullable virtual back-reference to Workout, allowing navigation back to the associated workout
    [Required]
    [ForeignKey("WorkoutId")]
    public virtual Workout? Workout { get; set; }

    [Required]
    [StringLength(60)]
    public string ExerciseName { get; set; } = string.Empty;

    public virtual ICollection<Set> Sets { get; set; } = new List<Set>();
}
