using System.ComponentModel.DataAnnotations;

namespace Gold.HealthTracker.DBModel;

public class SleepRecord
{
    // Primary key of the SleepRecord entity
    [Key]
    public int Id { get; set; }

    // Nullable virtual navigation property back to Person. The '?' and 'virtual' indicate it supports lazy loading and may not always have a value.
    [Required]
    public virtual Person? Person { get; set; }

    [Required]
    public DateOnly DateOfRecord { get; set; }
    
    public DateTime Bedtime { get; set; }
    
    public DateTime WakeUpTime { get; set; }
    
    public TimeSpan TimeAsleep => WakeUpTime - Bedtime;
    
    [Range(0, 10)]
    public int? SleepQuality { get; set; }
}
