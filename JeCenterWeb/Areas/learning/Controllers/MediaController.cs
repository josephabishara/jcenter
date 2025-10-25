using JeCenterWeb.Data;
using JeCenterWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JeCenterWeb.Areas.learning.Controllers
{
    [Area("learning")]
    [Authorize(Roles = "Student")]
    public class MediaController : Controller
    {
        string ControllerName = " الميديا ";
        string AppName = "السنتر";
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public MediaController(ApplicationDbContext context,
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
            return View();
        }
    }
}
