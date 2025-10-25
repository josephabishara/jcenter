using JeCenterWeb.Data;
using JeCenterWeb.Migrations;
using JeCenterWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JeCenterWeb.Areas.learning.Controllers
{
    [Area("learning")]
    [Authorize(Roles = "Student")]
    public class NotificationsController : Controller
    {
        string ControllerName = "الاشعارات ";
        string AppName = "السنتر";
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public NotificationsController(ApplicationDbContext context,
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser student = await _userManager.FindByIdAsync(userId);
            ViewData["UserName"] = student.FristName;
            var userbalance = await _context.UsersBalance.FindAsync(student.AccountID);
            ViewData["balance"] = userbalance.Balance * -1;

            var modellist = await _context.StudentNotifications
              .Where(t => t.StudentId == student.Id)
              .Include(t => t.GNotifications)
              .ToListAsync();
            return View(modellist);
        }

        public async Task<IActionResult> ReadNotification(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);

            var model = await _context.StudentNotifications
                .Where(t => t.StudentNotificationId == id)
                .Include(t => t.GNotifications)
                .FirstOrDefaultAsync();
            model.Seen = true;
            model.UpdateId = uid;
            model.UpdatedDate = DateTime.Now;

            _context.Update(model);
            await _context.SaveChangesAsync();


            return View(model);
        }
    }
}
