using JeCenterWeb.Data;
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
    public class ScheduleController : Controller
    {
        string ControllerName = " الجدول ";
        string AppName = "السنتر";
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ScheduleController(ApplicationDbContext context,
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
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
            int studentId = Convert.ToInt32(userId);
            int? PhaseId = _httpContextAccessor.HttpContext.Session.GetInt32("_phase");
            if (PhaseId == 0 || PhaseId == null)
            {
                return RedirectToRoute(new { controller = "Applications", action = "Index", area = "learning" });
            }
            var groups = await _context.StudentGroup
                .Where(g => g.StudentId == studentId && g.CGroups.CSyllabus.PhaseID == PhaseId)
                .Include(g => g.CGroups)
                .Include(g => g.CGroups.ResourceDays)
                .Include(g => g.CGroups.CRooms)
                .ToListAsync();
            return View(groups);
        }
    }
}
