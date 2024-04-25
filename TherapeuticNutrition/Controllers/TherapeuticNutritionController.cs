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
            if (Request.Cookies["token"] == null || Request.Cookies["login"] == null)
            {
                return Unauthorized("Необходимо авторизоваться");
            }

            var login = Request.Cookies["login"]?.ToString() ?? "fremov.ro";
            var pacient = await _therapeuticNutritionService.GetPacientByLogin(login);
            return Ok(pacient);
        }

        [HttpGet]
        [Route("login/login={login}&password={password}")]
        public async Task<IResult> Login(string login, string password)
        {
            try
            {
                var token = await _authorizationService.Login(login, password);

                Response.Cookies.Append("token", token);
                Response.Cookies.Append("login", login);

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
