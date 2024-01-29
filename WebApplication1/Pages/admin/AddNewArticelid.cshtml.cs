using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MotelManagement.Business.IService;
using MotelManagement.Data.Models;

namespace MotelManagement.Pages.admin
{
    public class AddNewArticelid : PageModel
    {
        private readonly IArticleService _service;
        private readonly MotelManagementContext _context;

        public AddNewArticelid(IArticleService userService, MotelManagementContext context, ILogger<LoginModel> logger)
        {
            this._service = userService;
            this._context = context;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string header, string Content, List<IFormFile> fileimage)
        {
            ArticleRoom room = new ArticleRoom();
            room.header = header;
            room.Content = Content;
            foreach (var item in fileimage)
            {
                var FileName = $"{DateTime.Now:yyyyMMddHHmmss}_{Path.GetFileName(item.FileName)}";
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload", FileName);
                using (var fileSrteam = new FileStream(filepath, FileMode.Create))
                {
                    await item.CopyToAsync(fileSrteam);
                }
                room.ImageArticle = FileName;
            }
            await _context.ArticleRoom.AddAsync(room);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Thêm tin tức thành công!";
            return Redirect("/admin/article/list");
        }
    }
}
