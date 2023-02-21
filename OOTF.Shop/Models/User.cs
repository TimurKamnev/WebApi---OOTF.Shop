using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Security.Claims;

namespace OOTF.Shopping.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public UserRole Role { get; set; }
    }
    public enum UserRole
    {
        Admin,
        Manager,
        Salesman
    }
}

