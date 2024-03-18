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

// Main loop to choose which record type to add for "Thomas"
bool exit = false;
while(!exit)
{
   Console.WriteLine("\nWelchen Eintrag möchtest du hinzufügen?");
   Console.WriteLine("1: Körpermessung");
   Console.WriteLine("2: Lauftraining");
   Console.WriteLine("3: Krafttraining");
   Console.WriteLine("4: Schlafmessung");
   Console.WriteLine("5: Exit");
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
         AddWorkoutRecord(existingPerson, context);
         break;
      case "4":
         AddSleepRecord(existingPerson, context);
         break;
      case "5":
         exit = true;
         break;
      default:
         Console.WriteLine("Ungültige Auswahl. Bitte versuche es erneut.");
         break;
   }
}

// Method to add a body measurement record for a person
static void AddBodyRecord(Person person, HealthTrackerContext context)
{
   // Prompt the user for each measurement, validating and parsing the input
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
   int metabolicRate = PromptForInt("Gib die metabolische Rate ein: ");
   int metabolicAge = PromptForInt("Gib dein metabolische Alter ein: ");

   // Add the new BodyRecord to the person's collection and save changes to the database
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

static void AddRunRecord(Person person, HealthTrackerContext context)
{
   string enduranceType = PromptForString("Gib den Ausdauertyp ein (z.B. Grundlagenausdauer, Kraftausdauer): ");
   float distance = PromptForFloat("Gib die Distanz in km ein: ");
   float averageSpeed = PromptForFloat("Gib die durchschnittliche Geschwindigkeit in km/h ein: ");
   int altitude = PromptForInt("Gib die erklommene Höhe in Metern ein: ");
   int cadence = PromptForInt("Gib die Kadenz (Schritte pro Minute) ein: ");
   string weather = PromptForString("Beschreibe das Wetter während des Laufs: ");
   string runningShoes = PromptForString("Gib die Laufschuhe ein: ");

   person.Runs.Add(new Run
   {
      DateOfRecord = DateTime.Now, // Für einheitliche Datumsangaben nutzen wir hier das aktuelle Datum
      EnduranceType = enduranceType,
      Distance = distance,
      AverageSpeed = averageSpeed,
      Altitude = altitude,
      Cadence = cadence,
      Weather = weather,
      RunningShoes = runningShoes
   });

   context.SaveChanges();
   Console.WriteLine("Lauftraining hinzugefügt.");
}

static void AddWorkoutRecord(Person person, HealthTrackerContext context)
{
    string trainingLocation = PromptForString("Gib den Trainingsort ein: ");
    var workout = new Workout { TrainingLocation = trainingLocation, DateOfRecord = DateTime.Now };

    bool addingExercises = true;
    while (addingExercises)
    {
        string exerciseName = PromptForString("Gib den Namen der Übung ein: ");
        Exercise exercise = new Exercise { ExerciseName = exerciseName };
        
        workout.Exercises.Add(exercise);

        bool addingSets = true;
        while (addingSets)
        {
            int reps = PromptForInt("Anzahl der Wiederholungen: ");
            float weight = PromptForFloat("Gewicht in kg: ");

            Set set = new Set { Repetitions = reps, Weight = weight };
            exercise.Sets.Add(set);

            string addAnotherSet = PromptForString("Möchtest du einen weiteren Satz hinzufügen? (j/n): ");
            addingSets = addAnotherSet.ToLower() == "j";
        }

        string addAnotherExercise = PromptForString("Möchtest du eine weitere Übung hinzufügen? (j/n): ");
        addingExercises = addAnotherExercise.ToLower() == "j";
    }

    person.Workouts.Add(workout);
    context.SaveChanges();
    Console.WriteLine("Workout-Datensatz hinzugefügt.");
}

static void AddSleepRecord(Person person, HealthTrackerContext context)
{
   DateTime dateOfRecord = PromptForDateTime("Gib das Datum der Messung ein (yyyy-mm-dd): ");
   DateTime bedtime = PromptForDateTime("Gib die Bettzeit ein (yyyy-mm-dd HH:mm): ");
   DateTime wakeUpTime = PromptForDateTime("Gib die Aufwachzeit ein (yyyy-mm-dd HH:mm): ");
   int sleepQuality = PromptForInt("Bewerte die Schlafqualität (1-10): ");

   // Berechnung der tatsächlichen Schlafdauer
   TimeSpan timeAsleep = wakeUpTime - bedtime;

   person.SleepRecords.Add(new SleepRecord
   {
      DateOfRecord = dateOfRecord,
      Bedtime = bedtime,
      WakeUpTime = wakeUpTime,
      TimeAsleep = timeAsleep,
      SleepQuality = sleepQuality
   });

   context.SaveChanges();
   Console.WriteLine("Schlafmessung hinzugefügt.");
}


// Helper methods for prompting and validating DateTime type user input
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

// Helper methods for prompting and validating float type user input
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

// Helper methods for prompting and validating int type user input
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

static string PromptForString(string message)
{
   Console.Write(message);
   string? input;
   while (string.IsNullOrEmpty(input = Console.ReadLine()))
   {
      Console.WriteLine("Ungültige Eingabe. Bitte versuche es erneut.");
      Console.Write(message);
   }
   return input;
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