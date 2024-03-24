using Gold.HealthTracker.DBModel;
using Gold.HealthTracker.DTOModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Gold.HealthTracker.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class RunController : ControllerBase
{
    private readonly HealthTrackerContext _context;

    public RunController(HealthTrackerContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RunDTO>>> GetRuns()
    {
        return await _context.Runs
            .Select(run => new RunDTO
            {
                Id = run.Id,
                DateOfRecord = run.DateOfRecord,
                EnduranceType = run.EnduranceType ?? "",
                Distance = run.Distance,
                AverageSpeed = run.AverageSpeed ?? 0,
                Altitude = run.Altitude ?? 0,
                Cadence = run.Cadence ?? 0,
                Weather = run.Weather ?? "",
                RunningShoes = run.RunningShoes ?? "",
                CaloriesBurned = run.CaloriesBurned,
                SessionRating = run.SessionRating,
                TrainingTime = run.TrainingTime,
                AverageHeartRate = run.AverageHeartRate,
                MaxHeartRate = run.MaxHeartRate
            }).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RunDTO>> GetRun(int id)
    {
        var run = await _context.Runs.FindAsync(id);

        if (run == null)
        {
            return NotFound();
        }

        return new RunDTO
        {
            Id = run.Id,
            DateOfRecord = run.DateOfRecord,
            EnduranceType = run.EnduranceType ?? "",
            Distance = run.Distance,
            AverageSpeed = run.AverageSpeed ?? 0,
            Altitude = run.Altitude ?? 0,
            Cadence = run.Cadence ?? 0,
            Weather = run.Weather ?? "",
            RunningShoes = run.RunningShoes ?? "",
            CaloriesBurned = run.CaloriesBurned,
            SessionRating = run.SessionRating,
            TrainingTime = run.TrainingTime,
            AverageHeartRate = run.AverageHeartRate,
            MaxHeartRate = run.MaxHeartRate
        };
    }

    [HttpPost]
    public async Task<ActionResult<RunDTO>> PostRun([FromBody] RunDTO runDTO)
    {
        var run = new Run
        {
            // Hier erfolgt die Übertragung der Daten von DTO zu Entity
            EnduranceType = runDTO.EnduranceType,
            Distance = runDTO.Distance,
            AverageSpeed = runDTO.AverageSpeed,
            Altitude = runDTO.Altitude,
            Cadence = runDTO.Cadence,
            Weather = runDTO.Weather,
            RunningShoes = runDTO.RunningShoes,
            CaloriesBurned = runDTO.CaloriesBurned,
            SessionRating = runDTO.SessionRating,
            TrainingTime = runDTO.TrainingTime,
            AverageHeartRate = runDTO.AverageHeartRate,
            MaxHeartRate = runDTO.MaxHeartRate,
            DateOfRecord = runDTO.DateOfRecord,
            // Achten Sie darauf, die Referenz zu einer existierenden Person herzustellen (falls benötigt)
        };

        _context.Runs.Add(run);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetRun", new { id = run.Id }, runDTO);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutRun(int id, [FromBody] RunDTO runDTO)
    {
        if (id != runDTO.Id)
        {
            return BadRequest();
        }

        var run = await _context.Runs.FindAsync(id);
        if (run == null)
        {
            return NotFound();
        }

        // Hier erfolgt die Übertragung der Daten von DTO zu Entity
        run.EnduranceType = runDTO.EnduranceType;
        run.Distance = runDTO.Distance;
        run.AverageSpeed = runDTO.AverageSpeed;
        run.Altitude = runDTO.Altitude;
        run.Cadence = runDTO.Cadence;
        run.Weather = runDTO.Weather;
        run.RunningShoes = runDTO.RunningShoes;
        run.CaloriesBurned = runDTO.CaloriesBurned;
        run.SessionRating = runDTO.SessionRating;
        run.TrainingTime = runDTO.TrainingTime;
        run.AverageHeartRate = runDTO.AverageHeartRate;
        run.MaxHeartRate = runDTO.MaxHeartRate;
        run.DateOfRecord = runDTO.DateOfRecord;

            _context.Entry(run).State = EntityState.Modified;

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!_context.Runs.Any(e => e.Id == id))
        {
            return NotFound();
        }
        else
        {
            throw;
        }
    }

    return NoContent();
}

[HttpDelete("{id}")]
public async Task<IActionResult> DeleteRun(int id)
{
    var run = await _context.Runs.FindAsync(id);
    if (run == null)
    {
        return NotFound();
    }

    _context.Runs.Remove(run);
    await _context.SaveChangesAsync();

    return NoContent();
}
}
