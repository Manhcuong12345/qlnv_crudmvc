using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class RegisterDto
    {
        public int Id { get; set; }

        [BindProperty]
        [Required]
        [EmailAddress]
        [Display(Name = "Địa chỉ Email")]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        [StringLength(100, ErrorMessage = "{0} dài từ {2} đến {1} ký tự.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu không giống nhau")]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        [Required]
        [StringLength(100, ErrorMessage = "{0} dài từ {2} đến {1} ký tự.", MinimumLength = 3)]
        [DataType(DataType.Text)]
        [Display(Name = "Tên tài khoản (viết liền - không dấu)")]
        public string UserName { set; get; }

       
}
