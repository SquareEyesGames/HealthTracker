using Microsoft.EntityFrameworkCore;

namespace Gold.HealthTracker.DBModel;

public class HealthTrackerContext : DbContext
{
    public DbSet<Person> Persons => Set<Person>();      // { get { return Set<Person>();} }
    public DbSet<BodyRecord> BodyRecords => Set<BodyRecord>();
    public DbSet<Run> Runs => Set<Run>();
    public DbSet<Workout> Workouts => Set<Workout>();
    public DbSet<SportSession> SportSessions => Set<SportSession>();
    public DbSet<NutritionRecord> NutritionRecords => Set<NutritionRecord>();
    public DbSet<SleepRecord> SleepRecords => Set<SleepRecord>();
    public DbSet<MentalRecord> MentalRecords => Set<MentalRecord>();
}
