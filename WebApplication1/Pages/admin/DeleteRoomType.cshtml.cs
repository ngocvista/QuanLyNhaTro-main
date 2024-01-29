using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MotelManagement.Business.IService;
using MotelManagement.Data.Models;

namespace MotelManagement.Pages.admin
{
    public class DeleteRoomTypeModel : PageModel
    {

        private readonly IRoomTypeService _control;
        public MotelManagementContext _context;

        public DeleteRoomTypeModel(IRoomTypeService _service, MotelManagementContext context)
        {
            this._control = _service;
            this._context = context;

        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                var result = _context.RoomTypes.Find(id);
                if (result != null)
                {
                    _context.RoomTypes.Remove(result);
                    var affectedRows = await _context.SaveChangesAsync();
                    if (affectedRows > 0)
                    {
                        TempData["DeleteRoomTypeMessage"] = "Đã xóa loại phòng thành công!";
                    }
                    else
                    {
                        TempData["DeleteRoomTypeMessage"] = "Không thể xóa loại phòng này!";
                    }
                }
                else
                {
                    TempData["DeleteRoomTypeMessage"] = "Không thể tìm thấy loại phòng để xóa!";
                }
            }
            catch (Exception ex)
            {
                TempData["DeleteRoomTypeMessage"] = "Có lỗi xảy ra: " + ex.Message;
            }
            return Redirect("/admin/room/listroomtype");
        }
    }
}

