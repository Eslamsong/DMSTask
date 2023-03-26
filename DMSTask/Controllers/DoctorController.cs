using DMSTask.Models.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DMSTask.Controllers
{
    public class DoctorController : Controller
    {
        private readonly ClinicDbContext clinicDb;

        public DoctorController(ClinicDbContext clinicDb)
        {
            this.clinicDb = clinicDb;
        }
        [HttpGet]
        public IActionResult Index(int id)// entering the Doctor Page by Query string
        {
            var DbDoctorAppointment = clinicDb.Appointments.Include(i=>i.Doctor)
                                                       .Where(i=>i.DoctorId == id &&i.AppointmentDateTime.Date==DateTime.Today).ToList();
            ViewBag.id = id;
            return View(DbDoctorAppointment);
        }
        [HttpPost]
        public IActionResult Index(DateTime startDate ,DateTime endDate,int id) 
        {
            var DbFilteredAppointment = clinicDb.Appointments.Include(i => i.Doctor)
                                                        .Where(i => i.DoctorId == id && i.AppointmentDateTime.Date >= startDate.Date && i.AppointmentDateTime.Date <= endDate.Date).ToList();

            ViewBag.id = id;
            return View(DbFilteredAppointment);
        }
    }
}
