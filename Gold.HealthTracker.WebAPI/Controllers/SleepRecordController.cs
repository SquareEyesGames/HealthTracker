using Gold.HealthTracker.DBModel;
using Gold.HealthTracker.DTOModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gold.HealthTracker.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SleepRecordController : ControllerBase
{
    private readonly HealthTrackerContext _context;

    public SleepRecordController(HealthTrackerContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SleepRecordDTO>>> GetSleepRecords()
    {
        return await _context.SleepRecords.Select(sr => new SleepRecordDTO
        {
            Id = sr.Id,
            DateOfRecord = sr.DateOfRecord,
            Bedtime = sr.Bedtime,
            WakeUpTime = sr.WakeUpTime,
            SleepQuality = sr.SleepQuality
        }).ToListAsync();
    }

    
}
