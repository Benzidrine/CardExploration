using System.Reflection;
using Microsoft.EntityFrameworkCore;
using CardExploration.models;

namespace CardExploration.DatabaseContext
{
    public class RecordDbContext : DbContext
    {
        //Set table in entity framework
        public DbSet<timeRecord> TimeRecords { get; set; }

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
            modelBuilder.Entity<timeRecord>().ToTable("TimeRecords", "test");
            modelBuilder.Entity<timeRecord>(entity =>
            {
                entity.HasKey(e => e.TimeRecordId);
                entity.Property(e => e.Time).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.TimeRecordId).ValueGeneratedOnAdd();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}