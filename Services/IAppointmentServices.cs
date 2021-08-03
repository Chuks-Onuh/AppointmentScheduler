using AppointmentScheduler.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentScheduler.Services
{
    public interface IAppointmentServices
    {
        public List<DoctorVm> GetDoctorList();
        public List<PatientVm> GetPatientList();
        public Task<int> AddUpdate(AppointmentVm model);

        public List<AppointmentVm> DoctorsEventsById(string doctorId);
        public List<AppointmentVm> PatientsEventsById(string patientId);

    }
}
