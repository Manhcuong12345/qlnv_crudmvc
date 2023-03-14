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
using QuanLyNV.Dto;
using Microsoft.Extensions.Hosting.Internal;

namespace QuanLyNV.Areas.Identity.Pages.Account.Manage {

    [Authorize]
    public partial class IndexModel : PageModel {
        [BindProperty]
        public UserDto UserDto { get; set; }
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public IndexModel (
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IMapper mapper,
            IWebHostEnvironment env) {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _env = env;
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
            UserDto = _mapper.Map<UserDto>(user);
        }

        //Hien thi thong tin
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            //su dung mapper
           UserDto = _mapper.Map<UserDto>(user);

            if (user == null)
            {
                return NotFound($"Không tải được tài khoản ID = '{_userManager.GetUserId(User)}'.");
            }

            //await LoadAsync();
            return Page();
        }

        //Cập nhập dữ liệu
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
            if (UserDto.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, UserDto.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Lỗi cập nhật số điện thoại.";
                    return RedirectToPage();
                }
            }

            //Check file
            if (UserDto.Avatar != null)
            {
                string ExitingFile = Path.Combine(_env.WebRootPath, "picture", UserDto.Avatar);
                System.IO.File.Delete(ExitingFile);
            }

            //Cập nhật các trường bổ sung
            user.Address = UserDto.Address;
            user.Birthday = UserDto.Birthday;
            user.FullName = UserDto.FullName;
            user.Gender = UserDto.Gender;
            user.Avatar = UploadFile(UserDto.PhotoPath);

            await _userManager.UpdateAsync(user);

            // Đăng nhập lại để làm mới Cookie (không nhớ thông tin cũ)
            await _signInManager.RefreshSignInAsync (user);
            StatusMessage = "Hồ sơ của bạn đã cập nhật";
            return RedirectToPage ();
        }

        //Function xu ly file
        private string UploadFile(IFormFile formFile)
        {
            // Hình ảnh phải được tải lên thư mục hình ảnh trong wwwroot
            // Để lấy đường dẫn của thư mục wwwroot, chúng tôi đang sử dụng dịch vụ HostingEnvironment do ASP.NET Cor cung cấp
            // Nếu bạn muốn tên tệp là duy nhất thì chúng tôi sẽ thêm một giá trị GUID mới và một dấu gạch dưới vào tên tệp
            string UniqueFileName = Guid.NewGuid().ToString() + "-" + formFile.FileName;
            string TargetPath = Path.Combine(_env.WebRootPath, "picture", UniqueFileName);
           // Sử dụng phương thức CopyTo() do giao diện IFormFile cung cấp để
            // // copy file vào thư mục wwwroot/picture
            using (var stream = new FileStream(TargetPath, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }
            return UniqueFileName;
        }
    }
}