using Gold.HealthTracker.ConsoleApp;
using Gold.HealthTracker.DBModel;
using Microsoft.EntityFrameworkCore;

HealthTrackerContext context = new();
DataEntry dataEntry = new(context);

// Ensures the database is created based on the current model
context.Database.EnsureCreated();

// Checks if there is already an existing Person named "Thomas" in the database
Person? existingPerson = context.Persons.FirstOrDefault(p => p.Name == "Thomas");

// If not, creates a new person named "Thomas" and example records
if(existingPerson == null)
{
   existingPerson = new Person() { Name = "Thomas"};
   Console.WriteLine($"Person mit Namen {existingPerson.Name} erstellt");
   context.Persons.Add(existingPerson);
   existingPerson.BodyRecords.Add(new BodyRecord
   {
      DateOfRecord = new DateOnly(2024,01,01),
      Bodyweight = 90.00f,
      BMI = 28.90f,
      BodyFat = 18.10f,
      FatlessBodyWeight = 73.34f,
      SubcutaneousBodyFat = 15.00f,
      VisceralFat = 11,
      BodyWater = 59.10f,
      SkeletalMuscle = 52.90f,
      MuscleMass = 77.80f,
      BoneMass = 4.10f,
      MetabolicRate = 1954,
      MetabolicAge = 39
   });
   existingPerson.Runs.Add(new Run
   {
      DateOfRecord = new DateTime(2024,01,01),
      EnduranceType = "GA1",
      Distance = 5.05f,
      TrainingTime = new TimeSpan(00,30,00),
      AverageSpeed = 7.10f,
      AverageHeartRate = 155,
      MaxHeartRate = 185,
      Altitude = 112,
      Cadence = 110,
      Weather = "sunny",
      RunningShoes = "New Balance",
      CaloriesBurned = 450,
      SessionRating = 6
   });
   existingPerson.Workouts.Add(new Workout
   {
      DateOfRecord = new DateTime(2024,01,01),
      TrainingLocation = "Keller",
      TrainingTime = TimeSpan.FromHours(1.5),
      AverageHeartRate = 130,
      MaxHeartRate = 160,
      CaloriesBurned = 500,
      SessionRating = 8,
      Exercises = new List<Exercise>
      {
         new Exercise
         {
            ExerciseName = "Bankdrücken",
            Sets = new List<Set>
            {
               new Set { Repetitions = 10, Weight = 60 },
               new Set { Repetitions = 8, Weight = 70 },
               new Set { Repetitions = 6, Weight = 80 }
            }
         },
         new Exercise
         {
            ExerciseName = "Crunches",
            Sets = new List<Set>
            {
               new Set { Repetitions = 30, Weight = 0 },
               new Set { Repetitions = 30, Weight = 0 },
            }
         }
      }
   });
   existingPerson.SportSessions.Add(new SportSession
   {
      DateOfRecord = new DateTime(2024, 01, 01),
      TrainingTime = new TimeSpan(01,30,00),
      Sport = "Squash",
      AverageHeartRate = 160,
      MaxHeartRate = 190,
      CaloriesBurned = 850,
      SessionRating = 9
   });
   existingPerson.SleepRecords.Add(new SleepRecord
   {
      DateOfRecord = new DateOnly(2024, 01, 02),
      Bedtime = new DateTime(2024, 01, 01, 23, 00, 00),
      WakeUpTime = new DateTime(2024, 01, 02, 07, 00, 00),
      SleepQuality = 8
   });
   existingPerson.NutritionRecords.Add(new NutritionRecord
   {
      DateOfRecord = new DateOnly(2024, 01, 04),
      CalorieIntake = 3500
   });
   existingPerson.MentalRecords.Add(new MentalRecord
   {
      DateOfRecord = new DateOnly(2024, 01, 01),
      StressLevel = 5,
      Mood = "Zufrieden"
   });
   Console.WriteLine($"Person mit Namen {existingPerson.Name} hinzugefügt und Id {existingPerson.Id} zugewiesen");
   context.SaveChanges();
}
else
{
   Console.WriteLine($"Person {existingPerson.Name} in der Datenbank gefunden");
}

// Main loop to choose which record type to add for "Thomas"
bool exit = false;
while(!exit)
{
   Console.WriteLine("\nWelchen Eintrag möchtest du hinzufügen?");
   Console.WriteLine("1: Körpermessung");
   Console.WriteLine("2: Lauftraining");
   Console.WriteLine("3: Krafttraining");
   Console.WriteLine("4: Allgemeines Training");
   Console.WriteLine("5: Schlafprotokoll");
   Console.WriteLine("6: Ernährungsprotokoll");
   Console.WriteLine("7: Psychoanamnese");
   Console.WriteLine("8: Exit");

   string? choice = Console.ReadLine();

   switch (choice)
   {
      case "1":
         dataEntry.AddBodyRecord(existingPerson);
         break;
      case "2":
         dataEntry.AddRun(existingPerson);
         break;
      case "3":
         dataEntry.AddWorkout(existingPerson);
         break;
      case "4":
         dataEntry.AddSportSession(existingPerson);
         break;
      case "5":
         dataEntry.AddSleepRecord(existingPerson);
         break;
      case "6":
         dataEntry.AddNutritionRecord(existingPerson);
         break;
      case "7":
         dataEntry.AddMentalRecord(existingPerson);
         break;
      case "8":
         exit = true;
         break;
      default:
         Console.WriteLine("Ungültige Auswahl. Bitte versuche es erneut.");
         break;
   }
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



// [Key]: Gibt an, dass eine Eigenschaft der Primärschlüssel der Tabelle ist.
// [Table("TableName")]: Legt den Namen der Tabelle fest, zu der die Entität gemappt werden soll.
// [Column("ColumnName")]: Definiert den Namen der Spalte, zu der eine Eigenschaft gemappt wird.
// [Required]: Markiert eine Eigenschaft als notwendig, sodass EF eine NOT NULL-Bedingung in der Datenbanktabelle erzeugt.
// [StringLength(255)]: Gibt die maximale Länge eines String-Felds an und kann genutzt werden, um die VARCHAR-Länge in der Datenbank zu beschränken.
// [ForeignKey("AnotherEntityId")]: Definiert eine Fremdschlüsselbeziehung zu einer anderen Entität.
// [Index(IsUnique = true)]: Wird verwendet, um einen Index für eine oder mehrere Spalten in der Datenbank zu erstellen. Die IsUnique-Eigenschaft kann festgelegt werden, um einen eindeutigen Index zu erzwingen.
// [ConcurrencyCheck]: Markiert eine Eigenschaft für Optimistic Concurrency Checks, um sicherzustellen, dass die Daten nicht von einem anderen Prozess geändert wurden, bevor die aktuelle Transaktion abgeschlossen ist.