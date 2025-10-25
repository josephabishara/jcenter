using JeCenterWeb.Data;
using JeCenterWeb.Models;
using JeCenterWeb.Models.Second;
using JeCenterWeb.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;

namespace JeCenterWeb.Areas.scp.Controllers
{
    [Area("scp")]
    [Authorize(Roles = "AdminSettings,PowerUser,SuperUser,User")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly SignInManager<ApplicationUser> _signInManager;
        string ControllerName = "الرئيسية";
        string AppName = "الأدمن";
        public DashboardController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment webHostEnvironment,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _signInManager = signInManager;
        }

        // GET: cs/Branch
        public async Task<IActionResult> Index()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var iuser = await _userManager.FindByIdAsync(userId);

            // DashboardViewModel model = new DashboardViewModel();

            if (iuser.UserTypeId == 1)
            {
                //var balnces = await _context.UsersBalance
                //    .Where(u => u.UserTypeId < 5 && u.UserTypeId > 1 && u.Balance > 0)
                //    .ToListAsync();


                //List<BalanceDashboardViewModel> MyViewModel = new List<BalanceDashboardViewModel>();
                //foreach (var item in balnces)
                //{
                //    var user = await _context.Users.Where(u => u.AccountID == item.Id).FirstOrDefaultAsync();
                //    if (user != null)
                //    {
                //        BalanceDashboardViewModel dashboardViewModel = new BalanceDashboardViewModel();
                //        dashboardViewModel.Balance = item.Balance;
                //        dashboardViewModel.FullName = item.FullName;
                //        dashboardViewModel.Id = item.Id;
                //        dashboardViewModel.Imgurl = user.imgurl;

                //        MyViewModel.Add(dashboardViewModel);
                //    }
                //}

                //model.Treasury = MyViewModel; 

                //ViewData["TCount"] = MyViewModel.Count();
                //model.TotalTreasury = MyViewModel.Sum(t => t.Balance);
                //DateTime myday = DateTime.Now.AddDays(-1);
                //DateTime journaldate = new DateTime(myday.Year, myday.Month, myday.Day, 0, 0, 0);
                //var entryLine = await _context.FinancialDocuments
                //    .Where(l => l.JournalEntryDate > journaldate)
                //    .ToListAsync();


                //  model.DashboardGroups = groupsToday.ToList();
                //  model.DashboardReviews = reviwsToday.ToList();
                //  model.DashboardExams= ExamsToday.ToList();

                //model.DashboardGroups = await _context.DashboardGroups.Where(g => g.LectureDate == DateTime.Today.AddDays(-1)).ToListAsync();
                //model.DashboardReviews = await _context.DashboardReviews.Where(g => g.ReviewDate == DateTime.Today.AddDays(-1)).ToListAsync();
                //model.DashboardExams = await _context.DashboardExams.Where(g => g.ReviewDate == DateTime.Today.AddDays(-1)).ToListAsync();

                //model.TeachersPayments = await _context.TeachersPayments
                //     .ToListAsync();




                // model.TotalCards = entryLine
                //     .Where(e => e.MovementTypeId == 5)
                //     .Sum(e => e.Value);

                // model.TotalCharged = entryLine
                //     .Where(e => e.MovementTypeId == 6)
                //     .Sum(e => e.Value);

                // model.TotalPayments = entryLine
                //     .Where(e => e.MovementTypeId == 2 || e.MovementTypeId == 3 || e.MovementTypeId == 11 || e.MovementTypeId == 12)
                //     .Sum(e => e.Value);

                // model.GroupsCount = await _context.CGroupSchedule
                //    .Where(e => e.LectureDate == DateTime.Today)
                //    .CountAsync();
                // model.ReviewsCount = await _context.ReviewsSchedule
                //.Where(e => e.ReviewDate == DateTime.Today && e.ReviewTypeId == 1)
                //.CountAsync();
                // model.ExamsCount = await _context.ReviewsSchedule
                //.Where(e => e.ReviewDate == DateTime.Today && e.ReviewTypeId == 2)
                //.CountAsync();

                // var tl = await _context.CGroupSchedule
                //  .Where(e => e.LectureDate == DateTime.Today)
                //  .Include(e => e.CGroups)
                //  .GroupBy(e => e.CGroups.TeacherId).ToListAsync();
                // model.TeacherCount = tl.Count();

                // var sl = await _context.StudentLecture
                //    .Where(e => e.LectureDate == DateTime.Today)
                //    .GroupBy(s => s.StudentID)
                //    .ToListAsync();
                // model.StudentCount = sl.Count();

                var dashboardCopons = await _context.DashboardCopons
                    .Where(c => c.CoponsOfAll > 0)
                    .OrderByDescending(c => c.CoponsOfAll)
                    .ToListAsync();
                return View(dashboardCopons);
            }
            else
            {
                return RedirectToAction(nameof(Profile));
            }


