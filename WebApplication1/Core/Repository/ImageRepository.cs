using MotelManagement.Core.IRepository;
using MotelManagement.Data.Models;

namespace MotelManagement.Core.Repository
{
    public class ImageRepository : BaseRepository<Image>, IImageRespository
    {
        public ImageRepository(MotelManagementContext context) : base(context)
        {
        }
    }
}
