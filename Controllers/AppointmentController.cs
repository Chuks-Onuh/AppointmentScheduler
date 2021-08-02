using AppointmentScheduler.Services;
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
            ViewBag.Doctor = _appointmentservice.GetDoctorList();
            return View();
        }
    }
}
