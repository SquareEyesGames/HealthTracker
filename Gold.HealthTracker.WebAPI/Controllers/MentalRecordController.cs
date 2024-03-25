using Gold.HealthTracker.DBModel;
using Gold.HealthTracker.DTOModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gold.HealthTracker.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MentalRecordController : ControllerBase
{
    private readonly HealthTrackerContext _context;

    public MentalRecordController(HealthTrackerContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MentalRecordDTO>>> GetMentalRecords()
    {
        return await _context.MentalRecords.Select(mr => new MentalRecordDTO
        {
            Id = mr.Id,
            DateOfRecord = mr.DateOfRecord,
            StressLevel = mr.StressLevel,
            Mood = mr.Mood
        }).ToListAsync();
    }
}
