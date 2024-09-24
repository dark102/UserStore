using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserStore.Models;

namespace UserStore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetUserById(Guid id)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                return NotFound($"Пользователь с Id {id} не найден");
            }
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody]User user)
        {
            if (this.HttpContext.Request.Headers.ContainsKey("x-Device"))
            {
                switch (this.HttpContext.Request.Headers["x-Device"])
                {
                    case "mail":
                        if (String.IsNullOrEmpty(user.Name))
                        {
                            ModelState.AddModelError("Name", "Поле \"Name\" обязательно к заполнению ");
                        }
                        if (String.IsNullOrEmpty(user.Email))
                        {
                            ModelState.AddModelError("Email", "Поле \"Email\" обязательно к заполнению ");
                        }
                        break;
                    case "mobile":
                        if (user.Phone == null)
                        {
                            ModelState.AddModelError("Phone", "Поле \"Phone\" обязательно к заполнению ");
                        }
                        break;
                    case "web":
                        if (String.IsNullOrEmpty(user.Surname))
                        {
                            ModelState.AddModelError("Surname", "Поле \"Фамилия\" обязательно к заполнению ");
                        }
                        if (String.IsNullOrEmpty(user.Name))
                        {
                            ModelState.AddModelError("Name", "Поле \"Имя\" обязательно к заполнению ");
                        }
                        if (String.IsNullOrEmpty(user.Patronymic))
                        {
                            ModelState.AddModelError("Patronymic", "Поле \"Отчество\" обязательно к заполнению ");
                        }
                        if (user.DateOfBirth == null)
                        {
                            ModelState.AddModelError("DateOfBirth", "Поле \"Дата рождения\" обязательно к заполнению ");
                        }
                        if (user.PassportNumber == null)
                        {
                            ModelState.AddModelError("PassportNumber", "Поле \"Номер паспорта\" обязательно к заполнению ");
                        }
                        if (String.IsNullOrEmpty(user.PlaceOfBirth))
                        {
                            ModelState.AddModelError("PlaceOfBirth", "Поле \"Место рождения\" обязательно к заполнению ");
                        }
                        if (user.Phone == null)
                        {
                            ModelState.AddModelError("Phone", "Поле \"Телефон\" обязательно к заполнению ");
                        }
                        if (String.IsNullOrEmpty(user.RegistrationAddress))
                        {
                            ModelState.AddModelError("RegistrationAddress", "Поле \"Адрес регистрации\" обязательно к заполнению ");
                        }
                        break;
                    default:
                        return BadRequest("Некоректный Headers: \"x-Device\"");
                }
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // если ошибок нет, сохраняем в базу данных
                _context.users.Add(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }
            else
                return BadRequest("Отсутствует Headers: \"x-Device\"");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Search(string? surname, string? name, string? patronymic, float? phone, string? email)
        {
            var user = _context.users.AsQueryable();
            if (surname != null)
                user = user.Where(x => x.Surname == surname);
            if (name != null)
                user = user.Where(x => x.Name == name);
            if (patronymic != null)
                user = user.Where(x => x.Patronymic == patronymic);
            if (phone != null)
                user = user.Where(x => x.Phone == phone);
            if (email != null)
                user = user.Where(x => x.Email == email);
            return await user.ToListAsync(); 

        }
    }
}
