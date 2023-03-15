using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using QuanLyNV.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace QuanLyNV.Areas.Admin.Pages.Role
{
    public class DeleteUserModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        [BindProperty]
        public UserDto UserDto { get; set; }
        private readonly IWebHostEnvironment _env;
        public DeleteUserModel(UserManager<AppUser> userManager,IWebHostEnvironment env)
        {
            
            _userManager = userManager;
            _env = env;
        }

        public class InputModel
        {
            [Required]
            public string ID { set; get; }
            public string UserName { set; get; }

        }

        [BindProperty]
        public InputModel Input { set; get; }

        [BindProperty]
        public bool IsConfirmed { set; get; }

        [TempData] // Sử dụng Session
        public string StatusMessage { get; set; }



        public async Task<IActionResult> OnPost()
        {

            if (!ModelState.IsValid)
            {
                return NotFound("Không xóa được");
            }

            var user = await _userManager.FindByIdAsync(Input.ID);
            if (user == null)
            {
                return NotFound("Không thấy user cần xóa");
            }

            //if (UserDto.Avatar != null)
            //{
            //    string ExitingFile = Path.Combine(_env.WebRootPath, "picture", UserDto.Avatar);
            //    System.IO.File.Delete(ExitingFile);
            //}

            ModelState.Clear();

            if (IsConfirmed)
            {
                //Xóa
                await _userManager.DeleteAsync(user);
                StatusMessage = "Đã xóa " + user.UserName;

                return RedirectToPage("User");
            }
            else
            {
                Input.UserName = user.UserName;
                IsConfirmed = true;

            }

            return Page();
        }

       
    }
}