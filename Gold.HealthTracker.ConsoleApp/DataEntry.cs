using Gold.HealthTracker.DBModel;

namespace Gold.HealthTracker.ConsoleApp;

public class DataEntry
{
    private readonly HealthTrackerContext _context;
    private readonly UserInput userInput = new();

    public DataEntry(HealthTrackerContext context)
    {
        _context = context;
    }

    // Method to add a body measurement record for a person
public void AddBodyRecord(Person person)
{
   // Prompt the user for each measurement, validating and parsing the input
   DateOnly dateOfRecord = userInput.PromptForDateOnly("Datum der Körpermessung (yyyy-mm-dd): ");
   float weight = userInput.PromptForFloat("Gib das Gewicht ein: ", 0f, 300f);
   float bmi = userInput.PromptForFloat("Gib den BMI ein: ", 0f, 100f);
   float bodyFat = userInput.PromptForFloat("Gib den Körperfettanteil ein: ", 0f, 100f);
   float fatlessBodyWeight = userInput.PromptForFloat("Gib das fettfreie Körpergewicht ein: ", 0f, 100f);
   float subcutaneousBodyFat = userInput.PromptForFloat("Gib das subkutane Körperfett ein: ", 0f, 100f);
   float visceralFat = userInput.PromptForFloat("Gib das viszerale Fett ein: ", 0f, 100f);
   float bodyWater = userInput.PromptForFloat("Gib den Körperwasseranteil ein: ", 0f, 100f);
   float skeletalMuscle = userInput.PromptForFloat("Gib die skelettale Muskelmasse ein: ", 0f, 100f);
   float muscleMass = userInput.PromptForFloat("Gib die Muskelmasse ein: ", 0f, 100f);
   float boneMass = userInput.PromptForFloat("Gib die Knochenmasse ein: ", 0f, 100f);
   int metabolicRate = userInput.PromptForInt("Gib deinen Grundumsatz ein: ", 0, 10000);
   int metabolicAge = userInput.PromptForInt("Gib dein metabolische Alter ein: ", 0, 200);

   // Add the new BodyRecord to the person's collection and save changes to the database
   BodyRecord bodyRecord = new()
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
      _context.SaveChanges();
      Console.WriteLine("Körpermessung hinzugefügt.");
}

public void AddRun(Person person)
{
   DateTime dateOfTraining = userInput.PromptForDateTime("Gib das Datum der Trainingseinheit ein (yyyy-mm-dd): ");
   string enduranceType = userInput.PromptForString("Gib den Ausdauertyp ein (z.B. Grundlagenausdauer, Kraftausdauer): ");
   TimeSpan trainingTime = userInput.PromptForTimeSpan("Gib die Dauer deiner Laufeinheit ein (HH:mm): ");
   float distance = userInput.PromptForFloat("Gib die Distanz in km ein: ", 0f, 1000f);
   float averageSpeed = userInput.PromptForFloat("Gib deine durchschnittliche Geschwindigkeit in min/km ein: ", 0, 1000);
   int averageHeartRate = userInput.PromptForInt("Gib deine durchschnittliche Herzfrequenz ein: ", 0, 250);
   int maxHeartRate = userInput.PromptForInt("Gib deine maximale Herzfrequenz ein: ", 0, 250);
   int altitude = userInput.PromptForInt("Gib die erklommene Höhe in Metern ein: ", 0, 10000);
   int cadence = userInput.PromptForInt("Gib die Kadenz (Schritte pro Minute) ein: ", 0, 1000);
   int caloriesBurned = userInput.PromptForInt("Gib die verbrannten Kalorien ein: ", 0, 100000);
   string weather = userInput.PromptForString("Beschreibe das Wetter während des Laufs: ");
   string runningShoes = userInput.PromptForString("Gib die Laufschuhe ein: ");
   int sessionRating = userInput.PromptForInt("Bewerte die Trainingssession (1-10): ", 0, 10);

   Run run = new()
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
   _context.SaveChanges();
   Console.WriteLine("Lauftraining hinzugefügt.");
}

public void AddWorkout(Person person)
{
   DateTime dateOfTraining = userInput.PromptForDateTime("Gib das Datum der Trainingseinheit ein (yyyy-mm-dd): ");
   string trainingLocation = userInput.PromptForString("Gib den Trainingsort ein: ");
   TimeSpan trainingTime = userInput.PromptForTimeSpan("Gib die Trainingsdauer ein (HH:mm): ");
   int averageHeartRate = userInput.PromptForInt("Gib die durchschnittliche Herzfrequenz ein: ", 0, 250);
   int maxHeartRate = userInput.PromptForInt("Gib die maximale Herzfrequenz ein: ", 0, 250);
   int caloriesBurned = userInput.PromptForInt("Gib die verbrannten Kalorien ein: ", 0, 100000);
   int sessionRating = userInput.PromptForInt("Bewerte die Trainingssession (1-10): ", 0, 10);
   
   Workout workout = new()
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
      string exerciseName = userInput.PromptForString("Gib den Namen der Übung ein: ");
      Exercise exercise = new() { ExerciseName = exerciseName };
      
      workout.Exercises.Add(exercise);

      bool addingSets = true;
      while (addingSets)
      {
         float reps = userInput.PromptForFloat("Anzahl der Wiederholungen: ", 0f, 1000f);
         float weight = userInput.PromptForFloat("Gewicht in kg: ", 0, 1000);

         Set set = new() { Repetitions = reps, Weight = weight };
         exercise.Sets.Add(set);

         string addAnotherSet = userInput.PromptForString("Möchtest du einen weiteren Satz hinzufügen? (j/n): ");
         addingSets = addAnotherSet.ToLower() == "j";
      }

      string addAnotherExercise = userInput.PromptForString("Möchtest du eine weitere Übung hinzufügen? (j/n): ");
      addingExercises = addAnotherExercise.ToLower() == "j";
   }

