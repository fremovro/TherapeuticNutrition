using Domain.Core.Interfaces;
using Domain.Core.Models;
using TherapeuticNutrition.Services.Interfaces;

namespace TherapeuticNutrition.Services.Services
{
    public class TherapeuticNutritionService : ITherapeuticNutritionService
    {
        private readonly ITherapeuticNutritionRepository _therapeuticNutritionRepository;

        public TherapeuticNutritionService(ITherapeuticNutritionRepository therapeuticNutritionRepository)
        {
            _therapeuticNutritionRepository = therapeuticNutritionRepository;
        }

        public async Task<List<Pacient>> GetAllPacients()
        {
            return await _therapeuticNutritionRepository.Get();
        }
    }
}
