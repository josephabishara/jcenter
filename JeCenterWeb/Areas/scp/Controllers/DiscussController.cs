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
    public class DiscussController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<TeachersController> _logger;
        //  private readonly FinancialDocumentEntryRepository _repository;
        string ControllerName = "الإشعارات";
        string AppName = "الأدمن";
        public DiscussController(ApplicationDbContext context,
             UserManager<ApplicationUser> userManager,
             SignInManager<ApplicationUser> signInManager,
             ILogger<TeachersController> logger,
             IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            // _repository = repository;
            // FinancialDocumentEntryRepository repository

        }
        [Authorize(Roles = "AdminSettings,PowerUser")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser,User")]
        public async Task<IActionResult> Notifications()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var modellist = await _context.UsersNotifications
                .Where(t => t.UserId == uid)
                .OrderByDescending(t => t.NotificationId)
                .Include(t => t.CNotifications)
                .ToListAsync();
            return View(modellist);
        }

        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser,User")]
        public async Task<IActionResult> ReadNotification(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);

            var model = await _context.UsersNotifications
                .Where(t => t.UsersNotificationId == id)
                .Include(t => t.CNotifications)
                .FirstOrDefaultAsync();
            model.Seen = true;
            model.UpdateId = uid;
            model.UpdatedDate = DateTime.Now;

            _context.Update(model);
            await _context.SaveChangesAsync();


            return View(model);
        }

        [Authorize(Roles = "AdminSettings")]
        public async Task<IActionResult> ReadedNotification(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            ReadedNotificationViewModel notificationViewModel = new ReadedNotificationViewModel();


            notificationViewModel.cNotifications = await _context.CNotifications
                .Where(t => t.NotificationId == id)
                .FirstOrDefaultAsync();

            if(notificationViewModel.cNotifications == null)
            {
                return RedirectToAction(nameof(NotiEmp));
            }

            var usersnoti = await _context.UsersNotifications.Where(u => u.NotificationId == id).ToListAsync();

            List<NotificationUserSeen> seen = new List<NotificationUserSeen>();

            foreach (var item in usersnoti)
            {
                var usermodel = await _context.Users.Where(u => u.Id == item.UserId).FirstOrDefaultAsync();
                NotificationUserSeen userSeen = new NotificationUserSeen()
                {
                    UserId = item.UserId,
                    Seen = item.Seen,
                    SeenDate = item.UpdatedDate,
                    UserName = usermodel.FullName,
                };
                seen.Add(userSeen);
            }

            notificationViewModel.NotificationUserSeen = seen;

            return View(notificationViewModel);
        }
        

        // DeleteNotification
        [Authorize(Roles = "AdminSettings,PowerUser")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            

            var cNotifications = await _context.CNotifications
                .Where(t => t.NotificationId == id)
                .FirstOrDefaultAsync();

            if (cNotifications == null)
            {
                return RedirectToAction(nameof(NotiEmp));
            }
             

            return View(cNotifications);
        }


        // DeleteNotiEmp
        [Authorize(Roles = "AdminSettings,PowerUser")]
        [HttpPost]
        public async Task<IActionResult> DeleteNotiEmp(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
 

            var cNotifications = await _context.CNotifications
                .Where(t => t.NotificationId == id)
                .FirstOrDefaultAsync();

            if (cNotifications == null)
            {
                return RedirectToAction(nameof(NotiEmp));
            }
            _context.Remove(cNotifications);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(NotiEmp));
        }
        
        // NotiEmp
        [Authorize(Roles = "AdminSettings,PowerUser")]
        public async Task<IActionResult> NotiEmp()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var notiemp = await _context.CNotifications
                .OrderByDescending(n => n.NotificationId).ToListAsync();
            return View(notiemp);

        }

        // GET: scp/AddNotiGroup/Create
        [Authorize(Roles = "AdminSettings,PowerUser")]
        public async Task<IActionResult> AddNotiEmp()
        {
            return View();
        }

        // POST: scp/Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "AdminSettings,PowerUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNotiEmp([Bind("NotificationId,NotiTitel,NotificationNote,Writed,Deleted,Active,CreateId,CreatedDate,UpdateId,UpdatedDate,DeleteId,DeletedDate")] CNotifications Notifications)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            if (ModelState.IsValid)
            {
                Notifications.CreateId = Convert.ToInt32(userId);
                Notifications.UpdateId = Convert.ToInt32(userId);
                Notifications.DeleteId = Convert.ToInt32(userId);
                Notifications.UpdatedDate = DateTime.Now;
                Notifications.CreatedDate = DateTime.Now;
                Notifications.DeletedDate = DateTime.Now;
                Notifications.Active = true;
                _context.Add(Notifications);
                await _context.SaveChangesAsync();

                var emps = await _context.Users.Where(e => e.UserTypeId < 5 && e.Active == true).ToListAsync();

                foreach (var item in emps)
                {
                    UsersNotifications usersNotifications = new UsersNotifications()
                    {
                        UserId = item.Id,
                        NotificationId = Notifications.NotificationId,
                        Active = true,
                        CreateId = uid,
                        CreatedDate = DateTime.Now,
                        Seen = false,
                    };

                    _context.Add(usersNotifications);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(NotiEmp));
            }
            return View(Notifications);
        }

        // NotiGroup
        [Authorize(Roles = "AdminSettings,PowerUser")]
        public async Task<IActionResult> NotiGroup()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var notiemp = await _context.GNotifications
                .OrderByDescending(n => n.NotificationId).ToListAsync();
            return View(notiemp);
        }

        // GET: scp/AddNotiGroup/Create
        [Authorize(Roles = "AdminSettings,PowerUser")]
        public IActionResult AddNotiGroup()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["Id"] = new SelectList(_userManager.Users.Where(f => f.UserTypeId == 5), "Id", "FullName");

            return View();
        }

        // POST: scp/Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AdminSettings,PowerUser")]
        public async Task<IActionResult> AddNotiGroup([Bind("NotificationId,GroupId,NotiTitel,NotificationNote,Writed,Deleted,Active,CreateId,CreatedDate,UpdateId,UpdatedDate,DeleteId,DeletedDate")] GNotifications Notifications)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            if (ModelState.IsValid)
            {
                Notifications.CreateId = Convert.ToInt32(userId);
                Notifications.UpdateId = Convert.ToInt32(userId);
                Notifications.DeleteId = Convert.ToInt32(userId);
                Notifications.UpdatedDate = DateTime.Now;
                Notifications.CreatedDate = DateTime.Now;
                Notifications.DeletedDate = DateTime.Now;
                Notifications.Active = true;
                _context.Add(Notifications);
                await _context.SaveChangesAsync();

                var students = await _context.StudentGroup.Where(e => e.GroupId == Notifications.GroupId).ToListAsync();

                foreach (var item in students)
                {
                    StudentNotifications studentNotifications = new StudentNotifications()
                    {
                        StudentId = item.StudentId,
                        NotificationId = Notifications.NotificationId,
                        Active = true,
                        CreateId = uid,
                        CreatedDate = DateTime.Now,
                        Seen = false,
                    };

                    _context.Add(studentNotifications);
                    await _context.SaveChangesAsync();
                }


                return RedirectToAction(nameof(NotiGroup));
            }
            return View(Notifications);
        }

        //public async Task<IActionResult> AddNotiGroup(NotificationViewModel notificationViewModel)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var user = await _userManager.FindByIdAsync(userId);
        //    int uid = Convert.ToInt32(userId);


        //    CNotifications cNotifications = new CNotifications()
        //    {
        //        Active = true,

        //        NotificationNote = notificationViewModel.NotificationNote,
        //        NotiTitel = notificationViewModel.NotiTitel,
        //        GroupId = notificationViewModel.GroupId,
        //        NotificationTypeId = notificationViewModel.NotificationTypeId,
        //        CreateId = uid,
        //        CreatedDate = DateTime.Now,
        //    };


        //    return View();

        //}



        [HttpGet]
        public object GetGroup(int Id)
        {

            return (_context.CGroups
                .Where(s => s.TeacherId == Id)
                .ToList().Select(x => new
                {
                    Id = x.GroupId,
                    Name = x.GroupName,

                }));
        }


    }
}
