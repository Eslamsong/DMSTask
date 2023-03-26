using DMSTask.Models;
using DMSTask.Models.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DMSTask.Controllers
{
    public class SecretaryController : Controller
    {
        private readonly ClinicDbContext clinicDb;
        private readonly IToastNotification toastNotification;

        public SecretaryController(ClinicDbContext clinicDb,IToastNotification toastNotification)
        {
            this.clinicDb = clinicDb;
            this.toastNotification = toastNotification;
        }
        public IActionResult Index()
        {
            var Appointments = clinicDb.Appointments.Include(i => i.Doctor).ToList();


            return View(Appointments);
        }
        [HttpGet]
        public IActionResult Create() {


            var DoctList = clinicDb.Doctors.ToList();
            ViewBag.Doctors = new SelectList(DoctList, "Id", "DoctorName");

            return View();
        }
        [HttpPost]
        public IActionResult Create(Appointment appointment, TimeSpan TimeSlot)
        {
            if (appointment.DoctorId == 0)
            {
                ModelState.AddModelError("", "You have to Select a Doctor");
                var DoctList = clinicDb.Doctors.ToList();
                ViewBag.Doctors = new SelectList(DoctList, "Id", "DoctorName");
                return View();
            }


            string DoctorDayOff = clinicDb.Doctors.Where(i => i.Id == appointment.DoctorId).FirstOrDefault().DayOff;
            string selectedDay = appointment.AppointmentDateTime.DayOfWeek.ToString();
            if (selectedDay == DoctorDayOff)
            {
                toastNotification.AddInfoToastMessage("Please Choose another Day, The Doctor isn't working on this day ");
                return Redirect("create");

            }

            var CheckDate = appointment.AppointmentDateTime.Date;
            var IsAvailable = clinicDb.Appointments.Any(i => i.AppointmentDateTime.TimeOfDay == TimeSlot &&i.AppointmentDateTime.Date == CheckDate && i.DoctorId == appointment.DoctorId );
            if (IsAvailable)
            {
                toastNotification.AddInfoToastMessage("Please Choose another Time, This Appointment is Already reserved ");
                return Redirect("create");


            }

            var NewDate = appointment.AppointmentDateTime.Add(TimeSlot);
            appointment.AppointmentDateTime = NewDate;

            clinicDb.Appointments.Add(appointment);
            clinicDb.SaveChanges();

            toastNotification.AddSuccessToastMessage("Appointment has been created Successfully");

            return RedirectToAction("Index");
        }
    }
}
