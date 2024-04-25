namespace Domain.Services.Interfaces
{
    public interface IAuthorizationService
    {
        Task Register(string login, string fio, string password);
        Task<string> Login(string login, string password);
    }
}