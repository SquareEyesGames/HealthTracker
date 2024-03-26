using Gold.HealthTracker.DBModel;
using Gold.HealthTracker.DTOModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Gold.HealthTracker.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BodyRecordController : ControllerBase
{
    private readonly HealthTrackerContext _context;

    public BodyRecordController(HealthTrackerContext context)
    {
        _context = context;
    }

    // GET: api/BodyRecord
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(BodyRecordDTO))]
    [ProducesResponseType(404)]
    public IEnumerable<BodyRecordDTO?> GetBodyRecords()
    {
        if(_context.BodyRecords == null)
            return (IEnumerable<BodyRecordDTO?>)NotFound();

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

    // GET: api/BodyRecord/5
    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(BodyRecordDTO))]
    [ProducesResponseType(404)]
    public async Task<ActionResult> GetBodyRecord(int id)
    {
        BodyRecord? foundBodyRecord = await _context.BodyRecords.FindAsync(id);

        if (foundBodyRecord == null)
        {
            return NotFound();
        }

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

    // POST: api/BodyRecord
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateBodyRecord(CreateBodyRecordDTO newBodyRecordDTO)
    {
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
        await _context.BodyRecords.AddAsync(newBodyRecord);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBodyRecord), new { id = newBodyRecord.Id }, newBodyRecord.Id);
    }

    // PUT: api/BodyRecord/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBodyRecord(int id, BodyRecordDTO bodyRecordDTO)
    {
        if (id != bodyRecordDTO.Id)
        {
            return BadRequest();
        }

        var bodyRecord = await _context.BodyRecords.FindAsync(id);
        if (bodyRecord == null)
        {
            return NotFound();
        }

        // Daten von DTO zu Entity übertragen
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/BodyRecord/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBodyRecord(int id)
    {
        var bodyRecord = await _context.BodyRecords.FindAsync(id);
        if (bodyRecord == null)
        {
            return NotFound();
        }

        _context.BodyRecords.Remove(bodyRecord);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
