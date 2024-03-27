using Gold.HealthTracker.DBModel;
using Gold.HealthTracker.DTOModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Gold.HealthTracker.WebAPI.Controllers;

// Marks this class as an API controller with a route base of 'controller' name
[ApiController]
[Route("[controller]")]
public class BodyRecordController : ControllerBase
{
    private readonly HealthTrackerContext _context;

    // Constructor with Dependency Injection for the database context
    public BodyRecordController(HealthTrackerContext context)
    {
        _context = context;
    }

    /// <summary>
    /// HTTP GET method to retrieve all BodyRecords
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(BodyRecordDTO))]
    [ProducesResponseType(404)]
    public IEnumerable<BodyRecordDTO?> GetBodyRecords()
    {
        // Checks if the BodyRecord set is null (which should never be true in a properly configured context)
        if(_context.BodyRecords == null)
            return (IEnumerable<BodyRecordDTO?>)NotFound();

        // Queries all BodyRecords from the database and converts them to BodyRecordDTOs
        return _context.BodyRecords
            .Select(bodyRecord => new BodyRecordDTO
            {
                Id = bodyRecord.Id,
                PersonId = bodyRecord.Person == null ? 0 : bodyRecord.Person.Id,
                DateOfRecord = bodyRecord.DateOfRecord.ToDateTime(TimeOnly.MinValue),
                Bodyweight = bodyRecord.Bodyweight,
                BMI = bodyRecord.BMI,
                BodyFat = bodyRecord.BodyFat,
                FatlessBodyWeight = bodyRecord.FatlessBodyWeight,
                SubcutaneousBodyFat = bodyRecord.SubcutaneousBodyFat,
                VisceralFat = bodyRecord.VisceralFat,
                BodyWater = bodyRecord.BodyWater,
                SkeletalMuscle = bodyRecord.SkeletalMuscle,
                MuscleMass = bodyRecord.MuscleMass,
                BoneMass = bodyRecord.BoneMass,
                MetabolicRate = bodyRecord.MetabolicRate,
                MetabolicAge = bodyRecord.MetabolicAge
            });
    }

    /// <summary>
    /// HTTP GET method with an 'id' parameter to retrieve a specific BodyRecord by its Id
    /// </summary>
    /// <param name="id">PrimaryKey of the BodyRecord</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(BodyRecordDTO))]
    [ProducesResponseType(404)]
    public async Task<ActionResult> GetBodyRecord(int id)
    {
        // Searches for a BodyRecord using the given ID
        BodyRecord? foundBodyRecord = await _context.BodyRecords.FindAsync(id);

        // If no BodyRecord is found, returns a 404 Not Found status
        if (foundBodyRecord == null)
        {
            return NotFound();
        }

        // Returns the found BodyRecord as a DTO
        return Ok(new BodyRecordDTO()
        {
            Id = foundBodyRecord.Id,
            PersonId = foundBodyRecord.Person?.Id ?? 0,
            DateOfRecord = foundBodyRecord.DateOfRecord.ToDateTime(TimeOnly.MinValue),
            Bodyweight = foundBodyRecord.Bodyweight,
            BMI = foundBodyRecord.BMI,
            BodyFat = foundBodyRecord.BodyFat,
            FatlessBodyWeight = foundBodyRecord.FatlessBodyWeight,
            SubcutaneousBodyFat = foundBodyRecord.SubcutaneousBodyFat,
            VisceralFat = foundBodyRecord.VisceralFat,
            BodyWater = foundBodyRecord.BodyWater,
            SkeletalMuscle = foundBodyRecord.SkeletalMuscle,
            MuscleMass = foundBodyRecord.MuscleMass,
            BoneMass = foundBodyRecord.BoneMass,
            MetabolicRate = foundBodyRecord.MetabolicRate,
            MetabolicAge = foundBodyRecord.MetabolicAge
        });
    }

    /// <summary>
    /// HTTP POST method to create a new BodyRecord
    /// </summary>
    /// <param name="newBodyRecordDTO">Creates a new BodyRecordDTO</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateBodyRecord(CreateBodyRecordDTO newBodyRecordDTO)
    {
        // Checks model state for any validation errors
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        BodyRecord newBodyRecord = new BodyRecord
        {
            Person = await _context.Persons.FindAsync(newBodyRecordDTO.PersonId),
            DateOfRecord = DateOnly.FromDateTime(newBodyRecordDTO.DateOfRecord),
            Bodyweight = newBodyRecordDTO.Bodyweight,
            BMI = newBodyRecordDTO.BMI,
            BodyFat = newBodyRecordDTO.BodyFat,
            FatlessBodyWeight = newBodyRecordDTO.FatlessBodyWeight,
            SubcutaneousBodyFat = newBodyRecordDTO.SubcutaneousBodyFat,
            VisceralFat = newBodyRecordDTO.VisceralFat,
            BodyWater = newBodyRecordDTO.BodyWater,
            SkeletalMuscle = newBodyRecordDTO.SkeletalMuscle,
            MuscleMass = newBodyRecordDTO.MuscleMass,
            BoneMass = newBodyRecordDTO.BoneMass,
            MetabolicRate = newBodyRecordDTO.MetabolicRate,
            MetabolicAge = newBodyRecordDTO.MetabolicAge
        };

        // Adds the new BodyRecord to the database
        await _context.BodyRecords.AddAsync(newBodyRecord);
        
        // Saves changes to the database
        await _context.SaveChangesAsync();

        // Returns a 201 Created status with the ID of the newly created BodyRecord
        return CreatedAtAction(nameof(GetBodyRecord), new { id = newBodyRecord.Id }, newBodyRecord.Id);
    }

    /// <summary>
    /// HTTP PUT method to update an existing BodyRecord
    /// </summary>
    /// <param name="id">PrimaryKey of the BodyRecord</param>
    /// <param name="bodyRecordDTO">Creates a new BodyRecordDTO</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBodyRecord(int id, CreateBodyRecordDTO bodyRecordDTO)
    {
        // Finds the BodyRecord to update
        BodyRecord? bodyRecordToUpdate = await _context.BodyRecords.FindAsync(id);
        if (bodyRecordToUpdate == null)
        {
            return NotFound();
        }

        // Updates BodyRecord data
        bodyRecordToUpdate.Person = await _context.Persons.FindAsync(bodyRecordDTO.PersonId);
        bodyRecordToUpdate.DateOfRecord = DateOnly.FromDateTime(bodyRecordDTO.DateOfRecord);
        bodyRecordToUpdate.Bodyweight = bodyRecordDTO.Bodyweight;
        bodyRecordToUpdate.BMI = bodyRecordDTO.BMI;
        bodyRecordToUpdate.BodyFat = bodyRecordDTO.BodyFat;
        bodyRecordToUpdate.FatlessBodyWeight = bodyRecordDTO.FatlessBodyWeight;
        bodyRecordToUpdate.SubcutaneousBodyFat = bodyRecordDTO.SubcutaneousBodyFat;
        bodyRecordToUpdate.VisceralFat = bodyRecordDTO.VisceralFat;
        bodyRecordToUpdate.BodyWater = bodyRecordDTO.BodyWater;
        bodyRecordToUpdate.SkeletalMuscle = bodyRecordDTO.SkeletalMuscle;
        bodyRecordToUpdate.MuscleMass = bodyRecordDTO.MuscleMass;
        bodyRecordToUpdate.BoneMass = bodyRecordDTO.BoneMass;
        bodyRecordToUpdate.MetabolicRate = bodyRecordDTO.MetabolicRate;
        bodyRecordToUpdate.MetabolicAge = bodyRecordDTO.MetabolicAge;

        try
        {
            // Attempts to save changes to the database. This includes updates made to the entity being modified.
            await _context.SaveChangesAsync();
        }
        // Catches exceptions that are thrown when there is a concurrency conflict. Concurrency conflicts occur when an entity has been changed in the database after it was loaded into memory.
        catch (DbUpdateConcurrencyException)
        {
            // If the BodyRecord with the specified ID does not exist, it returns a NotFound result (BodyRecord might have been deleted between the time it was fetched and the attempt to update it)
            if (!BodyRecordExists(id))
            {
                return NotFound();
            }
            // If the BodyRecord exists but another type of concurrency conflict has occurred...
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
    /// Checks if a specific BodyRecord exists in the database
    /// </summary>
    /// <param name="id">PrimaryKey of the BodyRecord</param>
    /// <returns></returns>
    private bool BodyRecordExists(int id)
    {
        return _context.BodyRecords.Any(e => e.Id == id);
    }

    /// <summary>
    /// HTTP DELETE method to remove a BodyRecord
    /// </summary>
    /// <param name="id">PrimaryKey of the BodyRecord</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteBodyRecord(int id)
    {
        // Finds the BodyRecord to be deleted
        BodyRecord? bodyRecordToDelete = await _context.BodyRecords.FindAsync(id);
        
        // If no BodyRecord is found, returns a 404 Not Found status
        if (bodyRecordToDelete == null)
        {
            return NotFound();
        }

        try
        {
            // Removes the BodyRecord from the database and saves changes
            _context.BodyRecords.Remove(bodyRecordToDelete);
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
