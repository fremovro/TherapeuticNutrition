using Domain.Core.Models;

namespace Domain.Core.Interfaces
{
    public interface ITherapeuticNutritionRepository
    {
        Task<List<Pacient>> Get();
    }
}