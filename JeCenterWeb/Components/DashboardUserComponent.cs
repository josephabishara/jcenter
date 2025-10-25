using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JeCenterWeb.Data;
using JeCenterWeb.Models;
using System.Security.Claims;

namespace ShareKiteWeb.Components
{
    [ViewComponent(Name = "DashboardUser")]
    public class DashboardUserComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardUserComponent(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            var usera = await _context.Users.FindAsync(user.Id);
             if (usera != null)
            {
                ViewData["FullName"] = usera.FullName;
                ViewData["img"] = usera.imgurl;
                ViewData["Mobile"] = usera.Mobile;
                ViewData["Email"] = usera.UserName;
            }
            else
            {
                ViewData["FirstName"] = "N/A ";
                ViewData["img"] = "images/Employees/default.jpg";
            }
            return View();
        }
    }
}
