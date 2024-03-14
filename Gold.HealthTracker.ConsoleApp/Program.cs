using Gold.HealthTracker.DBModel;
using Microsoft.EntityFrameworkCore;

HealthTrackerContext context = new();

// Ensures the database is created based on the current model
context.Database.EnsureCreated();

// Checks if there is already an existing Person named "Thomas" in the database
Person? existingPerson = context.Persons.FirstOrDefault(p => p.Name == "Thomas");

// If not, creates a new person named "Thomas"
if(existingPerson == null)
{
   existingPerson = new Person()
   {
      Name = "Thomas",
   };
   Console.WriteLine($"Person mit Namen {existingPerson.Name} erstellt");

   context.Persons.Add(existingPerson);
   Console.WriteLine($"Person mit Namen {existingPerson.Name} hinzugefügt und Id {existingPerson.Id} zugewiesen");
}
else
{
   Console.WriteLine($"Person mit id {existingPerson.Id} und Name {existingPerson.Name} existiert bereits in der Datenbank");
}

// Adds a new BodyRecord to "Thomas" and saves changes to the database
existingPerson.BodyRecords.Add(new BodyRecord {DateOfRecord = DateTime.Now, Bodyweight = 86.60f});
context.SaveChanges();
Console.WriteLine($"Ändergungen gespeichert");
ShowBodyRecords(existingPerson);

// Updates the last BodyRecord created for "Thomas", saves changes, and displays all body records
BodyRecord oldestRecord = existingPerson.BodyRecords.OrderBy(r => r.DateOfRecord).Last();
oldestRecord.Bodyweight = 85.00f;
context.SaveChanges();
Console.WriteLine($"Ändergungen gespeichert");
ShowBodyRecords(existingPerson);

// Optionally deletes the last BodyRecord for "Thomas" if any exist, saves changes, and displays the updated list
if(existingPerson.BodyRecords.Any())
{
   context.BodyRecords.Remove(oldestRecord);
   context.SaveChanges();
   Console.WriteLine($"Ändergungen gespeichert");
   ShowBodyRecords(existingPerson);
}

// Helper method to display all BodyRecords for "Thomas"
static void ShowBodyRecords(Person person)
{
   Console.WriteLine($"Alle Gewichtsmessungen für {person.Name}: ");
   foreach (var record in person.BodyRecords)
   {
      Console.WriteLine(record.DateOfRecord + ": " + record.Bodyweight + "kg");
   }
}