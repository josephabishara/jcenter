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
    public class BalanceController : Controller
    {
        string ControllerName = " محفظتي ";
        string AppName = "السنتر";
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public BalanceController(ApplicationDbContext context,
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
           
            // Get User ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int studentId = Convert.ToInt32(userId);

            ApplicationUser student = await _userManager.FindByIdAsync(userId);
            ViewData["UserName"] = student.FristName + " " + student.SecondName;
            var balance = await _context.UsersBalance.FindAsync(student.AccountID);
            ViewData["balance"] = balance.Balance * -1;
                 var journalEntryLine = await _context.FinancialJournalEntryLine
                .Where(f => f.AccountID == student.AccountID)
                .OrderByDescending(l=>l.JournalEntryDetilsID)
                .Include(f=>f.FinancialDocuments)
                .ToListAsync();

            int iapp = journalEntryLine.Count();
            if (iapp == 0)
                ViewData["mssg"] = "  ليس هناك حركة على هذا الحساب ,قم بالشحن على الهواء أو بكارت لأمكانية اشتراك في مجموعة ";
            return View(journalEntryLine);
        }
    }
}
