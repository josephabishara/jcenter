using JeCenterWeb.Data;
using JeCenterWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JeCenterWeb.Components
{
    public class LabelViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public LabelViewComponent(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            ViewData["FullName"] = user.FullName;
            int accountId = user.AccountID;
            var balance = await _context.UsersBalance.Where(u => u.Id == accountId).FirstOrDefaultAsync();
            ViewData["Balance"] = balance.Balance;
            ViewData["img"] = user.imgurl;
            return View();
        }

    }
}
