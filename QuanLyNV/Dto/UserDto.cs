
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using Microsoft.AspNetCore.Mvc;

public class UserDto
{ 
    public int Id { get; set; }

    [BindProperty]
    [Display(Name = "Tên tài khoản")]
    public string Username { get; set; }

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

}

