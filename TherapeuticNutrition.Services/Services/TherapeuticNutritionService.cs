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
            return await _therapeuticNutritionRepository.GetPacients();
        }

        public async Task<Pacient> GetPacientByLogin(string login)
        {
            var pacient = await _therapeuticNutritionRepository.GetPacientByLogin(login);

            var allergens = await _therapeuticNutritionRepository.GetPacientAllergens(pacient.Primarykey);
            pacient.Allergens = allergens;

            return await _therapeuticNutritionRepository.GetPacientByLogin(login);
        }

        public async Task<List<Allergen>> GetAllAllergens(string? login)
        {
            var allergens = await _therapeuticNutritionRepository.GetAllergens();
            if (login == null)
                return await _therapeuticNutritionRepository.GetAllergens();

            var pacient = await _therapeuticNutritionRepository.GetPacientByLogin(login);
            var pacientAllergens = await _therapeuticNutritionRepository.GetPacientAllergens(pacient.Primarykey);

            allergens.Select(e => e.IsFavorite = pacientAllergens.Contains(e));
            //foreach (var allergen in allergens)
            //{
            //    if (pacientAllergens.Contains(allergen))
            //    {
            //        allergen.IsFavorite = true;
            //    }
            //}

            return await _therapeuticNutritionRepository.GetAllergens();
        }
    }
}
