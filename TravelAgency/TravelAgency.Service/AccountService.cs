using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Interface.Models;
using TravelAgency.DAL;
using TravelAgency.Domain.Helpers;
using TravelAgency.Domain.Models;
using TravelAgency.Interface;
using TravelAgency.Interface.Models.RegAndLog;

namespace TravelAgency.Service
{
    public class AccountService : IAccountService
    {
        private DbContextOptions<DataContext> dbcontextOptions;
        public AccountService(DbContextOptions<DataContext> dbContextOptions)
        {
            dbcontextOptions = dbContextOptions;
        }
        public async Task<ClaimsIdentity> Login(LoginUser login)
        {
            if (string.IsNullOrWhiteSpace(login.Email))
              throw new Exception( "Вы не указали имайл" );
           
            await using var db = new DataContext(dbcontextOptions);
            var user = await db.Buyers.FirstOrDefaultAsync(x=>x.Email==login.Email);
           
            if (user == null)
               throw new Exception( "Пользователя с данной почтой не существует");
            login.Password = HashPasswordHelper.HashPassword(login.Password);

            if (user.Password != login.Password)
                throw new Exception( "Пароль указан не верно");
           
            var userIdentity = new User { Email = login.Email, Password = login.Password,Role=user.Role,Login=user.Login,PathImg=user.PathImg };
            var res = AuthenticateUserHelper.Authentificate(userIdentity);
            return res;
        }

        public async Task<ClaimsIdentity> Register(RegistrationUser registration)
        {

            await using var db = new DataContext(dbcontextOptions);
            var query = db.Buyers.AsQueryable();
            if (await query.Where(u => u.Email == registration.Email).AnyAsync())
                throw new Exception("Имайл уже занят");

            registration.CreatedAt = DateTime.UtcNow;
            registration.PathImage = "~/images/user.png";
            if (registration.Password != registration.PasswordConfirm)
            throw new Exception("Пароли не совпадают");
            registration.Password = HashPasswordHelper.HashPassword(registration.Password);

            var user = new User { Role = 0, CreatedAt = registration.CreatedAt, Email = registration.Email, Login = registration.Login, Password = registration.Password ,PathImg=registration.PathImage };
            await db.Buyers.AddAsync(user);
            await db.SaveChangesAsync();
            var res = AuthenticateUserHelper.Authentificate(user);

            return res;
        }
    }
}
