using AppointmentScheduler.Services;
using AppointmentScheduler.Utility;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentScheduler.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentServices _appointmentservice;

        public AppointmentController(IAppointmentServices appointmentServices)
        {
            _appointmentservice = appointmentServices;
        }
        public IActionResult Index()
        {
            ViewBag.Duration = Helper.GetTimeDropDown();
            ViewBag.DoctorList = _appointmentservice.GetDoctorList();
            ViewBag.PatientList = _appointmentservice.GetPatientList();
            return View();
        }
    }
}
