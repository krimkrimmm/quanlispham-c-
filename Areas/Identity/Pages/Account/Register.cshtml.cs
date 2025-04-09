// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using TMDT.Areas.Identity.Data;

namespace TMDT.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<LOGINUser> _signInManager;
        private readonly UserManager<LOGINUser> _userManager;
        //private readonly IUserStore<LOGINUser> _userStore;
        //private readonly IUserEmailStore<LOGINUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        public readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<LOGINUser> userManager
            //,IUserStore<LOGINUser> userStore
            ,SignInManager<LOGINUser> signInManager
            ,ILogger<RegisterModel> logger
            ,RoleManager<IdentityRole> roleManager
            //,IEmailSender emailSender
            )
        {
            _userManager = userManager;
            //_userStore = userStore;
            //_emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
            //_emailSender = emailSender;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Địa chỉ Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "{0} dài từ {2} đến {1} ký tự.", MinimumLength = 3)]
            [DataType(DataType.Password)]
            [Display(Name = "Mật khẩu")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Nhập lại mật khẩu")]
            [Compare("Password", ErrorMessage = "Mật khẩu không giống nhau")]
            public string ConfirmPassword { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "{0} dài từ {2} đến {1} ký tự,  (viết liền - không dấu)", MinimumLength = 3)]
            [DataType(DataType.Text)]
            [Display(Name = "Tên tài khoản")]
            public string UserName { set; get; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = new LOGINUser { UserName = Input.UserName, Email = Input.Email };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Đã tạo mới tài khoản thành công.");

                    // Kiểm tra và tạo vai trò "User" nếu chưa tồn tại
                    var roleExists = await _roleManager.RoleExistsAsync("User");
                    if (!roleExists)
                    {
                        var roleResult = await _roleManager.CreateAsync(new IdentityRole("User"));
                        if (!roleResult.Succeeded)
                        {
                            throw new InvalidOperationException("Không thể tạo vai trò.");
                        }
                    }

                    // Thêm người dùng vào vai trò "User"
                    var addToRoleResult = await _userManager.AddToRoleAsync(user, "User");
                    if (!addToRoleResult.Succeeded)
                    {
                        throw new InvalidOperationException("Không thể thêm người dùng vào vai trò.");
                    }

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    // Gửi email xác nhận (nếu cần)
                    try
                    {
                        //await _emailSender.SendEmailAsync(Input.Email, "Xác nhận email của bạn",
                        //    $"Vui lòng xác nhận tài khoản của bạn bằng cách <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>nhấp vào đây</a>.");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Đã xảy ra lỗi trong quá trình gửi email xác nhận.");
                    }

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        // Đăng nhập ngay nếu không yêu cầu xác nhận email
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }


        //cai nay khong can nua
        /*
        private LOGINUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<LOGINUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(LOGINUser)}'. " +
                    $"Ensure that '{nameof(LOGINUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<LOGINUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<LOGINUser>)_userStore;
        }*/
    }
}
