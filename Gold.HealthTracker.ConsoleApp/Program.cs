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
   existingPerson = new Person() { Name = "Thomas"};
   Console.WriteLine($"Person mit Namen {existingPerson.Name} erstellt");
   context.Persons.Add(existingPerson);
   Console.WriteLine($"Person mit Namen {existingPerson.Name} hinzugefügt und Id {existingPerson.Id} zugewiesen");
   context.SaveChanges();
}
else
{
   Console.WriteLine($"Person {existingPerson.Name} in der Datenbank gefunden");
}

bool exit = false;

while(!exit)
{
   Console.WriteLine("\nWelchen Eintrag möchtest du hinzufügen?");
   Console.WriteLine("1: Körpermessung");
   Console.WriteLine("2: Lauftraining");
   Console.WriteLine("3: Schlafmessung");
   Console.WriteLine("4: Exit");
   Console.Write("Auswahl: ");

   string? choice = Console.ReadLine();

   switch (choice)
   {
      case "1":
         AddBodyRecord(existingPerson, context);
         break;
      case "2":
         AddRunRecord(existingPerson, context);
         break;
      case "3":
         AddSleepRecord(existingPerson, context);
         break;
      case "4":
         exit = true;
         break;
      default:
         Console.WriteLine("Ungültige Auswahl. Bitte versuche es erneut.");
         break;
   }
}

static void AddBodyRecord(Person person, HealthTrackerContext context)
{
   DateTime date = PromptForDateTime("Gib das Datum der Messung ein (yyyy-mm-dd): ");
   float weight = PromptForFloat("Gib das Gewicht ein: ");
   float bmi = PromptForFloat("Gib den BMI ein: ");
   float bodyFat = PromptForFloat("Gib den Körperfettanteil ein: ");
   float fatlessBodyWeight = PromptForFloat("Gib das fettfreie Körpergewicht ein: ");
   float subcutaneousBodyFat = PromptForFloat("Gib das subkutane Körperfett ein: ");
   float visceralFat = PromptForFloat("Gib das viszerale Fett ein: ");
   float bodyWater = PromptForFloat("Gib den Körperwasseranteil ein: ");
   float skeletalMuscle = PromptForFloat("Gib die skelettale Muskelmasse ein: ");
   float muscleMass = PromptForFloat("Gib die Muskelmasse ein: ");
   float boneMass = PromptForFloat("Gib die Knochenmasse ein: ");
   float metabolicRate = PromptForInt("Gib die metabolische Rate ein: ");
   float metabolicAge = PromptForInt("Gib dein metabolische Alter ein: ");

   person.BodyRecords.Add(new BodyRecord
   {
      DateOfRecord = date,
      Bodyweight = weight,
      BMI = bmi,
      BodyFat = bodyFat,
      FatlessBodyWeight = fatlessBodyWeight,
      SubcutaneousBodyFat = subcutaneousBodyFat,
      VisceralFat = visceralFat,
      BodyWater = bodyWater,
      SkeletalMuscle = skeletalMuscle,
      MuscleMass = muscleMass,
      BoneMass = boneMass,
      MetabolicRate = metabolicRate,
      MetabolicAge = metabolicAge
   });

        context.SaveChanges();

        Console.WriteLine("Körpermessung hinzugefügt.");
}

static DateTime PromptForDateTime(string message)
{
    DateTime result;
    Console.Write(message);
    while (!DateTime.TryParse(Console.ReadLine(), out result))
    {
        Console.WriteLine("Ungültige Eingabe. Bitte versuche es erneut.");
        Console.Write(message);
    }
    return result;
}


static float PromptForFloat(string message)
{
    float result;
    Console.Write(message);
    while (!float.TryParse(Console.ReadLine(), out result))
    {
        Console.WriteLine("Ungültige Eingabe. Bitte gib eine Zahl ein.");
        Console.Write(message);
    }
    return result;
}


static int PromptForInt(string message)
{
    int result;
    Console.Write(message);
    while (!int.TryParse(Console.ReadLine(), out result))
    {
        Console.WriteLine("Ungültige Eingabe. Bitte gib eine Zahl ein.");
        Console.Write(message);
    }
    return result;
}

static void AddRunRecord(Person person, HealthTrackerContext context)
{
   
}

static void AddSleepRecord(Person person, HealthTrackerContext context)
{
   
}


// Adds a new BodyRecord to "Thomas" and saves changes to the database
// existingPerson.BodyRecords.Add(new BodyRecord {DateOfRecord = DateTime.Now, Bodyweight = 86.60f});
// context.SaveChanges();
// Console.WriteLine($"Ändergungen gespeichert");
// ShowBodyRecords(existingPerson);

// Updates the last BodyRecord created for "Thomas", saves changes, and displays all body records
// BodyRecord oldestRecord = existingPerson.BodyRecords.OrderBy(r => r.DateOfRecord).Last();
// oldestRecord.Bodyweight = 85.00f;
// context.SaveChanges();
// Console.WriteLine($"Ändergungen gespeichert");
// ShowBodyRecords(existingPerson);

// Optionally deletes the last BodyRecord for "Thomas" if any exist, saves changes, and displays the updated list
// if(existingPerson.BodyRecords.Any())
// {
//    context.BodyRecords.Remove(oldestRecord);
//    context.SaveChanges();
//    Console.WriteLine($"Ändergungen gespeichert");
//    ShowBodyRecords(existingPerson);
// }

// Helper method to display all BodyRecords for "Thomas"
// static void ShowBodyRecords(Person person)
// {
//    Console.WriteLine($"Alle Gewichtsmessungen für {person.Name}: ");
//    foreach (var record in person.BodyRecords)
//    {
//       Console.WriteLine(record.DateOfRecord + ": " + record.Bodyweight + "kg");
//    }
// }