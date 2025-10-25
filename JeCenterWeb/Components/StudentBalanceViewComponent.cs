using JeCenterWeb.Data;
using JeCenterWeb.Migrations;
using JeCenterWeb.Models;
using JeCenterWeb.Models.Second;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace JeCenterWeb.Components
{
    [ViewComponent(Name = "StudentBalance")]
    public class StudentBalanceViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentBalanceViewComponent(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            var student = await _context.Users.FindAsync(user.Id);
            var balance = await _context.UsersBalance.FindAsync(student.AccountID);
            ViewData["balance"] = balance.Balance * -1;

            return View();
        }
    }
}
