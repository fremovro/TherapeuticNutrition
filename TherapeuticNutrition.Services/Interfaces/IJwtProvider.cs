using Domain.Core.Models;

namespace Domain.Services.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateToken(Pacient pacient);
    }
}