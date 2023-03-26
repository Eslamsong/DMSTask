using System.ComponentModel.DataAnnotations;

namespace DMSTask.Models
{
    public class Clinic
    {
        [Key]
        public int Id { get; set; }
        public string ClinicName { get; set; } = null!;

        public ICollection<Doctor> Doctors { get; set; } =new HashSet<Doctor>();    

    }
}
