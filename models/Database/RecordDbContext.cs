using System.Reflection;
using Microsoft.EntityFrameworkCore;
using CardExploration.models;

namespace CardExploration.DatabaseContext
{
    public class RecordDbContext : DbContext
    {
        public DbSet<TimeRecord> TimeRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=TimeRecord.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });

            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
            modelBuilder.Entity<TimeRecord>().ToTable("TimeRecords", "test");
            modelBuilder.Entity<TimeRecord>(entity =>
            {
                entity.HasKey(e => e.TimeRecordId);
                entity.Property(e => e.Time).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.TimeRecordId).ValueGeneratedOnAdd();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}