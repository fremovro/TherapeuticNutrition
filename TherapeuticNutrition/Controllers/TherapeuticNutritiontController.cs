using Domain.Core.Models;
using Microsoft.AspNetCore.Mvc;
using TherapeuticNutrition.Services.Interfaces;

namespace TherapeuticNutrition.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TherapeuticNutritiontController : ControllerBase
    {
        private readonly ITherapeuticNutritionService _therapeuticNutritionService;

        public TherapeuticNutritiontController(ITherapeuticNutritionService therapeuticNutritionService)
        {
            _therapeuticNutritionService = therapeuticNutritionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pacient>>> Get()
        {
            var pacients = await _therapeuticNutritionService.GetAllPacients();
            return Ok(pacients);
        }
    }
}
