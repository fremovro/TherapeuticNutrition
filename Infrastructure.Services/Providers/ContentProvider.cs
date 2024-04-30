using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using Infrastructure.DataAccess.Repositories;
using Infrastructure.Services.Interfaces;

namespace Infrastructure.Services.Providers
{
    public class ContentProvider : IContentProvider
    {
        private readonly IFileRepository _fileRepository;
        public ContentProvider(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<string?> GetImageUrl(Guid relation)
        {
            var content = await _fileRepository.GetContentByRelation(relation);
            if (content == null) return null;

            return content;
        }
    }
}