            return View();
        }

        public async Task<IActionResult> Treasury()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var iuser = await _userManager.FindByIdAsync(userId);
            if (iuser.UserTypeId == 1)
            {
                var balnces = await _context.UsersBalance
             .Where(u => u.UserTypeId < 5 && u.UserTypeId > 1 && u.Balance > 0)
             .ToListAsync();


                List<BalanceDashboardViewModel> MyViewModel = new List<BalanceDashboardViewModel>();
                foreach (var item in balnces)
                {
                    var user = await _context.Users.Where(u => u.AccountID == item.Id).FirstOrDefaultAsync();
                    if (user != null)
                    {
                        BalanceDashboardViewModel dashboardViewModel = new BalanceDashboardViewModel();
                        dashboardViewModel.Balance = item.Balance;
                        dashboardViewModel.FullName = item.FullName;
                        dashboardViewModel.Id = item.Id;
                        dashboardViewModel.Imgurl = user.imgurl;

                        MyViewModel.Add(dashboardViewModel);
                    }
                }

                var Treasury = MyViewModel;

                ViewData["TCount"] = MyViewModel.Count();
                ViewData["TotalTreasury"] = MyViewModel.Sum(t => t.Balance);
                DateTime myday = DateTime.Now.AddDays(-1);
                DateTime journaldate = new DateTime(myday.Year, myday.Month, myday.Day, 0, 0, 0);


                return View(MyViewModel);
            }
            else
            {
                return RedirectToAction(nameof(Profile));
            }
            return View();
        }

        public async Task<IActionResult> Groups()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var iuser = await _userManager.FindByIdAsync(userId);
            if (iuser.UserTypeId == 1)
            {
                IEnumerable <DashboardGroups>? dashboardGroups = await _context.DashboardGroups
                            .Where(g => g.LectureDate == DateTime.Today)
                            .ToListAsync();
                return View(dashboardGroups);
            }
            else
            {
                return RedirectToAction(nameof(Profile));
            }
            return View();
        }
        public async Task<IActionResult> Reviews()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var iuser = await _userManager.FindByIdAsync(userId);
            if (iuser.UserTypeId == 1)
            {

                IEnumerable<DashboardReviews> dashboardReviews = await _context.DashboardReviews
                .Where(g => g.ReviewDate == DateTime.Today)
                .ToListAsync();
                return View(dashboardReviews);
            }
            else
            {
                return RedirectToAction(nameof(Profile));
            }
            return View();
        }
        public async Task<IActionResult> Exams()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var iuser = await _userManager.FindByIdAsync(userId);
            if (iuser.UserTypeId == 1)
            {

                IEnumerable<DashboardExams>? dashboardExams = await _context.DashboardExams
                .Where(g => g.ReviewDate == DateTime.Today)
                .ToListAsync();

                return View(dashboardExams);
            }
            else
            {
                return RedirectToAction(nameof(Profile));
            }
            return View();
        }

        // TeachersPayments
        public async Task<IActionResult> TeachersPayments(DateTime? date ,int? branchId)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var iuser = await _userManager.FindByIdAsync(userId);
            if (iuser.UserTypeId == 1)
            {
                if(date != null && branchId != null)
                {
                    IEnumerable<TeachersPayments>? TeachersPayment = await _context.TeachersPayments
                        .Where(p=>p.JournalEntryDate == date && p.BranchId == branchId)
                    .ToListAsync();
                    return View(TeachersPayment);
                }
                else
                {
                    IEnumerable<TeachersPayments>? TeachersPayments = await _context.TeachersPayments
                         .Where(p => p.JournalEntryDate == DateTime.Today.Date && p.BranchId == iuser.ParentId)
                         .ToListAsync();
                    return View(TeachersPayments);

                }
            }
            else
            {
                return RedirectToAction(nameof(Profile));
            }
           
            return View();
        }

        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var iuser = await _userManager.FindByIdAsync(userId);

            return View();
        }


        // ChangePassword
        public async Task<IActionResult> ChangePassword()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var iuser = await _userManager.FindByIdAsync(userId);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser employee = await _userManager.FindByIdAsync(userId);
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login");
                }
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }
                else if (result.Succeeded)
                {
                    await _signInManager.RefreshSignInAsync(user);
                    employee.Pwd = model.Password;
                    var result1 = _userManager.UpdateAsync(employee).Result;
                }

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // ChangeImage
        public async Task<IActionResult> ChangeImage()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var iuser = await _userManager.FindByIdAsync(userId);
            UpdateImageStudentUserViewModel model = new UpdateImageStudentUserViewModel
            {
                Id = uid,
                oldimgurl = iuser.imgurl,
                FullName = iuser.FullName,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateImage(int id, [Bind("Id,FullName,imgurl")] UpdateImageStudentUserViewModel model)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ApplicationUser employee = await _userManager.FindByIdAsync(id.ToString());

            if (employee.UserTypeId > 4)
                return RedirectToAction(nameof(Index));

            string imgurlpath = "images/StudentUsers/default.png";
            if (model.imgurl != null)
            {
                string folder = "images/StudentUsers/";
                folder += Guid.NewGuid().ToString() + "-" + model.imgurl.FileName;
                imgurlpath = folder;
                string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                string extension = Path.GetExtension(serverfolder);
                if (extension == ".jpg" || extension == ".jpeg" || extension == ".png")
                {
                    await model.imgurl.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }



            }

            // employee.FullName = model.FullName;
            employee.imgurl = imgurlpath;

            var result = _userManager.UpdateAsync(employee).Result;


            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Search(string word)
        {

            return View();
        }
    }
}
