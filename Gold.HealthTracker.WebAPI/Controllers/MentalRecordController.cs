using Gold.HealthTracker.DBModel;
using Gold.HealthTracker.DTOModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gold.HealthTracker.WebAPI.Controllers;

// Marks this class as an API controller with a route base of 'controller' name
[ApiController]
[Route("[controller]")]
public class MentalRecordController : ControllerBase
{
    private readonly HealthTrackerContext _context;

    // Constructor with Dependency Injection for the database context
    public MentalRecordController(HealthTrackerContext context)
    {
        _context = context;
    }

    /// <summary>
    /// HTTP GET method to retrieve all MentalRecords
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(MentalRecordDTO))]
    [ProducesResponseType(404)]
    public IEnumerable<MentalRecordDTO?> GetMentalRecords()
    {
        // Checks if the MentalRecord set is null (which should never be true in a properly configured context)
        if(_context.MentalRecords == null)
            return (IEnumerable<MentalRecordDTO?>)NotFound();

        // Queries all MentalRecords from the database and converts them to MentalRecordDTOs
        return _context.MentalRecords
            .Select(MentalRecord => new MentalRecordDTO
            {
                Id = MentalRecord.Id,
                PersonId = MentalRecord.Person == null ? 0 : MentalRecord.Person.Id,
                DateOfRecord = MentalRecord.DateOfRecord.ToDateTime(TimeOnly.MinValue),
                StressLevel = MentalRecord.StressLevel,
                Mood = MentalRecord.Mood
            });
    }

    /// <summary>
    /// HTTP GET method with an 'id' parameter to retrieve a specific MentalRecord by its Id
    /// </summary>
    /// <param name="id">PrimaryKey of the MentalRecord</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(MentalRecordDTO))]
    [ProducesResponseType(404)]
    public async Task<ActionResult> GetMentalRecord(int id)
    {
        // Searches for a MentalRecord using the given ID
        MentalRecord? foundMentalRecord = await _context.MentalRecords.FindAsync(id);

        // If no MentalRecord is found, returns a 404 Not Found status
        if (foundMentalRecord == null)
        {
            return NotFound();
        }

        // Returns the found MentalRecord as a DTO
        return Ok(new MentalRecordDTO()
        {
            Id = foundMentalRecord.Id,
            PersonId = foundMentalRecord.Person?.Id ?? 0,
            DateOfRecord = foundMentalRecord.DateOfRecord.ToDateTime(TimeOnly.MinValue),
            StressLevel = foundMentalRecord.StressLevel,
            Mood = foundMentalRecord.Mood
        });
    }

    /// <summary>
    /// HTTP POST method to create a new MentalRecord
    /// </summary>
    /// <param name="newMentalRecordDTO">Creates a new MentalRecordDTO</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateMentalRecord(CreateMentalRecordDTO newMentalRecordDTO)
    {
        // Checks model state for any validation errors
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        MentalRecord newMentalRecord = new MentalRecord
        {
            Person = await _context.Persons.FindAsync(newMentalRecordDTO.PersonId),
            DateOfRecord = DateOnly.FromDateTime(newMentalRecordDTO.DateOfRecord),
            StressLevel = newMentalRecordDTO.StressLevel,
            Mood = newMentalRecordDTO.Mood
        };

        // Adds the new MentalRecord to the database
        await _context.MentalRecords.AddAsync(newMentalRecord);
        
        // Saves changes to the database
        await _context.SaveChangesAsync();

        // Returns a 201 Created status with the ID of the newly created MentalRecord
        return CreatedAtAction(nameof(GetMentalRecord), new { id = newMentalRecord.Id }, newMentalRecord.Id);
    }

    /// <summary>
    /// HTTP PUT method to update an existing MentalRecord
    /// </summary>
    /// <param name="id">PrimaryKey of the MentalRecord</param>
    /// <param name="mentalRecordDTO">Creates a new MentalRecordDTO</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateMentalRecord(int id, CreateMentalRecordDTO mentalRecordDTO)
    {
        // Finds the MentalRecord to update
        MentalRecord? mentalRecordToUpdate = await _context.MentalRecords.FindAsync(id);
        if (mentalRecordToUpdate == null)
        {
            return NotFound();
        }

        // Updates MentalRecord data
        mentalRecordToUpdate.Person = await _context.Persons.FindAsync(mentalRecordDTO.PersonId);
        mentalRecordToUpdate.DateOfRecord = DateOnly.FromDateTime(mentalRecordDTO.DateOfRecord);
        mentalRecordToUpdate.StressLevel = mentalRecordDTO.StressLevel;
        mentalRecordToUpdate.Mood = mentalRecordDTO.Mood;

        try
        {
            // Attempts to save changes to the database. This includes updates made to the entity being modified.
            await _context.SaveChangesAsync();
        }
        // Catches exceptions that are thrown when there is a concurrency conflict. Concurrency conflicts occur when an entity has been changed in the database after it was loaded into memory.
        catch (DbUpdateConcurrencyException)
        {
            // If the MentalRecord with the specified ID does not exist, it returns a NotFound result (MentalRecord might have been deleted between the time it was fetched and the attempt to update it)
            if (!MentalRecordExists(id))
            {
                return NotFound();
            }
            // If the MentalRecord exists but another type of concurrency conflict has occurred...
            else
            {
                // ...rethrow the exception. This allows the exception to be handled by the framework or higher-level exception handlers.
                throw;
            }
        }

        // Returns a 204 No Content status to indicate successful update without returning data
        return NoContent();
    }

    /// <summary>
    /// Checks if a specific MentalRecord exists in the database
    /// </summary>
    /// <param name="id">PrimaryKey of the MentalRecord</param>
    /// <returns></returns>
    private bool MentalRecordExists(int id)
    {
        return _context.MentalRecords.Any(e => e.Id == id);
    }

    /// <summary>
    /// HTTP DELETE method to remove a MentalRecord
    /// </summary>
    /// <param name="id">PrimaryKey of the MentalRecord</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteMentalRecord(int id)
    {
        // Finds the MentalRecord to be deleted
        MentalRecord? mentalRecordToDelete = await _context.MentalRecords.FindAsync(id);
        
        // If no MentalRecord is found, returns a 404 Not Found status
        if (mentalRecordToDelete == null)
        {
            return NotFound();
        }

        try
        {
            // Removes the MentalRecord from the database and saves changes
            _context.MentalRecords.Remove(mentalRecordToDelete);
            await _context.SaveChangesAsync();

            // Returns a 204 NoContent status to indicate successful deletion without returning data
            return NoContent();
        }
        catch
        {
            // If an exception occurs, returns a 400 Bad Request status
            return BadRequest("Die Anfrage konnte nicht verarbeitet werden.");
        }
    }
}
