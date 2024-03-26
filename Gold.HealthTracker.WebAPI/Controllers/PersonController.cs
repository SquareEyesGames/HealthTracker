using System.Net;
using Gold.HealthTracker.DBModel;
using Gold.HealthTracker.DTOModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

namespace Gold.HealthTracker.WebAPI;

// Markiert die Klasse als API-Controller und definiert die Basisroute
[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly HealthTrackerContext _context;

    // Konstruktor mit Dependency Injection für den Datenbankkontext
    public PersonController(HealthTrackerContext context)
    {
        _context = context;
    }



    // HTTP GET-Methode, um alle Personen abzurufen
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(PersonDTO))]
    [ProducesResponseType(404)]
    public IEnumerable<PersonDTO?> GetPersons()
    {
        if(_context.BodyRecords == null)
            return (IEnumerable<PersonDTO?>)NotFound();

        // Abfrage aller Personen aus der Datenbank und Umwandlung in PersonDTOs
        return _context.Persons
            .Select(p =>
                new PersonDTO()
                {
                    Id = p.Id,
                    Name = p.Name
                });
    }



    // HTTP GET-Methode mit ID-Parameter, um eine spezifische Person abzurufen
    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(PersonDTO))]
    [ProducesResponseType(404)]
    public async Task<ActionResult> GetPerson(int id)
    {
        // Sucht eine Person mit der gegebenen ID
        Person? foundPerson = await _context.Persons.FindAsync(id);

        if(foundPerson == null)
        {
            return NotFound();
        }

        // Gibt die gefundene Person als DTO zurück
        return Ok(new PersonDTO()
        {
            Id = foundPerson.Id,
            Name = foundPerson.Name
        });
    }



    // HTTP POST-Methode, um eine neue Person zu erstellen
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreatePerson(CreatePersonDTO newPersonDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        Person newPerson = new Person {
            Name = newPersonDTO.Name
        };

        // Fügt eine neue Person zur Datenbank hinzu
        await _context.Persons.AddAsync(newPerson);
        
        // Speichert die Änderungen in der Datenbank
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPerson), new {id = newPerson.Id}, newPerson.Id);
    }



    // HTTP PUT-Methode, um eine bestehende Person zu aktualisieren
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePerson(int id, CreatePersonDTO personDTO)
    {
        // Sucht die zu aktualisierende Person
        var personToUpdate = await _context.Persons.FindAsync(id);
        if (personToUpdate == null)
        {
            return NotFound($"Keine Person mit der ID {id} gefunden.");
        }

        // Aktualisiert die Daten der Person
        personToUpdate.Name = personDTO.Name;
        // Füge hier weitere Zuweisungen hinzu, falls weitere Felder aktualisiert werden sollen

        try
        {
            // Speichert die Änderungen in der Datenbank
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PersonExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        // Gibt 204 No Content zurück, um den Erfolg der Operation zu signalisieren
        return NoContent();
    }

    private bool PersonExists(int id)
    {
        return _context.Persons.Any(e => e.Id == id);
    }



    // HTTP DELETE-Methode, um eine Person zu löschen
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeletePerson(int id)
    {
        // Sucht die Person, die gelöscht werden soll
        Person? personToDelete = await _context.Persons.FindAsync(id);

        // Wenn die Person nicht existiert, gibt 404 zurück
        if(personToDelete == null) 
        {
            return NotFound();
        }

        try
        {
            // Entfernt die Person aus der Datenbank & speichert Änderung in der Datenbank
            _context.Persons.Remove(personToDelete);
            await _context.SaveChangesAsync();

            // Gibt 204 No Content zurück, um den Erfolg ohne Datenrückgabe zu signalisieren
            return NoContent();
        }
        catch
        {
            // Gibt 400 Bad Request zurück
            return BadRequest("Die Anfrage konnte nicht verarbeitet werden.");
        }
    }
}
