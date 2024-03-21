using System.ComponentModel.DataAnnotations;

namespace Gold.HealthTracker.DBModel;

public class SportSession : GeneralActivity
{
    [Required]
    [StringLength(50)]
    public string Sport { get; set; } = string.Empty;
}
