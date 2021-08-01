using Microsoft.AspNetCore.Identity;

namespace AppointmentScheduler.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
    }
}

