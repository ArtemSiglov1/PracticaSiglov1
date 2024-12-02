using FluentValidation;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Interface.Models;
using TravelAgency.DAL;
using TravelAgency.Domain.Enums;
using TravelAgency.Domain.Helpers;
using TravelAgency.Domain.Models;
using TravelAgency.Interface;
using TravelAgency.Interface.Models.RegAndLog;
using TravelAgency.Service.Validators;

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
                var valid = new RegAndLogModel {LoginUser= login,RegistrationUser= null };
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
        public async Task SendEmail(string email, string confirmpas)
        {
            string path = @"D:\313-315RN\work\PracticaSiglov1\TravelAgency\password";
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Admin", "artemsiglov@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = "Welcome";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "<html>" + "<head>" + "<style>" +
                "body{font-family:Arial,sans-serif;background-color:#f2f2f2;}" +
                ".conteiner{max-width:600px;margin:0 auto;padding:20px;background-color:#fff;border-radius:10px;box-shadow:0px 0px 10px rgba(0,0,0,0.1);}" +
                ".header{text-aling:center;margin-bottom:20px;}" +
                ".message{font-size:16px;line-height:1.6;}" +
                ".container-code{background-color:f0f0f0;padding:5px;border-radius:5px;font-weight:bold;}" +
                ".code{text-align:center;}" +
                "</style>" +
                "</head>" +
                "<body>" +
                "<div class='container'>" +
                "<div class='header'><h1>Добро пожаловать на сайт книжного магазина!</h1></div>" +
                "<div class='message'>" +
                "<p>Пожалуйста, введите данный код на сайте, чтобы подтвердить ваш емаил и заверить регистрацию:</p>" +
                "<div class='container-code'><p class='code'>" + confirmpas + "</p></div>" +
                "</div>" + "</div>" + "</body>" + "</html>"
            };
            using var reader = new StreamReader(path);
            string password = await reader.ReadToEndAsync();
            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 465, true);
            await client.AuthenticateAsync("artemsiglov@gmail.com", password);
            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(true);
                }
        public async Task<BaseResponse<ClaimsIdentity>> ConfirmEmail(RegistrationUser model,string code,string confirmCode)
        {
            try
            {
                await using var db = new DataContext(dbcontextOptions);
                if (code != confirmCode)
                {
                    throw new Exception("Неверный код! Регистрация не выполнена");
                }
                model.CreatedAt = DateTime.UtcNow;
                model.Password = HashPasswordHelper.HashPassword(model.Password);
                model.PathImage = "/images/user.png";
                var user = new User()
                {
                    CreatedAt = model.CreatedAt,
                    Email = model.Email,
                    Login = model.Login,
                    Password = model.Password,
                    PathImg = model.PathImage,
                    Role = Roles.User
                };
                await db.AddAsync(user);
                await db.SaveChangesAsync();
                var res = AuthenticateUserHelper.Authentificate(user);
                return new BaseResponse<ClaimsIdentity>
                {
                    Data = res,
                    StatusCode = StatucCode.OK
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    Description=ex.Message,
                    StatusCode=StatucCode.InternalServerError
                };
            }
        }
            public async Task<BaseResponse<string>> Register(RegistrationUser registration)
            {
                try
                {
                Random random = new Random();
                string confCode = $"{random.Next(10)}{random.Next(10)}{random.Next(10)}{random.Next(10)}";
                    var valid = new RegAndLogModel { LoginUser = null, RegistrationUser = registration };
                    validationRules = new UserValidator(valid);
                    await validationRules.ValidateAndThrowAsync(valid);

                    await using var db = new DataContext(dbcontextOptions);
                    var query = db.Buyers.AsQueryable();
                    if (await query.Where(u => u.Email == registration.Email).AnyAsync())
                        return new BaseResponse<string>()
                        {
                            Description = "Пользователь с такой почтой уже есть"
                        };
                    registration.CreatedAt = DateTime.UtcNow;
                    registration.PathImage = "~/images/user.png";
                    if (registration.Password != registration.PasswordConfirm)
                    {
                        return new BaseResponse<string>()
                        {
                            Description = "Пароли не совпадают"
                        };
                    }
                    registration.Password = HashPasswordHelper.HashPassword(registration.Password);

                    var user = new User { Role = 0, CreatedAt = registration.CreatedAt, Email = registration.Email, Login = registration.Login, Password = registration.Password, PathImg = registration.PathImage };
                    await db.Buyers.AddAsync(user);
                    await db.SaveChangesAsync();
                    var res = AuthenticateUserHelper.Authentificate(user);

                    return new BaseResponse<string>()
                    {
                        Data = confCode,
                        Description = "Acc created",
                        StatusCode = StatucCode.OK
                    };
                }
                catch (ValidationException exception)
                {
                    var errorMessage = string.Join(";", exception.Errors.Select(e => e.ErrorMessage));
                    return new BaseResponse<string>()
                    {
                        Description = errorMessage,
                        StatusCode = StatucCode.BadRequest
                    };
                }

                catch (Exception e)
                {
                    return new BaseResponse<string>()
                    {
                        Description = e.Message,
                        StatusCode = StatucCode.InternalServerError
                    };
                }
            }
        }
    }

