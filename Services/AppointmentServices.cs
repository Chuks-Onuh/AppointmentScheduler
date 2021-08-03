using AppointmentScheduler.Data;
using AppointmentScheduler.Models;
using AppointmentScheduler.Utility;
using AppointmentScheduler.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<int> AddUpdate(AppointmentVm model)
        {
            var startDate = DateTime.Parse(model.StartDate);
            var endDate = DateTime.Parse(model.EndDate).AddMinutes(Convert.ToDouble(model.Duration));

            if (model == null && model.Id > 0)
            {
                // update
                return 1;
            }
            else
            {
                // create
                Appointment appointment = new Appointment()
                {
                    Title = model.Title,
                    Description = model.Description,
                    StartDate = startDate,
                    EndDate = endDate,
                    Duration = model.Duration,
                    DoctorId = model.DoctorId,
                    PatientId = model.PatientId,
                    IsDoctorApproved = false,
                    AdminId = model.AdminId
                };

                _ctx.Appointments.Add(appointment);
                await _ctx.SaveChangesAsync();
                return 2;
            }
        }

        public List<AppointmentVm> DoctorsEventsById(string doctorId)
        {
            return _ctx.Appointments.Where(x => x.DoctorId == doctorId).ToList()
                                    .Select(c => new AppointmentVm()
                                    {
                                        Id = c.Id,
                                        Description = c.Description,
                                        StartDate = c.StartDate.ToString("yyyy-MM-dd-HH:mm:ss"),
                                        EndDate = c.EndDate.ToString("yyyy-MM-dd-HH:mm:ss"),
                                        Duration = c.Duration,
                                        IsDoctorApproved = c.IsDoctorApproved
                                    }).ToList();
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

        public List<AppointmentVm> PatientsEventsById(string patientId)
        {
            return _ctx.Appointments.Where(x => x.PatientId == patientId).ToList()
                                    .Select(c => new AppointmentVm()
                                    {
                                        Id = c.Id,
                                        Description = c.Description,
                                        StartDate = c.StartDate.ToString("yyyy-MM-dd-HH:mm:ss"),
                                        EndDate = c.EndDate.ToString("yyyy-MM-dd-HH:mm:ss"),
                                        Duration = c.Duration,
                                        IsDoctorApproved = c.IsDoctorApproved
                                    }).ToList();
        }
    }
}
