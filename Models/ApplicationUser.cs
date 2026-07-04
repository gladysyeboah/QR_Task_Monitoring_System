using Microsoft.AspNetCore.Identity;

namespace QR_Field_Monitoring_System.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}