using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MotelManagement.Business.IService;
using MotelManagement.Business.Service;
using MotelManagement.Common;
using MotelManagement.Data.Models;
using System.Text.Json;

namespace MotelManagement.Pages.admin
{
    public class AddNewRoom : PageModel
    {
        private readonly IRoomService _service;
        private readonly IRoomTypeService _roomTypeService;
        private readonly ILogger<LoginModel> _logger;
        private readonly MotelManagementContext _context;
        public AddNewRoom(IRoomService userService , IRoomTypeService roomTypeService, MotelManagementContext context, ILogger<LoginModel> logger)
        {
            this._service = userService;
            this._roomTypeService = roomTypeService;    
            this._context = context;
            this._logger = logger; 
        }

        public async Task OnGetAsync()
        {
            List<RoomType> roomTypes = await _roomTypeService.GetAll();
            ViewData["roomTypes"] = roomTypes;
        }

        public async Task<IActionResult> OnPostAddnewAsync(string Name , string Description , decimal price , int MaxPerson , int RoomTypeId , List<IFormFile> fileimage)
        {
            try
            {
                Room room = new Room();
                room.Name = Name;
                room.Description = Description;
                room.Price = price;
                room.MaxPerson = MaxPerson;
                room.RoomTypeId = 1;
                room.StatusId = 2;
                Room tmp = await _service.AddNewRoom(room);
                foreach (var item in fileimage)
                {
                    var FileName = Path.GetFileName(item.FileName);
                    var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload", FileName);
                    using (var fileSrteam = new FileStream(filepath, FileMode.Create))
                    {
                        await item.CopyToAsync(fileSrteam);
                    }
                    Image image = new Image();
                    image.Url = FileName;
                    image.RoomId = tmp.RoomId;
                    await _context.Images.AddAsync(image);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            TempData["Message"] = "Error";
            return Redirect("/admin/room/list");
        }
    }


    public class DataForRoom
    {
        public Room Room { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
