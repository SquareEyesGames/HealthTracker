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
   Console.WriteLine("4: Allgemeines Training");
   Console.WriteLine("5: Schlafprotokoll");
   Console.WriteLine("6: Ernährungsprotokoll");
   Console.WriteLine("7: Psychoanamnese");
   Console.WriteLine("8: Exit");

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
         AddSportSessionRecord(existingPerson, context);
         break;
      case "5":
         AddSleepRecord(existingPerson, context);
         break;
      case "6":
         AddNutritionRecord(existingPerson, context);
         break;
      case "7":
         AddMentalRecord(existingPerson, context);
         break;
      case "8":
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
   DateOnly dateOfRecord = PromptForDateOnly("Datum der Körpermessung (yyyy-mm-dd): ");
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
   BodyRecord bodyRecord = new BodyRecord
   {
      DateOfRecord = dateOfRecord,
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
   };

      person.BodyRecords.Add(bodyRecord);
      context.SaveChanges();
      Console.WriteLine("Körpermessung hinzugefügt.");
}

static void AddRunRecord(Person person, HealthTrackerContext context)
{
   DateTime dateOfTraining = PromptForDateTime("Gib das Datum der Trainingseinheit ein (yyyy-mm-dd): ");
   string enduranceType = PromptForString("Gib den Ausdauertyp ein (z.B. Grundlagenausdauer, Kraftausdauer): ");
   TimeSpan trainingTime = PromptForTimeSpan("Gib die Dauer deiner Laufeinheit ein (HH:mm): ");
   float distance = PromptForFloat("Gib die Distanz in km ein: ");
   int averageHeartRate = PromptForInt("Gib deine durchschnittliche Herzfrequenz ein: ");
   float averageSpeed = PromptForFloat("Gib deine durchschnittliche Geschwindigkeit in km/h ein: ");
   int maxHeartRate = PromptForInt("Gib deine maximale Herzfrequenz ein: ");
   int altitude = PromptForInt("Gib die erklommene Höhe in Metern ein: ");
   int cadence = PromptForInt("Gib die Kadenz (Schritte pro Minute) ein: ");
   int caloriesBurned = PromptForInt("Gib die verbrannten Kalorien ein: ");
   string weather = PromptForString("Beschreibe das Wetter während des Laufs: ");
   string runningShoes = PromptForString("Gib die Laufschuhe ein: ");
   int sessionRating = PromptForInt("Bewerte die Trainingssession (1-10): ");

   Run run = new Run
   {
      DateOfRecord = dateOfTraining,
      TrainingTime = trainingTime,
      AverageHeartRate = averageHeartRate,
      MaxHeartRate = maxHeartRate,
      CaloriesBurned = caloriesBurned,
      SessionRating = sessionRating,
      EnduranceType = enduranceType,
      Distance = distance,
      AverageSpeed = averageSpeed,
      Altitude = altitude,
      Cadence = cadence,
      Weather = weather,
      RunningShoes = runningShoes
   };

   person.Runs.Add(run);
   context.SaveChanges();
   Console.WriteLine("Lauftraining hinzugefügt.");
}

static void AddWorkoutRecord(Person person, HealthTrackerContext context)
{
   DateTime dateOfTraining = PromptForDateTime("Gib das Datum der Trainingseinheit ein (yyyy-mm-dd): ");
   string trainingLocation = PromptForString("Gib den Trainingsort ein: ");
   TimeSpan trainingTime = PromptForTimeSpan("Gib die Trainingsdauer ein (HH:mm): ");
   int averageHeartRate = PromptForInt("Gib die durchschnittliche Herzfrequenz ein: ");
   int maxHeartRate = PromptForInt("Gib die maximale Herzfrequenz ein: ");
   int caloriesBurned = PromptForInt("Gib die verbrannten Kalorien ein: ");
   int sessionRating = PromptForInt("Bewerte die Trainingssession (1-10): ");
   
   Workout workout = new Workout 
   { 
      DateOfRecord = dateOfTraining,
      TrainingLocation = trainingLocation,
      TrainingTime = trainingTime,
      AverageHeartRate = averageHeartRate,
      MaxHeartRate = maxHeartRate,
      CaloriesBurned = caloriesBurned,
      SessionRating = sessionRating
   };

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
   Console.WriteLine("Workout hinzugefügt.");
}

