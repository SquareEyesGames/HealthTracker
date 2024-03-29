using Microsoft.AspNetCore.Mvc;
using Gold.HealthTracker.DBModel;
using Gold.HealthTracker.DTOModel;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

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

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(SportSessionDTO))]
    [ProducesResponseType(404)]
    public IEnumerable<SportSessionDTO?> GetSportSessions()
    {
        if (_context.SportSessions == null)
        {
            return (IEnumerable<SportSessionDTO?>)NotFound();
        }

        return _context.SportSessions
            .Select(sportSession => new SportSessionDTO
            {
                Id = sportSession.Id,
                PersonId = sportSession.Person == null ? 0 : sportSession.Person.Id,
                DateOfRecord = sportSession.DateOfRecord,
                Sport = sportSession.Sport,
                TrainingTime = sportSession.TrainingTime,
                AverageHeartRate = sportSession.AverageHeartRate,
                MaxHeartRate = sportSession.MaxHeartRate,
                CaloriesBurned = sportSession.CaloriesBurned,
                SessionRating = sportSession.SessionRating
            });
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(SportSessionDTO))]
    [ProducesResponseType(404)]
    public async Task<ActionResult> GetSportSession(int id)
    {
        SportSession? foundSportSession = await _context.SportSessions.FindAsync(id);

        if(foundSportSession == null)
        {
            return NotFound();
        }

        return Ok(new SportSessionDTO()
        {
            Id = foundSportSession.Id,
            PersonId = foundSportSession.Person?.Id ?? 0,
            DateOfRecord = foundSportSession.DateOfRecord,
            Sport = foundSportSession.Sport,
            AverageHeartRate = foundSportSession.AverageHeartRate,
            MaxHeartRate = foundSportSession.MaxHeartRate,
            CaloriesBurned = foundSportSession.CaloriesBurned,
            SessionRating = foundSportSession.SessionRating,
            TrainingTime = foundSportSession.TrainingTime
        });
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateSportSession(CreateSportSessionDTO newSportSessionDTO)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        SportSession newSportSession = new SportSession
        {
            Person = await _context.Persons.FindAsync(newSportSessionDTO.PersonId),
            DateOfRecord = newSportSessionDTO.DateOfRecord,
            Sport = newSportSessionDTO.Sport,
            TrainingTime = newSportSessionDTO.TrainingTime,
            AverageHeartRate = newSportSessionDTO.AverageHeartRate,
            MaxHeartRate = newSportSessionDTO.MaxHeartRate,
            CaloriesBurned = newSportSessionDTO.CaloriesBurned,
            SessionRating = newSportSessionDTO.SessionRating
        };

        await _context.SportSessions.AddAsync(newSportSession);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetSportSession), new { id = newSportSession.Id}, newSportSession.Id);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateSportSession(int id, CreateSportSessionDTO sportSessionDTO)
    {
        SportSession? sportSessionToUpdate = await _context.SportSessions.FindAsync(id);

        if(sportSessionToUpdate == null)
        {
            return NotFound();
        }

        sportSessionToUpdate.Person = await _context.Persons.FindAsync(sportSessionDTO.PersonId);
        sportSessionToUpdate.DateOfRecord = sportSessionDTO.DateOfRecord;
        sportSessionToUpdate.Sport = sportSessionDTO.Sport;
        sportSessionToUpdate.TrainingTime = sportSessionDTO.TrainingTime;
        sportSessionToUpdate.AverageHeartRate = sportSessionDTO.AverageHeartRate;
        sportSessionToUpdate.MaxHeartRate = sportSessionDTO.MaxHeartRate;
        sportSessionToUpdate.CaloriesBurned = sportSessionDTO.CaloriesBurned;
        sportSessionToUpdate.SessionRating = sportSessionDTO.SessionRating;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch(DbUpdateConcurrencyException)
        {
            if(!SportSessionExists(id))
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

    private bool SportSessionExists(int id)
    {
        return _context.SportSessions.Any(e => e.Id == id);
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeleteSportSession(int id)
    {
        SportSession? sportSessionToDelete = await _context.SportSessions.FindAsync(id);

        if(sportSessionToDelete == null)
        {
            return NotFound();
        }

        try
        {
            _context.SportSessions.Remove(sportSessionToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }
}
