using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MotelManagement.Business.IService;
using MotelManagement.Common;
using MotelManagement.Data.Models;
using System.Text.Json;

namespace MotelManagement.Pages
{
    public class ChangeInfoModel : PageModel
    {
        [BindProperty]
        public string ErrorMessage { get; set; }
        private readonly MotelManagementContext _context;
        private readonly IUserService _service;
        private readonly ILogger<ChangePasswordModel> _logger;
        public ChangeInfoModel(IUserService userService, ILogger<ChangePasswordModel> logger, MotelManagementContext context)
        {
            this._service = userService;
            this._logger = logger;
            _context = context;

        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostChangeInfoAsync(string fullName, string email, bool gender, string phone, string address, DateTime dob, string confirmPassword)
        {
            string json = HttpContext.Session.GetString("user");
            Console.WriteLine(json);
            if (json == null)
            {
                TempData["Message"] = "Failed";
                return Page();
            }
            else
            {
                User user = (User)JsonUtil.DeserializeObject<User>(json);
                if (user.Email.Equals(email) || user.Phone.Equals(phone))
                {
                    ErrorMessage = "Email / SĐT này đã được đăng ký, hãy sử dụng cái khác";
                    return Page();
                }
                else if (user.Password.Equals(confirmPassword))
                {
                    try
                    {
                        user.FullName = fullName;
                        user.Email = email;
                        user.Gender = gender;
                        user.Phone = phone;
                        user.Address = address;
                        user.Dob = dob;
                        await _service.ChangeInfo(user);
                        TempData["Message"] = "SuccChangeInfo";
                        return Redirect("../user/logout");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.ToString());
                    }
                }
                else 
                {
                    ErrorMessage = "Lỗi! Kiểm tra lại mật khẩu của bạn";
                    return Page();
                }
            }
            TempData["Message"] = "Wrong";
            return Page();

        }
    }
}
