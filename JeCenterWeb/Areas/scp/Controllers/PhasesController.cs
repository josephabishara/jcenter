using JeCenterWeb.Data;
using JeCenterWeb.Models;
using JeCenterWeb.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JeCenterWeb.Areas.scp.Controllers
{
    [Area("scp")]
    [Authorize(Roles = "AdminSettings,PowerUser")]
    public class PhasesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        string ControllerName = "المراحل";
        string AppName = "الأدمن";
        public PhasesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: scp/Phases
        public async Task<IActionResult> Index()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            return View(await _context.CPhases.Where(p => p.Parent == 0).ToListAsync());
        }

        public async Task<IActionResult> SubPhase(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            return View(await _context.CPhases.Where(p => p.Parent == id).ToListAsync());
        }

        // GET: scp/Phases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            if (id == null || _context.CPhases == null)
            {
                return NotFound();
            }
            PhaseSyllabusViewModel phaseViewModel = new PhaseSyllabusViewModel();
            phaseViewModel.CPhases = await _context.CPhases
                .FirstOrDefaultAsync(m => m.PhaseId == id);
            phaseViewModel.CSyllabus = await _context.CSyllabus
                .Where(s => s.PhaseID == id)
                .ToListAsync();

            return View(phaseViewModel);
        }

        // GET: scp/Phases/Create
        public IActionResult Create()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            return View();
        }

        // POST: scp/Phases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhaseName,ApplicationValue,imgurl")] PhasesViewModel Phases)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            if (ModelState.IsValid)
            {

                string imgurlpath = "images/Phases/default.png";
                if (Phases.imgurl != null)
                {
                    string folder = "images/Phases/";
                    folder += Guid.NewGuid().ToString() + "-" + Phases.imgurl.FileName;
                    imgurlpath = folder;
                    string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                    await Phases.imgurl.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
                }

                CPhases cPhases = new CPhases
                {
                    PhaseName = Phases.PhaseName,
                    ApplicationValue = Phases.ApplicationValue,
                    imgurl = imgurlpath,
                    Parent = 0,
                };

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                cPhases.CreateId = Convert.ToInt32(userId);
                cPhases.UpdateId = Convert.ToInt32(userId);
                cPhases.DeleteId = Convert.ToInt32(userId);
                cPhases.UpdatedDate = DateTime.Now;
                cPhases.CreatedDate = DateTime.Now;
                cPhases.DeletedDate = DateTime.Now;
                cPhases.Active = true;

                _context.Add(cPhases);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Phases);
        }

        // GET: scp/Phases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            if (id == null || _context.CPhases == null)
            {
                return NotFound();
            }

            var cPhases = await _context.CPhases.FindAsync(id);
            if (cPhases == null)
            {
                return NotFound();
            }
            return View(cPhases);
        }

        // POST: scp/Phases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PhaseId,PhaseName,ApplicationValue")] CPhases cPhases)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            if (id != cPhases.PhaseId)
            {
                return NotFound();
            }
            CPhases model = await _context.CPhases.FindAsync(cPhases.PhaseId);
            model.PhaseName = cPhases.PhaseName;
            model.ApplicationValue = cPhases.ApplicationValue;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            model.UpdateId = Convert.ToInt32(userId);
            model.UpdatedDate = DateTime.Now;
            model.Active = true;


            try
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CPhasesExists(cPhases.PhaseId))
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

        // GET: scp/Phases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CPhases == null)
            {
                return NotFound();
            }

            var cPhases = await _context.CPhases
                .FirstOrDefaultAsync(m => m.PhaseId == id);
            if (cPhases == null)
            {
                return NotFound();
            }

            return View(cPhases);
        }

        // POST: scp/Phases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CPhases == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CPhases'  is null.");
            }
            var cPhases = await _context.CPhases.FindAsync(id);
            if (cPhases != null)
            {
                _context.CPhases.Remove(cPhases);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CPhasesExists(int id)
        {
            return _context.CPhases.Any(e => e.PhaseId == id);
        }


        // POST: scp/Syllabus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSyllabus([Bind("SyllabusID,SyllabusName,PhaseID,imgurl")] SyllabusViewModel Syllabus)
        {

            string imgurlpath = "images/Syllabus/default.png";
            if (Syllabus.imgurl != null)
            {
                string folder = "images/Syllabus/";
                folder += Guid.NewGuid().ToString() + "-" + Syllabus.imgurl.FileName;
                imgurlpath = folder;
                string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                await Syllabus.imgurl.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
            }

            CSyllabus cSyllabus = new CSyllabus
            {
                SyllabusName = Syllabus.SyllabusName,
                PhaseID = Syllabus.PhaseID,
                imgurl = imgurlpath,
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
            return RedirectToRoute(new { controller = "Phases", action = "Details", id = Syllabus.PhaseID });


        }

        [HttpPost]
        public async Task<IActionResult> ChangeState(int id)
        {
            CPhases? cPhases = await _context.CPhases.FindAsync(id);
            if (cPhases.Active == false)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                cPhases.UpdateId = Convert.ToInt32(userId);
                cPhases.UpdatedDate = DateTime.Now;
                cPhases.Active = true;
                _context.Update(cPhases);
                await _context.SaveChangesAsync();
                if (cPhases.Parent == 0)
                {
                    List<CPhases> sub = await _context.CPhases.Where(p => p.Parent == id).ToListAsync();
                    foreach (var item in sub)
                    {
                        item.UpdateId = Convert.ToInt32(userId);
                        item.UpdatedDate = DateTime.Now;
                        item.Active = true;
                        _context.Update(item);
                        await _context.SaveChangesAsync();
                    }
                }

            }
            else if (cPhases.Active == true)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                cPhases.UpdateId = Convert.ToInt32(userId);
                cPhases.UpdatedDate = DateTime.Now;
                cPhases.Active = false;
                _context.Update(cPhases);
                await _context.SaveChangesAsync();
                if (cPhases.Parent == 0)
                {
                    List<CPhases> sub = await _context.CPhases.Where(p => p.Parent == id).ToListAsync();
                    foreach (var item in sub)
                    {
                        item.Active = false;
                        _context.Update(item);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            if (cPhases.Parent == 0)
                return RedirectToAction(nameof(Index));
            else if (cPhases.Parent > 0)
                return RedirectToRoute(new { action = "SubPhase", id = cPhases.Parent });

            return RedirectToAction(nameof(Index));
        }

    }
}
