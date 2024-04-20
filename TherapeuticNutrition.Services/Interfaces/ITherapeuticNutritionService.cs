using Domain.Core.Models;

namespace TherapeuticNutrition.Services.Interfaces
{
    public interface ITherapeuticNutritionService
    {
        Task<List<Pacient>> GetAllPacients();
    }
}