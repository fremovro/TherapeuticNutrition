using System.Runtime.InteropServices;
using AutoMapper;
using Domain.Core.Interfaces;
using Domain.Core.Models;
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

        public async Task<List<Pacient>> Get()
        {
            var pacientEntities = await _context.Pacients
                .AsNoTracking()
                .ToListAsync();

            return new List<Pacient>();
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

        public async Task<Pacient> GetByLogin(string login)
        {
            var pacientEnity = await _context.Pacients
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Login == login);
            if (pacientEnity == null) { throw new Exception(); }

            var pacientModel = Pacient.Create(pacientEnity.Primarykey, pacientEnity.Login, 
                pacientEnity.Fio, pacientEnity.Password, pacientEnity.Analysis, pacientEnity.Сonclusion);
            return pacientModel;
        }
    }
}
