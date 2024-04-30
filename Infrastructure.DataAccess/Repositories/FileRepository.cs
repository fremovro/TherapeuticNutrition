using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly TherapeuticNutritionDbContext _context;

        public FileRepository(TherapeuticNutritionDbContext context)
        {
            _context = context;
        }

        #region CRUD
        public async Task Add(Guid relation, string content)
        {
            var fileEntity = new Entities.Files.File()
            {
                Primarykey = Guid.NewGuid(),
                Relation = relation,
                Content = content
            };
            await _context.Files.AddAsync(fileEntity);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region File
        public async Task<string?> GetContentByRelation(Guid relation)
        {
            var fileEntity = await _context.Files
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Relation == relation);

            return fileEntity?.Content;
        }
        #endregion
    }
}
