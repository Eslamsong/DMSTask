﻿using DMSTask.Models;
using DMSTask.Models.DBContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DMSTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelperController : ControllerBase
    {
        private readonly ClinicDbContext clinicDb;

        public HelperController(ClinicDbContext clinicDb)
        {
            this.clinicDb = clinicDb;
        }
        [HttpGet]
        public IActionResult GetTimeSlots(int id ,string SelectedDate)
        {
            var ConvertedDate =DateTime.Parse(SelectedDate);
            var Doctor = clinicDb.Doctors.Where(i => i.Id == id).FirstOrDefault();

            int strTime = Doctor.From;
            int enTime = Doctor.To;

            var startTime = new TimeSpan(strTime, 0, 0); 
            var endTime = new TimeSpan(enTime, 0, 0); 
            var timeSlots = new List<TimeSpan>();

            var ReserevedAppointments = clinicDb.Appointments.Where(i => i.DoctorId == id && i.AppointmentDateTime.Date == ConvertedDate.Date).ToList();

            while (startTime < endTime)
            {
                timeSlots.Add(startTime);
                startTime = startTime.Add(new TimeSpan(0, 30, 0)); 

            }

            foreach (var item in ReserevedAppointments)
            {
                timeSlots.Remove(item.AppointmentDateTime.TimeOfDay);
            }

            return new JsonResult(timeSlots);
        }
    }
}
