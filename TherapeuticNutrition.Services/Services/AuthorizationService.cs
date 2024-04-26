using Domain.Core.Interfaces;
using Domain.Core.Models;
using Domain.Services.Interfaces;

namespace Domain.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITherapeuticNutritionRepository _therapeuticNutritionRepository;
        private readonly IJwtProvider _jwtProvider;

        public AuthorizationService(IPasswordHasher passwordHasher, 
            ITherapeuticNutritionRepository therapeuticNutritionRepository,
            IJwtProvider jwtProvider)
        {
            _passwordHasher = passwordHasher;
            _therapeuticNutritionRepository = therapeuticNutritionRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task Register(string login, string fio, string password)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            var pacient = Pacient.CreatePacient(Guid.NewGuid(), login, fio, hashedPassword, string.Empty, string.Empty,
                new List<Allergen>(), new List<Product>(), new List<Recipe>());


            await _therapeuticNutritionRepository.Add(pacient);
        }

        public async Task<string> Login(string login, string password)
        {
            var pacient = await _therapeuticNutritionRepository.GetPacientByLogin(login);

            var result = _passwordHasher.Verify(password, pacient.Password);
            if (result == false)
            {
                throw new Exception("Failed to login");
            }

            var token = _jwtProvider.GenerateToken(pacient);

            return token;
        }
    }
}
