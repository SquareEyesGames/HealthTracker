using Gold.HealthTracker.DBModel;
using Microsoft.EntityFrameworkCore;

HealthTrackerContext context = new();

context.Database.EnsureCreated();

Person? existingPerson = context.Persons
   .Include(p => p.BodyRecordList)
   .Include(p => p.RunList)
   .Include(p => p.WorkoutList)
   .Include(p => p.SportSessionList)
   .Include(p => p.SleepRecordList)
   .Include(p => p.NutritionRecordList)
   .Include(p => p.MentalRecordList)
   .FirstOrDefault(p => p.Name == "Thomas");

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

// Creates a new BodyRecord for existingPerson
existingPerson.BodyRecordList.Add(new BodyRecord {DateOfRecord = DateTime.Now, Bodyweight = 86.60f});
context.SaveChanges();
Console.WriteLine($"Ändergungen gespeichert");
ShowBodyRecords(existingPerson);

// Updates the last created BodyRecord
BodyRecord oldestRecord = existingPerson.BodyRecordList.OrderBy(r => r.DateOfRecord).Last();
oldestRecord.Bodyweight = 85.00f;
context.SaveChanges();
Console.WriteLine($"Ändergungen gespeichert");
ShowBodyRecords(existingPerson);

// Deletes the last BodyRecord for existingPerson (if any exist)
if(existingPerson.BodyRecordList.Any())
{
   context.BodyRecords.Remove(oldestRecord);
   context.SaveChanges();
   Console.WriteLine($"Ändergungen gespeichert");
   ShowBodyRecords(existingPerson);
}

// Prints all entrys in the BodyRecords Table
static void ShowBodyRecords(Person person)
{
   Console.WriteLine($"Alle Gewichtsmessungen für {person.Name}: ");
   foreach (var record in person.BodyRecordList)
   {
      Console.WriteLine(record.DateOfRecord + ": " + record.Bodyweight + "kg");
   }
}