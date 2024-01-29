namespace MotelManagement.Business.IService
{
    public interface IimageRoom 
    {
        public Task AddNewImageRoom(List<IFormFile> lstformfile, int roomid);
    }
}
