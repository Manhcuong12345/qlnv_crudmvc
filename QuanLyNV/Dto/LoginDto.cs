
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

public class LoginDto
    {
    [BindProperty]
    [Required(ErrorMessage = "Không để trống")]
    [Display(Name = "Nhập username hoặc email của bạn")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Nhập đúng thông tin")]
    public string UserNameOrEmail { set; get; }

    [BindProperty]
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu")]
    public string Password { get; set; }

    [BindProperty]
    [Display(Name = "Nhớ thông tin đăng nhập?")]
    public bool RememberMe { get; set; }
}

