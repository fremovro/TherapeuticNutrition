using System;
using AutoMapper;
using Domain.Core.Models;
using Infrastructure.DataAccess.Entities;

namespace TherapeuticNutrition.Mappings
{
    public class DbMappings : Profile
    {
        public DbMappings()
        {
            CreateMap<Domain.Core.Models.Pacient, Infrastructure.DataAccess.Entities.Pacient>().ReverseMap(); ;
        }
    }
}
