using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Enums;

namespace TravelAgency.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Roles Role { get; set;}
        public string PathImg { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
