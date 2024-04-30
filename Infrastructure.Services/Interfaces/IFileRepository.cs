namespace Infrastructure.DataAccess.Repositories
{
    public interface IFileRepository
    {
        Task Add(Guid relation, string content);
        Task<string?> GetContentByRelation(Guid relation);
    }
}