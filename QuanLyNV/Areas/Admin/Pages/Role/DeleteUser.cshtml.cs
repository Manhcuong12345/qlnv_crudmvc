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

namespace Album.Areas.Admin.Pages.Role
{
    public class DeleteUserModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;

        public DeleteUserModel(UserManager<AppUser> userManager)
        {
            
            _userManager = userManager;
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
        public bool isConfirmed { set; get; }

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
                return NotFound("Không thấy role cần xóa");
            }

            ModelState.Clear();

            if (isConfirmed)
            {
                //Xóa
                await _userManager.DeleteAsync(user);
                StatusMessage = "Đã xóa " + user.UserName;

                return RedirectToPage("User");
            }
            else
            {
                Input.UserName = user.UserName;
                isConfirmed = true;

            }

            return Page();
        }

       
    }
}