   person.Workouts.Add(workout);
   _context.SaveChanges();
   Console.WriteLine("Workout hinzugefügt.");
}

public void AddSportSession(Person person)
{
   DateTime dateOfTraining = userInput.PromptForDateTime("Gib das Datum der Trainingseinheit ein (yyyy-mm-dd): ");
   string sport = userInput.PromptForString("Gib die Sportart ein: ");
   TimeSpan trainingTime = userInput.PromptForTimeSpan("Gib die Trainingsdauer ein (HH:mm): ");
   int averageHeartRate = userInput.PromptForInt("Gib deine durchschnittliche Herzfrequenz ein: ", 0, 250);
   int maxHeartRate = userInput.PromptForInt("Gib deine maximale Herzfrequenz ein: ", 0, 250);
   int caloriesBurned = userInput.PromptForInt("Gib die verbrannten Kalorien ein: ", 0, 100000);
   int sessionRating = userInput.PromptForInt("Bewerte die Trainingssession (1-10): ", 0, 10);

   SportSession sportSession = new()
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
   _context.SaveChanges();
   Console.WriteLine("Sportsession hinzugefügt.");
}

public void AddSleepRecord(Person person)
{
   DateOnly dateOfRecord = userInput.PromptForDateOnly("Gib das Datum der Messung ein (yyyy-mm-dd): ");
   DateTime bedtime = userInput.PromptForDateTime("Gib die Bettzeit ein (yyyy-mm-dd HH:mm): ");
   DateTime wakeUpTime = userInput.PromptForDateTime("Gib die Aufwachzeit ein (yyyy-mm-dd HH:mm): ");
   int sleepQuality = userInput.PromptForInt("Bewerte die Schlafqualität (1-10): ", 0, 10);

   TimeSpan timeAsleep = wakeUpTime - bedtime;

   SleepRecord sleepRecord = new()
   {
      DateOfRecord = dateOfRecord,
      Bedtime = bedtime,
      WakeUpTime = wakeUpTime,
      TimeAsleep = timeAsleep,
      SleepQuality = sleepQuality
   };

   person.SleepRecords.Add(sleepRecord);
   _context.SaveChanges();
   Console.WriteLine("Schlafprotokoll hinzugefügt.");
}

public void AddNutritionRecord (Person person)
{
   DateOnly dateOfRecord = userInput.PromptForDateOnly("Gebe das Datum an für, den das Ernährungsprotokoll erstellt werden soll (yyyy-mm-dd)");
   int calorieIntake = userInput.PromptForInt("Füge deine Gesamtkalorienzufuhr hinzu: ", 0, 100000);

   NutritionRecord nutritionRecord = new()
   {
      DateOfRecord = dateOfRecord,
      CalorieIntake = calorieIntake
   };

   person.NutritionRecords.Add(nutritionRecord);
   _context.SaveChanges();
   Console.WriteLine("Ernährungsprotokoll hinzugefügt.");
}

public void AddMentalRecord (Person person)
{
   DateOnly dateOfRecord = userInput.PromptForDateOnly("Gebe das Datum an für, den die Psychoanamnese erstellt werden soll (yyyy-mm-dd)");
   int stressLevel = userInput.PromptForInt("Bewerte dein Stresslevel (1-10)", 0, 10);
   string mood = userInput.PromptForString("Wie fühlst du dich?");

   MentalRecord mentalRecord = new()
   {
      DateOfRecord = dateOfRecord,
      StressLevel = stressLevel,
      Mood = mood
   };

   person.MentalRecords.Add(mentalRecord);
   _context.SaveChanges();
   Console.WriteLine("Psychoanamnese hinzugefügt");
}
}
