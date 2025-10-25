using JeCenterWeb.Controllers;
using JeCenterWeb.Data;
using JeCenterWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JeCenterWeb.Areas.learning.Controllers
{
    [Area("learning")]
    [Authorize(Roles = "Student")]
    public class ApplicationsController : Controller
    {
        string ControllerName = " الاشتراكات  ";
        string AppName = "السنتر";
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public ApplicationsController(ApplicationDbContext context,
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
            int studentId = Convert.ToInt32(userId);
            var apps = await _context.StudentApplications
                .Where(a => a.StudentId == studentId)
                .Include(a => a.PhysicalYear)
                .Include(a => a.CPhases)
                .ToListAsync();

            int iapp =  apps.Count();
            if (iapp == 0)
            {
                ViewData["mssg"] = "لا توجد أشتراكات لهذا الطالب , أشترك في سنة دراسية لرؤية المناهج والمدرسين ";
            }
            else
            {
                ViewData["mssg"] = "أختار السنة الدراسية ";
            }
            return View(apps);
        }

        public async Task<IActionResult> Subscribe()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = "أشتراك في سنة دراسية جديدة";
            ViewData["BranchId"] = new SelectList(_context.CBranch, "BranchId", "BranchName");
            ViewData["PhysicalyearId"] = new SelectList(_context.PhysicalYear.Where(p => p.Active == true), "PhysicalyearId", "PhysicalyearName");
            ViewData["PhaseId"] = new SelectList(_context.CPhases.Where(p => p.Parent > 0 && p.Active == true), "PhaseId", "PhaseName");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser student = await _userManager.FindByIdAsync(userId);
            ViewData["UserName"] = student.FristName;
            var userbalance = await _context.UsersBalance.FindAsync(student.AccountID);
            ViewData["balance"] = userbalance.Balance * -1;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe([Bind("StudentId,PhysicalyearId,PhaseId")] StudentApplications application)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            application.StudentId = Convert.ToInt32(userId);
            application.Paided = false;
            application.DocNo = 0;
            application.CreateId = uid;
            application.CreatedDate = DateTime.Now;


            if (ModelState.IsValid)
            {
                var sub = await _context.StudentApplications
                    .Where(a => a.StudentId == application.StudentId && a.PhaseId == application.PhaseId)
                    .FirstOrDefaultAsync();
                if (sub == null)
                {
                    _context.Add(application);
                    await _context.SaveChangesAsync();
                    return RedirectToRoute(new { controller = "Applications", action = "Index", area = "learning" });
                }
                else
                {
                    var user = await _context.Users.FindAsync(application.StudentId);
                    if (user.UserTypeId != 7)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    ViewData["FullName"] = user.FullName;
                    ViewData["PhysicalyearId"] = new SelectList(_context.PhysicalYear.Where(p => p.Active == true), "PhysicalyearId", "PhysicalyearName", application.PhysicalyearId);
                    ViewData["PhaseId"] = new SelectList(_context.CPhases.Where(p => p.Parent > 0 && p.Active == true), "PhaseId", "PhaseName", application.PhaseId);
                    ViewData["message"] = "انت مسجل في في هذه المرحلة";
                    return View(application);
                }
            }
            return View();
        }
        
        public async Task<IActionResult> ChangePhysicalYear(int id)
        {
           
            var studentapp = await _context.StudentApplications.Where(s => s.ApplicationId == id).FirstOrDefaultAsync();
            HttpContext.Session.SetInt32(AccountController.SessionKeyPhase, studentapp.PhaseId);
            HttpContext.Session.SetInt32(AccountController.SessionKeyPhysicalyear, studentapp.PhysicalyearId);
            return RedirectToRoute(new { controller = "Dashboard", action = "Index", area = "learning" });
        }

    }
}
