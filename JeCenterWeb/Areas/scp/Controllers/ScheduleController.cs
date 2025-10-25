using JeCenterWeb.Data;
using JeCenterWeb.Models;
using JeCenterWeb.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Security.Claims;

namespace JeCenterWeb.Areas.scp.Controllers
{
    [Area("scp")]
    [Authorize(Roles = "AdminSettings,PowerUser,SuperUser,User")]
    public class ScheduleController : Controller
    {
        private readonly ApplicationDbContext _context;
        //private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly ILogger<TeachersController> _logger;
        string ControllerName = "الجداول";
        string AppName = "الأدمن";
        public ScheduleController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        //SignInManager<ApplicationUser> signInManager,
        //ILogger<TeachersController> logger,
        //IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            //_userManager = userManager;
            //_signInManager = signInManager;
            //_logger = logger;
            //_webHostEnvironment = webHostEnvironment;
        }


        public async Task<IActionResult> Teachers()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["date"] = DateTime.Today.ToString("yyyy-MM-dd");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            int branchId = user.online;
            ScheduleTeachersVIewModel vIewModel = new ScheduleTeachersVIewModel();
            if (user.UserTypeId == 1)
            {

                vIewModel.CGroupSchedule = await _context.CGroupSchedule
                     .Where(g => g.LectureDate == DateTime.Today)
                     .Include(g => g.CGroups)
                     .Include(g => g.CGroups.CSyllabus)
                     .Include(g => g.CGroups.CSyllabus.CPhases)
                     .Include(g => g.CGroups.StudentGroup)
                     .Include(g => g.CGroups.CRooms)
                     .ToListAsync();

                vIewModel.ReviewsSchedule = await _context.ReviewsSchedule
                      .Where(g => g.ReviewDate == DateTime.Today)
                        .Include(g => g.CSyllabus)
                          .Include(g => g.CSyllabus.CPhases)
                       .Include(g => g.CRooms)
                      .ToListAsync();

            }
            else if (user.UserTypeId < 5)
            {
                vIewModel.CGroupSchedule = await _context.CGroupSchedule
                    .Where(g => g.LectureDate == DateTime.Today)
                    .Where(g => g.CGroups.CRooms.BranchId == branchId)
                    .Include(g => g.CGroups)
                    .Include(g => g.CGroups.CSyllabus)
                    .Include(g => g.CGroups.CSyllabus.CPhases)
                    .Include(g => g.CGroups.StudentGroup)
                    .Include(g => g.CGroups.CRooms)
                    .ToListAsync();

                vIewModel.ReviewsSchedule = await _context.ReviewsSchedule
                      .Where(g => g.ReviewDate == DateTime.Today)
                      .Where(g => g.CRooms.BranchId == branchId)
                      .Include(g => g.CSyllabus)
                      .Include(g => g.CSyllabus.CPhases)
                      .Include(g => g.CRooms)
                      .ToListAsync();
            }


