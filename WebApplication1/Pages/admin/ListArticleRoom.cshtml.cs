using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MotelManagement.Business.IService;
using MotelManagement.Data.Models;

namespace MotelManagement.Pages.admin
{
    public class ListArticleRoomModel : PageModel
    {
        public readonly IArticleService _control;
        public MotelManagementContext _context;

        public ListArticleRoomModel(IArticleService _service, MotelManagementContext context)
        {
            this._control = _service;
            this._context = context;

        }
        public async Task<IActionResult> OnGetAsync()
        {

            //List<ArticleRoom> data = await _control.GetListArticleRooms();
            List<ArticleRoom> data = await _context.ArticleRoom.ToListAsync();
            ViewData["lstArticleRoom"] = data;
            return Page();
        }

    }
}
