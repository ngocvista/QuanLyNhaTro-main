using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MotelManagement.Business.IService;
using MotelManagement.Common;
using MotelManagement.Data.Models;
using System.Text.Json;

namespace MotelManagement.Pages
{
    public class ForgetPasswordModel : PageModel
    {
        private readonly IUserService _service;
        private readonly ILogger<ForgetPasswordModel> _logger;
        public ForgetPasswordModel(IUserService userService, ILogger<ForgetPasswordModel> logger)
        {
            this._service = userService;
            this._logger = logger;
        }
        public void OnGet()
        {

        }

    }
}
