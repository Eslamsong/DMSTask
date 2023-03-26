using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace DMSTask.Models.DBContext
{
    public class ClinicDbContext : DbContext
    {
        public ClinicDbContext()
        {
            
        }
        public ClinicDbContext(DbContextOptions<ClinicDbContext> options):base(options)
        {

            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var connectionString = Configuration.GetConnectionString("DefaultConnection");

        //}
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Appointment>()
        //        .Property(e => e.AppointmentDate)
        //        .HasConversion(
        //            v => v.ToDateTime(TimeOnly.MinValue),
        //            v => DateOnly.FromDateTime(v)
        //        );
        //}
        public DbSet<Clinic> Clinic { get; set; } = null!;
        public DbSet<Doctor> Doctors { get;set; } = null!;
        public DbSet<Appointment> Appointments { get;set; } = null!;
    }
}
