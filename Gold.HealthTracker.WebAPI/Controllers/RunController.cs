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
    [ProducesResponseType(200, Type = typeof(RunDTO))]
    [ProducesResponseType(404)]
    public IEnumerable<RunDTO?> GetRuns()
    {
        if (_context.Runs == null)
        {
            return (IEnumerable<RunDTO?>)NotFound();
        }

        return _context.Runs
            .Select(run => new RunDTO
            {
                Id = run.Id,
                PersonId = run.Person == null ? 0 : run.Person.Id,
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
            });
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(RunDTO))]
    [ProducesResponseType(404)]
    public async Task<ActionResult> GetRun(int id)
    {
        Run? foundRun = await _context.Runs.FindAsync(id);

        if (foundRun == null)
        {
            return NotFound();
        }

        return Ok (new RunDTO()
        {
            Id = foundRun.Id,
            PersonId = foundRun.Person?.Id ?? 0,
            DateOfRecord = foundRun.DateOfRecord,
            EnduranceType = foundRun.EnduranceType ?? "",
            Distance = foundRun.Distance,
            AverageSpeed = foundRun.AverageSpeed ?? 0,
            Altitude = foundRun.Altitude ?? 0,
            Cadence = foundRun.Cadence ?? 0,
            Weather = foundRun.Weather ?? "",
            RunningShoes = foundRun.RunningShoes ?? "",
            CaloriesBurned = foundRun.CaloriesBurned,
            SessionRating = foundRun.SessionRating,
            TrainingTime = foundRun.TrainingTime,
            AverageHeartRate = foundRun.AverageHeartRate,
            MaxHeartRate = foundRun.MaxHeartRate
        });
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateRun(CreateRunDTO newRunDTO)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Run newRun = new Run
        {
            Person = await _context.Persons.FindAsync(newRunDTO.PersonId),
            DateOfRecord = newRunDTO.DateOfRecord,
            EnduranceType = newRunDTO.EnduranceType,
            Distance = newRunDTO.Distance,
            AverageSpeed = newRunDTO.AverageSpeed,
            Altitude = newRunDTO.Altitude,
            Cadence = newRunDTO.Cadence,
            Weather = newRunDTO.Weather,
            RunningShoes = newRunDTO.RunningShoes,
            CaloriesBurned = newRunDTO.CaloriesBurned,
            SessionRating = newRunDTO.SessionRating,
            TrainingTime = newRunDTO.TrainingTime,
            AverageHeartRate = newRunDTO.AverageHeartRate,
            MaxHeartRate = newRunDTO.MaxHeartRate,
        };

        await _context.Runs.AddAsync(newRun);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRun), new { id = newRun.Id }, newRun.Id);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRun(int id, CreateRunDTO runDTO)
    {
        Run? runToUpdate = await _context.Runs.FindAsync(id);

        if (runToUpdate == null)
        {
            return NotFound();
        }

        runToUpdate.Person = await _context.Persons.FindAsync(runDTO.PersonId);
        runToUpdate.DateOfRecord = runDTO.DateOfRecord;
        runToUpdate.EnduranceType = runDTO.EnduranceType;
        runToUpdate.Distance = runDTO.Distance;
        runToUpdate.AverageSpeed = runDTO.AverageSpeed;
        runToUpdate.Altitude = runDTO.Altitude;
        runToUpdate.Cadence = runDTO.Cadence;
        runToUpdate.Weather = runDTO.Weather;
        runToUpdate.RunningShoes = runDTO.RunningShoes;
        runToUpdate.CaloriesBurned = runDTO.CaloriesBurned;
        runToUpdate.SessionRating = runDTO.SessionRating;
        runToUpdate.TrainingTime = runDTO.TrainingTime;
        runToUpdate.AverageHeartRate = runDTO.AverageHeartRate;
        runToUpdate.MaxHeartRate = runDTO.MaxHeartRate;
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RunExists(id))
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

    private bool RunExists(int id)
    {
        return _context.Runs.Any(e => e.Id == id);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteRun(int id)
    {
        Run? runToDelete = await _context.Runs.FindAsync(id);

        if (runToDelete == null)
        {
            return NotFound();
        }

        try
        {
        _context.Runs.Remove(runToDelete);
        await _context.SaveChangesAsync();
        return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }
}
