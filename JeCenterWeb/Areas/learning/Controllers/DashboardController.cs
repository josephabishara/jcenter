using JeCenterWeb.Data;
using JeCenterWeb.Models;
using JeCenterWeb.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JeCenterWeb.Areas.learning.Controllers
{
    [Area("learning")]
    [Authorize(Roles = "Student")]
    public class DashboardController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static readonly string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

        public DashboardController(ApplicationDbContext context,
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
         IHttpContextAccessor httpContextAccessor,
         IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["AppName"] = " المناهج ";
            ViewData["ControllerName"] = "صفحتي";
            //if (_httpContextAccessor.HttpContext.Session.GetInt32("_phase") == null)
            //{
            //    return RedirectToAction("logout", "Account");
            //}
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int studentid = Convert.ToInt32(userId);
            ApplicationUser student = await _userManager.FindByIdAsync(userId);
            string imgint = "Notapproved";
            if (student.imageint == 2)
            {
                imgint = "Approved";
            }
            ViewData["imgint"] = imgint;

            ViewData["UserName"] = student.FullName;
            ViewData["imgurl"] = student.imgurl;
            var balance = await _context.UsersBalance.FindAsync(student.AccountID);
            ViewData["balance"] = balance.Balance * -1;
            int? PhaseId = _httpContextAccessor.HttpContext.Session.GetInt32("_phase");
            if (PhaseId == 0 || PhaseId == null)
            {
                return RedirectToRoute(new { controller = "Applications", action = "Index", area = "learning" });
            }
            else
            {
                var cphase = await _context.CPhases.Where(s => s.PhaseId == PhaseId).FirstOrDefaultAsync();
                ViewData["phasename"] = cphase.PhaseName;
                var stdntapp = await _context.StudentApplications
                    .Where(a => a.PhaseId == PhaseId && a.StudentId == Convert.ToInt32(userId))
                    .FirstOrDefaultAsync();
                if (stdntapp != null)
                {
                    var phscyear = await _context.PhysicalYear.FindAsync(stdntapp.PhysicalyearId);
                    TimeSpan firstday = DateTime.Today.Subtract(phscyear.FromDate);
                    TimeSpan totaldays = phscyear.ToDate.Subtract(phscyear.FromDate);
                    float persntage = (float)firstday.Days / (float)totaldays.Days * 100;
                    int proccess = (int)persntage;
                    ViewData["proccess"] = proccess;
                    if (proccess < 50)
                    {
                        ViewData["bar"] = "progress-bar-success";
                        ViewData["proccessmessage"] = " فات" + proccess.ToString() + " % من السنة الدراسية";

                    }
                    else if (proccess < 75)
                    {
                        ViewData["bar"] = "progress-bar-warning";
                        ViewData["proccessmessage"] = " فات" + proccess.ToString() + " % من السنة الدراسية";
                    }
                    else if (proccess < 100)
                    {
                        ViewData["bar"] = "progress-bar-danger";
                        ViewData["proccessmessage"] = " فات" + proccess.ToString() + " % من السنة الدراسية";
                    }
                    else if (proccess > 100)
                    {
                        ViewData["proccess"] = 100;
                        ViewData["bar"] = "progress-bar-danger";
                        ViewData["proccessmessage"] = " انتهت السنة الدراسية ";
                    }

                }

            }

            ViewData["phone"] = student.Mobile;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateImage()
        {
            ViewData["ControllerName"] = "تغير الصورة الشخصية";
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int studentId = Convert.ToInt32(userId);

            var employee = await _context.Users.FindAsync(studentId);
            if (employee.UserTypeId != 7)
                return RedirectToAction(nameof(Index));
            UpdateImageStudentUserViewModel model = new UpdateImageStudentUserViewModel
            {
                Id = studentId,
                oldimgurl = employee.imgurl,
                FullName = employee.FullName,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateImage(int id, [Bind("Id,FullName,imgurl")] UpdateImageStudentUserViewModel model)
        {

            ApplicationUser employee = await _userManager.FindByIdAsync(id.ToString());

            //if (employee.UserTypeId != 7)
            //    return RedirectToAction(nameof(Index));

            string imgurlpath = "images/Students/default.png";

            var extension = Path.GetExtension(model.imgurl.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("ImgUrl", "Please upload a Correct image file");
                return View(model);
            } 

            if (model.imgurl == null)
            {
                ModelState.AddModelError("ImgUrl", "Please upload an image file.");
                return View(model);
            }
            if (model.imgurl != null)
            {
                string folder = "images/Students/";
                folder += Guid.NewGuid().ToString() + "-" + model.imgurl.FileName;
                imgurlpath = folder;
                string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                await model.imgurl.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
            }

            // employee.FullName = model.FullName;
            employee.imgurl = imgurlpath;
            employee.imageint = 1;
            var result = _userManager.UpdateAsync(employee).Result;


            return RedirectToAction(nameof(Index));
        }

    }
}