            return View(vIewModel);
        }
        public async Task<IActionResult> TeachersByDate(DateTime date)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["date"] = date.ToString("yyyy-MM-dd");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            int branchId = user.online;
            ScheduleTeachersVIewModel vIewModel = new ScheduleTeachersVIewModel();
            if (user.UserTypeId == 1)
            {
                vIewModel.CGroupSchedule = await _context.CGroupSchedule
                .Where(g => g.LectureDate == date)
                .Include(g => g.CGroups)
                .Include(g => g.CGroups.CSyllabus)
                .Include(g => g.CGroups.CSyllabus.CPhases)
                .Include(g => g.CGroups.StudentGroup)
                .Include(g => g.CGroups.CRooms)
                .ToListAsync();

                vIewModel.ReviewsSchedule = await _context.ReviewsSchedule
                .Where(g => g.ReviewDate == date)
                .Include(g => g.CSyllabus)
                .Include(g => g.CSyllabus.CPhases)
                .Include(g => g.CRooms)
                .ToListAsync();
            }
            else if (user.UserTypeId < 5)
            {
                vIewModel.CGroupSchedule = await _context.CGroupSchedule
                .Where(g => g.LectureDate == date)
                .Where(g => g.CGroups.CRooms.BranchId == branchId)
                .Include(g => g.CGroups)
                .Include(g => g.CGroups.CSyllabus)
                .Include(g => g.CGroups.CSyllabus.CPhases)
                .Include(g => g.CGroups.StudentGroup)
                .Include(g => g.CGroups.CRooms)
                .ToListAsync();

                vIewModel.ReviewsSchedule = await _context.ReviewsSchedule
                .Where(g => g.ReviewDate == date)
                .Where(g => g.CRooms.BranchId == branchId)
                .Include(g => g.CSyllabus)
                .Include(g => g.CSyllabus.CPhases)
                .Include(g => g.CRooms)
                .ToListAsync();
            }

            return View(vIewModel);
        }

        public async Task<IActionResult> TeachersById(DateTime date, int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["date"] = date.ToString("yyyy-MM-dd");
            ViewData["id"] = id;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            int branchId = user.online;
            ScheduleTeachersVIewModel vIewModel = new ScheduleTeachersVIewModel();
            if (id == 1 && user.UserTypeId == 1)
            {
                vIewModel.CGroupSchedule = await _context.CGroupSchedule
               .Where(g => g.LectureDate == date)
               .Include(g => g.CGroups)
               .Include(g => g.CGroups.CSyllabus)
               .Include(g => g.CGroups.CSyllabus.CPhases)
               .Include(g => g.CGroups.StudentGroup)
               .Include(g => g.CGroups.CRooms)
               .OrderBy(g => g.CGroups.SessionStart)
               .ToListAsync();

                vIewModel.ReviewsSchedule = await _context.ReviewsSchedule
                .Where(g => g.ReviewDate == date)
                .Include(g => g.CSyllabus)
                .Include(g => g.CSyllabus.CPhases)
                .Include(g => g.CRooms)
                 .OrderBy(g => g.SessionStart)
                .ToListAsync();

                return View(vIewModel);
            }
            else if (id == 1 && user.UserTypeId < 5)
            {
                vIewModel.CGroupSchedule = await _context.CGroupSchedule
               .Where(g => g.LectureDate == date)
                .Where(g => g.CGroups.CRooms.BranchId == branchId)
               .Include(g => g.CGroups)
               .Include(g => g.CGroups.CSyllabus)
               .Include(g => g.CGroups.CSyllabus.CPhases)
               .Include(g => g.CGroups.StudentGroup)
               .Include(g => g.CGroups.CRooms)
               .OrderBy(g => g.CGroups.SessionStart)
               .ToListAsync();

                vIewModel.ReviewsSchedule = await _context.ReviewsSchedule
                .Where(g => g.ReviewDate == date)
                .Where(g => g.CRooms.BranchId == branchId)
                .Include(g => g.CSyllabus)
                .Include(g => g.CSyllabus.CPhases)
                .Include(g => g.CRooms)
                 .OrderBy(g => g.SessionStart)
                .ToListAsync();

                return View(vIewModel);
            }
            else if (id == 2 && user.UserTypeId == 1)
            {
                vIewModel.CGroupSchedule = await _context.CGroupSchedule
               .Where(g => g.LectureDate == date)
               .Include(g => g.CGroups)
               .Include(g => g.CGroups.CSyllabus)
               .Include(g => g.CGroups.CSyllabus.CPhases)
                .Include(g => g.CGroups.StudentGroup)
               .Include(g => g.CGroups.CRooms)
               .OrderBy(g => g.CGroups.RoomId)
               .ToListAsync();

                vIewModel.ReviewsSchedule = await _context.ReviewsSchedule
               .Where(g => g.ReviewDate == date)
                .Include(g => g.CSyllabus)
                  .Include(g => g.CSyllabus.CPhases)
                .Include(g => g.CRooms)
                .OrderBy(g => g.RoomId)
               .ToListAsync();
                return View(vIewModel);
            }
            else if (id == 2 && user.UserTypeId < 5)
            {
                vIewModel.CGroupSchedule = await _context.CGroupSchedule
               .Where(g => g.LectureDate == date)
                .Where(g => g.CGroups.CRooms.BranchId == branchId)
               .Include(g => g.CGroups)
               .Include(g => g.CGroups.CSyllabus)
               .Include(g => g.CGroups.CSyllabus.CPhases)
                .Include(g => g.CGroups.StudentGroup)
               .Include(g => g.CGroups.CRooms)
               .OrderBy(g => g.CGroups.RoomId)
               .ToListAsync();

                vIewModel.ReviewsSchedule = await _context.ReviewsSchedule
               .Where(g => g.ReviewDate == date)
               .Where(g => g.CRooms.BranchId == branchId)
                .Include(g => g.CSyllabus)
                  .Include(g => g.CSyllabus.CPhases)
                .Include(g => g.CRooms)
                .OrderBy(g => g.RoomId)
               .ToListAsync();
                return View(vIewModel);
            }
            else if (id == 3 && user.UserTypeId == 1)
            {
                vIewModel.CGroupSchedule = await _context.CGroupSchedule
               .Where(g => g.LectureDate == date)
               .Include(g => g.CGroups)
               .Include(g => g.CGroups.CSyllabus)
               .Include(g => g.CGroups.CSyllabus.CPhases)
                .Include(g => g.CGroups.StudentGroup)
               .Include(g => g.CGroups.CRooms)
               .OrderBy(g => g.CGroups.TeacherName)
               .ToListAsync();

                vIewModel.ReviewsSchedule = await _context.ReviewsSchedule
               .Where(g => g.ReviewDate == date)
                .Include(g => g.CSyllabus)
                  .Include(g => g.CSyllabus.CPhases)
                   .Include(g => g.CRooms)
                .OrderBy(g => g.TeacherId)
               .ToListAsync();

                return View(vIewModel);
            }
            else if (id == 3 && user.UserTypeId < 5)
            {
                vIewModel.CGroupSchedule = await _context.CGroupSchedule
               .Where(g => g.LectureDate == date)
               .Where(g => g.CGroups.CRooms.BranchId == branchId)
               .Include(g => g.CGroups)
               .Include(g => g.CGroups.CSyllabus)
               .Include(g => g.CGroups.CSyllabus.CPhases)
                .Include(g => g.CGroups.StudentGroup)
               .Include(g => g.CGroups.CRooms)
               .OrderBy(g => g.CGroups.TeacherName)
               .ToListAsync();

                vIewModel.ReviewsSchedule = await _context.ReviewsSchedule
               .Where(g => g.ReviewDate == date)
                .Where(g => g.CRooms.BranchId == branchId)
                .Include(g => g.CSyllabus)
                  .Include(g => g.CSyllabus.CPhases)
                   .Include(g => g.CRooms)
                .OrderBy(g => g.TeacherId)
               .ToListAsync();

                return View(vIewModel);
            }
            vIewModel.CGroupSchedule = await _context.CGroupSchedule
              .Where(g => g.LectureDate == date)
              .Include(g => g.CGroups)
              .Include(g => g.CGroups.CSyllabus)
              .Include(g => g.CGroups.CSyllabus.CPhases)
               .Include(g => g.CGroups.StudentGroup)
              .Include(g => g.CGroups.CRooms)
              .ToListAsync();

            vIewModel.ReviewsSchedule = await _context.ReviewsSchedule
                  .Where(g => g.ReviewDate == date)
                   .Include(g => g.CSyllabus)
                   .Include(g => g.CSyllabus.CPhases)
                    .Include(g => g.CRooms)
                   .ToListAsync();

            return View(vIewModel);
        }

        public async Task<IActionResult> TeachersPrintById(int id, DateTime date)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["date"] = date.ToString("yyyy-MM-dd");
            ViewData["id"] = id;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            int branchId = user.online;
            ScheduleTeachersVIewModel vIewModel = new ScheduleTeachersVIewModel();
            if (id == 1 && user.UserTypeId == 1)
            {
                vIewModel.CGroupSchedule = await _context.CGroupSchedule
               .Where(g => g.LectureDate == date)
               .Include(g => g.CGroups)
               .Include(g => g.CGroups.CSyllabus)
               .Include(g => g.CGroups.CSyllabus.CPhases)
               .Include(g => g.CGroups.StudentGroup)
               .Include(g => g.CGroups.CRooms)
               .OrderBy(g => g.CGroups.SessionStart)
               .ToListAsync();

                vIewModel.ReviewsSchedule = await _context.ReviewsSchedule
                .Where(g => g.ReviewDate == date)
                 .Include(g => g.CSyllabus)
                   .Include(g => g.CSyllabus.CPhases)
                   .Include(g => g.CRooms)
                 .OrderBy(g => g.SessionStart)
                .ToListAsync();

                return View(vIewModel);
            }
            else if (id == 1 && user.UserTypeId < 5)
            {
                vIewModel.CGroupSchedule = await _context.CGroupSchedule
               .Where(g => g.LectureDate == date)
                .Where(g => g.CGroups.CRooms.BranchId == branchId)
               .Include(g => g.CGroups)
               .Include(g => g.CGroups.CSyllabus)
               .Include(g => g.CGroups.CSyllabus.CPhases)
               .Include(g => g.CGroups.StudentGroup)
               .Include(g => g.CGroups.CRooms)
               .OrderBy(g => g.CGroups.SessionStart)
               .ToListAsync();

                vIewModel.ReviewsSchedule = await _context.ReviewsSchedule
                .Where(g => g.ReviewDate == date)
                .Where(g => g.CRooms.BranchId == branchId)
                 .Include(g => g.CSyllabus)
                   .Include(g => g.CSyllabus.CPhases)
                   .Include(g => g.CRooms)
                 .OrderBy(g => g.SessionStart)
                .ToListAsync();

                return View(vIewModel);
            }
            else if (id == 2 && user.UserTypeId == 1)
            {
                vIewModel.CGroupSchedule = await _context.CGroupSchedule
               .Where(g => g.LectureDate == date)
               .Include(g => g.CGroups)
               .Include(g => g.CGroups.CSyllabus)
               .Include(g => g.CGroups.CSyllabus.CPhases)
                .Include(g => g.CGroups.StudentGroup)
               .Include(g => g.CGroups.CRooms)
               .OrderBy(g => g.CGroups.RoomId)
               .ToListAsync();

                vIewModel.ReviewsSchedule = await _context.ReviewsSchedule
               .Where(g => g.ReviewDate == date)
                .Include(g => g.CSyllabus)
               .Include(g => g.CSyllabus.CPhases)
                .Include(g => g.CRooms)
                .OrderBy(g => g.RoomId)
               .ToListAsync();
                return View(vIewModel);
            }
            else if (id == 2 && user.UserTypeId < 5)
            {
                vIewModel.CGroupSchedule = await _context.CGroupSchedule
               .Where(g => g.LectureDate == date)
                .Where(g => g.CGroups.CRooms.BranchId == branchId)
               .Include(g => g.CGroups)
               .Include(g => g.CGroups.CSyllabus)
               .Include(g => g.CGroups.CSyllabus.CPhases)
                .Include(g => g.CGroups.StudentGroup)
               .Include(g => g.CGroups.CRooms)
               .OrderBy(g => g.CGroups.RoomId)
               .ToListAsync();

                vIewModel.ReviewsSchedule = await _context.ReviewsSchedule
               .Where(g => g.ReviewDate == date)
               .Where(g => g.CRooms.BranchId == branchId)
                .Include(g => g.CSyllabus)
               .Include(g => g.CSyllabus.CPhases)
                .Include(g => g.CRooms)
                .OrderBy(g => g.RoomId)
               .ToListAsync();
                return View(vIewModel);
            }
            else if (id == 3 && user.UserTypeId == 1)
            {
                vIewModel.CGroupSchedule = await _context.CGroupSchedule
               .Where(g => g.LectureDate == date)
               .Include(g => g.CGroups)
               .Include(g => g.CGroups.CSyllabus)
               .Include(g => g.CGroups.CSyllabus.CPhases)
               .Include(g => g.CGroups.StudentGroup)
               .Include(g => g.CGroups.CRooms)
               .OrderBy(g => g.CGroups.TeacherName)
               .ToListAsync();

                vIewModel.ReviewsSchedule = await _context.ReviewsSchedule
                .Where(g => g.ReviewDate == date)
                .Include(g => g.CSyllabus)
                .Include(g => g.CSyllabus.CPhases)
                .Include(g => g.CRooms)
                .OrderBy(g => g.TeacherId)
                .ToListAsync();

                return View(vIewModel);
            }
            else if (id == 3 && user.UserTypeId < 5)
            {
                vIewModel.CGroupSchedule = await _context.CGroupSchedule
               .Where(g => g.LectureDate == date)
                .Where(g => g.CGroups.CRooms.BranchId == branchId)
               .Include(g => g.CGroups)
               .Include(g => g.CGroups.CSyllabus)
               .Include(g => g.CGroups.CSyllabus.CPhases)
               .Include(g => g.CGroups.StudentGroup)
               .Include(g => g.CGroups.CRooms)
               .OrderBy(g => g.CGroups.TeacherName)
               .ToListAsync();

                vIewModel.ReviewsSchedule = await _context.ReviewsSchedule
                .Where(g => g.ReviewDate == date)
                .Where(g => g.CRooms.BranchId == branchId)
                .Include(g => g.CSyllabus)
                .Include(g => g.CSyllabus.CPhases)
                .Include(g => g.CRooms)
                .OrderBy(g => g.TeacherId)
                .ToListAsync();

                return View(vIewModel);
            }
            vIewModel.CGroupSchedule = await _context.CGroupSchedule
              .Where(g => g.LectureDate == date)
              .Include(g => g.CGroups)
              .Include(g => g.CGroups.CSyllabus)
              .Include(g => g.CGroups.CSyllabus.CPhases)
              .Include(g => g.CGroups.StudentGroup)
              .Include(g => g.CGroups.CRooms)
              .ToListAsync();

            vIewModel.ReviewsSchedule = await _context.ReviewsSchedule
              .Where(g => g.ReviewDate == date)
              .Include(g => g.CSyllabus)
              .Include(g => g.CSyllabus.CPhases)
              .Include(g => g.CRooms)
              .ToListAsync();
            return View(vIewModel);
        }

        public async Task<IActionResult> TeachersPrint(DateTime date)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["date"] = date.ToString("yyyy-MM-dd");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            int branchId = user.online;
            ScheduleTeachersVIewModel vIewModel = new ScheduleTeachersVIewModel();
            if (user.UserTypeId == 1)
            {
                vIewModel.CGroupSchedule = await _context.CGroupSchedule
                  .Where(g => g.LectureDate == DateTime.Today)
                  .Include(g => g.CGroups)
                  .Include(g => g.CGroups.CSyllabus)
                  .Include(g => g.CGroups.CSyllabus.CPhases)
                  .Include(g => g.CGroups.StudentGroup)
                  .Include(g => g.CGroups.CRooms)
                  .ToListAsync();

                vIewModel.ReviewsSchedule = await _context.ReviewsSchedule
                  .Where(g => g.ReviewDate == date)
                  .Include(g => g.CSyllabus)
                  .Include(g => g.CSyllabus.CPhases)
                  .Include(g => g.CRooms)
                  .OrderBy(g => g.TeacherId)
                 .ToListAsync();
            }
            else if (user.UserTypeId < 5)
            {
                vIewModel.CGroupSchedule = await _context.CGroupSchedule
                  .Where(g => g.LectureDate == DateTime.Today)
                  .Where(g => g.CGroups.CRooms.BranchId == branchId)
                  .Include(g => g.CGroups)
                  .Include(g => g.CGroups.CSyllabus)
                  .Include(g => g.CGroups.CSyllabus.CPhases)
                  .Include(g => g.CGroups.StudentGroup)
                  .Include(g => g.CGroups.CRooms)
                  .ToListAsync();

                vIewModel.ReviewsSchedule = await _context.ReviewsSchedule
                  .Where(g => g.ReviewDate == date)
                  .Where(g => g.CRooms.BranchId == branchId)
                  .Include(g => g.CSyllabus)
                  .Include(g => g.CSyllabus.CPhases)
                  .Include(g => g.CRooms)
                  .OrderBy(g => g.TeacherId)
                 .ToListAsync();
            }
            return View(vIewModel);
        }
        // Rooms
        public async Task<IActionResult> Rooms()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            //  ViewData["date"] = DateTime.Today.ToString("yyyy-MM-dd");
            List<RoomScheduleViewModel> roomSchedule = new List<RoomScheduleViewModel>();
            //var rooms = await _context.CRooms.ToListAsync();

            //foreach (var item in rooms)
            //{
            //    RoomScheduleViewModel model = new RoomScheduleViewModel();

            //    model.Room = item;
            //    model.RoomGroupReviewsExamsScheduleViewModel = await _context.RoomGroupReviewsExamsScheduleViewModel
            //        .Where(g => g.RoomId == item.RoomId && g.Date == DateTime.Today.Date)
            //        .OrderBy(g => g.SessionStart)
            //        .ToListAsync();

            //    roomSchedule.Add(model);
            //}

            return View(roomSchedule);
        }

        public async Task<IActionResult> RoomsByDate(DateTime date)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["date"] = date.ToString("yyyy-MM-dd");
            List<RoomScheduleViewModel> roomSchedule = new List<RoomScheduleViewModel>();
            var rooms = await _context.CRooms.ToListAsync();

            foreach (var item in rooms)
            {
                RoomScheduleViewModel model = new RoomScheduleViewModel();

                model.Room = item;
                model.RoomGroupReviewsExamsScheduleViewModel = await _context.RoomGroupReviewsExamsScheduleViewModel
                  .Where(g => g.RoomId == item.RoomId && g.Date == date)
                  .OrderBy(g => g.LectureNo).ThenBy(g => g.SessionStart)
                  .ToListAsync();
                //model.GroupSchedule = await _context.CGroupSchedule
                //    .Where(g => g.CGroups.RoomId == item.RoomId && g.LectureDate == date)
                //    .OrderBy(g => g.CGroups.SessionStart)
                //    .Include(g => g.CGroups)
                //    .ToListAsync();

                roomSchedule.Add(model);
            }
            return View(roomSchedule);
        }

        public async Task<IActionResult> RoomsPrint(DateTime date)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["date"] = date.ToString("yyyy-MM-dd");
            List<RoomScheduleViewModel> roomSchedule = new List<RoomScheduleViewModel>();
            var rooms = await _context.CRooms.ToListAsync();

            foreach (var item in rooms)
            {
                RoomScheduleViewModel model = new RoomScheduleViewModel();

                model.Room = item;
                model.RoomGroupReviewsExamsScheduleViewModel = await _context.RoomGroupReviewsExamsScheduleViewModel
                  .Where(g => g.RoomId == item.RoomId && g.Date == date)
                  .OrderBy(g => g.SessionStart)
                  .ToListAsync();
                //model.GroupSchedule = await _context.CGroupSchedule
                //    .Where(g => g.CGroups.RoomId == item.RoomId && g.LectureDate == date)
                //    .OrderBy(g => g.CGroups.SessionStart)
                //    .Include(g => g.CGroups)
                //    .ToListAsync();

                roomSchedule.Add(model);
            }
            return View(roomSchedule);
        }

        // Groups
        public async Task<IActionResult> Groups()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["date"] = DateTime.Today.ToString("yyyy-MM-dd");
            var groups = await _context.CGroupSchedule
                .Where(g => g.LectureDate == DateTime.Today)
                .Include(g => g.CGroups)
                .Include(g => g.CGroups.CRooms)
                .OrderBy(g => g.CGroups.SessionStart)
                .ToListAsync();
            return View(groups);
        }

        // AddGroup
        public async Task<IActionResult> AddGroup(int id, DateTime date)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["TeacherId"] = new SelectList(_userManager.Users.Where(t => t.UserTypeId == 5 && t.Active == true), "Id", "FullName");
            ViewData["physicalyearId"] = new SelectList(_context.PhysicalYear, "PhysicalyearId", "PhysicalyearName");
            ViewData["SyllabusID"] = new SelectList(_context.TeacherSyllabus.Where(s => s.TeacherId == id), "SyllabusID", "SyllabusName");
            ViewData["DayOfWeekId"] = new SelectList(_context.ResourceDays, "DayOfWeekId", "DayOfWeekName");
            ViewData["RoomId"] = new SelectList(_context.CRooms, "RoomId", "RoomName", id);
            ViewData["date"] = date.ToString("yyyy-MM-dd");
            AddGroupScheduleViewModel model = new AddGroupScheduleViewModel();
            model.CGroupSchedule = await _context.CGroupSchedule
                .Where(g => g.LectureDate == date && g.CGroups.RoomId == id)
                .Include(g => g.CGroups)
                .OrderBy(g => g.LectureNo)
                .ToListAsync();
            model.ReviewsSchedule = await _context.ReviewsSchedule
                .Where(g => g.ReviewDate == date && g.RoomId == id)
                .ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddGroup([Bind("GroupName,GroupNoوTeacherName,RoomId,TeacherFee,CenterFee,ServiceFee,SessionStart,SessionEnd,PhysicalyearId,DayOfWeekId,SyllabusID,TeacherId,FirstDay,CrrDate,TypeId,lectureno,Active")] TeacherGroupsViewModel teacherGroupsViewModel)
        {
            if (ModelState.IsValid)
            {
                if (teacherGroupsViewModel.TypeId == 0)
                {
                    string userid = teacherGroupsViewModel.TeacherId.ToString();
                    var teacher = await _userManager.FindByIdAsync(userid);

                    int lastGroupNo = await _context.CGroups
                       .Where(g => g.TeacherId == teacherGroupsViewModel.TeacherId)
                       .MaxAsync(g => (int?)g.GroupNo) ?? 0;
                    var Syllabus = await _context.CSyllabus.FindAsync(teacherGroupsViewModel.SyllabusID);

                    teacherGroupsViewModel.GroupName = teacher.FullName + " - " + Syllabus.SyllabusName + " - " + (lastGroupNo + 1).ToString();
                    teacherGroupsViewModel.GroupNo = lastGroupNo + 1;
                    teacherGroupsViewModel.TeacherName = teacher.FullName;
                    teacherGroupsViewModel.DayOfWeekId = thdyofwk(teacherGroupsViewModel.FirstDay);
                    CGroups cGroups = new CGroups
                    {
                        GroupName = teacherGroupsViewModel.GroupName,
                        TeacherId = teacherGroupsViewModel.TeacherId,
                        TeacherName = teacherGroupsViewModel.TeacherName,
                        SyllabusID = teacherGroupsViewModel.SyllabusID,
                        GroupNo = teacherGroupsViewModel.GroupNo,
                        lectureno = teacherGroupsViewModel.lectureno,
                        physicalyearId = teacherGroupsViewModel.PhysicalyearId,
                        DayOfWeekId = teacherGroupsViewModel.DayOfWeekId,
                        SessionStart = teacherGroupsViewModel.SessionStart,
                        SessionEnd = teacherGroupsViewModel.SessionEnd,
                        TeacherFee = teacherGroupsViewModel.TeacherFee,
                        CenterFee = teacherGroupsViewModel.CenterFee,
                        ServiceFee = teacherGroupsViewModel.ServiceFee,
                        RoomId = teacherGroupsViewModel.RoomId,
                        FirstDay = teacherGroupsViewModel.FirstDay,
                        LecturePrice = teacherGroupsViewModel.TeacherFee + teacherGroupsViewModel.CenterFee + teacherGroupsViewModel.ServiceFee,
                        Active = teacherGroupsViewModel.Active,

                    };
                    _context.Add(cGroups);
                    await _context.SaveChangesAsync();

                    // cGroups.GroupId
                    CGroups group = await _context.CGroups.FindAsync(cGroups.GroupId);
                    var physicalYear = await _context.PhysicalYear.FindAsync(group.physicalyearId);

                    DateTime lastday = physicalYear.ToDate;
                    DateTime lectureDate = group.FirstDay.Date;
                    List<CGroupSchedule> ScheduleList = new List<CGroupSchedule>();
                    for (int i = 0; lectureDate < lastday; i++)
                    {
                        CGroupSchedule cGroupSchedule = new CGroupSchedule
                        {
                            GroupId = cGroups.GroupId,
                            LectureDate = lectureDate,
                            LectureNo = i + 1,
                            MonthCode = Convert.ToDateTime(lectureDate).Year.ToString("yyyy") + Convert.ToDateTime(lectureDate).Month.ToString("MM"),

                        };
                        _context.Add(cGroupSchedule);
                        await _context.SaveChangesAsync();
                        lectureDate = lectureDate.AddDays(7);
                        if (i > 60)
                        {
                            lectureDate = lastday;
                        }
                    }

                    TeacherGroups teacherGroups = new TeacherGroups
                    {
                        GroupId = cGroups.GroupId,
                        TeacherId = teacherGroupsViewModel.TeacherId,
                    };

                    _context.Add(teacherGroups);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    string type = " مراجعة - ";
                    if (teacherGroupsViewModel.TypeId == 2)
                    {
                        type = " امتحان - ";
                    }
                    string userid = teacherGroupsViewModel.TeacherId.ToString();
                    var teacher = await _userManager.FindByIdAsync(userid);

                    int lastGroupNo = await _context.CGroups
                       .Where(g => g.TeacherId == teacherGroupsViewModel.TeacherId)
                       .MaxAsync(g => (int?)g.GroupNo) ?? 0;
                    var Syllabus = await _context.CSyllabus.FindAsync(teacherGroupsViewModel.SyllabusID);

                    teacherGroupsViewModel.GroupName = type + teacher.FullName + " - " + Syllabus.SyllabusName;

                    ReviewsSchedule schedule = new ReviewsSchedule()
                    {
                        TeacherId = teacherGroupsViewModel.TeacherId,
                        ReviewTypeId = teacherGroupsViewModel.TypeId,
                        ReviewsScheduleName = teacherGroupsViewModel.GroupName,
                        ReviewDate = teacherGroupsViewModel.FirstDay,
                        MonthCode = Convert.ToDateTime(teacherGroupsViewModel.FirstDay).Year.ToString("yyyy") + Convert.ToDateTime(teacherGroupsViewModel.FirstDay).Month.ToString("MM"),
                        RoomId = teacherGroupsViewModel.RoomId,
                        SyllabusID = teacherGroupsViewModel.SyllabusID,
                        physicalyearId = teacherGroupsViewModel.PhysicalyearId,
                        CenterFee = teacherGroupsViewModel.CenterFee,
                        LecturePrice = teacherGroupsViewModel.TeacherFee + teacherGroupsViewModel.CenterFee + teacherGroupsViewModel.ServiceFee,
                        ServiceFee = teacherGroupsViewModel.ServiceFee,
                        SessionEnd = teacherGroupsViewModel.SessionEnd,
                        SessionStart = teacherGroupsViewModel.SessionStart,
                        TeacherFee = teacherGroupsViewModel.TeacherFee,
                        LectureNo = teacherGroupsViewModel.lectureno,
                        Paided = false,
                    };
                    _context.Add(schedule);
                    await _context.SaveChangesAsync();
                }


                string idate = Convert.ToDateTime(teacherGroupsViewModel.CrrDate).ToString("yyyy-MM-dd");

                return RedirectToRoute(new { action = "AddGroup", date = idate });
            }


            return RedirectToRoute(new { action = "AddGroup", id = teacherGroupsViewModel.RoomId, date = teacherGroupsViewModel.FirstDay });
            //ViewData["Gender"] = new SelectList(_context.ResourceGender, "GenderId", "GenderName", addUserViewModel.Gender);
            // return View(teacherGroupsViewModel);
        }
        public async Task<IActionResult> EditGroup(int id)
        {
            var Schedule = await _context.CGroupSchedule.FindAsync(id);
            var group = await _context.CGroups.FindAsync(Schedule.GroupId);
            ViewData["date"] = Schedule.LectureDate.ToString("yyyy-MM-dd");
            ViewData["RoomId"] = new SelectList(_context.CRooms, "RoomId", "RoomName", group.RoomId);
            ViewData["GroupName"] = group.GroupName;
            EditGroupViewModel editGroupViewModel = new EditGroupViewModel()
            {
                GroupId = Schedule.GroupId,
                SessionStart = group.SessionStart,
                SessionEnd = group.SessionEnd,
                CenterFee = group.CenterFee,
                ServiceFee = group.ServiceFee,
                TeacherFee = group.TeacherFee,
                RoomId = group.RoomId,
                date = Schedule.LectureDate,
                lectureno = group.lectureno,
                Active = group.Active,
            };
            return View(editGroupViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditGroup([Bind("GroupId,SessionStart,SessionEnd,TeacherFee,CenterFee,ServiceFee,RoomId,date,lectureno,Active")] EditGroupViewModel editGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                var group = await _context.CGroups.FindAsync(editGroupViewModel.GroupId);

                group.SessionStart = editGroupViewModel.SessionStart;
                group.SessionEnd = editGroupViewModel.SessionEnd;
                group.CenterFee = editGroupViewModel.CenterFee;
                group.TeacherFee = editGroupViewModel.TeacherFee;
                group.ServiceFee = editGroupViewModel.ServiceFee;
                group.RoomId = editGroupViewModel.RoomId;
                group.lectureno = editGroupViewModel.lectureno;
                group.Active = editGroupViewModel.Active;
                group.LecturePrice = editGroupViewModel.CenterFee + editGroupViewModel.TeacherFee + editGroupViewModel.ServiceFee;

                _context.Update(group);
                await _context.SaveChangesAsync();
                return RedirectToRoute(new { action = "RoomsByDate", date = editGroupViewModel.date.ToString("yyyy-MM-dd") });
            }
            ViewData["RoomId"] = new SelectList(_context.CRooms, "RoomId", "RoomName", editGroupViewModel.RoomId);
            return View(editGroupViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            // var Schedule = await _context.CGroupSchedule.FindAsync(id);
            var group = await _context.CGroups.FindAsync(id);
            _context.Remove(group);
            await _context.SaveChangesAsync();
            var groupSchedules = await _context.CGroupSchedule.Where(s => s.GroupId == id).ToListAsync();
            foreach (var item in groupSchedules)
            {
                _context.Remove(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToRoute(new { action = "Rooms", controller = "Schedule" });
        }
        public async Task<IActionResult> EditReview(int id)
        {
            var Schedule = await _context.ReviewsSchedule.FindAsync(id);

            ViewData["date"] = Schedule.ReviewDate.ToString("yyyy-MM-dd");
            ViewData["RoomId"] = new SelectList(_context.CRooms, "RoomId", "RoomName", Schedule.RoomId);
            ViewData["ReviewName"] = Schedule.ReviewsScheduleName;
            EditReviewViewModel editReviewViewModel = new EditReviewViewModel()
            {
                ReviewId = Schedule.ReviewsScheduleId,
                SessionStart = Schedule.SessionStart,
                SessionEnd = Schedule.SessionEnd,
                CenterFee = Schedule.CenterFee,
                ServiceFee = Schedule.ServiceFee,
                TeacherFee = Schedule.TeacherFee,
                RoomId = Schedule.RoomId,
                date = Schedule.ReviewDate,
                Type = Schedule.ReviewTypeId,
                lectureno = Schedule.LectureNo,
            };
            return View(editReviewViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditReview([Bind("ReviewId,SessionStart,SessionEnd,TeacherFee,CenterFee,ServiceFee,RoomId,date,Type,lectureno")] EditReviewViewModel editReviewViewModel)
        {
            if (ModelState.IsValid)
            {
                var reviw = await _context.ReviewsSchedule.FindAsync(editReviewViewModel.ReviewId);
                reviw.SessionStart = editReviewViewModel.SessionStart;
                reviw.SessionEnd = editReviewViewModel.SessionEnd;
                reviw.CenterFee = editReviewViewModel.CenterFee;
                reviw.TeacherFee = editReviewViewModel.TeacherFee;
                reviw.ServiceFee = editReviewViewModel.ServiceFee;
                reviw.RoomId = editReviewViewModel.RoomId;
                reviw.LectureNo = editReviewViewModel.lectureno;
                reviw.LecturePrice = editReviewViewModel.CenterFee + editReviewViewModel.TeacherFee + editReviewViewModel.ServiceFee;
                _context.Update(reviw);
                await _context.SaveChangesAsync();
                return RedirectToRoute(new { action = "RoomsByDate", date = editReviewViewModel.date.ToString("yyyy-MM-dd") });
            }
            ViewData["RoomId"] = new SelectList(_context.CRooms, "RoomId", "RoomName", editReviewViewModel.RoomId);
            return View(editReviewViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReview(int id)
        {
            // var Schedule = await _context.CGroupSchedule.FindAsync(id);
            var review = await _context.ReviewsSchedule.FindAsync(id);
            _context.Remove(review);
            await _context.SaveChangesAsync();


            return RedirectToRoute(new { action = "Rooms", controller = "Schedule" });
        }
        public static int thdyofwk(DateTime day)
        {
            int daystate = 0;
            string wkday = day.DayOfWeek.ToString();

            switch (wkday)
            {
                case "Saturday":
                    daystate = 1;
                    break;
                case "Sunday":
                    daystate = 2;
                    break;
                case "Monday":
                    daystate = 3;
                    break;
                case "Tuesday":
                    daystate = 4;
                    break;
                case "Wednesday":
                    daystate = 5;
                    break;
                case "Thursday":
                    daystate = 6;
                    break;
                case "Friday":
                    daystate = 7;
                    break;
                default:
                    break;
            }
            return daystate;
        }

        [HttpGet]
        //[Produces("application/json")]
        public object GetSyllabusID(int TeacherId)
        {
            //  var iSyllabus =  _context.CSyllabus.Where(s => s.PhaseID == PhaseId).ToList();

            return (_context.TeacherSyllabusViewModel.Where(s => s.TeacherId == TeacherId).ToList().Select(x => new
            {
                Id = x.SyllabusID,
                Name = x.Name,

            }));
        }

        // ChooseReview
        [HttpGet]
        public async Task<IActionResult> ChooseTeacherReview()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            List<ApplicationUser> Teachers = await _userManager.Users
                .Where(t => t.UserTypeId == 5)
                .OrderBy(t => t.FullName)
                .ToListAsync();

            return View(Teachers);
        }
        public async Task<IActionResult> CloseReview(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["id"] = id;
            var groups = await _context.ReviewsSchedule
                .Where(g => g.Paided == false && g.Closed == false && g.ReviewTypeId == 1 && g.TeacherId == id)
                .Include(g => g.CRooms)
                .Include(g => g.CSyllabus)
                .Include(g => g.CSyllabus.CPhases)
                .ToListAsync();
            return View(groups);
        }
        public async Task<IActionResult> ClosedReview(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["id"] = id;
            var groups = await _context.ReviewsSchedule
                .Where(g => g.Paided == false && g.Closed == true && g.ReviewTypeId == 1 && g.TeacherId == id)
                  .Include(g => g.CRooms)
                .Include(g => g.CSyllabus)
                .Include(g => g.CSyllabus.CPhases)
                .ToListAsync();
            return View(groups);
        }
        public async Task<IActionResult> PaidedReview(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["id"] = id;
            var groups = await _context.ReviewsSchedule
                .Where(g => g.Paided == true && g.Closed == true && g.ReviewTypeId == 1 && g.TeacherId == id)
                  .Include(g => g.CRooms)
                .Include(g => g.CSyllabus)
                .Include(g => g.CSyllabus.CPhases)
                .ToListAsync();
            return View(groups);
        }

        // ChooseExam
        [HttpGet]
        public async Task<IActionResult> ChooseTeacherExam()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;

            List<ApplicationUser> Teachers = await _userManager.Users
                .Where(t => t.UserTypeId == 5)
                .OrderBy(t => t.FullName)
                .ToListAsync();

            return View(Teachers);
        }
        public async Task<IActionResult> CloseExam(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["id"] = id;
            var groups = await _context.ReviewsSchedule
                .Where(g => g.Paided == false && g.Closed == false && g.ReviewTypeId == 2 && g.TeacherId == id)
                   .Include(g => g.CRooms)
                .Include(g => g.CSyllabus)
                .Include(g => g.CSyllabus.CPhases)
                .ToListAsync();
            return View(groups);
        }
        public async Task<IActionResult> ClosedExam(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["id"] = id;
            var groups = await _context.ReviewsSchedule
                .Where(g => g.Paided == false && g.Closed == true && g.ReviewTypeId == 2 && g.TeacherId == id)
                  .Include(g => g.CRooms)
                .Include(g => g.CSyllabus)
                .Include(g => g.CSyllabus.CPhases)
                .ToListAsync();
            return View(groups);
        }
        public async Task<IActionResult> PaidedExam(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["id"] = id;
            var groups = await _context.ReviewsSchedule
                .Where(g => g.Paided == true && g.Closed == true && g.ReviewTypeId == 2 && g.TeacherId == id)
                  .Include(g => g.CRooms)
                .Include(g => g.CSyllabus)
                .Include(g => g.CSyllabus.CPhases)
                .ToListAsync();
            return View(groups);
        }

        public async Task<IActionResult> ReviewDetails(int id)
        {
            LectureDetilsViewModel LectureDetils = new LectureDetilsViewModel();
            var LectureSch = await _context.ReviewsSchedule.FindAsync(id);
            //LectureSch.Closed = true;
            //_context.Update(LectureSch);
            //await _context.SaveChangesAsync();
            List<StudentLecture> studentLectures = await _context.StudentLecture
           .Where(l => l.scheduleId == id && l.LectureType == LectureSch.ReviewTypeId)
           .ToListAsync();
            LectureDetils.LectureId = id;
            LectureDetils.AttendCount = studentLectures.Count();
            LectureDetils.AbsentCount = 0;
            LectureDetils.DiscountCount = studentLectures.Where(l => l.Discount > 0).Count();
            LectureDetils.Closed = LectureSch.Closed;
            LectureDetils.Paided = LectureSch.Paided;
            ViewData["Head"] = "تفاصيل  " + LectureSch.ReviewsScheduleName;
            return View(LectureDetils);
        }

        public async Task<IActionResult> CloseLecture(int id)
        {
            var LectureSch = await _context.ReviewsSchedule.FindAsync(id);
            LectureSch.Closed = true;
            _context.Update(LectureSch);
            await _context.SaveChangesAsync();

            return RedirectToRoute(new { action = "ReviewDetails", id = id });
        }
        public async Task<IActionResult> OpenLecture(int id)
        {
            var LectureSch = await _context.ReviewsSchedule.FindAsync(id);
            LectureSch.Closed = false;
            _context.Update(LectureSch);
            await _context.SaveChangesAsync();

            return RedirectToRoute(new { action = "ReviewDetails", id = id });
        }
        public async Task<IActionResult> LectureTeacherDetails(int id)
        {
            var schedule = await _context.ReviewsSchedule.FindAsync(id);
            List<StudentLecture> studentLectures = await _context.StudentLecture
               .Where(l => l.scheduleId == id && l.LectureType == schedule.ReviewTypeId && l.Discount > 0 && l.Deleted == 1)
               .Include(s => s.Student)
               .ToListAsync();


            DateTime start = new DateTime(2023, 1, 1, schedule.SessionStart.Hours, schedule.SessionStart.Minutes, schedule.SessionStart.Seconds);
            DateTime end = new DateTime(2023, 1, 1, schedule.SessionEnd.Hours, schedule.SessionEnd.Minutes, schedule.SessionEnd.Seconds);

            List<StudentLecture> studentLectureswithout = await _context.StudentLecture
               .Where(l => l.scheduleId == id && l.LectureType == schedule.ReviewTypeId && l.Discount == 0 && l.Deleted == 1)
                .Include(s => s.Student)
               .ToListAsync();
            List<StudentLecture> studentLecturesall = await _context.StudentLecture
              .Where(l => l.scheduleId == id && l.LectureType == schedule.ReviewTypeId && l.Deleted == 1)
               .Include(s => s.Student)
              .ToListAsync();

            int countwithoutdisc = studentLectureswithout.Count();
            decimal totalwithoutdisc = studentLectureswithout.Sum(l => l.TeacherValue);
            int countwithdisc = studentLectures.Count();
            decimal totalwithdisc = studentLectures.Sum(l => l.TeacherValue);
            decimal totalteacher = studentLecturesall.Sum(l => l.TeacherValue);
            ViewData["lblteacher"] = schedule.ReviewsScheduleName;
            ViewData["id"] = id;
            ViewData["lblappoint"] = schedule.ReviewDate.ToString("yyyy-MM-dd") + " - من : " + start.ToString("t") + " الى : " + end.ToString("t");
            ViewData["lblcountwithoutdisc_teacher"] = "  عدد الحضور بدون خصم : " + countwithoutdisc.ToString();
            ViewData["lbltotalwithoutdisc_teacher"] = " اجمالي : " + totalwithoutdisc.ToString();
            ViewData["lblcountwithdisc_teacher"] = " عدد الحضور بخصم : " + countwithdisc.ToString();
            ViewData["lbltotalwithdisc_teacher"] = " اجمالي بعد الخصم : " + totalwithdisc.ToString();
            ViewData["lbltotalteacher"] = totalteacher.ToString();
            return View(studentLectures);
        }
        public async Task<IActionResult> LectureCenterDetails(int id)
        {
            var schedule = await _context.ReviewsSchedule.FindAsync(id);
            List<StudentLecture> studentLectures = await _context.StudentLecture
               .Where(l => l.scheduleId == id && l.LectureType == schedule.ReviewTypeId && l.Discount > 0)
               .Include(s => s.Student)
               .ToListAsync();


            DateTime start = new DateTime(2023, 1, 1, schedule.SessionStart.Hours, schedule.SessionStart.Minutes, schedule.SessionStart.Seconds);
            DateTime end = new DateTime(2023, 1, 1, schedule.SessionEnd.Hours, schedule.SessionEnd.Minutes, schedule.SessionEnd.Seconds);

            List<StudentLecture> studentLectureswithout = await _context.StudentLecture
               .Where(l => l.scheduleId == id && l.LectureType == schedule.ReviewTypeId && l.Discount == 0)
                .Include(s => s.Student)
               .ToListAsync();
            List<StudentLecture> studentLecturesall = await _context.StudentLecture
              .Where(l => l.scheduleId == id && l.LectureType == schedule.ReviewTypeId)
               .Include(s => s.Student)
              .ToListAsync();

            int countwithoutdisc = studentLectureswithout.Count();
            decimal totalcenterwithoutdisc = studentLectureswithout.Sum(l => l.CenterValue);
            decimal totalteacherwithoutdisc = studentLectureswithout.Sum(l => l.TeacherFee);
            int countwithdisc = studentLectures.Count();
            decimal totalcenterwithdisc = studentLectures.Sum(l => l.CenterValue);
            decimal totalteacherwithdisc = studentLectures.Sum(l => l.TeacherValue);
            decimal totalteacher = studentLecturesall.Sum(l => l.TeacherValue);
            ViewData["id"] = id;
            ViewData["lblappoint"] = schedule.ReviewDate.ToString("yyyy-MM-dd") + " - من : " + start.ToString("t") + " الى : " + end.ToString("t");

            ViewData["lblcountwithoutdisc_center"] = "  عدد الحضور بدون خصم : " + countwithoutdisc.ToString();
            ViewData["lbltotalwithoutdisc_center"] = " اجمالي : " + totalcenterwithoutdisc.ToString();
            ViewData["lblcountwithdisc_center"] = " عدد الحضور بخصم : " + countwithdisc.ToString();
            ViewData["lbltotalwithdisc_center"] = " اجمالي الخصم : " + totalcenterwithdisc.ToString();

            ViewData["lblcountwithoutdisc_teacher"] = "  عدد الحضور بدون خصم : " + countwithoutdisc.ToString();
            ViewData["lbltotalwithoutdisc_teacher"] = " اجمالي : " + totalteacherwithoutdisc.ToString();
            ViewData["lblcountwithdisc_teacher"] = " عدد الحضور بخصم : " + countwithdisc.ToString();
            ViewData["lbltotalwithdisc_teacher"] = " اجمالي الخصم : " + totalteacherwithdisc.ToString();
            ViewData["lbltotalteacher"] = totalteacher.ToString();
            return View(studentLectures);
        }

        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser")]
        public async Task<IActionResult> LectureAttend(int id)
        {
            var schedule = await _context.ReviewsSchedule.FindAsync(id);
            List<StudentLecture> studentLectures = await _context.StudentLecture
               .Where(l => l.scheduleId == id && l.LectureType == schedule.ReviewTypeId)
               .Include(s => s.Student)
               .OrderBy(s => s.StudentIndex)
               .ToListAsync();
            ViewData["scheduleId"] = id;
            if (schedule.ReviewTypeId == 1)
            {
                var reviewsSchedule = await _context.ReviewsSchedule.FindAsync(id);
                ViewData["Head"] = reviewsSchedule.ReviewsScheduleName + " - " + reviewsSchedule.ReviewDate.ToString("yyyy-MM-dd");
            }
            else if (schedule.ReviewTypeId == 2)
            {
                var reviewsSchedule = await _context.ReviewsSchedule.FindAsync(id);
                ViewData["Head"] = reviewsSchedule.ReviewsScheduleName + " - " + reviewsSchedule.ReviewDate.ToString("yyyy-MM-dd");
            }

            return View(studentLectures);
        }
        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser")]
        public async Task<IActionResult> LectureDiscount(int id)
        {
            var schedule = await _context.ReviewsSchedule.FindAsync(id);
            List<StudentLecture> studentLectures = await _context.StudentLecture
               .Where(l => l.scheduleId == id && l.LectureType == schedule.ReviewTypeId && l.Discount > 0)
               .Include(s => s.Student)
               .ToListAsync();
            return View(studentLectures);
        }

        [HttpPost]
        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser")]
        public async Task<IActionResult> ApprovedCoupon(ApprovedCouponViewModel approvedCouponViewModel)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = "   اعتماد كوبون";
            StudentLecture? coupon = await _context.StudentLecture.FindAsync(approvedCouponViewModel.Code);
            string iheader = "";
            if (coupon != null)
            {
                if (coupon.scheduleId == approvedCouponViewModel.scheduleId)
                {
                    // CheckCouponViewModel model = new CheckCouponViewModel();
                    //var iuser = await _context.Users.FindAsync(coupon.CreateId);

                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var user = await _userManager.FindByIdAsync(userId);

                    if (coupon.Deleted != 1)
                    {
                        coupon.Deleted = 1;
                        coupon.DeleteId = user.Id;
                        coupon.DeletedDate = DateTime.Now;
                        _context.Update(coupon);
                        await _context.SaveChangesAsync();
                    }
                    iheader = "تم أعتماد الكوبون";
                    return RedirectToRoute(new { action = "LectureAttend", id = approvedCouponViewModel.scheduleId, Message = iheader });
                }
                else
                {
                    iheader = "الكوبون غير موجود في المجموعة";
                    return RedirectToRoute(new { action = "LectureAttend", id = approvedCouponViewModel.scheduleId, Message = iheader });
                }





            }
            else
            {
                iheader = "الكوبون غير موجود";
                return RedirectToRoute(new { action = "LectureAttend", id = approvedCouponViewModel.scheduleId, Message = iheader });
            }


            return View();
        }
    }
}
