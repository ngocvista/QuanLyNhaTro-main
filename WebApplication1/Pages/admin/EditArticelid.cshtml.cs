using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MotelManagement.Data.Models;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MotelManagement.Pages.admin
{
    public class EditArticelidModel : PageModel
    {
        private readonly MotelManagementContext _context;
        public EditArticelidModel(MotelManagementContext context)
        {
            this._context = context;
        }
        public void OnGet(int id)
        {
            var articeld = _context.ArticleRoom.Find(id);
            if (articeld != null)
            {
                ViewData["id"] = articeld.Articelid;
                ViewData["header"] = articeld.header;
                ViewData["content"] = articeld.Content;
                ViewData["image"] = articeld.ImageArticle;
            }
        }

        public async Task<IActionResult> OnPost(int id, string header, string Content, List<IFormFile> fileimage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existArti = _context.ArticleRoom.Find(id);
                    if (existArti != null)
                    {
                        existArti.header = header;
                        existArti.Content = Content;
                        if (fileimage != null)
                        {
                            foreach (var item in fileimage)
                            {
                                var FileName = $"{DateTime.Now:yyyyMMddHHmmss}_{Path.GetFileName(item.FileName)}";
                                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload", FileName);
                                using (var fileSrteam = new FileStream(filepath, FileMode.Create))
                                {
                                    item.CopyToAsync(fileSrteam);
                                }
                                existArti.ImageArticle = FileName;
                            }
                        }
                        await _context.SaveChangesAsync();
                        TempData["SuccessMessage"] = "Cập nhật thành công!";
                    }
                }

            }
            catch
            {
                TempData["SuccessMessage"] = "Cập nhật thất bại!";
            }
            return Redirect("/admin/article/list");
        }
    }
}
