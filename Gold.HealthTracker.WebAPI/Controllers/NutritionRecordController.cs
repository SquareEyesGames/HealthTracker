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
    [ProducesResponseType(200, Type = typeof(NutritionRecordDTO))]
    [ProducesResponseType(404)]
    public IEnumerable<NutritionRecordDTO?> GetNutritionRecords()
    {
        if(_context.NutritionRecords == null)
        {
            return (IEnumerable<NutritionRecordDTO?>)NotFound();
        }

        return _context.NutritionRecords.Select(nutritionRecord => new NutritionRecordDTO
        {
            Id = nutritionRecord.Id,
            PersonId = nutritionRecord.Person == null ? 0 : nutritionRecord.Person.Id,
            DateOfRecord = nutritionRecord.DateOfRecord.ToDateTime(TimeOnly.MinValue),
            CalorieIntake = nutritionRecord.CalorieIntake
        });
    }



    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(NutritionRecordDTO))]
    [ProducesResponseType(404)]
    public async Task<ActionResult> GetNutritionRecord(int id)
    {
        NutritionRecord? foundNutritionRecord = await _context.NutritionRecords.FindAsync(id);

        if(foundNutritionRecord == null)
        {
            return NotFound();
        }

        return Ok(new NutritionRecordDTO()
        {
            Id = foundNutritionRecord.Id,
            PersonId = foundNutritionRecord.Person?.Id ?? 0,
            DateOfRecord = foundNutritionRecord.DateOfRecord.ToDateTime(TimeOnly.MinValue),
            CalorieIntake = foundNutritionRecord.CalorieIntake
        });
    }



    [HttpPost]
    [ProducesResponseType(201, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateNutritionRecord(NutritionRecordDTO newNutritionRecordDTO)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        NutritionRecord newNutritionRecord = new NutritionRecord
        {
            Person = await _context.Persons.FindAsync(newNutritionRecordDTO.PersonId),
            DateOfRecord = DateOnly.FromDateTime(newNutritionRecordDTO.DateOfRecord),
            CalorieIntake = newNutritionRecordDTO.CalorieIntake
        };

        await _context.NutritionRecords.AddAsync(newNutritionRecord);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetNutritionRecord), new { id = newNutritionRecord.Id}, newNutritionRecord.Id);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateNutritionRecord(int id, CreateNutritionRecordDTO nutritionRecordDTO)
    {
        NutritionRecord? nutritionRecordToUpdate = await _context.NutritionRecords.FindAsync(id);

        if(nutritionRecordToUpdate == null)
        {
            return NotFound();
        }

        nutritionRecordToUpdate.Person = await _context.Persons.FindAsync(nutritionRecordDTO.PersonId);
        nutritionRecordToUpdate.DateOfRecord = DateOnly.FromDateTime(nutritionRecordDTO.DateOfRecord);
        nutritionRecordToUpdate.CalorieIntake = nutritionRecordDTO.CalorieIntake;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch
        {
            if(!NutritionRecordExists(id))
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

    private bool NutritionRecordExists(int id)
    {
        return _context.NutritionRecords.Any(e => e.Id == id);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteNutritionRecord(int id)
    {
        NutritionRecord? nutritionRecordToDelete = await _context.NutritionRecords.FindAsync(id);

        if(nutritionRecordToDelete == null)
        {
            return NotFound();
        }

        try
        {
            _context.NutritionRecords.Remove(nutritionRecordToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }
}
