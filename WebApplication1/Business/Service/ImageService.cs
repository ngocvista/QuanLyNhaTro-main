using MotelManagement.Business.IService;
using MotelManagement.Core.IRepository;
using MotelManagement.Data.Models;

namespace MotelManagement.Business.Service
{
    public class ImageService : IimageRoom
    {
        private readonly IUnitOfWork _unitOfWork;
        public ImageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddNewImageRoom(List<IFormFile> lstformfile, int roomid)
        {
            try
            {
                foreach (var item in lstformfile)
                {
                    var FileName = Path.GetFileName(item.FileName);
                    var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload", FileName);
                    using (var fileSrteam = new FileStream(filepath, FileMode.Create))
                    {
                        await item.CopyToAsync(fileSrteam);
                    }
                    Image image = new Image();
                    image.Url = FileName;
                    image.RoomId = roomid;
                    await _unitOfWork.imageRespository.AddAsync(image);
                }
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
