using JeCenterWeb.Data;
using JeCenterWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JeCenterWeb.Components
{
    public class MenuAccessRightViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MenuAccessRightViewComponent(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            var usera = await _context.Users.FindAsync(user.Id);

            var menuAccessRights = await _context.UserAccessRight
                .Where(ur => ur.UserId == user.Id  && ur.AccessRight.MenuOrder == 1)
                .Include(ur => ur.AccessRight)
                .ToListAsync();
            return View(menuAccessRights);

        }
    }
}
