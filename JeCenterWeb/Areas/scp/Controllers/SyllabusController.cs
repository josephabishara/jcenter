using JeCenterWeb.Data;
using JeCenterWeb.Models;
using JeCenterWeb.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JeCenterWeb.Areas.scp.Controllers
{
    [Area("scp")]
    [Authorize(Roles = "AdminSettings,PowerUser")]
    public class SyllabusController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        string ControllerName = "المناهج";
        string AppName = "الأدمن";

        public SyllabusController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: scp/Syllabus
        public async Task<IActionResult> Index()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var applicationDbContext = _context.CSyllabus.Include(c => c.CPhases);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: scp/Syllabus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            if (id == null || _context.CSyllabus == null)
            {
                return NotFound();
            }

            var cSyllabus = await _context.CSyllabus
                .Include(c => c.CPhases)
                .FirstOrDefaultAsync(m => m.SyllabusID == id);
            if (cSyllabus == null)
            {
                return NotFound();
            }

            return View(cSyllabus);
        }

        // GET: scp/Syllabus/Create
        public IActionResult Create()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["PhaseID"] = new SelectList(_context.CPhases, "PhaseId", "PhaseName");
            return View();
        }

        // POST: scp/Syllabus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("imgurl,SyllabusName,PhaseID")] CSyllabusViewModel model)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            string imgurlpath = "images/Syllabus/default.png";
            if (model.imgurl != null)
            {
                string folder = "images/Syllabus/";
                folder += Guid.NewGuid().ToString() + "-" + model.imgurl.FileName;
                imgurlpath = folder;
                string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                await model.imgurl.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
            }

            if (ModelState.IsValid)
            {
                CSyllabus cSyllabus = new CSyllabus
                {
                    imgurl = imgurlpath ,
                    SyllabusName = model.SyllabusName,
                    PhaseID = model.PhaseID,
                };

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                cSyllabus.CreateId = Convert.ToInt32(userId);
                cSyllabus.UpdateId = Convert.ToInt32(userId);
                cSyllabus.DeleteId = Convert.ToInt32(userId);
                cSyllabus.UpdatedDate = DateTime.Now;
                cSyllabus.CreatedDate = DateTime.Now;
                cSyllabus.DeletedDate = DateTime.Now;
                cSyllabus.Active = true;
                _context.Add(cSyllabus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PhaseID"] = new SelectList(_context.CPhases, "PhaseId", "PhaseName");
            return View(model);
        }

        // GET: scp/Syllabus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            if (id == null || _context.CSyllabus == null)
            {
                return NotFound();
            }

            var cSyllabus = await _context.CSyllabus.FindAsync(id);
            if (cSyllabus == null)
            {
                return NotFound();
            }
            ViewData["PhaseID"] = new SelectList(_context.CPhases, "PhaseId", "PhaseName", cSyllabus.PhaseID);
            return View(cSyllabus);
        }

        // POST: scp/Syllabus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SyllabusID,SyllabusName,PhaseID")] CSyllabus cSyllabus)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            if (id != cSyllabus.SyllabusID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    cSyllabus.UpdateId = Convert.ToInt32(userId);
                    cSyllabus.UpdatedDate = DateTime.Now;
                    cSyllabus.Active = true;
                    _context.Update(cSyllabus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CSyllabusExists(cSyllabus.SyllabusID))
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
            ViewData["PhaseID"] = new SelectList(_context.CPhases, "PhaseId", "PhaseName", cSyllabus.PhaseID);
            return View(cSyllabus);
        }



        // POST: scp/Syllabus/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.CSyllabus == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CSyllabus'  is null.");
            }
            var cSyllabus = await _context.CSyllabus.FindAsync(id);
            if (cSyllabus != null)
            {
                _context.CSyllabus.Remove(cSyllabus);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CSyllabusExists(int id)
        {
            return _context.CSyllabus.Any(e => e.SyllabusID == id);
        }
    }
}
