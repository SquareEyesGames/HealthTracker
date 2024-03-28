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
    [ProducesResponseType(200, Type = typeof(SleepRecordDTO))]
    [ProducesResponseType(404)]
    public IEnumerable<SleepRecordDTO?> GetSleepRecords()
    {
        if(_context.SleepRecords == null)
        {
            return (IEnumerable<SleepRecordDTO?>)NotFound();
        }

        return _context.SleepRecords.Select(sleepRecord => new SleepRecordDTO
        {
            Id = sleepRecord.Id,
            PersonId = sleepRecord.Person == null ? 0 : sleepRecord.Person.Id,
            DateOfRecord = sleepRecord.DateOfRecord.ToDateTime(TimeOnly.MinValue),
            Bedtime = sleepRecord.Bedtime,
            WakeUpTime = sleepRecord.WakeUpTime,
            SleepQuality = sleepRecord.SleepQuality
        });
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(SleepRecordDTO))]
    [ProducesResponseType(404)]
    public async Task<ActionResult> GetSleepRecord(int id)
    {
        SleepRecord? foundSleepRecord = await _context.SleepRecords.FindAsync(id);

        if(foundSleepRecord == null)
        {
            return NotFound();
        }

        return Ok(new SleepRecordDTO()
        {
            Id = foundSleepRecord.Id,
            PersonId = foundSleepRecord.Person?.Id ?? 0,
            DateOfRecord = foundSleepRecord.DateOfRecord.ToDateTime(TimeOnly.MinValue),
            Bedtime = foundSleepRecord.Bedtime,
            WakeUpTime = foundSleepRecord.WakeUpTime,
            TimeAsleep = foundSleepRecord.TimeAsleep,
            SleepQuality = foundSleepRecord.SleepQuality
        });
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateSleepRecord(CreateSleepRecordDTO newSleepRecordDTO)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest();
        }

        SleepRecord newSleepRecord = new SleepRecord
        {
            Person = await _context.Persons.FindAsync(newSleepRecordDTO.PersonId),
            DateOfRecord = DateOnly.FromDateTime(newSleepRecordDTO.DateOfRecord),
            Bedtime = newSleepRecordDTO.Bedtime,
            WakeUpTime = newSleepRecordDTO.WakeUpTime,
            TimeAsleep = newSleepRecordDTO.TimeAsleep,
            SleepQuality = newSleepRecordDTO.SleepQuality
        };

        await _context.SleepRecords.AddAsync(newSleepRecord);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetSleepRecord), new {id = newSleepRecord.Id}, newSleepRecord.Id);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSleepRecord(int id, CreateSleepRecordDTO sleepRecordDTO)
    {
        SleepRecord? sleepRecordToUpdate = await _context.SleepRecords.FindAsync(id);

        if(sleepRecordToUpdate == null)
        {
            return NotFound();
        }

        sleepRecordToUpdate.Person = await _context.Persons.FindAsync(sleepRecordDTO.PersonId);
        sleepRecordToUpdate.DateOfRecord = DateOnly.FromDateTime(sleepRecordDTO.DateOfRecord);
        sleepRecordToUpdate.Bedtime = sleepRecordDTO.Bedtime;
        sleepRecordToUpdate.WakeUpTime = sleepRecordDTO.WakeUpTime;
        sleepRecordToUpdate.TimeAsleep = sleepRecordDTO.TimeAsleep;
        sleepRecordToUpdate.SleepQuality = sleepRecordDTO.SleepQuality;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch(DbUpdateConcurrencyException)
        {
            if(!SleepRecordExists(id))
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

    private bool SleepRecordExists(int id)
    {
        return _context.SleepRecords.Any(e => e.Id == id);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteSleepRecord(int id)
    {
        SleepRecord? sleepRecordToDelete = await _context.SleepRecords.FindAsync(id);

        if(sleepRecordToDelete == null)
        {
            return NotFound();
        }

        try
        {
            _context.SleepRecords.Remove(sleepRecordToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }
}
