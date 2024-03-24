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
    public async Task<ActionResult<IEnumerable<BodyRecordDTO>>> GetBodyRecords()
    {
        return await _context.BodyRecords
            .Select(br => new BodyRecordDTO
            {
                Id = br.Id,
                DateOfRecord = br.DateOfRecord,
                Bodyweight = br.Bodyweight,
                BMI = br.BMI,
                BodyFat = br.BodyFat,
                FatlessBodyWeight = br.FatlessBodyWeight,
                SubcutaneousBodyFat = br.SubcutaneousBodyFat,
                VisceralFat = br.VisceralFat,
                BodyWater = br.BodyWater,
                SkeletalMuscle = br.SkeletalMuscle,
                MuscleMass = br.MuscleMass,
                BoneMass = br.BoneMass,
                MetabolicRate = br.MetabolicRate,
                MetabolicAge = br.MetabolicAge
            })
            .ToListAsync();
    }

    // GET: api/BodyRecord/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BodyRecordDTO>> GetBodyRecord(int id)
    {
        var bodyRecord = await _context.BodyRecords.FindAsync(id);

        if (bodyRecord == null)
        {
            return NotFound();
        }

        return new BodyRecordDTO
        {
            Id = bodyRecord.Id,
            DateOfRecord = bodyRecord.DateOfRecord,
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
        };
    }

    // POST: api/BodyRecord
    [HttpPost]
    public async Task<ActionResult<BodyRecordDTO>> PostBodyRecord(BodyRecordDTO bodyRecordDTO)
    {
        var bodyRecord = new BodyRecord
        {
            // Daten von DTO zu Entity übertragen
        };
        _context.BodyRecords.Add(bodyRecord);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBodyRecord), new { id = bodyRecord.Id }, bodyRecordDTO);
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
