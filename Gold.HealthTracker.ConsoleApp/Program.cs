// See https://aka.ms/new-console-template for more information
using Gold.HealthTracker.DBModel;

HealthTrackerContext context = new();

context.Database.EnsureCreated();

var existingPerson = context.Persons.FirstOrDefault(p => p.Name == "Thomas");

if(existingPerson == null)
{
var person = new Person()
{
     Name = "Thomas",
     BodyRecordList = new List<BodyRecord>()
     {
        new BodyRecord() {Bodyweight = 87.45f}
     },
     RunList = new List<Run>()
     {
        new Run() {Distance = 7.78f, DateOfRecord = DateTime.Now}
     },
     NutritionRecordList = new List<NutritionRecord>()
     {
         new NutritionRecord() { CalorieIntake = 2750}
     },
     MentalRecordList = new List<MentalRecord>()
     {
        new MentalRecord() { StressLevel = 7 }
     }
};

Console.WriteLine($"Person mit id {person.Id} und name {person.Name} erstellt");

context.Persons.Add(person);

Console.WriteLine($"Person mit id {person.Id} und name {person.Name} hinzugefügt");

context.SaveChanges();

Console.WriteLine($"Person mit id {person.Id} und name {person.Name} gespeichert");
}
else
{
   Console.WriteLine($"Person mit id {existingPerson.Id} und Name {existingPerson.Name} existiert bereits in der Datenbank");
}

foreach(var pers in context.Persons)
    Console.WriteLine($"Person mit id {pers.Id} und name {pers.Name} ausgewählt.");