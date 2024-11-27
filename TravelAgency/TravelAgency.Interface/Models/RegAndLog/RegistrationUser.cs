using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Interface.Models.RegAndLog
{
   public class RegistrationUser
    {
        public string PathImage { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
