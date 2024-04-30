using System.Drawing;

namespace Infrastructure.Services.Interfaces
{
    public interface IContentProvider
    {
        Task<string?> GetImageUrl(Guid relation);
    }
}