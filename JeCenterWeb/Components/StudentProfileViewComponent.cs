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
    [ViewComponent(Name = "StudentProfile")]
    public class StudentProfileViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentProfileViewComponent(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            var student = await _context.Users.FindAsync(user.Id);
           

            ViewData["UserName"] = student.FullName;
            ViewData["imgurlprofile"] = student.imgurl;
           return View();
        }
    }
}
