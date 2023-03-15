using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyNV.Data;
using QuanLyNV.Dto;
using QuanLyNV.Models;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyNV.Controllers
{
    public class TicketController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public TicketController(AppDbContext context, UserManager<AppUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        ////http://localhost:5000/Users/app/user
        //[HttpGet("/app/user")]
        //Parameter là các biến được sử dụng trong một hàm mà giá trị của biến đó được cung cấp bởi lời gọi hàm
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tickets = await _context.Ticket.OrderByDescending(m => m.Id).ToListAsync();
            return View(tickets);
        }

        [HttpGet]
        public async Task<IActionResult> AddTicket()
        {
            var tickets = new Ticket { };
            return View("TicketForm", tickets);
        }

        [HttpPost]
        public async Task<IActionResult> AddTicket(Ticket model)
        {
            if (!ModelState.IsValid)
            {
                return View("TicketForm", model);
            }

            //var tickets = new Ticket
            //{
            //    Title = model.Title,
            //    Description = model.Description,
            //    CreatedDate = DateTime.Now,
            //    State = model.State
            //};

            //Mapper
            var tickets = _mapper.Map<Ticket>(model);
            tickets.CreatedDate= DateTime.Now;

            tickets.OwnerId = _userManager.GetUserName(User);

            _context.Ticket.Add(tickets);
            _context.SaveChanges();

            //_toastNotification.AddSuccessToastMessage("Movie created successfully");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Ticket model)
        {
            var tickets = await _context.Ticket.FindAsync(model.Id);

            if (tickets == null)
                return NotFound();

            tickets.Title = model.Title;
            tickets.Description = model.Description;
            tickets.State = model.State;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //Xóa
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var tickets = await _context.Ticket.FirstOrDefaultAsync(x => x.Id == id);

            if (tickets == null)
                return NotFound();

            _context.Ticket.Remove(tickets);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
