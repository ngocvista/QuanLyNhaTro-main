using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MotelManagement.Business.IService;
using MotelManagement.Common;
using MotelManagement.Data.Models;
using System.Text.Json;

namespace MotelManagement.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public string ErrorMessage { get; set; }
        private readonly MotelManagementContext _context;
        private readonly IUserService _service;
        private readonly ILogger<RegisterModel> _logger;
        public RegisterModel(IUserService userService, ILogger<RegisterModel> logger, MotelManagementContext context)
        {
            this._service = userService;
            this._logger = logger;
            _context = context;

        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostRegisterAsync(User user)
        {
            Console.WriteLine(JsonSerializer.Serialize(user));
            try
            {
                // Kiểm tra xem người dùng đã tồn tại hay chưa
                var isEmailUserExists = _context.Users.Where(x => x.Email == user.Email).FirstOrDefault();
                var isPhoneUserExists = _context.Users.Where(x => x.Phone == user.Phone).FirstOrDefault();
                Console.WriteLine(isEmailUserExists);
                Console.WriteLine(isPhoneUserExists);
                if (isEmailUserExists != null)
                {
                    ErrorMessage = "Email này đã được đăng ký, hãy sử dụng email khác";
                    return Page();
                }
                if (isPhoneUserExists != null)
                {
                    ErrorMessage = "Số điện thoại này đã được đăng ký, hãy sử dụng số khác";
                    return Page();
                }
                //await _service.Register(user);
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                ErrorMessage = "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                TempData["Message"] = "Failed";
            }
            return Redirect("~/user/login");
        }



    }
}
