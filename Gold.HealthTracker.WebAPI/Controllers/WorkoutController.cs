using Microsoft.AspNetCore.Mvc;
using Gold.HealthTracker.DBModel;
using Gold.HealthTracker.DTOModel;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Gold.HealthTracker.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkoutController : ControllerBase
{
    private readonly HealthTrackerContext _context;

    public WorkoutController(HealthTrackerContext context)
    {
        _context = context;
    }

    // GET: api/Workout
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkoutDTO>>> GetWorkouts()
    {
        return await _context.Workouts
            .Include(w => w.Exercises)
            .ThenInclude(e => e.Sets)
            .Select(w => new WorkoutDTO
            {
                Id = w.Id,
                DateOfRecord = w.DateOfRecord,
                TrainingLocation = w.TrainingLocation,
                TrainingTime = w.TrainingTime,
                Exercises = w.Exercises.Select(e => new ExerciseDTO
                {
                    Id = e.Id,
                    ExerciseName = e.ExerciseName,
                    Sets = e.Sets.Select(s => new SetDTO
                    {
                        Id = s.Id,
                        Repetitions = s.Repetitions,
                        Weight = s.Weight
                    }).ToList()
                }).ToList()
            }).ToListAsync();
    }

    // Weitere CRUD-Operationen hier...
}
