using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLyNV.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace QuanLyNV.Areas.Admin.Pages.Role
{
 
    public class IndexModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public IndexModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public List<IdentityRole> Roles {set; get;}

        [TempData] // Sử dụng Session lưu thông báo
        public string StatusMessage { get; set; }
 
        public async Task<IActionResult> OnGet()
        {
            Roles =  await _roleManager.Roles.ToListAsync();
            return Page();
        }
    }
}