static void AddSportSessionRecord(Person person, HealthTrackerContext context)
{
   DateTime dateOfTraining = PromptForDateTime("Gib das Datum der Trainingseinheit ein (yyyy-mm-dd): ");
   string sport = PromptForString("Gib die Sportart ein: ");
   TimeSpan trainingTime = PromptForTimeSpan("Gib die Trainingsdauer ein (HH:mm): ");
   int averageHeartRate = PromptForInt("Gib deine durchschnittliche Herzfrequenz ein: ");
   int maxHeartRate = PromptForInt("Gib deine maximale Herzfrequenz ein: ");
   int caloriesBurned = PromptForInt("Gib die verbrannten Kalorien ein: ");
   int sessionRating = PromptForInt("Bewerte die Trainingssession (1-10): ");

   SportSession sportSession = new SportSession
   {
      DateOfRecord = dateOfTraining,
      Sport = sport,
      TrainingTime = trainingTime,
      AverageHeartRate = averageHeartRate,
      MaxHeartRate = maxHeartRate,
      CaloriesBurned = caloriesBurned,
      SessionRating = sessionRating,
   };

   person.SportSessions.Add(sportSession);
   context.SaveChanges();
   Console.WriteLine("Sportsession hinzugefügt.");
}

static void AddSleepRecord(Person person, HealthTrackerContext context)
{
   DateOnly dateOfRecord = PromptForDateOnly("Gib das Datum der Messung ein (yyyy-mm-dd): ");
   DateTime bedtime = PromptForDateTime("Gib die Bettzeit ein (yyyy-mm-dd HH:mm): ");
   DateTime wakeUpTime = PromptForDateTime("Gib die Aufwachzeit ein (yyyy-mm-dd HH:mm): ");
   int sleepQuality = PromptForInt("Bewerte die Schlafqualität (1-10): ");

   TimeSpan timeAsleep = wakeUpTime - bedtime;

   SleepRecord sleepRecord = new SleepRecord
   {
      DateOfRecord = dateOfRecord,
      Bedtime = bedtime,
      WakeUpTime = wakeUpTime,
      TimeAsleep = timeAsleep,
      SleepQuality = sleepQuality
   };

   person.SleepRecords.Add(sleepRecord);
   context.SaveChanges();
   Console.WriteLine("Schlafprotokoll hinzugefügt.");
}

static void AddNutritionRecord (Person person, HealthTrackerContext context)
{
   DateOnly dateOfRecord = PromptForDateOnly("Gebe das Datum an für, den das Ernährungsprotokoll erstellt werden soll (yyyy-mm-dd)");
   int calorieIntake = PromptForInt("Füge deine Gesamtkalorienzufuhr hinzu: ");

   NutritionRecord nutritionRecord = new NutritionRecord
   {
      DateOfRecord = dateOfRecord,
      CalorieIntake = calorieIntake
   };

   person.NutritionRecords.Add(nutritionRecord);
   context.SaveChanges();
   Console.WriteLine("Ernährungsprotokoll hinzugefügt.");
}

static void AddMentalRecord (Person person, HealthTrackerContext context)
{
   DateOnly dateOfRecord = PromptForDateOnly("Gebe das Datum an für, den die Psychoanamnese erstellt werden soll (yyyy-mm-dd)");
   int stressLevel = PromptForInt("Bewerte dein Stresslevel (1-10)");
   string mood = PromptForString("Wie fühlst du dich?");

   MentalRecord mentalRecord = new()
   {
      DateOfRecord = dateOfRecord,
      StressLevel = stressLevel,
      Mood = mood
   };

   person.MentalRecords.Add(mentalRecord);
   context.SaveChanges();
   Console.WriteLine("Psychoanamnese hinzugefügt");
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

// Helper methods for prompting and validating DateOnly type user input
static DateOnly PromptForDateOnly(string message)
{
   DateOnly result;
   Console.WriteLine(message);
   while(!DateOnly.TryParse(Console.ReadLine(), out result))
   {
      Console.WriteLine("Ungültige Eingabe. Bitte versuche es erneut.");
      Console.WriteLine(message);
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

static TimeSpan PromptForTimeSpan(string message)
{
   Console.Write(message);
   TimeSpan result;
   while (!TimeSpan.TryParseExact(Console.ReadLine(), "hh\\:mm", null, out result))
   {
      Console.WriteLine("Ungültige Eingabe. Bitte im Format HH:mm eingeben.");
      Console.Write(message);
   }
   return result;
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