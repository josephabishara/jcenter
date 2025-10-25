using JeCenterWeb.Data;
using JeCenterWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JeCenterWeb.Areas.scp.Controllers
{
    [Area("scp")]
    [Authorize(Roles = "AdminSettings")]
    public class PhysicalYearsController : Controller
    {
        string ControllerName = "السنوات الدراسية";
        string AppName = "الأدمن";
        private readonly ApplicationDbContext _context;

        public PhysicalYearsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: scp/PhysicalYears
        public async Task<IActionResult> Index()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            return View(await _context.PhysicalYear.ToListAsync());
        }

        // GET: scp/PhysicalYears/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            if (id == null || _context.PhysicalYear == null)
            {
                return NotFound();
            }

            var physicalYear = await _context.PhysicalYear
                .FirstOrDefaultAsync(m => m.PhysicalyearId == id);
            if (physicalYear == null)
            {
                return NotFound();
            }

            return View(physicalYear);
        }

        // GET: scp/PhysicalYears/Create
        public IActionResult Create()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            return View();
        }

        // POST: scp/PhysicalYears/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhysicalyearId,PhysicalyearName,FromDate,ToDate,Writed,Deleted,Active,CreateId,CreatedDate,UpdateId,UpdatedDate,DeleteId,DeletedDate")] PhysicalYear physicalYear)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                physicalYear.CreateId = Convert.ToInt32(userId);
                physicalYear.UpdateId = Convert.ToInt32(userId);
                physicalYear.DeleteId = Convert.ToInt32(userId);
                physicalYear.UpdatedDate = DateTime.Now;
                physicalYear.CreatedDate = DateTime.Now;
                physicalYear.DeletedDate = DateTime.Now;
                physicalYear.Active = true;

                _context.Add(physicalYear);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(physicalYear);
        }

        // GET: scp/PhysicalYears/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            if (id == null || _context.PhysicalYear == null)
            {
                return NotFound();
            }

            var physicalYear = await _context.PhysicalYear.FindAsync(id);
            if (physicalYear == null)
            {
                return NotFound();
            }
            return View(physicalYear);
        }

        // POST: scp/PhysicalYears/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PhysicalyearId,PhysicalyearName,FromDate,ToDate,Writed,Deleted,Active,CreateId,CreatedDate,UpdateId,UpdatedDate,DeleteId,DeletedDate")] PhysicalYear physicalYear)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            if (id != physicalYear.PhysicalyearId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    physicalYear.UpdateId = Convert.ToInt32(userId);
                    physicalYear.UpdatedDate = DateTime.Now;
                    physicalYear.Active = true;
                    _context.Update(physicalYear);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhysicalYearExists(physicalYear.PhysicalyearId))
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
            return View(physicalYear);
        }



        // POST: scp/PhysicalYears/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    ViewData["AppName"] = AppName;
        //    ViewData["ControllerName"] = ControllerName;
        //    if (_context.PhysicalYear == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.PhysicalYear'  is null.");
        //    }
        //    var physicalYear = await _context.PhysicalYear.FindAsync(id);
        //    if (physicalYear != null)
        //    {
        //        _context.PhysicalYear.Remove(physicalYear);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        // POST: scp/PhysicalYears/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Change(int id)
        {
            
            var physicalYear = await _context.PhysicalYear.FindAsync(id);
            if (physicalYear.Active == true)
            {
                physicalYear.Active = false;
            }
            else if (physicalYear.Active == false)
            {
                physicalYear.Active = true;
            }
            _context.PhysicalYear.Update(physicalYear);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhysicalYearExists(int id)
        {
            return _context.PhysicalYear.Any(e => e.PhysicalyearId == id);
        }
    }
}
