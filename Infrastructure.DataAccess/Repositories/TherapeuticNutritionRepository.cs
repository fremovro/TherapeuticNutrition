using Domain.Core.Interfaces;
using Domain.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repositories
{
    public class TherapeuticNutritionRepository : ITherapeuticNutritionRepository
    {
        private readonly TherapeuticNutritionDbContext _context;

        public TherapeuticNutritionRepository(TherapeuticNutritionDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pacient>> Get()
        {
            var pacientEntities = await _context.Pacients
                .AsNoTracking()
                .ToListAsync();

            return new List<Pacient>();
        }
    }
}
