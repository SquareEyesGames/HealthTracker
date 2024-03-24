using Gold.HealthTracker.DBModel;
using Gold.HealthTracker.DTOModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public IEnumerable<PersonDTO?> GetPersons()
    {
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
    public async Task<PersonDTO?> GetPerson(int id)
    {
        // Sucht eine Person mit der gegebenen ID
        Person? foundPerson = await _context.Persons.FindAsync(id);

        // Gibt die gefundene Person als DTO zurück
        return new PersonDTO()
        {
            Id = foundPerson?.Id ?? 0,
            Name = foundPerson?.Name
        };
    }

    // HTTP POST-Methode, um eine neue Person zu erstellen
    [HttpPost]
    public async Task CreatePerson(PersonDTO newPerson)
    {
        // Fügt eine neue Person zur Datenbank hinzu
        await _context.Persons.AddAsync(new Person {
            Name = newPerson.Name
        });
        
        // Speichert die Änderungen in der Datenbank
        await _context.SaveChangesAsync();
    }

    // HTTP DELETE-Methode, um eine Person zu löschen
    [HttpDelete("{id}")]
    public async Task DeletePerson(int id)
    {
        // Sucht die Person, die gelöscht werden soll
        var personToDelete = await _context.Persons.FindAsync(id);

        // Wenn die Person nicht existiert, beendet die Methode
        if(personToDelete == null) return;

        // Entfernt die Person aus der Datenbank & speichert Änderung in der Datenbank
        _context.Persons.Remove(personToDelete);
        await _context.SaveChangesAsync();
    }
}
