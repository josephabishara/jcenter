using JeCenterWeb.Data;
using JeCenterWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JeCenterWeb.Areas.scp.Controllers
{
    [Area("scp")]
    [Authorize(Roles = "AdminSettings,PowerUser")]
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        string ControllerName = " الإدارات";
        string AppName = "الأدمن";
        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: scp/Departments
        public async Task<IActionResult> Index()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            return View(await _context.CDepartment.ToListAsync());
        }



        // GET: scp/Departments/Create
        public IActionResult Create()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            return View();
        }

        // POST: scp/Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentId,DepartmentName,Writed,Deleted,Active,CreateId,CreatedDate,UpdateId,UpdatedDate,DeleteId,DeletedDate")] CDepartment cDepartment)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                cDepartment.CreateId = Convert.ToInt32(userId);
                cDepartment.UpdateId = Convert.ToInt32(userId);
                cDepartment.DeleteId = Convert.ToInt32(userId);
                cDepartment.UpdatedDate = DateTime.Now;
                cDepartment.CreatedDate = DateTime.Now;
                cDepartment.DeletedDate = DateTime.Now;
                cDepartment.Active = true;
                _context.Add(cDepartment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cDepartment);
        }

        // GET: scp/Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            if (id == null || _context.CDepartment == null)
            {
                return NotFound();
            }

            var cDepartment = await _context.CDepartment.FindAsync(id);
            if (cDepartment == null)
            {
                return NotFound();
            }
            return View(cDepartment);
        }

        // POST: scp/Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartmentId,DepartmentName,Writed,Deleted,Active,CreateId,CreatedDate,UpdateId,UpdatedDate,DeleteId,DeletedDate")] CDepartment cDepartment)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            if (id != cDepartment.DepartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    cDepartment.UpdateId = Convert.ToInt32(userId);
                    cDepartment.UpdatedDate = DateTime.Now;
                    cDepartment.Active = true;
                    _context.Update(cDepartment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CDepartmentExists(cDepartment.DepartmentId))
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
            return View(cDepartment);
        }



        // POST: scp/Departments/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            if (_context.CDepartment == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CDepartment'  is null.");
            }
            var cDepartment = await _context.CDepartment.FindAsync(id);
            if (cDepartment != null)
            {
                _context.CDepartment.Remove(cDepartment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CDepartmentExists(int id)
        {
            return _context.CDepartment.Any(e => e.DepartmentId == id);
        }
    }
}
