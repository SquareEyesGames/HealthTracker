using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gold.HealthTracker.DBModel;

public class NutritionRecord
{
    [Key]
    public int Id { get; set; }

    // Nullable virtual back-reference to Person, supporting lazy loading
    [Required]
    [ForeignKey("PersonId")]
    public virtual Person? Person { get; set; }

    [Required]
    public DateOnly DateOfRecord { get; set; }
    
    // Calory Intake for the whole day
    [Required]
    [Range(0, 10000)]
    public int CalorieIntake { get; set; }
}
