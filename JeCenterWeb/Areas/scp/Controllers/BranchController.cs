using JeCenterWeb.Data;
using JeCenterWeb.Models;
using JeCenterWeb.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JeCenterWeb.Areas.scp.Controllers
{
    [Area("scp")]
    [Authorize(Roles = "AdminSettings,PowerUser,SuperUser,User")]
  
    public class BranchController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        string ControllerName = "الفروع";
        string AppName = "الأدمن";

        public BranchController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: cs/Branch
        [Authorize(Roles = "AdminSettings,PowerUser")]
        public async Task<IActionResult> Index()
        {

            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var branches = await _context.CBranch.Include(b => b.CRooms).ToListAsync();
            return View(branches);
        }

        [HttpGet]
      
        public async Task<IActionResult> LogInBranch()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var user = await _userManager.FindByIdAsync(userId);
            user.online = 0;
            var result = _userManager.UpdateAsync(user).Result;


            var branches = await _context.CBranch.ToListAsync();
            return View(branches);
        }

        [HttpGet]
         
        public async Task<IActionResult> SignInBranch(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var user = await _userManager.FindByIdAsync(userId);
            user.online = id;
            var result = _userManager.UpdateAsync(user).Result;


            return RedirectToRoute(new { area = "scp", controller = "Dashboard", action = "Index" });
        }

        // GET: cs/Branch/Details/5
        [Authorize(Roles = "AdminSettings,PowerUser")]
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            if (id == null || _context.CBranch == null)
            {
                return NotFound();
            }
            CBranchViewModel Branchviewmodel = new CBranchViewModel();
            Branchviewmodel.CBranch = await _context.CBranch.FindAsync(id);
            Branchviewmodel.CRooms = await _context.CRooms.Where(r => r.BranchId == id).ToListAsync();

            return View(Branchviewmodel);
        }

        // GET: cs/Branch/Create
        [Authorize(Roles = "AdminSettings,PowerUser")]
        public IActionResult Create()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            return View();
        }

        // POST: cs/Branch/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AdminSettings,PowerUser")]
        public async Task<IActionResult> Create([Bind("BranchId,BranchName,RoomsCount,Writed,Deleted,Active,CreateId,CreatedDate,UpdateId,UpdatedDate,DeleteId,DeletedDate")] CBranch cBranch)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                cBranch.CreateId = Convert.ToInt32(userId);
                cBranch.UpdateId = Convert.ToInt32(userId);
                cBranch.DeleteId = Convert.ToInt32(userId);
                cBranch.UpdatedDate = DateTime.Now;
                cBranch.CreatedDate = DateTime.Now;
                cBranch.DeletedDate = DateTime.Now;
                cBranch.Active = true;
                _context.Add(cBranch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cBranch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AdminSettings,PowerUser")]
        public async Task<IActionResult> AddRoom([Bind("RoomId,RoomName,Capacity,Floor,BranchId")] CRooms cRooms)
        {
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


        // DeleteRoom

        // POST: scp/Rooms/Delete/5

        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AdminSettings,PowerUser")]
        public async Task<IActionResult> DeleteRoom(int id)
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
    }
}
