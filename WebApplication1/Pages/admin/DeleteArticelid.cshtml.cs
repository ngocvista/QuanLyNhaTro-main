using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MotelManagement.Business.IService;
using MotelManagement.Data.Models;

namespace MotelManagement.Pages.admin
{
    public class DeleteArticelidModel : PageModel
    {
        public readonly IArticleService _control;
        public MotelManagementContext _context;

        public DeleteArticelidModel(IArticleService _service, MotelManagementContext context)
        {
            this._control = _service;
            this._context = context;

        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                var result = _context.ArticleRoom.Find(id);
                if (result != null)
                {
                    _context.ArticleRoom.Remove(result);
                    var affectedRows = await _context.SaveChangesAsync();
                    if (affectedRows > 0)
                    {
                        TempData["DeleteMessage"] = "Đã xóa tin tức thành công!";
                    }
                    else
                    {
                        TempData["DeleteMessage"] = "Không thể xóa tin tức này!";
                    }
                }
                else
                {
                    TempData["DeleteMessage"] = "Không thể tìm thấy tin tức để xóa!";
                }
            }
            catch (Exception ex)
            {
                TempData["DeleteMessage"] = "Có lỗi xảy ra: " + ex.Message;
            }
            return Redirect("/admin/article/list");
        }
    }
}
