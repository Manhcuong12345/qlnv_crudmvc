using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Linq;
using System.Threading.Tasks;
using QuanLyNV.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using QuanLyNV.Dto;

namespace QuanLyNV.Areas.Identity.Pages.Account.Manage
{
    public partial class EmailModel : PageModel
    {
        [BindProperty]
        public EmailDto EmailDto { get; set; }
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public EmailModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        //public string Username { get; set; }

        //public string Email { get; set; }

        //public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        //[BindProperty]
        //public InputModel Input { get; set; }

        //public class InputModel
        //{
        //    [Required]
        //    [EmailAddress]
        //    [Display(Name = "Đổi sang email mới")]
        //    public string NewEmail { get; set; }
        //}

        private async Task LoadAsync(AppUser user)
        {
            var email = await _userManager.GetEmailAsync(user);
            Email = email;

            EmailDto = new EmailDto
            {
                NewEmail = email,
            };

            EmailDto.IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Khôg nạp được tài khoản ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostChangeEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Khôg nạp được tài khoản ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var email = await _userManager.GetEmailAsync(user);
            if (EmailDto.NewEmail != email)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateChangeEmailTokenAsync(user, EmailDto.NewEmail);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes (code));
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmailChange",
                    pageHandler: null,
                    values: new { userId = userId, email = EmailDto.NewEmail, code = code },
                    protocol: Request.Scheme);
                await _emailSender.SendEmailAsync(
                    EmailDto.NewEmail,
                    "Xác nhận",
                    $"Hãy xác nhận Email của bạn bằng cách <a href='{callbackUrl}'>bấm vào đây</a>.");

                EmailDto.StatusMessage = "Hãy mở email để xác nhận thay đổi";
                return RedirectToPage();
            }

            EmailDto.StatusMessage = "Bạn đã thay đổi email.";
            return RedirectToPage();
        }

        //Ham xac nhan email
        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Khôg nạp được tài khoản ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "Xác nhận Email",
                $"Xác nhận email <a href='{callbackUrl}'>bấm vào đây</a>.");

            EmailDto.StatusMessage = "Hãy mở email để xác nhận";
            return RedirectToPage();    
        }

        public string Email { get; set; }

    }
}
