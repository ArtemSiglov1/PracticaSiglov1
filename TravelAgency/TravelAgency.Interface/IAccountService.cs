using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TestUsers.Interface.Models;
using TravelAgency.Interface.Models.RegAndLog;

namespace TravelAgency.Interface
{
    public interface IAccountService
    {
       public Task<ClaimsIdentity> Register(RegistrationUser registration);
       public Task<ClaimsIdentity> Login(LoginUser login);
    }
}
