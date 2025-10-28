using JeCenterWeb.Data;
using JeCenterWeb.Migrations;
using JeCenterWeb.Models;
using JeCenterWeb.Models.Repository;
using JeCenterWeb.Models.Second;
using JeCenterWeb.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using NuGet.Versioning;
using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using static Azure.Core.HttpHeader;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JeCenterWeb.Areas.scp.Controllers
{
    [Area("scp")]
    [Authorize(Roles = "AdminSettings,PowerUser,SuperUser,User")]
    public class TreasuryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<TeachersController> _logger;
        //  private readonly FinancialDocumentEntryRepository _repository;
        string ControllerName = "المدرسين";
        string AppName = "الأدمن";
        public TreasuryController(ApplicationDbContext context,
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

        //  iTreasury =  primary =  كشف حساب الخزنة

        // بيان	مدخلات	مصروفات	وقت العملية	حالة العملية 
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            int accountId = user.AccountID;
            var balance = await _context.UsersBalance.Where(u => u.Id == accountId).FirstOrDefaultAsync();
            ViewData["Balance"] = balance.Balance;
            ViewData["FullName"] = balance.FullName;
            ViewData["date"] = DateTime.Now.Date.ToString("yyyy-MM-dd");
            var model = await _context.FinancialJournalEntryLine
                .Where(f => f.AccountID == accountId && f.JournalEntryDate.Date == DateTime.Now.Date)
                .Include(f => f.FinancialDocuments)
                .OrderByDescending(l => l.JournalEntryDetilsID)
                .ToListAsync();
            return View(model);
        }

        public async Task<IActionResult> IndexByDate(DateTime date)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            int accountId = user.AccountID;
            var balance = await _context.UsersBalance.Where(u => u.Id == accountId).FirstOrDefaultAsync();
            ViewData["Balance"] = balance.Balance;
            ViewData["FullName"] = balance.FullName;
            ViewData["date"] = date.Date.ToString("yyyy-MM-dd");
            var model = await _context.FinancialJournalEntryLine
                .Where(f => f.AccountID == accountId && f.JournalEntryDate.Date == date.Date)
                .Include(f => f.FinancialDocuments)
                .OrderByDescending(l => l.JournalEntryDetilsID)
                .ToListAsync();
            return View(model);
        }

        public async Task<IActionResult> Expenses()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            if (user.online == 0)
            {
                return RedirectToRoute(new { area = "scp", controller = "Branch", action = "LogInBranch" });
            }
            int treasuryID = user.AccountID;
            var balance = await _context.UsersBalance
                .Where(u => u.Id == treasuryID)
                .FirstOrDefaultAsync();
            ViewData["Balance"] = balance.Balance;
            ViewData["TreasuryID"] = treasuryID;
            ViewData["MovementTypeId"] = 2;
            ViewData["JournalEntryDate"] = DateTime.Today;
            //var physicalyear = await _context.PhysicalYear
            //    .Where(p => p.Active == true)
            //    .OrderByDescending(p => p.PhysicalyearId)
            //    .FirstOrDefaultAsync();
            ViewData["PhysicalyearId"] = new SelectList(_context.PhysicalYear, "PhysicalyearId", "PhysicalyearName");
            ViewData["AccountID"] = new SelectList(_context.FinancialAccount.Where(f => f.AccountTypeId == 0 && f.AdvancedPermission == false), "FinancialAccountId", "AccountName");
            ViewData["BranchId"] = new SelectList(_context.CBranch, "BranchId", "BranchName");

            var Expenses = await _context.FinancialDocuments
                .Where(f => f.TreasuryID == treasuryID && f.MovementTypeId == 2 )
                .OrderByDescending(l => l.FinancialDocumentId)
                .ToListAsync();
            return View(Expenses);
        }

        [HttpPost]
        public async Task<IActionResult> AddExpenses([Bind("AccountId,TreasuryID,MovementTypeId,JournalEntryDate,physicalyearId,BranchId,Value,Note,imgurl")] ExpensesViewModel expensesViewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var user = await _userManager.FindByIdAsync(userId);
            bool isattach = false;

            if (user.online == 0)
            {
                return RedirectToRoute(new { area = "scp", controller = "Branch", action = "LogInBranch" });
            }

            string imgurlpath = string.Empty;
            if (expensesViewModel.imgurl != null)
            {
                isattach = true;
                string folder = "images/expensesattach/";
                folder += Guid.NewGuid().ToString() + "-" + expensesViewModel.imgurl.FileName;
                imgurlpath = folder;
                string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                await expensesViewModel.imgurl.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
            }

            FinancialDocuments financialDocuments = new FinancialDocuments()
            {
                JournalEntryDate = DateTime.Today,
                Notes = expensesViewModel.Note,
                TreasuryID = expensesViewModel.TreasuryID,
                AccountID = expensesViewModel.AccountId,
                Value = expensesViewModel.Value,
                MovementTypeId = expensesViewModel.MovementTypeId,
                physicalyearId = expensesViewModel.physicalyearId,
                Active = true,
                BranchId = user.online,
                CreateId = uid,
                CreatedDate = DateTime.Now,
                imgurl = imgurlpath,
                Attached = isattach,
                Approve = 0,
            };

            _context.Add(financialDocuments);
            await _context.SaveChangesAsync();
            //  await createFinancialJournalEntryLine(financialDocuments);

            return RedirectToRoute(new { action = "Expenses" });
        }

        //  TreasuryCashing = secondary =  صرف للمدرسين
        // payment vs receipt

        public async Task<IActionResult> Payment()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            if (user.online == 0)
            {
                return RedirectToRoute(new { area = "scp", controller = "Branch", action = "LogInBranch" });
            }
            int treasuryID = user.AccountID;
            var balance = await _context.UsersBalance.Where(u => u.Id == treasuryID).FirstOrDefaultAsync();
            ViewData["Balance"] = balance.Balance;
            ViewData["TreasuryID"] = treasuryID;
            ViewData["MovementTypeId"] = 3;
            ViewData["JournalEntryDate"] = DateTime.Today;
            var physicalyear = await _context.PhysicalYear
                .Where(p => p.Active == true)
                .OrderByDescending(p => p.PhysicalyearId)
                .FirstOrDefaultAsync();
            ViewData["physicalyearId"] = physicalyear.PhysicalyearId;
            ViewData["Id"] = new SelectList(_userManager.Users.Where(f => f.UserTypeId == 5 && f.Active == true), "Id", "FullName");

            var Expenses = await _context.FinancialDocuments
                .Where(f => f.TreasuryID == treasuryID && f.MovementTypeId == 3)
                .OrderByDescending(l => l.FinancialDocumentId)
                .ToListAsync();
            return View(Expenses);
        }

        [HttpPost]
        public async Task<IActionResult> AddPayment([Bind("AccountId,TreasuryID,MovementTypeId,JournalEntryDate,physicalyearId,Value,GroupscheduleId")] PaymentViewModel paymentViewModel)
        {
            var groupschedule = await _context.CGroupSchedule.FindAsync(paymentViewModel.GroupscheduleId);
            if (groupschedule.Paided)
            {
                return RedirectToRoute(new { controller = "Teachers", action = "LectureTeacherDetails", area = "scp", id = groupschedule.GroupscheduleId });
            }
            else
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int uid = Convert.ToInt32(userId);
                var user = await _userManager.FindByIdAsync(userId);
                if (user.online == 0)
                {
                    return RedirectToRoute(new { area = "scp", controller = "Branch", action = "LogInBranch" });
                }
                
                var studentLecture = _context.StudentLecture
                    .Where(s => s.scheduleId == groupschedule.GroupscheduleId && s.LectureType == 0 )
                    .ToList();
                
                var group = await _context.CGroups.FindAsync(groupschedule.GroupId);
                var room = await _context.CRooms.FindAsync(group.RoomId);
                string Note = " تسوية حصة " + group.GroupName + " بتاريخ : " + groupschedule.LectureDate;
                var teacher = await _context.Users.FindAsync(paymentViewModel.AccountId);

                group.CountAttend = studentLecture.Count();
                _context.Update(group);
                await _context.SaveChangesAsync();
                FinancialDocuments financialDocuments = new FinancialDocuments()
                {
                    JournalEntryDate = DateTime.Today,
                    Notes = Note,
                    TreasuryID = paymentViewModel.TreasuryID,
                    AccountID = teacher.AccountID,
                    Value = paymentViewModel.Value,
                    MovementTypeId = paymentViewModel.MovementTypeId,
                    physicalyearId = paymentViewModel.physicalyearId,
                    Active = true,
                    BranchId = user.online,
                    CreateId = uid,
                    CreatedDate = DateTime.Now,
                    GroupscheduleId = groupschedule.GroupscheduleId,
                };

                _context.Add(financialDocuments);
                await _context.SaveChangesAsync();
                await createTeacherJournalEntryLine(financialDocuments);

                groupschedule.Paided = true;
                groupschedule.PaidId = uid;
                groupschedule.PaidDate = DateTime.Now;
                _context.Update(groupschedule);
                await _context.SaveChangesAsync();


              // 1️⃣ جلب آخر 3 محاضرات مغلقة ومدفوعة في نفس المجموعة
                var last3Lectures = await _context.CGroupSchedule
                    .Where(s => s.GroupId == groupschedule.GroupId  && s.Paided)
                    .OrderByDescending(s => s.LectureDate)
                    .Take(3)
                    .Select(s => s.GroupscheduleId)
                    .ToListAsync();

                if (last3Lectures.Any())
                {
                    // 2️⃣ جلب الطلاب المنتمين إلى هذه المجموعة
                    var studentsInGroup = await _context.StudentGroup
                        .Where(g => g.GroupId == groupschedule.GroupId)
                        .ToListAsync();
                    var studentsToRemove = new List<StudentGroup>();

                    // 3️⃣ فحص حضور كل طالب في آخر 3 محاضرات
                    foreach (var student in studentsInGroup)
                    {
                        int attendedCount = await _context.StudentLecture
                            .Where(sl => sl.StudentID == student.StudentId &&
                                         last3Lectures.Contains(sl.scheduleId))
                            .CountAsync();

                        // 🔸 لو الطالب ما حضرش أي من آخر 3 محاضرات
                        if (attendedCount == 0)
                        {
                            studentsToRemove.Add(student);
                        }
                    }

                    // 4️⃣ حذف الطلبة الغائبين
                    if (studentsToRemove.Any())
                    {
                        _context.StudentGroup.RemoveRange(studentsToRemove);
                        await _context.SaveChangesAsync();
                    }


                }

                return RedirectToRoute(new { controller = "Teachers", action = "LectureTeacherDetails", area = "scp", id = groupschedule.GroupscheduleId });

            }

            return RedirectToRoute(new { controller = "Teachers", action = "LectureTeacherDetails", area = "scp", id = groupschedule.GroupscheduleId });
        }

        // PaymentReview
        public async Task<IActionResult> PaymentReview()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            if (user.online == 0)
            {
                return RedirectToRoute(new { area = "scp", controller = "Branch", action = "LogInBranch" });
            }
            int treasuryID = user.AccountID;
            var balance = await _context.UsersBalance.Where(u => u.Id == treasuryID).FirstOrDefaultAsync();
            ViewData["Balance"] = balance.Balance;
            ViewData["TreasuryID"] = treasuryID;
            ViewData["MovementTypeId"] = 11;
            ViewData["JournalEntryDate"] = DateTime.Today;
            var physicalyear = await _context.PhysicalYear
                .Where(p => p.Active == true)
                .OrderByDescending(p => p.PhysicalyearId)
                .FirstOrDefaultAsync();
            ViewData["physicalyearId"] = physicalyear.PhysicalyearId;
            ViewData["Id"] = new SelectList(_userManager.Users.Where(f => f.UserTypeId == 5 && f.Active == true), "Id", "FullName");

            var Expenses = await _context.FinancialDocuments
                .Where(f => f.TreasuryID == treasuryID && f.MovementTypeId == 11)
                .OrderByDescending(l => l.FinancialDocumentId)
                .ToListAsync();
            return View(Expenses);
        }
        [HttpPost]
        public async Task<IActionResult> AddPaymentReview([Bind("AccountId,TreasuryID,MovementTypeId,JournalEntryDate,physicalyearId,Value,GroupscheduleId")] PaymentViewModel paymentViewModel)
        {
            var reviewsSchedule = await _context.ReviewsSchedule.FindAsync(paymentViewModel.GroupscheduleId);
            if (reviewsSchedule.Paided)
            {
                return RedirectToRoute(new { controller = "Schedule", action = "LectureTeacherDetails", area = "scp", id = reviewsSchedule.ReviewsScheduleId });
            }
            else
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int uid = Convert.ToInt32(userId);
                var user = await _userManager.FindByIdAsync(userId);
                if (user.online == 0)
                {
                    return RedirectToRoute(new { area = "scp", controller = "Branch", action = "LogInBranch" });
                }

                var studentLecture = _context.StudentLecture
                   .Where(s => s.scheduleId == reviewsSchedule.ReviewsScheduleId && s.LectureType == 1)
                   .ToList();

                var room = await _context.CRooms.FindAsync(reviewsSchedule.RoomId);
                string Note = " تسوية  مراجعة" + reviewsSchedule.ReviewsScheduleName + " بتاريخ : " + reviewsSchedule.ReviewDate;
                var teacher = await _context.Users.FindAsync(paymentViewModel.AccountId);

                reviewsSchedule.CountAttend = studentLecture.Count();
                _context.Update(reviewsSchedule);
                await _context.SaveChangesAsync();


                FinancialDocuments financialDocuments = new FinancialDocuments()
                {
                    JournalEntryDate = DateTime.Today,
                    Notes = Note,
                    TreasuryID = paymentViewModel.TreasuryID,
                    AccountID = teacher.AccountID,
                    Value = paymentViewModel.Value,
                    MovementTypeId = paymentViewModel.MovementTypeId,
                    physicalyearId = paymentViewModel.physicalyearId,
                    Active = true,
                    BranchId = user.online,
                    CreateId = uid,
                    CreatedDate = DateTime.Now,
                    GroupscheduleId = reviewsSchedule.ReviewsScheduleId,
                };

                _context.Add(financialDocuments);
                await _context.SaveChangesAsync();
                await createTeacherJournalEntryLine(financialDocuments);

                reviewsSchedule.Paided = true;
                reviewsSchedule.PaidId = uid;
                reviewsSchedule.PaidDate = DateTime.Now;
                _context.Update(reviewsSchedule);
                await _context.SaveChangesAsync();

                return RedirectToRoute(new { controller = "Schedule", action = "LectureTeacherDetails", area = "scp", id = reviewsSchedule.ReviewsScheduleId });

            }
            return RedirectToRoute(new { controller = "Schedule", action = "LectureTeacherDetails", area = "scp", id = reviewsSchedule.ReviewsScheduleId });
        }

        // PaymentExam

        public async Task<IActionResult> PaymentExam()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            if (user.online == 0)
            {
                return RedirectToRoute(new { area = "scp", controller = "Branch", action = "LogInBranch" });
            }
            int treasuryID = user.AccountID;
            var balance = await _context.UsersBalance.Where(u => u.Id == treasuryID).FirstOrDefaultAsync();
            ViewData["Balance"] = balance.Balance;
            ViewData["TreasuryID"] = treasuryID;
            ViewData["MovementTypeId"] = 12;
            ViewData["JournalEntryDate"] = DateTime.Today;
            var physicalyear = await _context.PhysicalYear
                .Where(p => p.Active == true)
                .OrderByDescending(p => p.PhysicalyearId)
                .FirstOrDefaultAsync();
            ViewData["physicalyearId"] = physicalyear.PhysicalyearId;
            ViewData["Id"] = new SelectList(_userManager.Users.Where(f => f.UserTypeId == 5 && f.Active == true), "Id", "FullName");

            var Expenses = await _context.FinancialDocuments
                .Where(f => f.TreasuryID == treasuryID && f.MovementTypeId == 12)
                .OrderByDescending(l => l.FinancialDocumentId)
                .ToListAsync();
            return View(Expenses);
        }

        [HttpPost]
        public async Task<IActionResult> AddPaymentExam([Bind("AccountId,TreasuryID,MovementTypeId,JournalEntryDate,physicalyearId,Value,GroupscheduleId")] PaymentViewModel paymentViewModel)
        {
            var reviewsSchedule = await _context.ReviewsSchedule.FindAsync(paymentViewModel.GroupscheduleId);
            if (reviewsSchedule.Paided)
            {
                return RedirectToRoute(new { controller = "Schedule", action = "LectureTeacherDetails", area = "scp", id = reviewsSchedule.ReviewsScheduleId });
            }
            else
            {

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _userManager.FindByIdAsync(userId);
                int uid = Convert.ToInt32(userId);
                if (user.online == 0)
                {
                    return RedirectToRoute(new { area = "scp", controller = "Branch", action = "LogInBranch" });
                }

                var studentLecture = _context.StudentLecture
                 .Where(s => s.scheduleId == reviewsSchedule.ReviewsScheduleId && s.LectureType == 2)
                 .ToList();

                var room = await _context.CRooms.FindAsync(reviewsSchedule.RoomId);
                string Note = " تسوية  أمتحان " + reviewsSchedule.ReviewsScheduleName + " بتاريخ : " + reviewsSchedule.ReviewDate;
                var teacher = await _context.Users.FindAsync(paymentViewModel.AccountId);

                reviewsSchedule.CountAttend = studentLecture.Count();
                _context.Update(reviewsSchedule);
                await _context.SaveChangesAsync();

                FinancialDocuments financialDocuments = new FinancialDocuments()
                {
                    JournalEntryDate = DateTime.Today,
                    Notes = Note,
                    TreasuryID = paymentViewModel.TreasuryID,
                    AccountID = teacher.AccountID,
                    Value = paymentViewModel.Value,
                    MovementTypeId = paymentViewModel.MovementTypeId,
                    physicalyearId = paymentViewModel.physicalyearId,
                    Active = true,
                    BranchId = user.online,
                    CreateId = uid,
                    CreatedDate = DateTime.Now,
                    GroupscheduleId = reviewsSchedule.ReviewsScheduleId,
                };

                _context.Add(financialDocuments);
                await _context.SaveChangesAsync();
                await createTeacherJournalEntryLine(financialDocuments);

                reviewsSchedule.Paided = true;
                reviewsSchedule.PaidId = uid;
                reviewsSchedule.PaidDate = DateTime.Now;
                _context.Update(reviewsSchedule);
                await _context.SaveChangesAsync();

                return RedirectToRoute(new { controller = "Schedule", action = "LectureTeacherDetails", area = "scp", id = reviewsSchedule.ReviewsScheduleId });
            }
            return RedirectToRoute(new { controller = "Schedule", action = "LectureTeacherDetails", area = "scp", id = reviewsSchedule.ReviewsScheduleId });
        }

        [HttpGet]
        public object Getschedule(int Id)
        {
            DateTime tomro = DateTime.Today.AddDays(1);
            return (_context.CGroupSchedule
                .Where(s => s.CGroups.TeacherId == Id
                && s.LectureDate <= tomro
                && s.Closed == true
                && s.Paided == false)
                .Include(s => s.CGroups)
                .ToList().Select(x => new
                {
                    Id = x.GroupscheduleId,
                    Name = x.CGroups.GroupName + " : " + x.LectureDate.ToString("dd/MM/yyyy"),

                }));
        }

        // GetscheduleValue 

        [HttpGet]
        public async Task<string> GetscheduleValue(int GroupscheduleId)
        {
            decimal TotalPaided = await _context.StudentLecture
                .Where(g => g.scheduleId == GroupscheduleId && g.LectureType == 0 && g.Deleted == 1)
                .SumAsync(g => g.TeacherValue);

            return TotalPaided.ToString();
        }

        // GetReviewschedule

        [HttpGet]
        public object GetReviewschedule(int Id)
        {
            DateTime tomro = DateTime.Today.AddDays(-1);
            return (_context.ReviewsSchedule
                .Where(s => s.TeacherId == Id && s.ReviewTypeId == 1
                && s.ReviewDate >= tomro
                && s.Closed == true
                && s.Paided == false)
                .ToList().Select(x => new
                {
                    Id = x.ReviewsScheduleId,
                    Name = x.ReviewsScheduleName + " : " + x.ReviewDate.ToString("dd/MM/yyyy"),

                }));
        }

        // GetReviewscheduleValue  
        [HttpGet]
        public async Task<string> GetReviewscheduleValue(int GroupscheduleId)
        {
            decimal TotalPaided = await _context.StudentLecture
                .Where(g => g.scheduleId == GroupscheduleId && g.LectureType == 1 && g.Deleted == 1)
                .SumAsync(g => g.TeacherValue);

            return TotalPaided.ToString();
        }

        // GetExamschedule
        [HttpGet]
        public object GetExamschedule(int Id)
        {
            DateTime tomro = DateTime.Today.AddDays(-1);
            return (_context.ReviewsSchedule
                .Where(s => s.TeacherId == Id && s.ReviewTypeId == 2
                && s.ReviewDate >= tomro
                && s.Closed == true
                && s.Paided == false)
                .ToList().Select(x => new
                {
                    Id = x.ReviewsScheduleId,
                    Name = x.ReviewsScheduleName + " : " + x.ReviewDate.ToString("dd/MM/yyyy"),

                }));
        }

        // GetExamscheduleValue   GetExamscheduleValue
        [HttpGet]
        public async Task<string> GetExamscheduleValue(int GroupscheduleId)
        {
            decimal TotalPaided = await _context.StudentLecture
                .Where(g => g.scheduleId == GroupscheduleId && g.LectureType == 2 && g.Deleted == 1)
                .SumAsync(g => g.TeacherValue);

            return TotalPaided.ToString();
        }

        //  TreasuryTransform =  success =  توريد لخزنة
        public async Task<IActionResult> Transformation()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            if (user.online == 0)
            {
                return RedirectToRoute(new { area = "scp", controller = "Branch", action = "LogInBranch" });
            }
            int treasuryID = user.AccountID;
            var balance = await _context.UsersBalance.Where(u => u.Id == treasuryID).FirstOrDefaultAsync();
            ViewData["Balance"] = balance.Balance;
            ViewData["TreasuryID"] = treasuryID;
            ViewData["MovementTypeId"] = 4;
            ViewData["JournalEntryDate"] = DateTime.Today;
            DateTime date = DateTime.Today;
            var physicalYear = await _context.PhysicalYear
            .Where(y => y.FromDate <= date && y.ToDate >= date)
           .OrderByDescending(p => p.PhysicalyearId)
           .FirstOrDefaultAsync();
            int PhysicalyearId = physicalYear.PhysicalyearId;

            ViewData["physicalyearId"] = PhysicalyearId;
            ViewData["AccountID"] = new SelectList(_context.FinancialAccount.Where(f => f.AccountTypeId < 5 && f.AccountTypeId > 0 && f.Active == true), "FinancialAccountId", "AccountName");
            ViewData["BranchId"] = new SelectList(_context.CBranch, "BranchId", "BranchName");
            var Expenses = await _context.FinancialDocuments
                .Where(f => f.TreasuryID == treasuryID && f.MovementTypeId == 4)
                .OrderByDescending(l => l.FinancialDocumentId)
                .ToListAsync();
            return View(Expenses);
        }


        [HttpPost]
        public async Task<IActionResult> AddTransformation([Bind("AccountId,TreasuryID,MovementTypeId,JournalEntryDate,physicalyearId,Value,Note,BranchId")] ExpensesViewModel expensesViewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            int uid = Convert.ToInt32(userId);
            if (expensesViewModel.Value == 0 || expensesViewModel.BranchId == 0 || expensesViewModel.AccountId == 0 || expensesViewModel.Value == null)
            {
                return RedirectToRoute(new { area = "scp", controller = "Treasury", action = "Transformation" });
            }
            if (user.online == 0)
            {
                return RedirectToRoute(new { area = "scp", controller = "Branch", action = "LogInBranch" });
            }

            var facc = await _context.FinancialAccount.FindAsync(expensesViewModel.AccountId);
            var tacc = await _context.FinancialAccount.FindAsync(expensesViewModel.TreasuryID);
            FinancialDocuments financialDocuments = new FinancialDocuments()
            {
                JournalEntryDate = DateTime.Today,
                Notes = expensesViewModel.Note + " من " + tacc.AccountName + " الى " + facc.AccountName,
                TreasuryID = expensesViewModel.TreasuryID,
                AccountID = expensesViewModel.AccountId,
                Value = expensesViewModel.Value,
                MovementTypeId = expensesViewModel.MovementTypeId,
                physicalyearId = expensesViewModel.physicalyearId,
                BranchId = user.online,
                Active = true,
                Receipted = false,
                CreateId = uid,
                CreatedDate = DateTime.Now,
            };

            _context.Add(financialDocuments);
            await _context.SaveChangesAsync();
            // await createFinancialJournalEntryLine(financialDocuments);
            return RedirectToRoute(new { action = "Transformation" });
        }

        public async Task<IActionResult> DeleteTransformation(int id)
        {
            var Transformation = await _context.FinancialDocuments.FindAsync(id);
            _context.Remove(Transformation);
            await _context.SaveChangesAsync();
            return RedirectToRoute(new { action = "Transformation" });
        }

        //  TreasuryReceive =  warning =  استلام من خزنة اخري

        public async Task<IActionResult> Receipt()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            if (user.online == 0)
            {
                return RedirectToRoute(new { area = "scp", controller = "Branch", action = "LogInBranch" });
            }
            int treasuryID = user.AccountID;
            var balance = await _context.UsersBalance
                .Where(u => u.Id == treasuryID)
                .FirstOrDefaultAsync();
            ViewData["Balance"] = balance.Balance;
            ViewData["TreasuryID"] = treasuryID;

            var Receipt = await _context.FinancialDocuments
                .Where(f => f.AccountID == treasuryID && f.MovementTypeId == 4)
                .OrderByDescending(l => l.FinancialDocumentId)
                .ToListAsync();
            return View(Receipt);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveTransformation(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            if (user.online == 0)
            {
                return RedirectToRoute(new { area = "scp", controller = "Branch", action = "LogInBranch" });
            }
            var financialDocuments = await _context.FinancialDocuments.FindAsync(id);
            financialDocuments.BranchId = user.online;
            financialDocuments.Receipted = true;
            financialDocuments.ReceiptDate = DateTime.Now;
            _context.Update(financialDocuments);
            await _context.SaveChangesAsync();
            await createTransformationrJournalEntryLine(financialDocuments);
            return RedirectToRoute(new { action = "Receipt" });
        }



        //  TreasuryTransform = danger =  توريد للأدمن  

        public async Task<IActionResult> Supply()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            if (user.online == 0)
            {
                return RedirectToRoute(new { area = "scp", controller = "Branch", action = "LogInBranch" });
            }
            int accountId = user.AccountID;
            var balance = await _context.UsersBalance.Where(u => u.Id == accountId).FirstOrDefaultAsync();
            ViewData["Balance"] = balance.Balance;
            return View();
        }
        public async Task createFinancialJournalEntryLine(FinancialDocuments financialDocuments)
        {

            FinancialJournalEntryLine journalEntryLine1 = new FinancialJournalEntryLine()
            {
                FinancialDocumentId = financialDocuments.FinancialDocumentId,
                physicalyearId = financialDocuments.physicalyearId,
                JournalEntryDate = financialDocuments.JournalEntryDate,
                CreateId = financialDocuments.CreateId,
                CreatedDate = financialDocuments.CreatedDate,
            };

            if (financialDocuments.MovementTypeId == 2)
            {
                journalEntryLine1.AccountID = financialDocuments.AccountID;
                journalEntryLine1.Debit = financialDocuments.Value;
                journalEntryLine1.Credit = 0;
            }
            else if (financialDocuments.MovementTypeId == 1)
            {
                journalEntryLine1.AccountID = financialDocuments.TreasuryID;
                journalEntryLine1.Debit = 0;
                journalEntryLine1.Credit = financialDocuments.Value;
            }

            _context.Add(journalEntryLine1);
            await _context.SaveChangesAsync();

            FinancialJournalEntryLine journalEntryLine2 = new FinancialJournalEntryLine()
            {
                FinancialDocumentId = financialDocuments.FinancialDocumentId,
                physicalyearId = financialDocuments.physicalyearId,
                JournalEntryDate = financialDocuments.JournalEntryDate,
                CreateId = financialDocuments.CreateId,
                CreatedDate = financialDocuments.CreatedDate,
            };

            if (financialDocuments.MovementTypeId == 2)
            {
                journalEntryLine2.AccountID = financialDocuments.TreasuryID;
                journalEntryLine2.Debit = 0;
                journalEntryLine2.Credit = financialDocuments.Value;
            }
            else if (financialDocuments.MovementTypeId == 1)
            {
                journalEntryLine2.AccountID = financialDocuments.AccountID;
                journalEntryLine2.Debit = financialDocuments.Value;
                journalEntryLine2.Credit = 0;
            }

            _context.Add(journalEntryLine2);
            await _context.SaveChangesAsync();

        }

        public async Task createTeacherJournalEntryLine(FinancialDocuments financialDocuments)
        {
            FinancialJournalEntryLine journalEntryLine1 = new FinancialJournalEntryLine()
            {
                FinancialDocumentId = financialDocuments.FinancialDocumentId,
                physicalyearId = financialDocuments.physicalyearId,
                JournalEntryDate = financialDocuments.JournalEntryDate,
                CreateId = financialDocuments.CreateId,
                CreatedDate = financialDocuments.CreatedDate,

            };

            journalEntryLine1.AccountID = financialDocuments.AccountID;
            journalEntryLine1.Debit = financialDocuments.Value;
            journalEntryLine1.Credit = 0;

            _context.Add(journalEntryLine1);
            await _context.SaveChangesAsync();

            FinancialJournalEntryLine journalEntryLine2 = new FinancialJournalEntryLine()
            {
                FinancialDocumentId = financialDocuments.FinancialDocumentId,
                physicalyearId = financialDocuments.physicalyearId,
                JournalEntryDate = financialDocuments.JournalEntryDate,
                CreateId = financialDocuments.CreateId,
                CreatedDate = financialDocuments.CreatedDate,
            };

            journalEntryLine2.AccountID = financialDocuments.TreasuryID;
            journalEntryLine2.Debit = 0;
            journalEntryLine2.Credit = financialDocuments.Value;

            _context.Add(journalEntryLine2);
            await _context.SaveChangesAsync();
        }
        public async Task createTransformationrJournalEntryLine(FinancialDocuments financialDocuments)
        {
            FinancialJournalEntryLine journalEntryLine1 = new FinancialJournalEntryLine()
            {
                FinancialDocumentId = financialDocuments.FinancialDocumentId,
                physicalyearId = financialDocuments.physicalyearId,
                JournalEntryDate = financialDocuments.JournalEntryDate,
                CreateId = financialDocuments.CreateId,
                CreatedDate = financialDocuments.CreatedDate,
            };

            journalEntryLine1.AccountID = financialDocuments.AccountID;
            journalEntryLine1.Debit = financialDocuments.Value;
            journalEntryLine1.Credit = 0;

            _context.Add(journalEntryLine1);
            await _context.SaveChangesAsync();

            FinancialJournalEntryLine journalEntryLine2 = new FinancialJournalEntryLine()
            {
                FinancialDocumentId = financialDocuments.FinancialDocumentId,
                physicalyearId = financialDocuments.physicalyearId,
                JournalEntryDate = financialDocuments.JournalEntryDate,
                CreateId = financialDocuments.CreateId,
                CreatedDate = financialDocuments.CreatedDate,
            };

            journalEntryLine2.AccountID = financialDocuments.TreasuryID;
            journalEntryLine2.Debit = 0;
            journalEntryLine2.Credit = financialDocuments.Value;

            _context.Add(journalEntryLine2);
            await _context.SaveChangesAsync();
        }

        // GetscheduleValue


        [HttpGet]
        [Route("ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
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

                await _signInManager.RefreshSignInAsync(user);

                var iuser = await _context.Users.FindAsync(user.Id);
                user.Pwd = model.Password;
                _context.Update(iuser);
                await _context.SaveChangesAsync();

                return RedirectToRoute(new { action = "Index", Controller = "Dashboard" });
            }
            return View(model);
        }


    }
}
