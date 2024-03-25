using Gold.HealthTracker.DBModel;
using Gold.HealthTracker.DTOModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gold.HealthTracker.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class NutritionRecordController : ControllerBase
{
    private readonly HealthTrackerContext _context;

    public NutritionRecordController(HealthTrackerContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<NutritionRecordDTO>>> GetNutritionRecords()
    {
        return await _context.NutritionRecords.Select(nr => new NutritionRecordDTO
        {
            Id = nr.Id,
            DateOfRecord = nr.DateOfRecord,
            CalorieIntake = nr.CalorieIntake
        }).ToListAsync();
    }
}
