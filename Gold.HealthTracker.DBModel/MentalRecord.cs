using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gold.HealthTracker.DBModel;

public class MentalRecord
{
    [Key]
    public int Id { get; set; }
    
    // Nullable virtual back-reference to Person, supporting lazy loading
    [Required]
    [ForeignKey("PersonId")]
    public virtual Person? Person { get; set; }

    [Required]
    public DateOnly DateOfRecord { get; set; }

    [Range(0, 10)]
    public int? StressLevel { get; set; }

    [StringLength(50)]
    public string? Mood { get; set; }
}
