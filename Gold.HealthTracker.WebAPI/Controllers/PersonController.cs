using System.Net;
using Gold.HealthTracker.DBModel;
using Gold.HealthTracker.DTOModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

namespace Gold.HealthTracker.WebAPI;

// Marks this class as an API controller with a route base of 'controller' name (Person in this case)
[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly HealthTrackerContext _context;

    // Constructor with Dependency Injection for the database context
    public PersonController(HealthTrackerContext context)
    {
        _context = context;
    }



    /// <summary>
    /// HTTP GET method to retrieve all persons
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(PersonDTO))]
    [ProducesResponseType(404)]
    public IEnumerable<PersonDTO?> GetPersons()
    {
        // Checks if the Person set is null (which should never be true in a properly configured context)
        if(_context.BodyRecords == null)
            return (IEnumerable<PersonDTO?>)NotFound();

        // Queries all persons from the database and converts them to PersonDTOs
        return _context.Persons
            .Select(p => new PersonDTO()
            {
                Id = p.Id,
                Name = p.Name
            });
    }



    /// <summary>
    /// HTTP GET method with an 'id' parameter to retrieve a specific Person by their ID
    /// </summary>
    /// <param name="id">PrimaryKey of the Person</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(PersonDTO))]
    [ProducesResponseType(404)]
    public async Task<ActionResult> GetPerson(int id)
    {
        // Searches for a person using the given ID
        Person? foundPerson = await _context.Persons.FindAsync(id);

        // If no person is found, returns a 404 Not Found status
        if(foundPerson == null)
        {
            return NotFound();
        }

        // Returns the found person as a DTO
        return Ok(new PersonDTO()
        {
            Id = foundPerson.Id,
            Name = foundPerson.Name
        });
    }



    /// <summary>
    /// HTTP POST method to create a new person
    /// </summary>
    /// <param name="newPersonDTO">Creates a new PersonDTO</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreatePerson(CreatePersonDTO newPersonDTO)
    {
        // Checks model state for any validation errors
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        Person newPerson = new Person {
            Name = newPersonDTO.Name
        };

        // Adds the new person to the database
        await _context.Persons.AddAsync(newPerson);

        // Saves changes to the database
        await _context.SaveChangesAsync();

        // Returns a 201 Created status with the ID of the newly created person
        return CreatedAtAction(nameof(GetPerson), new {id = newPerson.Id}, newPerson.Id);
    }



    /// <summary>
    /// HTTP PUT method to update an existing person
    /// </summary>
    /// <param name="id">PrimaryKey of the Person</param>
    /// <param name="personDTO">Creates a new PersonDTO</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePerson(int id, CreatePersonDTO personDTO)
    {
        // Finds the person to update
        Person? personToUpdate = await _context.Persons.FindAsync(id);
        if (personToUpdate == null)
        {
            return NotFound($"Keine Person mit der ID {id} gefunden.");
        }

        // Updates person data
        personToUpdate.Name = personDTO.Name;

        try
        {
            // Attempts to save changes to the database. This includes updates made to the entity being modified.
            await _context.SaveChangesAsync();
        }
        // Catches exceptions that are thrown when there is a concurrency conflict. Concurrency conflicts occur when an entity has been changed in the database after it was loaded into memory.
        catch (DbUpdateConcurrencyException)
        {
            // If the Person with the specified ID does not exist, it returns a NotFound result (person might have been deleted between the time it was fetched and the attempt to update it)
            if (!PersonExists(id))
            {
                return NotFound();
            }
            // If the person exists but another type of concurrency conflict has occurred...
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
    /// Checks if a specific person exists in the database
    /// </summary>
    /// <param name="id">PrimaryKey of the Person</param>
    /// <returns></returns>
    private bool PersonExists(int id)
    {
        return _context.Persons.Any(e => e.Id == id);
    }



    /// <summary>
    /// HTTP DELETE method to remove a person
    /// </summary>
    /// <param name="id">PrimaryKey of the person</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeletePerson(int id)
    {
        // Finds the person to be deleted
        Person? personToDelete = await _context.Persons.FindAsync(id);

        // If no person is found, returns a 404 Not Found status
        if(personToDelete == null) 
        {
            return NotFound();
        }

        try
        {
            // Removes the person from the database and saves changes
            _context.Persons.Remove(personToDelete);
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
