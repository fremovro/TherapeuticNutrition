using Domain.Core.Interfaces;
using Domain.Core.Models;
using Domain.Services.Interfaces;

namespace Domain.Services
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

        public async Task<Pacient> GetPacientByLogin(string login)
        {
            return await _therapeuticNutritionRepository.GetByLogin(login);
        }
    }
}
