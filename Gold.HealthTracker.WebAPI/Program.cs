using Gold.HealthTracker.DBModel;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Fügt die Controller-Unterstützung hinzu
builder.Services.AddControllers();
// Konfiguriert Swagger/OpenAPI für die API-Dokumentation und Interaktion
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Konfiguriert den Datenbankkontext mit der Verbindungszeichenfolge aus der appsettings.json
builder.Services.AddDbContext<HealthTrackerContext>(o =>
    o.UseSqlServer("Name=ConnectionStrings:HealthTrackerDB")
);

var app = builder.Build();

// Configures the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Mapt Controller-Endpunkte
app.MapControllers();

app.Run();
