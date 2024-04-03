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

    // Retrieve all workouts including exercises and sets
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(WorkoutDTO))]
    [ProducesResponseType(404)]
    public async Task<ActionResult<IEnumerable<WorkoutDTO>>> GetWorkouts()
    {
        
        var workouts = await _context.Workouts
            .Include(workout => workout.Exercises)
                .ThenInclude(exercise => exercise.Sets)
            .Select(workout => new WorkoutDTO
            {
                Id = workout.Id,
                PersonId = workout.Person == null ? 0 : workout.Person.Id,
                DateOfRecord = workout.DateOfRecord,
                TrainingTime = workout.TrainingTime,
                AverageHeartRate = workout.AverageHeartRate,
                MaxHeartRate = workout.MaxHeartRate,
                CaloriesBurned = workout.CaloriesBurned,
                SessionRating = workout.SessionRating,
                TrainingLocation = workout.TrainingLocation,
                Exercises = workout.Exercises.Select(exercise => new ExerciseDTO
                {
                    ExerciseName = exercise.ExerciseName,
                    Sets = exercise.Sets.Select(set => new SetDTO
                    {
                        Repetitions = set.Repetitions,
                        Weight = set.Weight
                    }).ToList()
                }).ToList()
            }).ToListAsync();

        if (workouts == null)
        {
            return NotFound();
        }
        
        return Ok(workouts);
    }

    // Retrieve a single workout by ID including exercises and sets
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkoutDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WorkoutDTO>> GetWorkout(int id)
    {
        var foundWorkout = await _context.Workouts
            .Include(workout => workout.Exercises)
                .ThenInclude(exercise => exercise.Sets)
            .Where(workout => workout.Id == id)
            .Select(workout => new WorkoutDTO
            {
                Id = workout.Id,
                PersonId = workout.Person.Id,
                DateOfRecord = workout.DateOfRecord,
                TrainingTime = workout.TrainingTime,
                AverageHeartRate = workout.AverageHeartRate,
                MaxHeartRate = workout.MaxHeartRate,
                CaloriesBurned = workout.CaloriesBurned,
                SessionRating = workout.SessionRating,
                TrainingLocation = workout.TrainingLocation,
                Exercises = workout.Exercises.Select(exercise => new ExerciseDTO
                {
                    Id = exercise.Id,
                    ExerciseName = exercise.ExerciseName,
                    Sets = exercise.Sets.Select(set => new SetDTO
                    {
                        Id = set.Id,
                        Repetitions = set.Repetitions,
                        Weight = set.Weight
                    }).ToList()
                }).ToList()
            }).FirstOrDefaultAsync();

        if (foundWorkout == null)
        {
            return NotFound();
        }

        return Ok(foundWorkout);
    }

    // Add a new workout with exercises and sets
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<WorkoutDTO>> CreateWorkout(CreateWorkoutDTO createWorkoutDTO)
    {
        if (!createWorkoutDTO.PersonId.HasValue)
        {
            return BadRequest("PersonId is required.");
        }

        var person = await _context.Persons.FindAsync(createWorkoutDTO.PersonId.Value);

        if (person == null)
        {
            return NotFound($"No person found with ID {createWorkoutDTO.PersonId.Value}.");
        }
    
        var workout = new Workout
        {
            Person = await _context.Persons.FindAsync(createWorkoutDTO.PersonId),
            DateOfRecord = createWorkoutDTO.DateOfRecord,
            TrainingTime = createWorkoutDTO.TrainingTime,
            AverageHeartRate = createWorkoutDTO.AverageHeartRate,
            MaxHeartRate = createWorkoutDTO.MaxHeartRate,
            CaloriesBurned = createWorkoutDTO.CaloriesBurned,
            SessionRating = createWorkoutDTO.SessionRating,
            TrainingLocation = createWorkoutDTO.TrainingLocation,
            Exercises = createWorkoutDTO.Exercises.Select(e => new Exercise
            {
                ExerciseName = e.ExerciseName,
                Sets = e.Sets.Select(s => new Set
                {
                    Repetitions = s.Repetitions,
                    Weight = s.Weight
                }).ToList()
            }).ToList()
        };

        _context.Workouts.Add(workout);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetWorkout), new { id = workout.Id }, new WorkoutDTO
        {
            // Populate DTO from the created entity, similar to how it's done in GetWorkout.
        });
    }

    // Update an existing workout with exercises and sets
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateWorkout(int id, CreateWorkoutDTO workoutDTO)
    {
        
        var workoutToUpdate = await _context.Workouts
            .Include(w => w.Exercises)
            .ThenInclude(e => e.Sets)
            .FirstOrDefaultAsync(w => w.Id == id);

        if (workoutToUpdate == null)
            {
                return NotFound();
            }

        if (workoutToUpdate == null)
        {
            return NotFound("Workout not found.");
        }

        // Updating workout properties
        workoutToUpdate.DateOfRecord = workoutDTO.DateOfRecord;
        workoutToUpdate.TrainingTime = workoutDTO.TrainingTime;
        workoutToUpdate.AverageHeartRate = workoutDTO.AverageHeartRate;
        workoutToUpdate.MaxHeartRate = workoutDTO.MaxHeartRate;
        workoutToUpdate.CaloriesBurned = workoutDTO.CaloriesBurned;
        workoutToUpdate.SessionRating = workoutDTO.SessionRating;
        workoutToUpdate.TrainingLocation = workoutDTO.TrainingLocation;

        // Updating exercises and sets
        workoutToUpdate.Exercises.Clear(); // Clear existing exercises to replace with updated ones

        foreach (var exerciseDTO in workoutDTO.Exercises)
        {
            var exercise = new Exercise
            {
                ExerciseName = exerciseDTO.ExerciseName,
                Sets = exerciseDTO.Sets.Select(s => new Set
                {
                    Repetitions = s.Repetitions,
                    Weight = s.Weight
                }).ToList()
            };

            workoutToUpdate.Exercises.Add(exercise);
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            if (!WorkoutExists(id))
            {
                return NotFound("Workout not found.");
            }
            else
            {
                return BadRequest("Unable to update the workout: " + ex.Message);
            }
        }

        return NoContent(); // Success, workout updated
    }

    private bool WorkoutExists(int id)
    {
        return _context.Workouts.Any(w => w.Id == id);
    }

    // Delete a workout by ID
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteWorkout(int id)
    {
        Workout? workoutToDelete = await _context.Workouts.FindAsync(id);

        if (workoutToDelete == null)
        {
            return NotFound("Workout not found.");
        }

        try
        {
            _context.Workouts.Remove(workoutToDelete);
            await _context.SaveChangesAsync();

            return NoContent(); // Success, workout deleted
        }
        catch
        {
            return BadRequest("Die Anfrage konnte nicht verarbeitet werden.");
        }
    }
}
