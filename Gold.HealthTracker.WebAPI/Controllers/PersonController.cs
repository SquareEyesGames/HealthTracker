using Gold.HealthTracker.DBModel;
using Gold.HealthTracker.DTOModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Gold.HealthTracker.WebAPI;

[ApiController]
[Route("[controller]")]
public class PersonController
{
    private readonly HealthTrackerContext _context;

    public PersonController(HealthTrackerContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IEnumerable<PersonDTO?> GetPersons()
    {
        return _context.Persons
            .Select(p =>
                new PersonDTO()
                {
                    Id = p.Id,
                    Name = p.Name
                });
    }

    [HttpGet("{id}")]
    public async Task<PersonDTO?> GetPerson(int id)
    {
        Person? foundPerson = await _context.Persons.FindAsync(id);
        return new PersonDTO()
        {
            Id = foundPerson?.Id ?? 0,
            Name = foundPerson?.Name
        };
    }

    [HttpPost]
    public async Task CreatePerson(PersonDTO newPerson)
    {
        await _context.Persons.AddAsync(new Person {
            Name = newPerson.Name
        });

        await _context.SaveChangesAsync();
    }

    [HttpDelete("{id}")]
    public async Task DeletePerson(int id)
    {
        var personToDelete = await _context.Persons.FindAsync(id);

        if(personToDelete == null) return;

        _context.Persons.Remove(personToDelete);
        await _context.SaveChangesAsync();
    }
}
