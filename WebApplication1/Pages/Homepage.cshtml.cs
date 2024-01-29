using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MotelManagement.Data.Models;

namespace MotelManagement.Pages
{
    public class HomepageModel : PageModel
    {
        private readonly MotelManagement.Data.Models.MotelManagementContext _context;

        public HomepageModel(MotelManagement.Data.Models.MotelManagementContext context)
        {
            _context = context;
        }

        
    }
}
