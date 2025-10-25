using JeCenterWeb.Data;
using JeCenterWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JeCenterWeb.Components
{
    [ViewComponent(Name = "NotificationNav")]
    public class NotificationNavComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public NotificationNavComponent(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            var usera = await _context.Users.FindAsync(user.Id);
            var notiuser = await _context.StudentNotifications
                 .Where(n => n.StudentId == usera.Id && n.Seen == false)
                 .ToListAsync();


            if (notiuser.Count > 0)
            {
                ViewData["notimessg"] = " يوجد " + notiuser.Count.ToString() + " إشعارات ";
                ViewData["noticount"] = notiuser.Count;
            }
            else
            {
                ViewData["notimessg"] = " لا يوجد إشعارات جديدة";
                ViewData["noticount"] = notiuser.Count;
            }
            return View();
        }
    }
}
