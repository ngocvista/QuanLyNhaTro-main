using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MotelManagement.Business.IService;
using MotelManagement.Common;
using MotelManagement.Data.Models;
using System.Text.Json;

namespace MotelManagement.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string ErrorMessage { get; set; }
        private readonly IUserService _service;
        private readonly ILogger<LoginModel> _logger;
        public LoginModel(IUserService userService, ILogger<LoginModel> logger)
        {
            this._service = userService;
            this._logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostLoginAsync(string email, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    ErrorMessage = "Vui lòng nhập đủ thông tin.";
                    return Page();
                }

                User user = await _service.Login(email, password);

                if (user != null)
                {
                    string json = JsonUtil.SerializeObject(user);
                    HttpContext.Session.SetString("user", json);

                    if (user.RoleName != null)
                    {
                        return Redirect("~/admin/dashboard");
                    }

                    return Redirect("~/room/list");

                }
                else
                {
                    ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng. Vui lòng thử lại.";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            TempData["Message"] = "Error";
            return Redirect("~/errors");

        }
    }
}
