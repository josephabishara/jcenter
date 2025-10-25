using JeCenterWeb.Data;
using JeCenterWeb.Migrations;
using JeCenterWeb.Models;
using JeCenterWeb.Models.Second;
using JeCenterWeb.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NuGet.DependencyResolver;
using System.Composition;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace JeCenterWeb.Areas.scp.Controllers
{
    [Area("scp")]
    [Authorize(Roles = "AdminSettings,PowerUser,SuperUser,User")]
    public class TeachersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<TeachersController> _logger;
        string ControllerName = "المدرسين";
        string AppName = "الأدمن";
        public TeachersController(ApplicationDbContext context,
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
        }
        public async Task<IActionResult> Index()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = "مدرسين مفعلين";
            List<ApplicationUser> users = await _userManager.Users
                .Where(t => t.UserTypeId == 5 && t.Active == true)
                .ToListAsync();
            return View(users);
        }
        public async Task<IActionResult> Deactivate()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = "مدرسين غير مفعلين";
            List<ApplicationUser> users = await _userManager.Users
                .Where(t => t.UserTypeId == 5 && t.Active == false).ToListAsync();
            return View(users);
        }
        public async Task<IActionResult> SearchActiveByWord(string word)
        {
            ViewData["CurrentFilter"] = word;
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = "مدرسين  مفعلين";
            List<ApplicationUser> users = await _userManager.Users
                .Where(t => t.UserTypeId == 5 && t.Active == true && t.FullName.Contains(word)).ToListAsync();
            return View(users);
        }
        public async Task<IActionResult> SearchDeactivateByWord(string word)
        {
            ViewData["CurrentFilter"] = word;
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = "مدرسين غير مفعلين";
            List<ApplicationUser> users = await _userManager.Users
                .Where(t => t.UserTypeId == 5 && t.Active == false && t.FullName.Contains(word)).ToListAsync();
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["Password"] = CreateRandomPassword(6);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TeacherViewModel teacherViewModel)
        {
            if (ModelState.IsValid)
            {

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int uid = Convert.ToInt32(userId);
                string imgurlpath = "images/Teachers/default.png";
                if (teacherViewModel.imgurl != null)
                {
                    string folder = "images/Teachers/";
                    folder += Guid.NewGuid().ToString() + "-" + teacherViewModel.imgurl.FileName;
                    imgurlpath = folder;
                    string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                    await teacherViewModel.imgurl.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
                }
                var teacher = new ApplicationUser();

                teacher.Email = "t" + teacherViewModel.Mobile + "@elra3ycenter.com";
                teacher.UserName = teacherViewModel.Mobile;
                teacher.FullName = teacherViewModel.FullName;
                teacher.PhoneNumber = teacherViewModel.Mobile;
                teacher.Mobile = teacherViewModel.Mobile;
                teacher.UserTypeId = 5;
                teacher.imgurl = imgurlpath;
                teacher.Pwd = teacherViewModel.Password;
                teacher.AccountID = 0;
                teacher.Active = true;
                teacher.CreateId = uid;
                teacher.CreatedDate = DateTime.Now;

                var result = await _userManager.CreateAsync(teacher, teacherViewModel.Password);
                if (result.Succeeded)
                {
                    List<string> role = new List<string>();
                    role.Add("Teacher");
                    await _userManager.AddToRolesAsync(teacher, role);


                    FinancialAccount financialAccount = new FinancialAccount
                    {
                        AccountName = teacherViewModel.FullName,
                        AccountTypeId = 5,
                        Active = true,
                        CreateId = uid,
                        CreatedDate = DateTime.Now,
                    };

                    _context.Add(financialAccount);
                    await _context.SaveChangesAsync();
                    int AccountId = financialAccount.FinancialAccountId;
                    var user = await _context.Users.FindAsync(teacher.Id);
                    user.AccountID = AccountId;

                    _context.Update(user);
                    await _context.SaveChangesAsync();

                    return RedirectToRoute(new { Areas = "scp", controller = "Teachers", action = "Details", id = user.Id });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            else
            {
                return View(teacherViewModel);
            }

            //ViewData["Gender"] = new SelectList(_context.ResourceGender, "GenderId", "GenderName", addUserViewModel.Gender);
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var teacher = await _context.Users.Where(u => u.Id == id & u.UserTypeId == 5)
                  .FirstOrDefaultAsync();
            EditTeacherViewModel model = new EditTeacherViewModel();
            if (teacher != null)
            {
                ViewData["id"] = id;
                ViewData["FullName"] = teacher.FullName;
                model.id = id;
                model.FullName = teacher.FullName;
                model.Mobile = teacher.Mobile;
                return View(model);
            }

            return RedirectToRoute(new { controller = "Teachers", action = "Index", area = "scp" });

        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("id,FullName,Mobile")] EditTeacherViewModel teacherViewModel)
        {
            var user = await _context.Users.FindAsync(teacherViewModel.id);

            user.UserName = teacherViewModel.Mobile;
            user.FullName = teacherViewModel.FullName;
            user.PhoneNumber = teacherViewModel.Mobile;
            user.Mobile = teacherViewModel.Mobile;
            user.Active = true;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToRoute(new { area = "scp", controller = "Teachers", action = "Details", id = teacherViewModel.id });
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeState(int id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id.ToString());
            if (user.Active == false)
                user.Active = true;
            else if (user.Active == true)
                user.Active = false;

            await _userManager.UpdateAsync(user);

            return RedirectToRoute(new { action = "Details", id = id });

        }
        public static string CreateRandomPassword(int PasswordLength)
        {
            string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyz";

            Random randNum = new Random();

            char[] chars = new char[PasswordLength];

            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }

            return new string(chars);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            TeacherPageViewModel teacher = new TeacherPageViewModel();
            teacher.Teacher = await _context.Users.FindAsync(id);
            var usersBalance = await _context.UsersBalance.FindAsync(teacher.Teacher.AccountID);
            teacher.TeacherTotal = usersBalance.Balance;
            teacher.TeacherAssistant = await _context.Users.Where(u => u.ParentId == id).CountAsync();
            teacher.TeacherCards = 0;
            teacher.TeacherGroup = await _context.CGroups.Where(g => g.TeacherId == id).CountAsync();
            teacher.TeacherExams = await _context.ReviewsSchedule.Where(g => g.TeacherId == id && g.ReviewTypeId == 2).CountAsync();
            teacher.TeacherReviews = await _context.ReviewsSchedule.Where(g => g.TeacherId == id && g.ReviewTypeId == 1).CountAsync();
            teacher.TeacherStudent = await _context.StudentGroup.Where(s => s.CGroups.TeacherId == id).CountAsync();
            teacher.TeacherVideos = await _context.TeachesVideos.Where(s => s.TeacherId == id).CountAsync();
            teacher.TeacherDiscount = await _context.StudentDiscount.Where(d => d.TeacherId == id).CountAsync();
            return View(teacher);
        }
        [HttpGet]
        public async Task<IActionResult> ChooseTeacher()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            List<ApplicationUser> Teachers = await _userManager.Users
                .Where(t => t.UserTypeId == 5 && t.Active == true)
                .Where(t => t.CGroups.Count > 0)
                .Include(t => t.CGroups)
                .OrderBy(t => t.FullName)
                .ToListAsync();

            return View(Teachers);
        }
        public async Task<IActionResult> Groups(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var teacher = await _context.Users.FindAsync(id);
            ViewData["TeacherName"] = teacher.FullName;
            ViewData["TeacherId"] = id;
            ViewData["physicalyearId"] = new SelectList(_context.PhysicalYear.Where(y => y.Active == true), "PhysicalyearId", "PhysicalyearName");
            ViewData["SyllabusID"] = new SelectList(_context.TeacherSyllabus.Where(s => s.TeacherId == id), "SyllabusID", "SyllabusName");
            ViewData["DayOfWeekId"] = new SelectList(_context.ResourceDays, "DayOfWeekId", "DayOfWeekName");
            ViewData["RoomId"] = new SelectList(_context.CRooms, "RoomId", "RoomName");

            var groups = await _context.CGroups
                .Where(g => g.TeacherId == id)
                .Include(g => g.CSyllabus)
                .Include(g => g.PhysicalYear)
                .Include(g => g.ResourceDays)
                .ToListAsync();
            return View(groups);
        }
        public async Task<IActionResult> Reviews(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var teacher = await _context.Users.FindAsync(id);
            ViewData["TeacherName"] = teacher.FullName;
            ViewData["TeacherId"] = id;
            ViewData["physicalyearId"] = new SelectList(_context.PhysicalYear.Where(y => y.Active == true), "PhysicalyearId", "PhysicalyearName");
            ViewData["SyllabusID"] = new SelectList(_context.TeacherSyllabus.Where(s => s.TeacherId == id), "SyllabusID", "SyllabusName");
            ViewData["RoomId"] = new SelectList(_context.CRooms, "RoomId", "RoomName");

            var Reviews = await _context.ReviewsSchedule
                .Where(g => g.TeacherId == id && g.ReviewTypeId == 1)
                .Include(g => g.CSyllabus)
                .Include(g => g.PhysicalYear)
                .ToListAsync();
            return View(Reviews);
        }
        public async Task<IActionResult> Exams(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var teacher = await _context.Users.FindAsync(id);
            ViewData["TeacherName"] = teacher.FullName;
            ViewData["TeacherId"] = id;
            ViewData["physicalyearId"] = new SelectList(_context.PhysicalYear.Where(y => y.Active == true), "PhysicalyearId", "PhysicalyearName");
            ViewData["SyllabusID"] = new SelectList(_context.TeacherSyllabus.Where(s => s.TeacherId == id), "SyllabusID", "SyllabusName");
            ViewData["RoomId"] = new SelectList(_context.CRooms, "RoomId", "RoomName");

            var Reviews = await _context.ReviewsSchedule
                .Where(g => g.TeacherId == id && g.ReviewTypeId == 2)
                .Include(g => g.CSyllabus)
                .Include(g => g.PhysicalYear)
                .ToListAsync();
            return View(Reviews);
        }
        public async Task<IActionResult> Videos(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var teacher = await _context.Users.FindAsync(id);
            ViewData["TeacherName"] = teacher.FullName;
            ViewData["TeacherId"] = id;
            ViewData["SyllabusID"] = new SelectList(_context.TeacherSyllabus.Where(s => s.TeacherId == id), "SyllabusID", "SyllabusName");

            var Videos = await _context.TeachesVideos
                .Where(g => g.TeacherId == id)
                .Include(g => g.CSyllabus)
                .ToListAsync();
            return View(Videos);
        }
        public async Task<IActionResult> GroupTeacher(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var teacher = await _context.Users.FindAsync(id);
            ViewData["TeacherName"] = teacher.FullName;
            ViewData["TeacherId"] = id;

            ViewData["physicalyearId"] = new SelectList(_context.PhysicalYear.Where(y => y.Active == true), "PhysicalyearId", "PhysicalyearName");
            var groups = await _context.CGroups
            .Where(g => g.TeacherId == id)
            .Include(g => g.CSyllabus)
            .Include(g => g.CSyllabus.CPhases)
            .Include(g => g.PhysicalYear)
            .Include(g => g.CRooms)
            .Include(g => g.ResourceDays)
            .Include(g => g.StudentGroup)
            .ToListAsync();
            return View(groups);
        }

        public async Task<IActionResult> GroupTeacherByYear([Bind("TeacherId,PhysicalyearId")] GroupTeacherViewModel model)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var teacher = await _context.Users.FindAsync(model.TeacherId);
            ViewData["TeacherName"] = teacher.FullName;
            ViewData["TeacherId"] = model.TeacherId;

            ViewData["physicalyearId"] = new SelectList(_context.PhysicalYear.Where(y => y.Active == true), "PhysicalyearId", "PhysicalyearName");
            var groups = await _context.CGroups
                .Where(g => g.TeacherId == model.TeacherId && g.physicalyearId == model.PhysicalyearId)
                .Include(g => g.CSyllabus)
                .Include(g => g.CSyllabus.CPhases)
                .Include(g => g.PhysicalYear)
                .Include(g => g.CRooms)
                .Include(g => g.ResourceDays)
                .Include(g => g.StudentGroup)
                .ToListAsync();

            var physicalYear = await _context.PhysicalYear.FindAsync(model.PhysicalyearId);
            ViewData["PhysicalyearName"] = physicalYear.PhysicalyearName;

            return View(groups);
        }

        public async Task<IActionResult> Assistans(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var teacher = await _context.Users.FindAsync(id);
            ViewData["TeacherName"] = teacher.FullName;
            ViewData["TeacherId"] = id;

            var Assistans = await _context.Users
                .Where(g => g.ParentId == id)
                .ToListAsync();
            return View(Assistans);
        }
        [HttpGet]
        public async Task<IActionResult> CreateAssistant(int id)
        {
            //  ViewData["Gender"] = new SelectList(_context.ResourceGender, "GenderId", "GenderName");
            ViewData["Password"] = CreateRandomPassword(8);
            ViewData["ParentId"] = id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAssistant(AssistantViewModel assistantViewModel)
        {
            if (ModelState.IsValid)
            {
                string imgurlpath = "images/Assistant/default.png";
                if (assistantViewModel.imgurl != null)
                {
                    string folder = "images/Assistant/";
                    folder += Guid.NewGuid().ToString() + "-" + assistantViewModel.imgurl.FileName;
                    imgurlpath = folder;
                    string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                    await assistantViewModel.imgurl.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
                }
                var user = new ApplicationUser();

                user.Email = assistantViewModel.Email;
                user.UserName = assistantViewModel.Mobile;
                user.FullName = assistantViewModel.FullName;
                user.PhoneNumber = assistantViewModel.Mobile;
                user.Mobile = assistantViewModel.Mobile;
                user.UserTypeId = 6;
                user.imgurl = imgurlpath;
                user.Pwd = assistantViewModel.Password;
                user.ParentId = assistantViewModel.ParentId;

                var result = await _userManager.CreateAsync(user, assistantViewModel.Password);
                if (result.Succeeded)
                {
                    List<string> role = new List<string>();
                    role.Add("Assistant");

                    await _userManager.AddToRolesAsync(user, role);
                    return RedirectToRoute(new { action = "Assistans", id = assistantViewModel.ParentId });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            //ViewData["Gender"] = new SelectList(_context.ResourceGender, "GenderId", "GenderName", addUserViewModel.Gender);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddGroup(TeacherGroupsViewModel teacherGroupsViewModel)
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
                Active = true,
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
                    MonthCode = Convert.ToDateTime(lectureDate).ToString("yyyy") + Convert.ToDateTime(lectureDate).ToString("MM"),

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

            return RedirectToRoute(new { action = "Groups", id = teacherGroupsViewModel.TeacherId });
        }
        [HttpPost]
        public async Task<IActionResult> AddReview(ReviewsSchedule reviewsSchedule)
        {
            reviewsSchedule.MonthCode = Convert.ToDateTime(reviewsSchedule.ReviewDate).ToString("yyyy") + Convert.ToDateTime(reviewsSchedule.ReviewDate).ToString("MM");
            _context.Add(reviewsSchedule);
            await _context.SaveChangesAsync();
            return RedirectToRoute(new { action = "Reviews", id = reviewsSchedule.TeacherId });
        }
        [HttpPost]
        public async Task<IActionResult> AddExam(ReviewsSchedule reviewsSchedule)
        {
            reviewsSchedule.MonthCode = Convert.ToDateTime(reviewsSchedule.ReviewDate).ToString("yyyy") + Convert.ToDateTime(reviewsSchedule.ReviewDate).ToString("MM");
            _context.Add(reviewsSchedule);
            await _context.SaveChangesAsync();
            return RedirectToRoute(new { action = "Exams", id = reviewsSchedule.TeacherId });
        }
        public async Task<IActionResult> GroupSchedule(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var group = await _context.CGroups.FindAsync(id);
            var teacher = await _context.Users.FindAsync(group.TeacherId);
            ViewData["TeacherName"] = teacher.FullName;
            ViewData["TeacherId"] = group.TeacherId;
            ViewData["GenderId"] = new SelectList(_context.ResourceGender, "GenderId", "lecture");
            ViewData["physicalyearId"] = new SelectList(_context.PhysicalYear.Where(y => y.Active == true), "PhysicalyearId", "PhysicalyearName");
            ViewData["SyllabusID"] = new SelectList(_context.TeacherSyllabus.Where(s => s.TeacherId == id), "SyllabusID", "SyllabusName");
            ViewData["DayOfWeekId"] = new SelectList(_context.ResourceDays, "DayOfWeekId", "DayOfWeekName");
            ViewData["RoomId"] = new SelectList(_context.CRooms, "RoomId", "RoomName");

            var groups = await _context.CGroupSchedule
                .Where(g => g.GroupId == id)
                .Include(g => g.CGroups)
                .Include(g => g.CGroups.CRooms)
                .ToListAsync();
            return View(groups);
        }

        // Close Lecture 
        public async Task<IActionResult> lectureDetails(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            int uid = Convert.ToInt32(userId);
            bool OpenLecture = await Isaccessright(user.Id, 11);
            if (OpenLecture)
            {
                ViewData["OpenLecture"] = "true";
            }
            else
            {
                ViewData["OpenLecture"] = "false";
            }
            LectureDetilsViewModel LectureDetils = new LectureDetilsViewModel();
            var LectureSch = await _context.CGroupSchedule.FindAsync(id);

            int groupid = LectureSch.GroupId;
            ViewData["GroupId"] = groupid;
            var group = await _context.CGroups.FindAsync(groupid);
            List<StudentLecture> studentLectures = await _context.StudentLecture
                .Where(l => l.scheduleId == id && l.LectureType == 0)
                .ToListAsync();
            LectureDetils.LectureId = id;

            LectureDetils.AttendCount = studentLectures.Count();
            int countofstudentingroup = await _context.StudentGroup
                .Where(s => s.GroupId == groupid)
                .CountAsync();
            LectureDetils.AbsentCount = LectureDetils.AttendCount - countofstudentingroup;
            LectureDetils.DiscountCount = studentLectures.Where(l => l.Discount > 0).Count();
            LectureDetils.Closed = LectureSch.Closed;
            LectureDetils.Paided = LectureSch.Paided;
            ViewData["Head"] = "تفاصيل حصة: المدرس: " + group.TeacherName + " -- مجموعة:  " + group.GroupName;
            return View(LectureDetils);
        }

        public async Task<IActionResult> CloseLecture(int id)
        {
            var LectureSch = await _context.CGroupSchedule.FindAsync(id);
            LectureSch.Closed = true;
            _context.Update(LectureSch);
            await _context.SaveChangesAsync();

            return RedirectToRoute(new { action = "lectureDetails", id = id });
        }

        public async Task<IActionResult> OpenLecture(int id)
        {
            var LectureSch = await _context.CGroupSchedule.FindAsync(id);
            LectureSch.Closed = false;
            _context.Update(LectureSch);
            await _context.SaveChangesAsync();

            return RedirectToRoute(new { action = "lectureDetails", id = id });
        }

        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser,User")]
        public async Task<IActionResult> LectureAttend(int id, string? message)
        {
            ViewData["scheduleId"] = id;
            ViewData["Message"] = message;
            List<StudentLecture> studentLectures = await _context.StudentLecture
               .Where(l => l.scheduleId == id && l.LectureType == 0)
               .Include(s => s.Student)
               .ToListAsync();

            var studentLecture = await _context.StudentLecture
              .Where(l => l.scheduleId == id && l.LectureType == 0)
              .FirstOrDefaultAsync();

            var groupSchedule = await _context.CGroupSchedule.FindAsync(studentLecture.scheduleId);
            var group = await _context.CGroups.FindAsync(groupSchedule.GroupId);

            ViewData["Head"] = "تفاصيل حصة: المدرس: " + group.TeacherName + " -- مجموعة:  " + group.GroupName;
            return View(studentLectures);
        }

        [HttpPost]
        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser,User")]
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

        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser")]
        public async Task<IActionResult> LectureDiscount(int id)
        {
            List<StudentLecture> studentLectures = await _context.StudentLecture
               .Where(l => l.scheduleId == id && l.Discount > 0)
               .Include(s => s.Student)
               .ToListAsync();
            return View(studentLectures);
        }
        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser")]
        public async Task<IActionResult> LectureAbsent(int id)
        {
            var StudentLecture = await _context.CGroupSchedule
               .Where(l => l.GroupscheduleId == id)
               .FirstOrDefaultAsync();

            string GroupId = StudentLecture.GroupId.ToString();
            string GroupscheduleId = id.ToString();
            string StoredProc = "exec LectureAbsentViewModel " + GroupId + " , " + GroupscheduleId;
            var sqlQuery = await _context.LectureAbsentViewModel.FromSql($"exec LectureAbsentViewModel {GroupId} ,{GroupscheduleId} ").ToListAsync();

            return View(sqlQuery);

        }

        public async Task<IActionResult> LectureTeacherDetails(int id)
        {
            List<StudentLecture> studentLectures = await _context.StudentLecture
               .Where(l => l.scheduleId == id && l.LectureType == 0 && l.Discount > 0 && l.Deleted == 1)
               .Include(s => s.Student)
               .ToListAsync();
            var schedule = await _context.CGroupSchedule.FindAsync(id);
            var group = await _context.CGroups.FindAsync(schedule.GroupId);

            DateTime start = new DateTime(2023, 1, 1, group.SessionStart.Hours, group.SessionStart.Minutes, group.SessionStart.Seconds);
            DateTime end = new DateTime(2023, 1, 1, group.SessionEnd.Hours, group.SessionEnd.Minutes, group.SessionEnd.Seconds);

            List<StudentLecture> studentLectureswithout = await _context.StudentLecture
               .Where(l => l.scheduleId == id && l.LectureType == 0 && l.Discount == 0 && l.Deleted == 1)
                .Include(s => s.Student)
               .ToListAsync();
            List<StudentLecture> studentLecturesall = await _context.StudentLecture
              .Where(l => l.scheduleId == id && l.LectureType == 0 && l.Deleted == 1)
               .Include(s => s.Student)
              .ToListAsync();

            int countwithoutdisc = studentLectureswithout.Count();
            decimal totalwithoutdisc = studentLectureswithout.Sum(l => l.TeacherValue);
            int countwithdisc = studentLectures.Count();
            decimal totalwithdisc = studentLectures.Sum(l => l.TeacherValue);
            decimal totalteacher = studentLecturesall.Sum(l => l.TeacherValue);
            ViewData["header"] = 0;
            ViewData["lblteacher"] = group.TeacherName;
            ViewData["lblappoint"] = schedule.LectureDate.ToString("yyyy-MM-dd") + " - من : " + start.ToString("t") + " الى : " + end.ToString("t");
            ViewData["lblcountwithoutdisc_teacher"] = "  عدد الحضور بدون خصم : " + countwithoutdisc.ToString();
            ViewData["lbltotalwithoutdisc_teacher"] = " اجمالي : " + totalwithoutdisc.ToString();
            ViewData["lblcountwithdisc_teacher"] = " عدد الحضور بخصم : " + countwithdisc.ToString();
            ViewData["lbltotalwithdisc_teacher"] = " اجمالي بعد الخصم : " + totalwithdisc.ToString();
            ViewData["lbltotalteacher"] = totalteacher.ToString();
            return View(studentLectures);
        }

        public async Task<IActionResult> LectureCenterDetails(int id)
        {
            List<StudentLecture> studentLectures = await _context.StudentLecture
               .Where(l => l.scheduleId == id && l.LectureType == 0 && l.Discount > 0)
               .Include(s => s.Student)
               .ToListAsync();
            var schedule = await _context.CGroupSchedule.FindAsync(id);
            var group = await _context.CGroups.FindAsync(schedule.GroupId);

            DateTime start = new DateTime(2023, 1, 1, group.SessionStart.Hours, group.SessionStart.Minutes, group.SessionStart.Seconds);
            DateTime end = new DateTime(2023, 1, 1, group.SessionEnd.Hours, group.SessionEnd.Minutes, group.SessionEnd.Seconds);

            List<StudentLecture> studentLectureswithout = await _context.StudentLecture
               .Where(l => l.scheduleId == id && l.LectureType == 0 && l.Discount == 0)
                .Include(s => s.Student)
               .ToListAsync();
            List<StudentLecture> studentLecturesall = await _context.StudentLecture
              .Where(l => l.scheduleId == id && l.LectureType == 0)
               .Include(s => s.Student)
              .ToListAsync();

            int countwithoutdisc = studentLectureswithout.Count();
            decimal totalcenterwithoutdisc = studentLectureswithout.Sum(l => l.CenterValue);
            decimal totalteacherwithoutdisc = studentLectureswithout.Sum(l => l.TeacherFee);
            int countwithdisc = studentLectures.Count();
            decimal totalcenterwithdisc = studentLectures.Sum(l => l.CenterValue);
            decimal totalteacherwithdisc = studentLectures.Sum(l => l.TeacherValue);
            decimal totalteacher = studentLecturesall.Sum(l => l.TeacherValue);
            ViewData["header"] = 0;
            ViewData["lblteacher"] = group.TeacherName;
            ViewData["lblappoint"] = schedule.LectureDate.ToString("yyyy-MM-dd") + " - من : " + start.ToString("t") + " الى : " + end.ToString("t");

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



        [HttpPost]
        public async Task<IActionResult> CloseedLecture(int id)
        {
            var LectureSch = await _context.CGroupSchedule.FindAsync(id);
            LectureSch.Closed = true;
            _context.Update(LectureSch);
            await _context.SaveChangesAsync();
            //   LectureSch.
            return RedirectToRoute(new { action = "GroupSchedule", id = LectureSch.GroupId });
        }

        [HttpPost]
        public async Task<IActionResult> CollectLecture(int id)
        {
            var LectureSch = await _context.CGroupSchedule.FindAsync(id);
            LectureSch.Paided = true;
            LectureSch.Closed = true;
            _context.Update(LectureSch);
            await _context.SaveChangesAsync();

            var Lecture = await _context.CloseStudentLectureViewModel
               .Where(l => l.scheduleId == id & l.LectureType == 0)
               .FirstOrDefaultAsync();


            //   LectureSch.
            return RedirectToRoute(new { action = "GroupSchedule", id = LectureSch.GroupId });
        }
        public async Task<IActionResult> Syllabus(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var teacher = await _context.Users.FindAsync(id);
            ViewData["TeacherName"] = teacher.FullName;

            ViewData["PhaseId"] = new SelectList(_context.CPhases.Where(p => p.Active == true && p.Parent > 0), "PhaseId", "PhaseName");
            ViewData["TeacherId"] = id;

            var teacherSyllabus = await _context.TeacherSyllabus
                .Where(t => t.TeacherId == id)
                .OrderBy(s => s.PhaseId)
                .ToListAsync();
            return View(teacherSyllabus);
        }

        public async Task<IActionResult> EditSyllabus(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var model = await _context.TeacherSyllabus.FindAsync(id);
            ViewData["TeacherName"] = model.TeacherName;
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSyllabus(int id, [Bind("TeacherSyllabusId,TeacherId,PhaseId,SyllabusID,TeacherName,PhaseName,SyllabusName,ApplicationCenter,ApplicationTeacher")] TeacherSyllabus model)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            if (id != model.TeacherSyllabusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var teacherSyllabus = await _context.TeacherSyllabus.FindAsync(id);

                teacherSyllabus.ApplicationCenter = model.ApplicationCenter;
                teacherSyllabus.ApplicationTeacher = model.ApplicationTeacher;
                teacherSyllabus.ApplicationValue = model.ApplicationCenter + model.ApplicationTeacher;
                teacherSyllabus.UpdateId = Convert.ToInt32(userId);
                teacherSyllabus.UpdatedDate = DateTime.Now;
                teacherSyllabus.Active = true;
                _context.Update(teacherSyllabus);
                await _context.SaveChangesAsync();


                return RedirectToRoute(new { action = "Syllabus", id = teacherSyllabus.TeacherId });
            }
            return View(model);
        }



        //public async Task<IActionResult> Syllabusname()
        //{
        //    int counter = 0;
        //    var model = await _context.TeacherSyllabus.ToListAsync();
        //    foreach (var item in model)
        //    {
        //        var Syllabus = await _context.CSyllabus.FindAsync(item.SyllabusID);
        //        item.SyllabusName = Syllabus.SyllabusName;
        //        var phase = await _context.CPhases.FindAsync(item.PhaseId);
        //        item.PhaseName = phase.PhaseName;
        //        var teacher = await _context.Users.FindAsync(item.TeacherId);
        //        item.TeacherName = teacher.FullName;
        //        _context.Update(item);
        //        await _context.SaveChangesAsync();
        //        counter++;
        //    }
        //    ViewData["done"]= counter;
        //    return View();
        //}


        //[Produces("application/json")]  Cardwrite
        [HttpGet]
        public object GetSyllabusID(int PhaseId)
        {
            //  var iSyllabus =  _context.CSyllabus.Where(s => s.PhaseID == PhaseId).ToList();
            return (_context.CSyllabus.Where(s => s.PhaseID == PhaseId).ToList().Select(x => new
            {
                Id = x.SyllabusID,
                Name = x.SyllabusName,

            }));
        }
        [HttpPost]
        public async Task<IActionResult> AddSyllabus([Bind("TeacherId,SyllabusID,PhaseId,ApplicationCenter,ApplicationTeacher")] TeacherSyllabus teacherSyllabus)
        {
            var Syllabus = await _context.CSyllabus.FindAsync(teacherSyllabus.SyllabusID);
            teacherSyllabus.SyllabusName = Syllabus.SyllabusName;

            var phase = await _context.CPhases.FindAsync(teacherSyllabus.PhaseId);
            teacherSyllabus.PhaseName = phase.PhaseName;

            var teacher = await _context.Users.FindAsync(teacherSyllabus.TeacherId);
            teacherSyllabus.TeacherName = teacher.FullName;

            teacherSyllabus.ApplicationValue = teacherSyllabus.ApplicationCenter + teacherSyllabus.ApplicationTeacher;

            _context.Add(teacherSyllabus);
            await _context.SaveChangesAsync();

            return RedirectToRoute(new { action = "Syllabus", id = teacherSyllabus.TeacherId });
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
        public async Task<IActionResult> DiscountTeacher(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var teacher = await _context.Users.FindAsync(id);
            ViewData["TeacherName"] = teacher.FullName;
            ViewData["TeacherId"] = id;
            // ViewData["physicalyearId"] = new SelectList(_context.PhysicalYear.Where(p => p.Active == true), "PhysicalyearId", "PhysicalyearName");
            // ViewData["StudentId"] = new SelectList(_context.FinancialAccount.Where(s => s.AccountTypeId == 7), "FinancialAccountId", "AccountName");
            var Discount = await _context.StudentDiscount
                .Where(g => g.TeacherId == id)
                .Include(t => t.FinancialAccount)
                .ToListAsync();
            return View(Discount);
        }
        // AddDiscount
        [HttpPost]
        public async Task<IActionResult> AddDiscount([Bind("LectureType,StudentId,TeacherId,CenterDiscount,teacherDiscount,physicalyearId,Notes")] StudentDiscount studentDiscount)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            studentDiscount.Active = true;
            studentDiscount.CreateId = uid;
            studentDiscount.UpdateId = 0;
            studentDiscount.CreatedDate = DateTime.Now;
            studentDiscount.Discount = studentDiscount.CenterDiscount + studentDiscount.teacherDiscount;
            _context.Add(studentDiscount);
            await _context.SaveChangesAsync();
            return RedirectToRoute(new { action = "DiscountTeacher", id = studentDiscount.TeacherId });
        }
        [HttpGet]
        public async Task<IActionResult> ChangePassword(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["Password"] = CreateRandomPassword(8);
            var employee = await _context.Users.FindAsync(id);
            if (employee.UserTypeId < 5)
                return RedirectToAction(nameof(Index));
            ChangePasswordStudentUserViewModel model = new ChangePasswordStudentUserViewModel
            {
                Id = id,
                FullName = employee.FullName,
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(int id, [Bind("Id,Password,FullName")] ChangePasswordStudentUserViewModel model)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ApplicationUser employee = await _userManager.FindByIdAsync(id.ToString());

            if (employee.UserTypeId != 5)
                return RedirectToAction(nameof(Index));

            // employee.FullName = model.FullName;
            employee.Pwd = model.Password;
            var result1 = _userManager.UpdateAsync(employee).Result;
            var result = _userManager.AddPasswordAsync(employee, model.Password).Result;


            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> TeachersSchedule()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["today"] = DateTime.Today.ToString("yyyy-MM-dd");
            DateTime dd = DateTime.Today.Date;
            dd = dd.AddDays(1);

            var groups = await _context.CGroupSchedule
                .Where(g => g.LectureDate > DateTime.Today.Date && g.LectureDate < dd)
                .Include(g => g.CGroups)
                .Include(g => g.CGroups.CRooms)
                .OrderBy(g => g.CGroups.TeacherName)
                .ToListAsync();
            return View(groups);
        }
        public async Task<IActionResult> SessionStartSchedule()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["today"] = DateTime.Today.ToString("yyyy-MM-dd");
            DateTime dd = DateTime.Today.Date;
            dd = dd.AddDays(1);

            var groups = await _context.CGroupSchedule
                .Where(g => g.LectureDate > DateTime.Today.Date && g.LectureDate < dd)
                .Include(g => g.CGroups)
                .Include(g => g.CGroups.CRooms)
                .OrderBy(g => g.CGroups.SessionStart)
                .ToListAsync();
            return View(groups);
        }
        public async Task<IActionResult> RoomSchedule()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["today"] = DateTime.Today.ToString("yyyy-MM-dd");
            DateTime dd = DateTime.Today.Date;
            dd = dd.AddDays(1);

            var groups = await _context.CGroupSchedule
                .Where(g => g.LectureDate > DateTime.Today.Date && g.LectureDate < dd)
                .Include(g => g.CGroups)
                .Include(g => g.CGroups.CRooms)
                .OrderBy(g => g.CGroups.RoomId)
                .ToListAsync();
            return View(groups);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateImage(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var employee = await _context.Users.FindAsync(id);
            if (employee.UserTypeId != 5)
                return RedirectToAction(nameof(Index));
            UpdateImageStudentUserViewModel model = new UpdateImageStudentUserViewModel
            {
                Id = id,
                oldimgurl = employee.imgurl,
                FullName = employee.FullName,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateImage(int id, [Bind("Id,FullName,imgurl")] UpdateImageStudentUserViewModel model)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ApplicationUser employee = await _userManager.FindByIdAsync(id.ToString());

            if (employee.UserTypeId != 5)
                return RedirectToAction(nameof(Index));

            string imgurlpath = "images/Teachers/default.jpg";
            if (model.imgurl != null)
            {
                string folder = "images/Teachers/";
                folder += Guid.NewGuid().ToString() + "-" + model.imgurl.FileName;
                imgurlpath = folder;
                string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                await model.imgurl.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
            }

            // employee.FullName = model.FullName;
            employee.imgurl = imgurlpath;

            var result = _userManager.UpdateAsync(employee).Result;


            return RedirectToAction(nameof(Index));
        }


        // Teacher Scedule Report 
        public async Task<IActionResult> TeacherScheduleReport(int? id)
        {
            if (id == 1)
            {
                ViewData["Error"] = "تاريخ البداية مطلوب";
            }
            if (id == 2 || id == 3)
            {
                ViewData["Error"] = "تاريخ البداية يجب أن يكون أقل من تاريخ النهاية";
            }


            ViewData["TeacherId"] = new SelectList(_userManager.Users.Where(t => t.UserTypeId == 5 && t.Active == true), "Id", "FullName");
            return View();
        }

        public async Task<IActionResult> TeacherScheduleReportByDate([Bind("StartDate,EndDate,TeacherId,SyllabusID")] DateRangeTeacherViewModel dateViewModel)
        {
            if (dateViewModel.StartDate == null)
            {
                return RedirectToAction("TeacherScheduleReport", 1);
            }
            if (dateViewModel.EndDate != null)
            {
                if (dateViewModel.StartDate > dateViewModel.EndDate)
                {
                    return RedirectToAction("TeacherScheduleReport", 2);
                }
                if (dateViewModel.StartDate == dateViewModel.EndDate)
                {
                    return RedirectToAction("TeacherScheduleReport", 3);
                }
            }

            if (dateViewModel.EndDate == null)
            {
                if (dateViewModel.TeacherId != 0)
                {
                    if (dateViewModel.SyllabusID == 0)
                    {
                        var report = await _context.TeacherScheduleReport
                            .Where(g => g.LectureDate == dateViewModel.StartDate && g.TeacherId == dateViewModel.TeacherId)
                            .ToListAsync();
                        var teacher = await _userManager.Users.Where(u => u.Id == dateViewModel.TeacherId).FirstOrDefaultAsync();
                        ViewData["StartDate"] = dateViewModel.StartDate;
                        ViewData["EndDate"] = dateViewModel.EndDate;
                        ViewData["ReportType"] = 2;
                        ViewData["ReportName"] = "تقرير يومي لـ " + teacher.FullName;
                        ViewData["TeacherId"] = new SelectList(_userManager.Users.Where(t => t.UserTypeId == 5 && t.Active == true), "Id", "FullName");
                        if (report.Count == 0)
                        {
                            ViewData["max"] = 0;
                        }
                        else
                        {
                            ViewData["max"] = report.Max(d => d.Count);
                        }
                        return View(report.OrderBy(d => d.LectureDate));
                    }
                    else
                    {
                      var report = await _context.TeacherScheduleReport
                            .Where(g => g.LectureDate == dateViewModel.StartDate && g.TeacherId == dateViewModel.TeacherId && g.SyllabusID == dateViewModel.SyllabusID)
                            .ToListAsync();
                        var teacher = await _userManager.Users.Where(u => u.Id == dateViewModel.TeacherId).FirstOrDefaultAsync();
                        ViewData["StartDate"] = dateViewModel.StartDate;
                        ViewData["EndDate"] = dateViewModel.EndDate;
                        ViewData["ReportType"] = 2;
                        ViewData["ReportName"] = "تقرير يومي لـ " + teacher.FullName;
                        ViewData["TeacherId"] = new SelectList(_userManager.Users.Where(t => t.UserTypeId == 5 && t.Active == true), "Id", "FullName");
                        if (report.Count == 0)
                        {
                            ViewData["max"] = 0;
                        }
                        else
                        {
                            ViewData["max"] = report.Max(d => d.Count);
                        }
                        return View(report.OrderBy(d => d.LectureDate));
                    }
                }
                else
                {
                    ViewData["ReportName"] = "   ";
                    ViewData["TeacherId"] = new SelectList(_userManager.Users.Where(t => t.UserTypeId == 5 && t.Active == true), "Id", "FullName");
                    ViewData["max"] = 0;
                    return View();
                }

            }
            else
            {
                if (dateViewModel.TeacherId == 0)
                {
                    ViewData["ReportName"] = "   ";
                    ViewData["TeacherId"] = new SelectList(_userManager.Users.Where(t => t.UserTypeId == 5 && t.Active == true), "Id", "FullName");
                    ViewData["max"] = 0;
                    return View();
                }
                else
                {
                    if (dateViewModel.SyllabusID == 0)
                    {
                        var report = await _context.TeacherScheduleReport
                            .Where(g => g.LectureDate >= dateViewModel.StartDate && g.LectureDate <= dateViewModel.EndDate && g.TeacherId == dateViewModel.TeacherId)
                            .ToListAsync();
                        var teacher = await _userManager.Users.Where(u => u.Id == dateViewModel.TeacherId).FirstOrDefaultAsync();
                        ViewData["StartDate"] = dateViewModel.StartDate;
                        ViewData["EndDate"] = dateViewModel.EndDate;
                        ViewData["ReportType"] = 4;
                        ViewData["ReportName"] = "تقرير شهري لـ " + teacher.FullName;
                        ViewData["TeacherId"] = new SelectList(_userManager.Users.Where(t => t.UserTypeId == 5 && t.Active == true), "Id", "FullName");
                        if (report.Count == 0)
                        {
                            ViewData["max"] = 0;
                        }
                        else
                        {
                            ViewData["max"] = report.Max(d => d.Count);
                        }

                        return View(report.OrderBy(d => d.LectureDate));
                    }
                    else
                    {
                        var report = await _context.TeacherScheduleReport
                            .Where(g => g.LectureDate >= dateViewModel.StartDate && g.LectureDate <= dateViewModel.EndDate && g.TeacherId == dateViewModel.TeacherId && g.SyllabusID == dateViewModel.SyllabusID)
                            .ToListAsync();
                        var teacher = await _userManager.Users.Where(u => u.Id == dateViewModel.TeacherId).FirstOrDefaultAsync();
                        ViewData["StartDate"] = dateViewModel.StartDate;
                        ViewData["EndDate"] = dateViewModel.EndDate;
                        ViewData["ReportType"] = 4;
                        ViewData["ReportName"] = "تقرير شهري لـ " + teacher.FullName;
                        ViewData["TeacherId"] = new SelectList(_userManager.Users.Where(t => t.UserTypeId == 5 && t.Active == true), "Id", "FullName");
                        if (report.Count == 0)
                        {
                            ViewData["max"] = 0;
                        }
                        else
                        {
                            ViewData["max"] = report.Max(d => d.Count);
                        }
                        return View(report.OrderBy(d => d.LectureDate));
                    }

                }
            }

            return View();
        }


        [HttpGet]
        public object Syllabuslist(int Id)
        {
            var Syllabuslist = _context.TeacherSyllabus
                .Where(s => s.TeacherId == Id)
                .ToList();

            return (_context.TeacherSyllabus
                .Where(s => s.TeacherId == Id)
                .ToList().Select(x => new
                {
                    Id = x.SyllabusID,
                    Name = x.SyllabusName + " - " + x.PhaseName,

                }));
        }


        public async Task<bool> Isaccessright(int userid, int accessid)
        {
            bool result = false;

            var access = await _context.UserAccessRight
                .Where(u => u.UserId == userid && u.AccessId == accessid)
                .FirstOrDefaultAsync();

            if (access == null)
            {
                result = false;
            }
            else
            {
                result = true;
            }

            return result;
        }
    }
}
