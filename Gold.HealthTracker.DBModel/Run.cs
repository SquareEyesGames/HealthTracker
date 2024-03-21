using System.ComponentModel.DataAnnotations;

namespace Gold.HealthTracker.DBModel;

public class Run : GeneralActivity
{
    [StringLength(40)]
    public string? EnduranceType { get; set; }

    [Required]
    [Range(0, 10000f)]
    public float Distance { get; set; }
    
    [Range(0, 1000f)]
    public float? AverageSpeed { get; set; }
    
    [Range(0, 10000)]
    public int? Altitude { get; set; }
    
    [Range(0, 1000)]
    public int? Cadence { get; set; }
    
    public string? Weather { get; set; }
    
    public string? RunningShoes { get; set; }
}
