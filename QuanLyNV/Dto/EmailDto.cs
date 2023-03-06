using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace QuanLyNV.Dto
{
    public class EmailDto
    {
        [BindProperty]
        public string Username { get; set; }

        [EmailAddress]
        [Display(Name = "Nhập chính xác địa chỉ email")]
        public string Email { get; set; }

        [BindProperty]
        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        [EmailAddress]
        [Display(Name = "Đổi sang email mới")]
        public string NewEmail { get; set; }
    }
}
