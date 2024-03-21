using Microsoft.EntityFrameworkCore;

namespace Gold.HealthTracker.DBModel;

// DbContext class for EF Core configuration and mapping
public class HealthTrackerContext : DbContext
{
    // DbSets represent tables in database
    public DbSet<Person> Persons => Set<Person>();      // = { get { return Set<Person>();} }
    public DbSet<BodyRecord> BodyRecords => Set<BodyRecord>();
    public DbSet<Run> Runs => Set<Run>();
    public DbSet<Workout> Workouts => Set<Workout>();
    public DbSet<SportSession> SportSessions => Set<SportSession>();
    public DbSet<NutritionRecord> NutritionRecords => Set<NutritionRecord>();
    public DbSet<SleepRecord> SleepRecords => Set<SleepRecord>();
    public DbSet<MentalRecord> MentalRecords => Set<MentalRecord>();

    public HealthTrackerContext()
        : base()
    {
        
    }

    public HealthTrackerContext(DbContextOptions options)
        : base(options)
    {
        
    }

    // Configures the DbContext with SQL Server and enables lazy loading proxies.
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseLazyLoadingProxies();     // Enables lazy loading for virtual navigation properties
    }
}
