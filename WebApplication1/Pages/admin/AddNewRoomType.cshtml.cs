using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MotelManagement.Business.IService;
using MotelManagement.Data.Models;

namespace MotelManagement.Pages.admin
{
    public class AddNewRoomTypeModel : PageModel
    {
        private readonly IRoomTypeService _service;
        private readonly ILogger<LoginModel> _logger;
        public AddNewRoomTypeModel(IRoomTypeService userService, ILogger<LoginModel> logger)
        {
            this._service = userService;
            this._logger = logger;
        }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAddNewRoomTypeAsync(RoomType roomType)
        {
            _logger.LogDebug("da log duoc vao trong ham");
            try
            {
                await _service.AddNewRoomType(roomType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            TempData["SuccessAddRoomTypeMessage"] = "Thêm loại phòng thành công!";
            return Redirect("/admin/room/listRoomType");
        }
    }
}
