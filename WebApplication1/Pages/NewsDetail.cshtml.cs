using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MotelManagement.Data.Models;

namespace MotelManagement.Pages
{
    public class NewsDetailModel : PageModel
    {
        public MotelManagement.Data.Models.MotelManagementContext _context;
        public MotelManagement.Data.Models.ArticleRoom _room;
        public NewsDetailModel(MotelManagementContext context, ArticleRoom room)
        {
            _context = context;
            _room = room;
        }
        public void OnGet(int id)
        {
            var news = _context.ArticleRoom.Find(id);
            if (news != null)
            {
                _room = news;
            }
        }
    }
}
