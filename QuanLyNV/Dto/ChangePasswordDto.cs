using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;


    public class ChangePasswordDto
    {
        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password hiện tại")]
        public string OldPassword { get; set; }

        [BindProperty]
        [Required]
        [StringLength(100, ErrorMessage = "{0} dài {2} đến {1} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password mới")]
        public string NewPassword { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại password mới")]
        [Compare("NewPassword", ErrorMessage = "Password phải giống nhau.")]
        public string ConfirmPassword { get; set; }

        //public string Email { get; set; }

        
}

