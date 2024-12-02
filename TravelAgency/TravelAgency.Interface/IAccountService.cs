using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TestUsers.Interface.Models;
using TravelAgency.Interface.Models.RegAndLog;

namespace TravelAgency.Interface
{
    public interface IAccountService
    {
       public Task<BaseResponse<string>> Register(RegistrationUser registration);
       public Task<BaseResponse<ClaimsIdentity>> Login(LoginUser login);
        public Task<BaseResponse<ClaimsIdentity>> ConfirmEmail(RegistrationUser user, string code, string confirmcode);
    }
}
