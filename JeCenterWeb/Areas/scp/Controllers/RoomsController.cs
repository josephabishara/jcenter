using JeCenterWeb.Data;
using JeCenterWeb.Models;
using JeCenterWeb.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JeCenterWeb.Areas.scp.Controllers
{
    [Area("scp")]
    [Authorize(Roles = "AdminSettings,PowerUser")]
    public class RoomsController : Controller
    {
        private readonly ApplicationDbContext _context;
        string ControllerName = "القاعات";
        string AppName = "الأدمن";

        public RoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: scp/Rooms
        public async Task<IActionResult> Index()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var applicationDbContext = _context.CRooms.Include(c => c.CBranch);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> branch(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var applicationDbContext = await _context.CRooms
                .Where(r=>r.BranchId == id)
                .Include(c => c.CBranch)
                .ToListAsync();
            return View( applicationDbContext);
        }


        // GET: scp/Rooms/Create
        public IActionResult Create()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["BranchId"] = new SelectList(_context.CBranch, "BranchId", "BranchName");
            return View();
        }

        // POST: scp/Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomId,RoomName,Capacity,Floor,BranchId,Writed,Deleted,Active,CreateId,CreatedDate,UpdateId,UpdatedDate,DeleteId,DeletedDate")] CRooms cRooms)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                cRooms.CreateId = Convert.ToInt32(userId);
                cRooms.UpdateId = Convert.ToInt32(userId);
                cRooms.DeleteId = Convert.ToInt32(userId);
                cRooms.UpdatedDate = DateTime.Now;
                cRooms.CreatedDate = DateTime.Now;
                cRooms.DeletedDate = DateTime.Now;
                cRooms.Active = true;
                _context.Add(cRooms);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchId"] = new SelectList(_context.CBranch, "BranchId", "BranchName", cRooms.BranchId);
            return View(cRooms);
        }

        // GET: scp/Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            if (id == null || _context.CRooms == null)
            {
                return NotFound();
            }

            var cRooms = await _context.CRooms.FindAsync(id);
            if (cRooms == null)
            {
                return NotFound();
            }
            ViewData["BranchId"] = new SelectList(_context.CBranch, "BranchId", "BranchName", cRooms.BranchId);
            return View(cRooms);
        }

        // POST: scp/Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomId,RoomName,Capacity,Floor,BranchId,Writed,Deleted,Active,CreateId,CreatedDate,UpdateId,UpdatedDate,DeleteId,DeletedDate")] CRooms cRooms)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            if (id != cRooms.RoomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


                    cRooms.UpdateId = Convert.ToInt32(userId);
                    cRooms.UpdatedDate = DateTime.Now;
                    cRooms.Active = true;
                    _context.Update(cRooms);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CRoomsExists(cRooms.RoomId))
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
            ViewData["BranchId"] = new SelectList(_context.CBranch, "BranchId", "BranchName", cRooms.BranchId);
            return View(cRooms);
        }



        // POST: scp/Rooms/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.CRooms == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CRooms'  is null.");
            }
            var cRooms = await _context.CRooms.FindAsync(id);
            if (cRooms != null)
            {
                _context.CRooms.Remove(cRooms);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CRoomsExists(int id)
        {
            return _context.CRooms.Any(e => e.RoomId == id);
        }


        // Schedule
        public async Task<IActionResult> Schedule()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            List<RoomScheduleViewModel> roomSchedule = new List<RoomScheduleViewModel>();
            var rooms = await _context.CRooms.ToListAsync();

            foreach (var item in rooms)
            {
                RoomScheduleViewModel model = new RoomScheduleViewModel();

                model.Room = item;
                //model.GroupSchedule = await _context.CGroupSchedule
                //    .Where(g => g.CGroups.RoomId == item.RoomId && g.LectureDate == DateTime.Today)
                //    .OrderBy(g => g.CGroups.SessionStart)
                //    .Include(g => g.CGroups)
                //    .ToListAsync();

                roomSchedule.Add(model);
            }
            return View(roomSchedule);
        }
    }
}
