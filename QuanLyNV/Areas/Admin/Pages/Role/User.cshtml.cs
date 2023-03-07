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
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace QuanLyNV.Areas.Admin.Pages.Role {
    public class UserModel : PageModel {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<AppUser> _signManager;
       
        public UserModel (RoleManager<IdentityRole> roleManager,
                          UserManager<AppUser> userManager,
                          SignInManager<AppUser> signManager,
                          IMapper mapper) {
            _roleManager = roleManager;
            _userManager = userManager;
            _signManager = signManager;
            _mapper = mapper;
        }

        public List<UserDto> users;
        public int TotalPages {set; get;}

        [TempData] //Sử dụng Session
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet=true)]
        public int PageNumber {set;get;}

        public IActionResult OnPost() => NotFound("Cấm post");

        //Lấy thông tin cần hiển thị ra giao diện người dùng
        public async Task<IActionResult> OnGet() {
            var listUser = _userManager.Users.OrderByDescending(x => x.Id);
            const int User_Page = 5;

            int totalUsers = await listUser.CountAsync();
            TotalPages = (int)Math.Ceiling((double)totalUsers / User_Page);

            if (PageNumber < 1)
                PageNumber = 1;
            if (PageNumber > TotalPages)
                PageNumber = PageNumber;

            var queryData = listUser.Skip(User_Page * (PageNumber - 1)).Take(User_Page);
            //users = await queryData.ToListAsync();
            //Su dung Mapper 
            users = _mapper.Map<List<UserDto>>(queryData);

            return Page();
        }
    }
}