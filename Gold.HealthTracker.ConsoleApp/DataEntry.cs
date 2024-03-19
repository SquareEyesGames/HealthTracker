using Gold.HealthTracker.DBModel;

namespace Gold.HealthTracker.ConsoleApp;

public class DataEntry
{
    private HealthTrackerContext _context;

    public DataEntry(HealthTrackerContext context)
    {
        _context = context;
    }


}
