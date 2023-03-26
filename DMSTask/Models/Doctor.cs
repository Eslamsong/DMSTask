using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMSTask.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        public string DoctorName { get; set; } = null!;

        public string DayOff { get; set; } = "Friday";

        public int From { get; set; } 
        public int To { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();

        [ForeignKey("Clinic")]
        public int ClinicId { get;set; }
        public Clinic Clinic { get; set; }=new Clinic();


    }
}
