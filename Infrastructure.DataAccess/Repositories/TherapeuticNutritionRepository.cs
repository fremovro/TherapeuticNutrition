using System.Collections.Generic;
using System.Runtime.InteropServices;
using AutoMapper;
using Domain.Core.Interfaces;
using Domain.Core.Models;
using Infrastructure.DataAccess.Entities.Relations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repositories
{
    public class TherapeuticNutritionRepository : ITherapeuticNutritionRepository
    {
        private readonly TherapeuticNutritionDbContext _context;
        private readonly IMapper _mapper;

        public TherapeuticNutritionRepository(TherapeuticNutritionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Pacient>> GetPacients()
        {
            var pacientEntities = await _context.Pacients
                .AsNoTracking()
                .ToListAsync();

            return new List<Pacient>();
        }

        public async Task<List<Allergen>> GetAllergens()
        {
            var allergenEntities = await _context.Allergens
                .AsNoTracking()
                .ToListAsync();
            var allergens = allergenEntities
                .Select(e => Allergen.CreateAllergen(e.Primarykey, e.Name, e.Reaction, e.DangerDegree))
                .ToList();

            return allergens;
        }

        public async Task<List<Allergen>> GetPacientAllergens(Guid pacientKey)
        {
            var pacientAllergens = await _context.PacientAllergens
                .Where(e => e.Pacient == pacientKey)
                .AsNoTracking()
                .ToListAsync();

            var pacientAllergenKeys = pacientAllergens
                .Select(e => e.Allergen);
            var allergenEntities = await _context.Allergens
                .Where(e => pacientAllergenKeys.Contains(e.Primarykey))
                .AsNoTracking()
                .ToListAsync();

            var allergens = allergenEntities
                .Select(e => Allergen.CreateAllergen(e.Primarykey, e.Name, e.Reaction, e.DangerDegree))
                .ToList();

            return allergens;
        }

        public async Task Add(Pacient pacient)
        {
            var pacientEntity = new Entities.Pacient()
            {
                Primarykey = pacient.Primarykey,
                Login = pacient.Login,
                Fio = pacient.Fio,
                Password = pacient.Password,
                Analysis = pacient.Analysis,
                Сonclusion = pacient.Conclusion
            };
            await _context.Pacients.AddAsync(pacientEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<Pacient> GetPacientByLogin(string login)
        {
            var pacientEnity = await _context.Pacients
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Login == login);
            //if (pacientEnity == null) { throw new Exception(); }

            var pacientModel = Pacient.CreatePacient(pacientEnity.Primarykey, pacientEnity.Login,
                pacientEnity.Fio, pacientEnity.Password, pacientEnity.Analysis, pacientEnity.Сonclusion);
            return pacientModel;
        }
    }
}
