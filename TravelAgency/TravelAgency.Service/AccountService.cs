using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Interface.Models;
using TravelAgency.DAL;
using TravelAgency.Domain.Enums;
using TravelAgency.Domain.Helpers;
using TravelAgency.Domain.Models;
using TravelAgency.Domain.Validators;
using TravelAgency.Domain.ViewModels;
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
        private UserValidator validationRules { get; set; }
        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginUser login)
        {
            try
            {
                var validations = new LoginViewModel() { Email = login.Email, Password = login.Password };
                var valid = new LoginRegistrView {Login= validations,Register= null };
                validationRules = new UserValidator(valid);
                await validationRules.ValidateAndThrowAsync(valid);
                await using var db = new DataContext(dbcontextOptions);
                var user = await db.Buyers.FirstOrDefaultAsync(x => x.Email == login.Email);

                if (user == null)
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь не найден"
                    };
                login.Password = HashPasswordHelper.HashPassword(login.Password);

                if (user.Password != login.Password)
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Неверный пароль или почта"
                    };
                var userIdentity = new User { Email = login.Email, Password = login.Password, Role = user.Role, Login = user.Login, PathImg = user.PathImg };
                var res = AuthenticateUserHelper.Authentificate(userIdentity);
                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = res,
                    StatusCode = StatucCode.OK
                };
            }
            catch (ValidationException exception)
            {
                var errorMessage = string.Join(";", exception.Errors.Select(e => e.ErrorMessage));
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = errorMessage,
                    StatusCode = StatucCode.BadRequest
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatucCode.InternalServerError
                };
            }
        }
            public async Task<BaseResponse<ClaimsIdentity>> Register(RegistrationUser registration)
            {
                try
                {
                    var validations = new RegisterViewModel() { Email = registration.Email, Password = registration.Password, Login = registration.Login, PasswordConfirm = registration.PasswordConfirm };
                    var valid = new LoginRegistrView { Login = null, Register = validations };
                    validationRules = new UserValidator(valid);
                    await validationRules.ValidateAndThrowAsync(valid);

                    await using var db = new DataContext(dbcontextOptions);
                    var query = db.Buyers.AsQueryable();
                    if (await query.Where(u => u.Email == registration.Email).AnyAsync())
                        return new BaseResponse<ClaimsIdentity>()
                        {
                            Description = "Пользователь с такой почтой уже есть"
                        };
                    registration.CreatedAt = DateTime.UtcNow;
                    registration.PathImage = "~/images/user.png";
                    if (registration.Password != registration.PasswordConfirm)
                    {
                        return new BaseResponse<ClaimsIdentity>()
                        {
                            Description = "Пользователь с такой почтой уже есть"
                        };
                    }
                    registration.Password = HashPasswordHelper.HashPassword(registration.Password);

                    var user = new User { Role = 0, CreatedAt = registration.CreatedAt, Email = registration.Email, Login = registration.Login, Password = registration.Password, PathImg = registration.PathImage };
                    await db.Buyers.AddAsync(user);
                    await db.SaveChangesAsync();
                    var res = AuthenticateUserHelper.Authentificate(user);

                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Data = res,
                        Description = "Acc created",
                        StatusCode = StatucCode.OK
                    };
                }
                catch (ValidationException exception)
                {
                    var errorMessage = string.Join(";", exception.Errors.Select(e => e.ErrorMessage));
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = errorMessage,
                        StatusCode = StatucCode.BadRequest
                    };
                }

                catch (Exception e)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = e.Message,
                        StatusCode = StatucCode.InternalServerError
                    };
                }
            }
        }
    }

