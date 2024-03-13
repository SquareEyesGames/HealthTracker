// See https://aka.ms/new-console-template for more information
using Gold.HealthTracker.DBModel;

Console.WriteLine("Hello, World!");

HealthTrackerContext context = new();

context.Database.EnsureCreated();

// var thomas = new Person(){
//     Name = "Goldie"
// };

// Console.WriteLine($"Person mit id {thomas.Id} und name {thomas.Name} erstellt");

// context.Persons.Add(thomas);

// Console.WriteLine($"Person mit id {thomas.Id} und name {thomas.Name} hinzugefügt");

// context.SaveChanges();

// Console.WriteLine($"Person mit id {thomas.Id} und name {thomas.Name} gespeichert");

foreach(var pers in context.Persons)
    Console.WriteLine($"Person mit id {pers.Id} und name {pers.Name} ausgewählt");