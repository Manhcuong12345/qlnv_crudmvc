using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
// using Album.Binder;
using QuanLyNV.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using AutoMapper;

namespace Album.Areas.Identity.Pages.Account.Manage {

    [Authorize]
    public partial class IndexModel : PageModel {
        [BindProperty]
        public UserDto userDto { get; set; }
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public IndexModel (
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IMapper mapper) {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        //[Display(Name = "Tên tài khoản")]
        //public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        //[BindProperty]
        //public InputModel Input { get; set; }

        //public class InputModel {
        //    public string Id { get; set; }

        //    [Phone]
        //    [Display (Name = "Số điện thoại")]
        //    public string PhoneNumber { get; set; }
        
        //    [MaxLength (100)]
        //    [Display(Name = "Họ tên đầy đủ")]
        //    public string FullName { set; get; }

        //    [MaxLength (255)]
        //    [Display(Name = "Địa chỉ")]
        //    public string Address { set; get; }

        //    [DataType (DataType.Date)]
        //    [Display(Name = "Ngày sinh d/m/y")]
        //    // [ModelBinder(BinderType=typeof(DayMonthYearBinder))]
        //    [DisplayFormat(ApplyFormatInEditMode=true, DataFormatString = "{0:dd/MM/yyyy}")]
        //    public DateTime? Birthday { set; get; }

        //    public byte[] Poster { get; set; }
        //}

        // Nạp thông tin từ User vào Model
        private async Task LoadAsync (AppUser user) {
            //var userName = await _userManager.GetUserNameAsync(user);
            //var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            //userDto = new UserDto
            //{
            //    Username = userName,
            //    PhoneNumber = phoneNumber,
            //    Birthday = user.Birthday,
            //    Address = user.Address,
            //    FullName = user.FullName,
            //    //Poster = user.Poster
            //};
            //Su dung mapper 
            userDto = _mapper.Map<UserDto>(user);
        }

        //Hien thi thong tin
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            //su dung mapper
            userDto = _mapper.Map<UserDto>(user);

            if (user == null)
            {
                return NotFound($"Không tải được tài khoản ID = '{_userManager.GetUserId(User)}'.");
            }

            //await LoadAsync(data);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync () {
            var user = await _userManager.GetUserAsync (User);
            
            if (user == null) {
                return NotFound ($"Không có tài khoản ID: '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid) {
                //Load hien thi du lieu ra
                await LoadAsync(user);
                return Page ();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync (user);
            //kiểm tra số đt có trùng hay khồng
            if (userDto.PhoneNumber != phoneNumber) {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync (user, userDto.PhoneNumber);
                if (!setPhoneResult.Succeeded) {
                    StatusMessage = "Lỗi cập nhật số điện thoại.";
                    return RedirectToPage ();
                }
            }


            //Cập nhật các trường bổ sung
            user.Address = userDto.Address;
            user.Birthday = userDto.Birthday;
            user.FullName = userDto.FullName;
            user.Gender = userDto.Gender;
            
            await _userManager.UpdateAsync(user);

            // Đăng nhập lại để làm mới Cookie (không nhớ thông tin cũ)
            await _signInManager.RefreshSignInAsync (user);
            StatusMessage = "Hồ sơ của bạn đã cập nhật";
            return RedirectToPage ();
        }
    }
}