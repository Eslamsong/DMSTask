using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMSTask.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Type the Patient Name")]
        public string PatientName { get; set; } = null!;
        [Required(ErrorMessage = "Please Select The Birth Date of The Patient")]
        public DateTime PatientBirthDate { get; set; }
        [Required(ErrorMessage = "Please Select The Appointment Date")]
        public DateTime AppointmentDateTime { get; set; }
        //[Required]
        //public DateTime AppointmentTime { get; set; }
        [Required(ErrorMessage ="Please choose a doctor")]
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
    }
}
