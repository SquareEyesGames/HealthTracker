using System.Data.Common;

namespace Gold.HealthTracker.DTOModel;

// Definiert ein einfaches Objekt für die Übertragung von Personendaten
public class PersonDTO
{
    public int Id { get; set;}
    public string Name { get; set; } = string.Empty;
    
}

public class CreatePersonDTO
{
    public string Name { get; set; } = string.Empty;
}
