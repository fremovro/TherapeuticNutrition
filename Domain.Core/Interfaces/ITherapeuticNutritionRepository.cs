using Domain.Core.Models;

namespace Domain.Core.Interfaces
{
    public interface ITherapeuticNutritionRepository
    {
        Task Add(Pacient pacient);
        Task<List<Allergen>> GetAllergens();
        Task<List<Allergen>> GetPacientAllergens(Guid pacientKey);
        Task<Pacient> GetPacientByLogin(string login);
        Task<List<Pacient>> GetPacients();
    }
}