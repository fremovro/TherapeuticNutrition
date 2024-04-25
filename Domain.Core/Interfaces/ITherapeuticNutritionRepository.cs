using Domain.Core.Models;

namespace Domain.Core.Interfaces
{
    public interface ITherapeuticNutritionRepository
    {
        Task Add(Pacient pacient);
        Task<List<Pacient>> Get();
        Task<Pacient> GetByLogin(string login);
    }
}