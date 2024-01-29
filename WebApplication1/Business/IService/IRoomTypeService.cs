using MotelManagement.Data.Models;

namespace MotelManagement.Business.IService
{
    public interface IRoomTypeService
    {
        Task<List<RoomType>> GetAll();
        Task AddNewRoomType(RoomType room);
        Task<bool> DeleteRoomType(int id);
    }
}
