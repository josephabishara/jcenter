using JeCenterWeb.Data;
using JeCenterWeb.Models;
using JeCenterWeb.Models.Second;
using JeCenterWeb.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace JeCenterWeb.Areas.learning.Controllers
{
    [Area("learning")]
    [Authorize(Roles = "Student")]
    public class GroupsController : Controller
    {
        string ControllerName = " المجموعات ";
        string AppName = "السنتر";
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GroupsController(ApplicationDbContext context,
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index(int? id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = "المواد / المناهج";
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser student = await _userManager.FindByIdAsync(userId);
            ViewData["UserName"] = student.FristName;
            var userbalance = await _context.UsersBalance.FindAsync(student.AccountID);
            ViewData["balance"] = userbalance.Balance * -1;

            int? PhaseId = _httpContextAccessor.HttpContext.Session.GetInt32("_phase");
            int? PhysicalyearId = _httpContextAccessor.HttpContext.Session.GetInt32("_physicalyear");
            ViewData["PhysicalyearId"] = PhysicalyearId;
            if (PhaseId == 0 || PhaseId == null || PhysicalyearId == 0 || PhysicalyearId == null)
            {
                return RedirectToRoute(new { controller = "Applications", action = "Index", area = "learning" });
            }

            var Syllabuslist = await _context.CSyllabus.Where(s => s.PhaseID == PhaseId)
                .Include(s => s.CGroups.Where(g => g.physicalyearId == PhysicalyearId))
                .Include(s => s.CPhases)
                .ToListAsync();
            return View(Syllabuslist);

        }

        public async Task<IActionResult> Teachers(int? id)
        {
            ViewData["AppName"] = AppName;
           
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser student = await _userManager.FindByIdAsync(userId);
            ViewData["UserName"] = student.FristName;
            var userbalance = await _context.UsersBalance.FindAsync(student.AccountID);
            ViewData["balance"] = userbalance.Balance * -1;
            int? PhaseId = _httpContextAccessor.HttpContext.Session.GetInt32("_phase");
            int? PhysicalyearId = _httpContextAccessor.HttpContext.Session.GetInt32("_physicalyear");
            ViewData["PhysicalyearId"] = PhysicalyearId;

            var syllabus = await _context.CSyllabus
                .Where(s => s.SyllabusID == id)
                .Include(s => s.CPhases)
                .FirstOrDefaultAsync();
       
            ViewData["ControllerName"] = " مدرسين " + syllabus.SyllabusName + syllabus.CPhases.PhaseName;
            if (id == null)
            {
                return RedirectToRoute(new { controller = "Groups", action = "Index", area = "learning" });
            }
            else
            {
                // DISTINCT 
                var Teacherlist = await _context.CGroups
                     .Where(g => g.SyllabusID == id && g.Teacher.Active == true && g.physicalyearId == PhysicalyearId && g.Active == true)
                     .Include(g => g.Teacher)
                     .GroupBy(g => new { g.TeacherId, g.TeacherName, g.SyllabusID, g.Teacher.imgurl })
                     .Select(m => new TeachersGroupsViewModel
                     { id = m.Key.TeacherId, TeacherName = m.Key.TeacherName, imgurl = m.Key.imgurl, SyllabusID = m.Key.SyllabusID, CountOfGroups = m.Count() })
                    .Distinct().ToListAsync();

                return View(Teacherlist);
            }

            return View();
        }
        public async Task<IActionResult> TeacherGroups(int? id, int SyllabusID)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser student = await _userManager.FindByIdAsync(userId);
            ViewData["UserName"] = student.FristName;
            var userbalance = await _context.UsersBalance.FindAsync(student.AccountID);
            ViewData["balance"] = userbalance.Balance * -1;
            int? PhaseId = _httpContextAccessor.HttpContext.Session.GetInt32("_phase");
            int? PhysicalyearId = _httpContextAccessor.HttpContext.Session.GetInt32("_physicalyear");
            ViewData["PhysicalyearId"] = PhysicalyearId;
            if (id == null)
            {

            }
            else
            {
                var studentlectures = await _context.StudentGroup
                    .Where(s => s.StudentId == student.Id)
                    .Select(s => s.GroupId)
                    .ToListAsync();

                var Teacherlist = await _context.CGroups
                   .Where(s => s.SyllabusID == SyllabusID && s.TeacherId == id && s.physicalyearId == PhysicalyearId && s.Active == true)
                   .Where(f => !studentlectures.Contains(f.GroupId))
                   .Include(s => s.TeacherGroups)
                   .Include(s => s.StudentGroup)
                   .Include(s => s.ResourceDays)
                   .Include(s => s.CRooms)
                   .Include(s => s.CRooms.CBranch)
                   .OrderBy(s => s.DayOfWeekId)
                   .ToListAsync();
                return View(Teacherlist);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateApplication([Bind("StudentId,PhysicalyearId,PhaseId")] CreateStudentApplicationViewModel model)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            if (ModelState.IsValid)
            {
                StudentApplications application = new StudentApplications
                {
                    //BranchId = model.BranchId,
                    StudentId = model.StudentId,
                    PhysicalyearId = model.PhysicalyearId,
                    PhaseId = model.PhaseId,
                    CreateId = uid,
                    CreatedDate = DateTime.Now,
                };

                _context.Add(application);
                await _context.SaveChangesAsync();

                CGroups group = await _context.CGroups.SingleOrDefaultAsync();
            }

            return View();
        }

        public async Task<IActionResult> TeacherGroup(int? id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = "المواد / المناهج";

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int studentId = Convert.ToInt32(userId);

            StudentGroup studentGroup = await _context.StudentGroup
               .Where(g => g.StudentId == studentId && g.GroupId == id)
               .FirstOrDefaultAsync();

            if (studentGroup is not null)
            {
                return RedirectToRoute(new { area = "learning", controller = "Balanc", action = "Index"});
            }

            CGroups? group = await _context.CGroups
                    .Where(g => g.GroupId == id)
                    .Include(s => s.TeacherGroups)
                    .Include(s => s.ResourceDays)
                    .Include(s => s.CRooms)
                    .Include(s => s.CRooms.CBranch)
                    .Include(s => s.CSyllabus.CPhases)
                    .FirstOrDefaultAsync();
            ApplicationUser student = await _userManager.FindByIdAsync(userId);
            ViewData["UserName"] = student.FristName;
            var userbalance = await _context.UsersBalance.FindAsync(student.AccountID);
            ViewData["balance"] = userbalance.Balance * -1;
            decimal balance = userbalance.Balance * -1;
            int? PhaseId = _httpContextAccessor.HttpContext.Session.GetInt32("_phase");
            int? PhysicalyearId = _httpContextAccessor.HttpContext.Session.GetInt32("_physicalyear");
            ViewData["PhysicalyearId"] = PhysicalyearId;
            if (group == null)
            {
                return View(group);
            }
            else
            {
                //var application = await _context.StudentApplications
                //    .Where(a=>a.StudentId== studentId && a.PhysicalyearId== group.physicalyearId)
                //    .FirstOrDefaultAsync();
                bool Paided = await GetApplication(studentId, group.physicalyearId, group.CSyllabus.PhaseID);
                decimal LecturePrice = group.LecturePrice;
                decimal applicationvalue = 0;
                if (Paided == false)
                {
                    applicationvalue = group.CSyllabus.CPhases.ApplicationValue;
                    ViewData["mssg1"] = applicationvalue.ToString() + " جنيه قيمة الأستمارة والاستماره لاترد ";
                }
                ViewData["mssg2"] = LecturePrice.ToString() + " جنيه ثمن الحصة الأولى  " ;
                bool checkPhaseApplication = await CheckPhaseApplication(studentId, group.physicalyearId, group.CSyllabus.PhaseID, group.TeacherId, group.SyllabusID);
                decimal applicationPhaseValue = 0;
                if (checkPhaseApplication == false)
                {
                    var teachersyllabuapp = await _context.TeacherSyllabus
                        .Where(a => a.TeacherId == group.TeacherId && a.SyllabusID == group.CSyllabus.SyllabusID)
                        .FirstOrDefaultAsync();
                    ViewData["mssg3"] = teachersyllabuapp.ApplicationValue.ToString() + " جنيه ثمن استمارة مادة " + teachersyllabuapp.SyllabusName + " مع " + teachersyllabuapp.TeacherName ;
                    applicationPhaseValue = teachersyllabuapp.ApplicationValue;
                }

                ViewData["total"] = LecturePrice + applicationvalue + applicationPhaseValue;
                if (applicationvalue + LecturePrice + applicationPhaseValue > balance)
                {
                    ViewData["allow"] = "0";
                    ViewData["balance"] = balance;
                }
                else
                {
                    ViewData["allow"] = "1";
                }

                return View(group);
            }

            return View();
        }

        public async Task<bool> GetApplication(int studentId, int PhysicalyearId, int phaseid)
        {
            int result = 0;
            var app = await _context.StudentApplications
                .Where(a => a.StudentId == studentId & a.PhysicalyearId == PhysicalyearId & a.PhaseId == phaseid)
                .FirstOrDefaultAsync();

            return app.Paided;
        }


        public async Task<bool> CheckPhaseApplication(int? studentId, int PhysicalyearId, int phaseid, int TeacherId, int SyllabusID)
        {
            var app = await _context.StudentPhaseApplications
                .Where(a => a.StudentId == studentId && a.PhysicalyearId == PhysicalyearId && a.PhaseId == phaseid && a.TeacherId == TeacherId && a.SyllabusID == SyllabusID)
                .FirstOrDefaultAsync();
            if (app == null)
            { return false; }
            else
            { return true; }


            return app.Paided;
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reservation(int? id)
        {
            // Get Group By id
            CGroups? group = await _context.CGroups
                    .Where(g => g.GroupId == id)
                    .Include(s => s.TeacherGroups)
                    .Include(s => s.ResourceDays)
                    .Include(s => s.CRooms)
                    .Include(s => s.CRooms.CBranch)
                    .Include(s => s.CSyllabus.CPhases)
                    .Include(s => s.PhysicalYear)
                    .FirstOrDefaultAsync();

            // Get User ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            int studentId = Convert.ToInt32(userId);
            ApplicationUser student = await _userManager.FindByIdAsync(userId);
            ViewData["UserName"] = student.FristName;
            var userbalance = await _context.UsersBalance.FindAsync(student.AccountID);
            ViewData["balance"] = userbalance.Balance * -1;
            decimal balance = userbalance.Balance * -1;


            // Test If Student had Application Or Not
            bool paided = await GetApplication(studentId, group.physicalyearId, group.CSyllabus.PhaseID);
            if (paided == false)
            {
                // If Not Buy One
                var studentApplications = await _context.StudentApplications
                     .Where(a => a.StudentId == studentId && a.PhysicalyearId == group.physicalyearId)
                 .FirstOrDefaultAsync();

                //  FinancialDocuments 
                FinancialDocuments DocumentApplication = new FinancialDocuments
                {
                    JournalEntryDate = System.DateTime.Today,
                    Notes = "  استمارة اشتراك لسنة " + group.PhysicalYear.PhysicalyearName + " لمرحلة  " + group.CSyllabus.CPhases.PhaseName,
                    TreasuryID = 0,
                    AccountID = student.AccountID,
                    Value = group.CSyllabus.CPhases.ApplicationValue,
                    MovementTypeId = 14,
                    physicalyearId = group.physicalyearId,
                    Active = true,

                    CreateId = uid,
                    CreatedDate = DateTime.Now,
                };
                _context.Add(DocumentApplication);
                await _context.SaveChangesAsync();


                // FinancialJournalEntryLine
                FinancialJournalEntryLine JournalEntryLineApplication = new FinancialJournalEntryLine
                {
                    physicalyearId = DocumentApplication.physicalyearId,
                    AccountID = DocumentApplication.AccountID,
                    Debit = DocumentApplication.Value,
                    Credit = 0,
                    JournalEntryDate = DocumentApplication.JournalEntryDate,
                    FinancialDocumentId = DocumentApplication.FinancialDocumentId,
                    Active = true,

                    CreateId = uid,
                    CreatedDate = DateTime.Now,
                };
                _context.Add(JournalEntryLineApplication);
                await _context.SaveChangesAsync();

                studentApplications.Paided = true;
                studentApplications.DocNo = JournalEntryLineApplication.FinancialDocumentId;
                _context.Update(studentApplications);
                await _context.SaveChangesAsync();
            }


            bool checkPhaseApplication = await CheckPhaseApplication(studentId, group.physicalyearId, group.CSyllabus.PhaseID, group.TeacherId, group.SyllabusID);
            int applicationPhaseValue = 0;
            string notephseapp = "";
            if (checkPhaseApplication == false)
            {
                var teachersyllabuapp = await _context.TeacherSyllabus
                    .Where(a => a.TeacherId == group.TeacherId && a.SyllabusID == group.CSyllabus.SyllabusID)
                    .FirstOrDefaultAsync();
                notephseapp = teachersyllabuapp.ApplicationValue.ToString() + " ثمن استمارة مادة " + teachersyllabuapp.SyllabusName + " مع " + teachersyllabuapp.TeacherName;
                applicationPhaseValue = teachersyllabuapp.ApplicationValue;
            }

            if (applicationPhaseValue > 0)
            {
                FinancialDocuments documentphaseApplication = new FinancialDocuments
                {
                    JournalEntryDate = System.DateTime.Today,
                    Notes = notephseapp,
                    TreasuryID = 0,
                    AccountID = student.AccountID,
                    Value = applicationPhaseValue,
                    MovementTypeId = 15,
                    physicalyearId = group.physicalyearId,
                    Active = true,
                    
                    CreateId = uid,
                    CreatedDate = DateTime.Now,
                };
                _context.Add(documentphaseApplication);
                await _context.SaveChangesAsync();

                // FinancialJournalEntryLine
                FinancialJournalEntryLine JournalEntryLinephaseApplication = new FinancialJournalEntryLine
                {
                    physicalyearId = documentphaseApplication.physicalyearId,
                    AccountID = documentphaseApplication.AccountID,
                    Debit = documentphaseApplication.Value,
                    Credit = 0,
                    JournalEntryDate = documentphaseApplication.JournalEntryDate,
                    FinancialDocumentId = documentphaseApplication.FinancialDocumentId,
                    Active = true,
                    
                    CreateId = uid,
                    CreatedDate = DateTime.Now,
                };
                _context.Add(JournalEntryLinephaseApplication);
                await _context.SaveChangesAsync();

                StudentPhaseApplications studentPhaseApplications = new StudentPhaseApplications()
                {
                    Active = true,
                    ApplicationValue = applicationPhaseValue,
                    StudentId = studentId,
                    Paided = true,
                    DocNo = documentphaseApplication.FinancialDocumentId,
                    SyllabusID = group.SyllabusID,
                    SyllabusName = group.CSyllabus.SyllabusName,
                    TeacherId = group.TeacherId,
                    TeacherName = group.TeacherName,
                    PhysicalyearId = group.physicalyearId,
                    PhaseName = group.CSyllabus.CPhases.PhaseName,
                    PhaseId = group.CSyllabus.CPhases.PhaseId,
                    CreateId = uid,
                    CreatedDate = DateTime.Now,
                };

                _context.Add(studentPhaseApplications);
                await _context.SaveChangesAsync();
            }



            //  Financial Documents 
            FinancialDocuments DocumentGroup = new FinancialDocuments
            {
                JournalEntryDate = System.DateTime.Today,
                Notes = " حجز مجموعة" + group.GroupName,
                TreasuryID = 0,
                AccountID = student.AccountID,
                Value = group.LecturePrice,
                MovementTypeId = 13,
                physicalyearId = group.physicalyearId,
                Active = true,

                CreateId = uid,
                CreatedDate = DateTime.Now,
            };
            _context.Add(DocumentGroup);
            await _context.SaveChangesAsync();

            // Financial Journal EntryLine
            FinancialJournalEntryLine JournalEntryLineGroup = new FinancialJournalEntryLine
            {
                physicalyearId = DocumentGroup.physicalyearId,
                AccountID = DocumentGroup.AccountID,
                Debit = DocumentGroup.Value,
                Credit = 0,
                JournalEntryDate = DocumentGroup.JournalEntryDate,
                FinancialDocumentId = DocumentGroup.FinancialDocumentId,
                Active = true,

                CreateId = uid,
                CreatedDate = DateTime.Now,
            };
            _context.Add(JournalEntryLineGroup);
            await _context.SaveChangesAsync();

            // StudentGroup
            StudentGroup studentGroup = new StudentGroup
            {
                GroupId = group.GroupId,
                StudentId = studentId,
                CreateId = uid,
                CreatedDate = DateTime.Now,
            };
            _context.Add(studentGroup);
            await _context.SaveChangesAsync();

            StudentBalancePending studentBalancePending = new StudentBalancePending()
            {
                AccountID = student.AccountID,
                TeacherId = group.TeacherId,
                Credit = JournalEntryLineGroup.Debit,
                GroupId = group.GroupId,
                Debit = 0,


            };
            _context.Add(studentBalancePending);
            await _context.SaveChangesAsync();

            return RedirectToRoute(new { controller = "Schedule", action = "Index", area = "learning" });
        }
       
        
        // reservation 
        /*
            *  testapplicationfromphysicalyear
            *    int applicationId = await GetApplication(studentId, group.physicalyearId);
                decimal applicationvalue = group.CSyllabus.CPhases.ApplicationValue;
                decimal LecturePrice = group.LecturePrice;

            *  if (applicationreserved == 0)
            *          getprice => GroupID
            *          
            *  INSERT INTO Studentstatement => applicationreservedPrice
            *  INSERT INTO StudentGroup
            *  INSERT INTO Studentlecture
            *  INSERT INTO Studentstatement => groupname
            *  INSERT INTO Application
            *  
            *          
            * */
    }
}
