using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JeCenterWeb.Data;
using JeCenterWeb.Models;
using JeCenterWeb.Models.ViewModels;
using System.Security.Claims;



namespace Josephabishara.Areas.CP.Controllers
{
    [Area("scp")]
    [AllowAnonymous]

    public class SliderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        string ControllerName = "Slider";

        public SliderController(ApplicationDbContext context,
             IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Controller"] = ControllerName;
            ViewData["Controller"] = "Slider";
            return _context.CSlider != null ?
                       View(await _context.CSlider.Where(s => s.Active == true).ToListAsync()) :
                       Problem("Entity set 'ApplicationDbContext.AboutWelcome'  is null.");
        }

        public IActionResult Create()
        {
            ViewData["Controller"] = ControllerName;
            return View();
        }

        // POST: CP/Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Titel,SubTitel,SliderImg,Url,BtnName")] CPSlider cPSlider)
        {
            ViewData["Controller"] = ControllerName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {
                string imgurlpath = "images/slider/default.jpg";
                if (cPSlider.SliderImg != null)
                {
                    string folder = "images/slider/";
                    folder += Guid.NewGuid().ToString() + "-" + cPSlider.SliderImg.FileName;
                    imgurlpath = folder;
                    string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                    await cPSlider.SliderImg.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
                }
                CSlider slider = new CSlider
                {
                    Titel = cPSlider.Titel,
                    SubTitel = cPSlider.SubTitel,
                    SliderImg = imgurlpath,
                    Url = cPSlider.Url,
                    BtnName = cPSlider.BtnName,
                };


                slider.CreateId = Convert.ToInt32(userId);
                //slider.UpdateId = user.Id;
                //slider.DeleteId = user.Id;
                //slider.UpdatedDate = DateTime.Now;
                slider.CreatedDate = DateTime.Now;
                //slider.DeletedDate = DateTime.Now;
                slider.Active = true;
                _context.Add(slider);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }


        // GET: CP/Slider/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Controller"] = ControllerName;
            if (id == null || _context.CSlider == null)
            {
                return NotFound();
            }


            var slider = await _context.CSlider.FindAsync(id);
            CPSlider cPSlider = new CPSlider
            {
                SliderId = slider.SliderId,
                Titel = slider.Titel,
                SubTitel = slider.SubTitel,
                Url = slider.Url,
                BtnName = slider.BtnName,

            };
            if (slider == null)
            {
                return NotFound();
            }
            return View(cPSlider);
        }

        // POST: CP/Slider/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SliderId,Titel,SubTitel,Url,BtnName,SliderImg")] CPSlider cPSlider)
        {
            ViewData["Controller"] = ControllerName;
            if (id != cPSlider.SliderId)
            {
                return NotFound();
            }
            CSlider slide = await _context.CSlider.FindAsync(id);
            string imgurlpath = "images/slider/default.jpg";
            if (cPSlider.SliderImg != null)
            {
                string folder = "images/slider/";
                folder += Guid.NewGuid().ToString() + "-" + cPSlider.SliderImg.FileName;
                imgurlpath = folder;
                string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                await cPSlider.SliderImg.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
                slide.SliderImg = imgurlpath;
            }



            slide.Titel = cPSlider.Titel;
            slide.SubTitel = cPSlider.SubTitel;
            slide.Url = cPSlider.Url;
            slide.BtnName = cPSlider.BtnName;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var user = await _context.Users.FindAsync(userId);
            slide.UpdateId =  Convert.ToInt32(userId); 
            slide.UpdatedDate = DateTime.Now;
            //slide.Active = true;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.CSlider.Update(slide);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SliderExists(slide.SliderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(cPSlider);
        }

        // GET: CP/slide/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["Controller"] = ControllerName;
            if (id == null || _context.CSlider == null)
            {
                return NotFound();
            }

            var slider = await _context.CSlider
                .FirstOrDefaultAsync(m => m.SliderId == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // POST: CP/Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewData["Controller"] = ControllerName;
            if (_context.CSlider == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Slider'  is null.");
            }
            CSlider slider = await _context.CSlider.FindAsync(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
             
            slider.DeleteId = Convert.ToInt32(userId); 
            slider.DeletedDate = DateTime.Now;
            slider.Active = false;
            if (slider != null)
            {
                _context.CSlider.Update(slider);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SliderExists(int id)
        {
            return (_context.CSlider?.Any(e => e.SliderId == id)).GetValueOrDefault();
        }
    }
}
