namespace Gold.HealthTracker.DBModel;

public class Run : GeneralActivity
{
    public string EnduranceType { get; set; } = string.Empty;
    public float Distance { get; set; }
    public TimeSpan RunTime { get; set; }
    public float AverageSpeed { get; set; }
    public int AverageHeartRate { get; set; }
    public int MaxHeartRate { get; set; }
    public int Altitude { get; set; }
    public int Cadence { get; set; }
    public string Weather { get; set; } = string.Empty;
    public string RunningShoes { get; set; } = string.Empty;
}
