using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JeCenterWeb.Data;
using JeCenterWeb.Models;
namespace JeCenterWeb.Components
{
    [ViewComponent(Name = "BlogsHome")]
    public class BlogsHomeViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public BlogsHomeViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        // WelcomeListViewComponent
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<CBlogs> modellist = await _context.CBlogs
                .Where(b => b.Active == true)
                .Take(6)
                .OrderByDescending(b=>b.BlogsId)
                .ToListAsync();
            return View(modellist);
        }
    }
}
