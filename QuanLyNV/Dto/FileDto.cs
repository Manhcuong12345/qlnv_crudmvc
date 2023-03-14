using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNV.Dto
{
    public class FileDto
    {
        //[MaxLength(255)]
        //public string Name { set; get; }

        //[BindProperty]
        //[Display(Name = "Ảnh đại diện")]
        //public IFormFile PhotoPath { set; get; }

        public string Id { get; set; }

        [BindProperty]
        [Display(Name = "Tên tài khoản")]
        public string UserName { get; set; }

        [BindProperty]
        [Phone]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [BindProperty]
        [MaxLength(100)]
        [Display(Name = "Họ tên đầy đủ")]
        public string FullName { set; get; }

        [BindProperty]
        [MaxLength(255)]
        [Display(Name = "Địa chỉ")]
        public string Address { set; get; }

        [BindProperty]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày sinh d/m/y")]
        // [ModelBinder(BinderType=typeof(DayMonthYearBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Birthday { set; get; }

        [BindProperty]
        [Display(Name = "Giới tính")]
        public string Gender { set; get; }

        [NotMapped]
        [Display(Name = "Ảnh đại diện")]
        public IFormFile PhotoPath { get; set; }

        public string Name { get; set; }
    }
}
