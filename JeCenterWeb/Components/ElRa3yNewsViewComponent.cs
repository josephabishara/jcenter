using JeCenterWeb.Data;
using JeCenterWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace JeCenterWeb.Components
{
    public class ElRa3yNewsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public ElRa3yNewsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ElRa3yNews> elRa3yNews = _context.ElRa3yNews.ToList();

            return View(elRa3yNews);
        }
    }
}
