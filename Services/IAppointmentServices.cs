using AppointmentScheduler.ViewModels;
using System.Collections.Generic;

namespace AppointmentScheduler.Services
{
    public interface IAppointmentServices
    {
        public List<DoctorVm> GetDoctorList();
        public List<PatientVm> GetPatientList();
    }
}
