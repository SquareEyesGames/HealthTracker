using Microsoft.AspNetCore.Mvc;
using Gold.HealthTracker.DBModel;
using Gold.HealthTracker.DTOModel;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Gold.HealthTracker.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SportSessionController : ControllerBase
{
    private readonly HealthTrackerContext _context;

    public SportSessionController(HealthTrackerContext context)
    {
        _context = context;
    }

    // GET: api/SportSession
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SportSessionDTO>>> GetSportSessions()
    {
        return await _context.SportSessions
            .Select(ss => new SportSessionDTO
            {
                Id = ss.Id,
                DateOfRecord = ss.DateOfRecord,
                Sport = ss.Sport,
                TrainingTime = ss.TrainingTime,
                AverageHeartRate = ss.AverageHeartRate,
                MaxHeartRate = ss.MaxHeartRate,
                CaloriesBurned = ss.CaloriesBurned,
                SessionRating = ss.SessionRating
            }).ToListAsync();
    }

}
