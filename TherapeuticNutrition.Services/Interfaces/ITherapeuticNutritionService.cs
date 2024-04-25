using Domain.Core.Models;

namespace Domain.Services.Interfaces
{
    public interface ITherapeuticNutritionService
    {
        Task<Pacient> GetPacientByLogin(string login);
        Task<List<Pacient>> GetAllPacients();
    }
}