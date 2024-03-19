namespace Gold.HealthTracker.ConsoleApp;

public class UserInput
{
    // Helper methods for prompting and validating DateTime type user input
public DateTime PromptForDateTime(string message)
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
public DateOnly PromptForDateOnly(string message, DateOnly minDate = default, DateOnly maxDate = default)
{
    // Sets minValue if not declared
    if (minDate == default)
    {
        minDate = new DateOnly(1900, 1, 1); // No dates before 1900
    }

    // Sets maxValue if not declared
    if (maxDate == default)
    {
        maxDate = DateOnly.FromDateTime(DateTime.Today); // No furute dates
    }

    DateOnly result;
    Console.Write(message);
    while (!DateOnly.TryParse(Console.ReadLine(), out result) || result < minDate || result > maxDate)
    {
        if (result < minDate)
        {
            Console.WriteLine($"Ungültige Eingabe. Das Datum muss nach dem {minDate:yyyy-MM-dd} liegen.");
        }
        else if (result > maxDate)
        {
            Console.WriteLine($"Ungültige Eingabe. Das Datum muss vor dem {maxDate:yyyy-MM-dd} liegen.");
        }
        else
        {
            Console.WriteLine("Ungültige Eingabe. Bitte versuche es erneut.");
        }
        Console.Write(message);
    }
    return result;
}

// Helper methods for prompting and validating float type user input
public float PromptForFloat(string message, float minValue, float maxValue)
{
   float result;
   Console.Write(message);
   while (!float.TryParse(Console.ReadLine(), out result) || result < minValue || result > maxValue)
   {
      Console.WriteLine("Ungültige Eingabe. Bitte gib eine Zahl ein.");
      Console.Write(message);
   }
   return result;
}

// Helper methods for prompting and validating int type user input
public int PromptForInt(string message, int minValue, int maxValue)
{
   int result;
   Console.Write(message);
   while (!int.TryParse(Console.ReadLine(), out result) || result < minValue || result > maxValue)
   {
      Console.WriteLine("Ungültige Eingabe. Bitte gib eine Zahl ein.");
      Console.Write(message);
   }
   return result;
}

public string PromptForString(string message)
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

public TimeSpan PromptForTimeSpan(string message)
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
}
