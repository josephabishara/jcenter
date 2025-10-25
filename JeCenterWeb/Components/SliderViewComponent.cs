using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JeCenterWeb.Data;
using JeCenterWeb.Models;

namespace JeCenterWeb.Components
{
    [ViewComponent(Name = "SliderView")]
    public class SliderViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public SliderViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<CSlider> modellist =await _context.CSlider.ToListAsync();
            return View(modellist);
        }
    }
}
