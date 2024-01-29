using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MotelManagement.Business.IService;
using MotelManagement.Data.Models;
using System;

namespace MotelManagement.Pages.admin
{
    public class UserListRoomTypeModel : PageModel
    {
        private readonly IRoomTypeService _roomService;
        private readonly IRoomTypeService _roomTypeService;
        
        public UserListRoomTypeModel(IRoomTypeService roomService, IRoomTypeService roomTypeService)
        {
            _roomService = roomService;
            _roomTypeService = roomTypeService;
        }
        public async Task OnGet()
        {
			List<RoomType> result = await _roomService.GetAll();
			ViewData["roomsType"] = result;
		}
    }
}
