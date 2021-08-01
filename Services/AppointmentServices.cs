using AppointmentScheduler.Data;
using AppointmentScheduler.Utility;
using AppointmentScheduler.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace AppointmentScheduler.Services
{
    public class AppointmentServices : IAppointmentServices
    {
        private readonly AppDbContext _ctx;
        private readonly RoleManager<IdentityRole> _userrole;

        public AppointmentServices(AppDbContext context, RoleManager<IdentityRole> userRole)
        {
            _ctx = context;
            _userrole = userRole;

        }

        public List<DoctorVm> GetDoctorList()
        {
            var doctors = (from user in _ctx.Users
                           join userRoles in _ctx.UserRoles on user.Id equals userRoles.UserId
                           join roles in _ctx.Roles.Where(x => x.Name == Helper.Doctor) on userRoles.RoleId equals roles.Id
                           select new DoctorVm
                           {
                               Id = user.Id,
                               Name = user.Name
                           }
                           ).ToList();

            return doctors;
            //var role = _ctx.UserRoles.Select(x => x.RoleId == "6a2bd9ef-000f-4c14-bb11-7c37913636f0");
            //var doctorList = await _ctx.Users
            //                  .Select(x => new DoctorVm()
            //                  {
            //                      Id = x.Id,
            //                      Name = x.Name,
            //                      Role = role

            //                  }).ToListAsync();
            //return doctorList;

        }

        public List<PatientVm> GetPatientList()
        {
            var patient = (from user in _ctx.Users
                           join userRoles in _ctx.UserRoles on user.Id equals userRoles.UserId
                           join roles in _ctx.Roles.Where(x => x.Name == Helper.Patient) on userRoles.RoleId equals roles.Id
                           select new PatientVm
                           {
                               Id = user.Id,
                               Name = user.Name
                           }
                         ).ToList();

            return patient;
        }
    }
}
