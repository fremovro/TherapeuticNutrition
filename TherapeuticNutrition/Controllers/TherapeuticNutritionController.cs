using Domain.Core.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace TherapeuticNutrition.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TherapeuticNutritionController : ControllerBase
    {
        private readonly ITherapeuticNutritionService _therapeuticNutritionService;
        private readonly Domain.Services.Interfaces.IAuthorizationService _authorizationService;

        public TherapeuticNutritionController(ITherapeuticNutritionService therapeuticNutritionService, 
            Domain.Services.Interfaces.IAuthorizationService authorizationService)
        {
            _therapeuticNutritionService = therapeuticNutritionService;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pacient>>> Get()
        {
            var pacients = await _therapeuticNutritionService.GetAllPacients();
            return Ok(pacients);
        }

        [HttpGet]
        [Route("get/pacient")]
        public async Task<ActionResult<Pacient>> GetPacient()
        {
            if (Request.Cookies["token"] == null 
                || Request.Cookies["login"] == null)
            {
                return Unauthorized("Необходимо авторизоваться");
            }

            var login = Request.Cookies["login"]?.ToString() ?? "fremov.ro";
            var pacient = await _therapeuticNutritionService.GetPacientByLogin(login);
            return Ok(pacient);
        }

        [HttpGet]
        [Route("get/allergens")]
        public async Task<ActionResult<Pacient>> GetAllergens()
        {
            if (Request.Cookies["token"] == null
                || Request.Cookies["login"] == null)
            {
                return Unauthorized("Необходимо авторизоваться");
            }

            var login = Request.Cookies["login"]?.ToString();
            var allergens = await _therapeuticNutritionService.GetAllAllergens(login);
            return Ok(allergens);
        }

        [HttpGet]
        [Route("login/login={login}&password={password}")]
        public async Task<IResult> Login(string login, string password)
        {
            try
            {
                var token = await _authorizationService.Login(login, password);

                var cookieOptions = new CookieOptions
                {
                    Path = "/",
                    HttpOnly = false,
                    Expires = DateTimeOffset.UtcNow.AddDays(1),
                    IsEssential = true,
                    SameSite = SameSiteMode.None,
                    Secure = true
                };
                Response.Cookies.Append("token", token, cookieOptions);
                Response.Cookies.Append("login", login, cookieOptions);

                var pacient = await _therapeuticNutritionService.GetPacientByLogin(login);
                return Results.Ok(pacient); 
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex);
            }
        }

        [HttpPost("{login, password, fio}", Name = "register")]
        public async Task<IResult> Register(string login, string password, string fio)
        {
            await _authorizationService.Register(login, fio, password);

            return Results.Ok();
        }
    }
}
