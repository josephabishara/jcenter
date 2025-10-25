using JeCenterWeb.Data;
using JeCenterWeb.Migrations;
using JeCenterWeb.Models;
using JeCenterWeb.Models.Second;
using JeCenterWeb.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using NuGet.DependencyResolver;
using System.Data;
using System.Linq;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace JeCenterWeb.Areas.scp.Controllers
{
    [Area("scp")]
    [Authorize(Roles = "AdminSettings,PowerUser,SuperUser,User")]
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<TeachersController> _logger;
        string ControllerName = "الطلبة";
        string AppName = "الأدمن";
        public StudentsController(ApplicationDbContext context,

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
        public IActionResult Index()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            return View();
        }


        public async Task<IActionResult> Create()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["Password"] = CreateRandomPassword(6);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FristName,SecondName,LastName,FamilyName,Mobile,Password,Address,NationalID,Birthdate,School,ParentJob,ParentMobile")] CreateStudentViewModel registerViewModel)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            string imgurl = "images/Students/default.jpg";

            if (ModelState.IsValid)
            {
                var student = new ApplicationUser();

                student.Email = "s" + registerViewModel.Mobile + "@elra3ycenter.com";
                student.Mobile = registerViewModel.Mobile;
                student.UserName = registerViewModel.Mobile;
                student.FullName = registerViewModel.FristName + " " + registerViewModel.SecondName + " " + registerViewModel.LastName + " " + registerViewModel.FamilyName;
                student.FristName = registerViewModel.FristName;
                student.SecondName = registerViewModel.SecondName;
                student.FamilyName = registerViewModel.FamilyName;
                student.LastName = registerViewModel.LastName;
                student.Address = registerViewModel.Address;
                student.NationalID = registerViewModel.NationalID;
                student.Birthdate = registerViewModel.Birthdate;
                student.School = registerViewModel.School;
                student.AccountID = 0;
                student.ParentJob = registerViewModel.ParentJob;
                student.ParentMobile = registerViewModel.ParentMobile;
                student.Pwd = registerViewModel.Password;
                student.imgurl = imgurl;
                student.Active = true;
                student.UserTypeId = 7;
                student.CreateId = uid;
                student.CreatedDate = DateTime.Now;

                var result = await _userManager.CreateAsync(student, registerViewModel.Password);
                if (result.Succeeded)
                {
                    // await _signInManager.SignInAsync(user, false);
                    List<string> role = new List<string>();
                    role.Add("Student");
                    await _userManager.AddToRolesAsync(student, role);

                    FinancialAccount financialAccount = new FinancialAccount
                    {
                        AccountName = registerViewModel.FristName + " " + registerViewModel.SecondName + " " + registerViewModel.LastName + " " + registerViewModel.FamilyName,
                        AccountTypeId = 7,
                        Active = true,
                        CreateId = uid,
                        CreatedDate = DateTime.Now,
                    };

                    _context.Add(financialAccount);
                    await _context.SaveChangesAsync();
                    int AccountId = financialAccount.FinancialAccountId;
                    var user = await _context.Users.FindAsync(student.Id);
                    user.AccountID = AccountId;

                    _context.Update(user);
                    await _context.SaveChangesAsync();


                    return RedirectToRoute(new { Areas = "scp", controller = "Students", action = "Details", id = user.Id });
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
                return View(registerViewModel);
            }
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var student = await _context.Users.Where(u => u.Id == id & u.UserTypeId == 7)
                  .FirstOrDefaultAsync();

            if (student != null)
            {
                ViewData["id"] = id;
                ViewData["FullName"] = student.FullName;
                return View(student);
            }

            return RedirectToRoute(new { controller = "Students", action = "Index", area = "scp" });

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("FristName,SecondName,LastName,FamilyName,Mobile,Address,NationalID,Birthdate,School,ParentJob,ParentMobile")] EditStudentViewModel model)
        {
            var user = await _context.Users.FindAsync(id);

            user.UserName = model.Mobile;
            user.Mobile = model.Mobile;
            user.FullName = model.FristName + " " + model.SecondName + " " + model.LastName + " " + model.FamilyName;
            user.FristName = model.FristName;
            user.SecondName = model.SecondName;
            user.FamilyName = model.FamilyName;
            user.LastName = model.LastName;
            user.Address = model.Address;
            user.NationalID = model.NationalID;
            user.Birthdate = model.Birthdate;
            user.School = model.School;
            user.ParentJob = model.ParentJob;
            user.ParentMobile = model.ParentMobile;
            user.Active = true;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToRoute(new { area = "scp", controller = "Students", action = "Details", id = user.Id });
            }

            return View();
        }


        public async Task<IActionResult> Details(int id, int? messageid, decimal? charched)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["id"] = id;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            bool ReplyLectureisaccess = await Isaccessright(user.Id, 6);
            if (ReplyLectureisaccess)
            {
                ViewData["ReplyLecture"] = "true";
            }
            else
            {
                ViewData["ReplyLecture"] = "false";
            }
            bool Discountisaccess = await Isaccessright(user.Id, 7);
            if (Discountisaccess)
            {
                ViewData["Discount"] = "true";
            }
            else
            {
                ViewData["Discount"] = "false";
            }

            bool ChangeState = await Isaccessright(user.Id, 9);
            if (ChangeState)
            {
                ViewData["ChangeState"] = "true";
            }
            else
            {
                ViewData["ChangeState"] = "false";
            }

            bool replyApplication = await Isaccessright(user.Id, 12);
            if (replyApplication)
            {
                ViewData["replyApplication"] = "true";
            }
            else
            {
                ViewData["replyApplication"] = "false";
            }

            StudentDetailsViewModel studentDetailsViewModel = new StudentDetailsViewModel();
            if (messageid != null)
            {
                if (messageid == 1)
                    ViewData["message"] = "هذا الكارت مشحون من قبل أو ليس موجود";
                if (messageid == 2)
                    ViewData["message"] = " تم شحن الكارت بقيمة " + charched.ToString() + " جنيه ";
                if (messageid == 3)
                    ViewData["message"] = " تم الشحن على الهواء بقيمة " + charched.ToString() + " جنيه ";
                if (messageid == 4)
                    ViewData["message"] = " الرصيد غير كافي لدفع مبلغ " + charched.ToString() + " جنيه ";
            }
            // .Where(s => s.UserId == id)
            //  VideoCapture capture = new VideoCapture(0);

            studentDetailsViewModel.student = await _context.Users.Where(u => u.Id == id & u.UserTypeId == 7)
                .FirstOrDefaultAsync();
            if (studentDetailsViewModel.student != null)
            {
                decimal oldBalance = 0;
                //if (student.ParentId != null && student.ParentId != 0)
                //{
                //    var studentstatement = await _secondcontext.Studentstatement
                //        .Where(s => s.StudentID == student.ParentId)
                //        .ToListAsync();
                //    oldBalance = (decimal)(studentstatement.Sum(b => b.Depit) - studentstatement.Sum(b => b.Credit));
                //}
                ViewData["OldBalance"] = 0;
                ViewData["StudentName"] = studentDetailsViewModel.student.FullName;
                var balance = await _context.UsersBalance.FindAsync(studentDetailsViewModel.student.AccountID);

                ViewData["Balance"] = balance.Balance * -1;
                var pending = await _context.StudentBalancePendingViewModel
                  .Where(p => p.AccountID == studentDetailsViewModel.student.AccountID)
                  .ToListAsync();
                decimal pendingbalance = 0;
                if (pending != null)
                {
                    pendingbalance = pending.Sum(p => p.balance);
                }

                var StudentComments = await _context.StudentComments
                    .Where(p => p.StudentID == id && p.ParentId == 0)
                    .OrderByDescending(s=>s.StudentCommentId)
                    .ToListAsync();
                List<CommentsViewModel> commentsViewModels = new List<CommentsViewModel>();

                foreach (var comment in StudentComments)
                {
                    CommentsViewModel commentsViewModel = new CommentsViewModel();
                  

                    if (comment.CreateId == 0)
                    {
                        commentsViewModel.CreateName = "N/A";
                        commentsViewModel.imgurl = "images/StudentUsers/default.png";
                    }
                    else
                    {
                        var commentermodel = await _context.Users.Where(c => c.Id == comment.CreateId).FirstOrDefaultAsync();
                        commentsViewModel.CreateName = commentermodel.FullName;
                        commentsViewModel.imgurl = commentermodel.imgurl;
                    }

                    commentsViewModel.Note = comment.Note;
                    commentsViewModel.CreatedDate = comment.CreatedDate;
                    commentsViewModel.Id = comment.StudentCommentId;
                    commentsViewModels.Add(commentsViewModel);
                }
                studentDetailsViewModel.CommentsViewModel = commentsViewModels;


                // Admin Comment 
                var AdminComments = await _context.StudentComments
                  .Where(p => p.StudentID == id && p.ParentId == 1)
                    .OrderByDescending(s => s.StudentCommentId)
                  .ToListAsync();
                List<CommentsViewModel> admincommentsViewModels = new List<CommentsViewModel>();

                foreach (var comment in AdminComments)
                {
                    CommentsViewModel commentsViewModel = new CommentsViewModel();

                    if (comment.CreateId == 0)
                    {
                        commentsViewModel.CreateName = "Super Admin";
                        commentsViewModel.imgurl = "images/StudentUsers/default.png";
                    }
                    else
                    {
                        var commentermodel = await _context.Users.Where(c => c.Id == comment.CreateId).FirstOrDefaultAsync();
                        commentsViewModel.CreateName = commentermodel.FullName;
                        commentsViewModel.imgurl = commentermodel.imgurl;
                    }
                    commentsViewModel.Note = comment.Note;
                    commentsViewModel.CreatedDate = comment.CreatedDate;
                    commentsViewModel.Id = comment.StudentCommentId;
                    admincommentsViewModels.Add(commentsViewModel);
                }
                studentDetailsViewModel.ParentCommentsViewModel = admincommentsViewModels;

                ViewData["Pending"] = pendingbalance;

                return View(studentDetailsViewModel);
            }

            return RedirectToRoute(new { controller = "Students", action = "Index", area = "scp" });
        }

        // SearchActiveByWord
        public async Task<IActionResult> SearchActiveByWord(string word)
        {
            ViewData["CurrentFilter"] = word;
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = "بحث عن طلبة : " + word;
            List<ApplicationUser> users = await _userManager.Users
                .Where(t => t.UserTypeId == 7 && (t.FullName.Contains(word) || t.Mobile == word))
                .ToListAsync();
            List<SearchActiveByWordViewModel> viewModellist = new List<SearchActiveByWordViewModel>();
            foreach (var item in users)
            {
                var balance = await _context.UsersBalance.FindAsync(item.AccountID);
                SearchActiveByWordViewModel model = new SearchActiveByWordViewModel()
                {
                    Id = item.Id,
                    FullName = item.FullName,
                    Mobile = item.Mobile,
                    Balance = balance.Balance * -1,
                    Active = item.Active,
                };
                viewModellist.Add(model);
            }

            return View(viewModellist);
        }

        //ChargeCard

        [HttpGet]
        public async Task<IActionResult> ChargeCard(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            if (user.online == 0)
            {
                return RedirectToRoute(new { area = "scp", controller = "Branch", action = "LogInBranch" });
            }
            var student = await _context.Users.Where(u => u.Id == id & u.UserTypeId == 7)
                  .FirstOrDefaultAsync();
            ViewData["BranchId"] = new SelectList(_context.CBranch, "BranchId", "BranchName");
            if (student != null)
            {
                ViewData["id"] = id;
                ViewData["FullName"] = student.FullName;
                return View();
            }

            return RedirectToRoute(new { controller = "Students", action = "Index", area = "scp" });

        }

        [HttpPost]
        public async Task<IActionResult> ChargeCard([Bind("id,Value,BranchId")] ChargeValueViewModel model)
        {
            ViewData["CurrentFilter"] = model.Value;
            ViewData["AppName"] = AppName;
            var card = await _context.ChargingCard
                 .Where(c => c.CardCode == model.Value && c.State == false)
                 .FirstOrDefaultAsync();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            int uid = Convert.ToInt32(userId);
            if (user.online == 0)
            {
                return RedirectToRoute(new { area = "scp", controller = "Branch", action = "LogInBranch" });
            }
            DateTime date = DateTime.Today;
            var physicalYear = await _context.PhysicalYear
              .Where(y => y.FromDate <= date && y.ToDate >= date)
           .OrderByDescending(p => p.PhysicalyearId)
           .FirstOrDefaultAsync();
            int PhysicalyearId = physicalYear.PhysicalyearId;
            if (card == null)
            {
                ViewData["BranchId"] = new SelectList(_context.CBranch, "BranchId", "BranchName", model.BranchId);
                return RedirectToRoute(new { controller = "Students", action = "Details", area = "scp", id = model.id, messageid = 1 });
            }
            else
            {
                decimal cardvalue = card.CardValue;
                var student = await _context.Users.Where(u => u.Id == model.id & u.UserTypeId == 7)
                 .FirstOrDefaultAsync();

                card.StudentID = model.id;
                card.ChargingDate = DateTime.Now;
                card.State = true;
                _context.Update(card);
                await _context.SaveChangesAsync();

                //  FinancialDocuments 
                FinancialDocuments documents = new FinancialDocuments
                {
                    JournalEntryDate = System.DateTime.Today,
                    Notes = "شحن كارت : " + model.Value,
                    TreasuryID = 0,
                    AccountID = student.AccountID,
                    Value = cardvalue,
                    MovementTypeId = 5,
                    physicalyearId = PhysicalyearId,
                    Active = true,
                    BranchId = user.online,
                    CreateId = uid,
                    CreatedDate = DateTime.Now,
                };
                _context.Add(documents);
                await _context.SaveChangesAsync();

                // FinancialJournalEntryLine
                FinancialJournalEntryLine JournalEntryLineGroup = new FinancialJournalEntryLine
                {
                    physicalyearId = PhysicalyearId,
                    AccountID = documents.AccountID,
                    Debit = 0,
                    Credit = documents.Value,
                    JournalEntryDate = documents.JournalEntryDate,
                    FinancialDocumentId = documents.FinancialDocumentId,
                    Active = true,
                    BranchId = user.online,
                    CreateId = uid,
                    CreatedDate = DateTime.Now,
                };
                _context.Add(JournalEntryLineGroup);
                await _context.SaveChangesAsync();



                return RedirectToRoute(new { controller = "Students", action = "Details", area = "scp", id = model.id, messageid = 2, charched = documents.Value });
            }

            return View();
        }

        //ChargeValue
        [HttpGet]
        public async Task<IActionResult> ChargeValue(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            int uid = Convert.ToInt32(userId);
            if (user.online == 0)
            {
                return RedirectToRoute(new { area = "scp", controller = "Branch", action = "LogInBranch" });
            }
            var student = await _context.Users.Where(u => u.Id == id & u.UserTypeId == 7)
                  .FirstOrDefaultAsync();
            ViewData["BranchId"] = new SelectList(_context.CBranch, "BranchId", "BranchName");
            if (student != null)
            {
                ViewData["id"] = id;
                ViewData["FullName"] = student.FullName;
                return View();
            }

            return RedirectToRoute(new { controller = "Students", action = "Index", area = "scp" });

        }
        [HttpPost]
        public async Task<IActionResult> ChargeValue([Bind("id,Value,BranchId")] ChargeValueViewModel model)
        {
            ViewData["CurrentFilter"] = model.Value;
            ViewData["AppName"] = AppName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            int uid = Convert.ToInt32(userId);
            if (user.online == 0)
            {
                return RedirectToRoute(new { area = "scp", controller = "Branch", action = "LogInBranch" });
            }
            if (ModelState.IsValid)
            {
                DateTime date = DateTime.Today;
                var physicalYear = await _context.PhysicalYear
                  .Where(y => y.FromDate <= date && y.ToDate >= date)
               .OrderByDescending(p => p.PhysicalyearId)
               .FirstOrDefaultAsync();
                int PhysicalyearId = physicalYear.PhysicalyearId;


                decimal value = Convert.ToDecimal(model.Value);

                var student = await _context.Users.FindAsync(model.id);
                // Get treasury ID

                var treasury = await _context.Users.FindAsync(uid);
                //  FinancialDocuments 
                FinancialDocuments documents = new FinancialDocuments
                {
                    JournalEntryDate = System.DateTime.Today,
                    Notes = "شحن على الهواء بـ " + model.Value + " لرقم  " + student.Mobile,
                    TreasuryID = treasury.AccountID,
                    AccountID = student.AccountID,
                    Value = value,
                    MovementTypeId = 6,
                    physicalyearId = PhysicalyearId,
                    Active = true,
                    BranchId = user.online,
                    CreateId = uid,
                    CreatedDate = DateTime.Now,
                };
                _context.Add(documents);
                await _context.SaveChangesAsync();

                // FinancialJournalEntryLine
                FinancialJournalEntryLine JournalEntryLineGroup = new FinancialJournalEntryLine
                {
                    physicalyearId = PhysicalyearId,
                    AccountID = documents.AccountID,
                    Debit = 0,
                    Credit = documents.Value,
                    JournalEntryDate = documents.JournalEntryDate,
                    FinancialDocumentId = documents.FinancialDocumentId,
                    Active = true,
                    BranchId = user.online,
                    CreateId = uid,
                    CreatedDate = DateTime.Now,
                };
                _context.Add(JournalEntryLineGroup);
                await _context.SaveChangesAsync();

                FinancialJournalEntryLine JournalEntryLineGroup2 = new FinancialJournalEntryLine
                {
                    physicalyearId = PhysicalyearId,
                    AccountID = treasury.AccountID,
                    Debit = documents.Value,
                    Credit = 0,
                    JournalEntryDate = documents.JournalEntryDate,
                    FinancialDocumentId = documents.FinancialDocumentId,
                    Active = true,
                    BranchId = user.online,
                    CreateId = uid,
                    CreatedDate = DateTime.Now,
                };
                _context.Add(JournalEntryLineGroup2);
                await _context.SaveChangesAsync();

                return RedirectToRoute(new { controller = "Students", action = "Details", area = "scp", id = model.id, messageid = 3, charched = documents.Value });

            }
            else
            {
                ViewData["BranchId"] = new SelectList(_context.CBranch, "BranchId", "BranchName", model.BranchId);
                ModelState.AddModelError(string.Empty, " أكتب قيمة الشحن ");
            }
            return View(model);
        }

        //ChargeValue ChargeValue
        [HttpPost]
        public async Task<IActionResult> ChangeNote([Bind("id,Note")] StudentNoteViewModel model)
        {
            ViewData["AppName"] = AppName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            int uid = Convert.ToInt32(userId);

            var student = await _context.Users.FindAsync(model.id);

            student.Note = model.Note;
            _context.Update(student);
            await _context.SaveChangesAsync();

            StudentComments studentComments = new StudentComments()
            {
                StudentID = model.id,
                Note = model.Note,
                CreateId = uid,
                CreatedDate = DateTime.Now,
                Active = true,
                ParentId = 0
            };
            _context.Add(studentComments);
            await _context.SaveChangesAsync();

            return RedirectToRoute(new { controller = "Students", action = "Details", area = "scp", id = model.id });

        }

        // AdminNote 
        [HttpPost]
        public async Task<IActionResult> AdminNote([Bind("id,Note")] StudentNoteViewModel model)
        {
            ViewData["AppName"] = AppName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            int uid = Convert.ToInt32(userId);

            var student = await _context.Users.FindAsync(model.id);

            student.Note = model.Note;
            _context.Update(student);
            await _context.SaveChangesAsync();

            StudentComments studentComments = new StudentComments()
            {
                StudentID = model.id,
                Note = model.Note,
                CreateId = uid,
                CreatedDate = DateTime.Now,
                Active = true,
                ParentId = 1
            };
            _context.Add(studentComments);
            await _context.SaveChangesAsync();

            return RedirectToRoute(new { controller = "Students", action = "Details", area = "scp", id = model.id });

        }



        [HttpPost]
        public async Task<IActionResult> returnValue([Bind("id,Value")] ChargeValueViewModel model)
        {
            ViewData["CurrentFilter"] = model.Value;
            ViewData["AppName"] = AppName;

            DateTime date = DateTime.Today;
            var physicalYear = await _context.PhysicalYear
             .Where(y => y.FromDate <= date && y.ToDate >= date)
           .OrderByDescending(p => p.PhysicalyearId)
           .FirstOrDefaultAsync();
            int PhysicalyearId = physicalYear.PhysicalyearId;



            decimal value = Convert.ToDecimal(model.Value);

            var student = await _context.Users.FindAsync(model.id);
            // Get treasury ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var treasury = await _context.Users.FindAsync(uid);
            //  FinancialDocuments 
            FinancialDocuments documents = new FinancialDocuments
            {
                JournalEntryDate = System.DateTime.Today,
                Notes = " استرجاع نقدي " + model.Value + " - " + student.FullName + " - " + student.Mobile,
                TreasuryID = treasury.AccountID,
                AccountID = student.AccountID,
                Value = value,
                MovementTypeId = 7,
                physicalyearId = PhysicalyearId,
                Active = true,
                Approve = 0,
                CreateId = uid,
                CreatedDate = DateTime.Now,
            };

            _context.Add(documents);
            await _context.SaveChangesAsync();

            // FinancialJournalEntryLine
            FinancialJournalEntryLine JournalEntryLine1 = new FinancialJournalEntryLine
            {
                physicalyearId = PhysicalyearId,
                AccountID = documents.AccountID,
                Debit = documents.Value,
                Credit = 0,
                JournalEntryDate = documents.JournalEntryDate,
                FinancialDocumentId = documents.FinancialDocumentId,
                Active = true,
                CreateId = uid,
                CreatedDate = DateTime.Now,
            };
            _context.Add(JournalEntryLine1);
            await _context.SaveChangesAsync();




            FinancialJournalEntryLine JournalEntryLine2 = new FinancialJournalEntryLine
            {
                physicalyearId = PhysicalyearId,
                AccountID = treasury.AccountID,
                Debit = 0,
                Credit = documents.Value,
                JournalEntryDate = documents.JournalEntryDate,
                FinancialDocumentId = documents.FinancialDocumentId,
                Active = true,
                CreateId = uid,
                CreatedDate = DateTime.Now,
            };
            _context.Add(JournalEntryLine2);
            await _context.SaveChangesAsync();

            return RedirectToRoute(new { controller = "Students", action = "Balance", area = "scp", id = model.id, messageid = 3, charched = documents.Value });

        }

        [HttpPost]
        public async Task<IActionResult> ChangeState(int id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id.ToString());
            if (user.UserTypeId != 7)
                return RedirectToAction(nameof(Index));

            if (user.Active == false)
                user.Active = true;
            else if (user.Active == true)
                user.Active = false;

            await _userManager.UpdateAsync(user);

            return RedirectToRoute(new { action = "Details", id = id });

        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["Password"] = CreateRandomPassword(6);
            ViewData["id"] = id;
            var user = await _context.Users.FindAsync(id);
            ViewData["FullName"] = user.FullName;

            if (user.UserTypeId != 7)
                return RedirectToAction(nameof(Index));
            ChangePasswordStudentUserViewModel model = new ChangePasswordStudentUserViewModel
            {
                Id = id,
                FullName = user.FullName,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(int id, [Bind("Id,Password,FullName")] ChangePasswordStudentUserViewModel model)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ApplicationUser employee = await _userManager.FindByIdAsync(id.ToString());

            if (employee.UserTypeId != 7)
                return RedirectToAction(nameof(Index));

            employee.PasswordHash = _userManager.PasswordHasher.HashPassword(employee, model.Password);
            var result = await _userManager.UpdateAsync(employee);
            if (result.Succeeded)
            {
                var acc = await _context.Users.FindAsync(id);
                acc.Pwd = model.Password;
                _context.Update(acc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, " المستخدم أو كلمة المرور غير صحيحة رجاء الاعادة ");
            }



            return RedirectToAction(nameof(Index));
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


        // maowad.aspx?  StudentID=1   &   physicalyearID=2   &   PhaseID=23
        // ~/learning/Groups/Index

        public async Task<IActionResult> SubscribesForGroups(int? id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["id"] = id;
            var user = await _context.Users.FindAsync(id);
            if (user.UserTypeId != 7)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["FullName"] = user.FullName;
            ViewData["StudentName"] = user.FullName;
            var apps = await _context.StudentApplications
                .Where(a => a.StudentId == id)
                .Include(a => a.PhysicalYear)
                .Include(a => a.CPhases)
                .ToListAsync();
            return View(apps);

        }

        public async Task<IActionResult> Syllabus(int? id, int PhaseId, int PhysicalyearId)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = "المواد / المناهج";
            ViewData["id"] = id;
            ViewData["PhysicalyearId"] = PhysicalyearId;
            var applicationUser = await _context.Users.FindAsync(id);
            if (applicationUser.UserTypeId != 7)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["FullName"] = applicationUser.FullName;
            // int PhaseId = applicationUser.PhaseId;
            // .Where(s => s.PhaseID == PhaseId)
            var Syllabuslist = await _context.CSyllabus
                .Where(s => s.PhaseID == PhaseId)
                    .Include(s => s.CGroups.Where(g => g.physicalyearId == PhysicalyearId))
                    .Include(s => s.CPhases)
                    .ToListAsync();
            return View(Syllabuslist);
        }


        // Groups.aspx?StudentID=1&PhaseID=23&curriculumID=61
        // ~/learning/Groups/Teachers/2
        public async Task<IActionResult> Teachers(int? id, int SyllabusID, int PhaseID, int PhysicalyearId)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = " المدرسين ";
            ViewData["id"] = id;
            ViewData["PhysicalyearId"] = PhysicalyearId;
            ApplicationUser? applicationUser = await _userManager.FindByIdAsync(id.ToString());
            if (applicationUser.UserTypeId != 7)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["FullName"] = applicationUser.FullName;
            // int PhaseId = applicationUser.PhaseId;

            // DISTINCT 
            var Teacherlist = await _context.CGroups
                      .Where(g => g.SyllabusID == SyllabusID && g.Teacher.Active == true && g.physicalyearId == PhysicalyearId && g.CSyllabus.PhaseID == PhaseID)
                      .Include(g => g.Teacher)
                      .Include(g => g.CSyllabus)
                      .GroupBy(g => new { g.TeacherId, g.TeacherName, g.SyllabusID, g.Teacher.imgurl })
                      .Select(m => new TeachersGroupsViewModel
                      { id = m.Key.TeacherId, TeacherName = m.Key.TeacherName, imgurl = m.Key.imgurl, SyllabusID = m.Key.SyllabusID, CountOfGroups = m.Count() })
                     .Distinct().ToListAsync();

            return View(Teacherlist);

        }


        // Teachergroup.aspx?TeacherID=27&PhaseID=23&StudentID=1
        // ~/learning/Groups/TeacherGroups/6?SyllabusID=2
        public async Task<IActionResult> TeacherGroups(int? id, int SyllabusID, int teacherid, int PhaseID, int PhysicalyearId)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["id"] = id;
            ViewData["PhysicalyearId"] = PhysicalyearId;
            ApplicationUser? applicationUser = await _userManager.FindByIdAsync(id.ToString());

            if (applicationUser.UserTypeId != 7)
            {
                return RedirectToAction(nameof(Index));
            }
            // int PhaseId = applicationUser.PhaseId;
            ViewData["FullName"] = applicationUser.FullName;



            var studentlectures = await _context.StudentGroup
                   .Where(s => s.StudentId == applicationUser.Id)
                   .Select(s => s.GroupId)
                   .ToListAsync();

            var Teacherlist = await _context.CGroups
               .Where(s => s.SyllabusID == SyllabusID && s.TeacherId == teacherid && s.physicalyearId == PhysicalyearId)
               .Where(f => !studentlectures.Contains(f.GroupId))
               .Include(s => s.TeacherGroups)
               .Include(s => s.ResourceDays)
               .Include(s => s.CRooms)
               .Include(s => s.CRooms.CBranch)
               .OrderBy(s => s.DayOfWeekId)
               .ToListAsync();
            return View(Teacherlist);
        }

        // studentgroup.aspx?curriculumID=61&TeacherID=27&GroupID=7877&countlectuere=1&resrve=0&StudentID=1
        // ~/learning/Groups/TeacherGroup/12
        public async Task<IActionResult> TeacherGroup(int? id, int GroupId, int PhaseID, int PhysicalyearId)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = "المواد / المناهج";
            ViewData["id"] = id;
            ViewData["PhysicalyearId"] = PhysicalyearId;

            StudentGroup studentGroup = await _context.StudentGroup
                .Where(g => g.StudentId == id && g.GroupId == GroupId)
                .FirstOrDefaultAsync();

            if (studentGroup is not null)
            {
                return RedirectToRoute(new { area = "scp", controller = "Students", action = "Balance", id = studentGroup.StudentId });
            }

            var applicationUser = await _context.Users.FindAsync(id);
            var userbalance = await _context.UsersBalance.FindAsync(applicationUser.AccountID);
            ViewData["balance"] = userbalance.Balance * -1;
            decimal balance = userbalance.Balance * -1;
            if (applicationUser == null)
            {
                return RedirectToAction(nameof(Index));
            }
            if (applicationUser.UserTypeId != 7)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["FullName"] = applicationUser.FullName;

            CGroups? group = await _context.CGroups
                    .Where(g => g.GroupId == GroupId)
                    .Include(s => s.TeacherGroups)
                   .Include(s => s.ResourceDays)
                   .Include(s => s.CRooms)
                   .Include(s => s.CRooms.CBranch)
                   .Include(s => s.CSyllabus.CPhases)
                   .FirstOrDefaultAsync();

            if (group == null)
            {
                return View(group);
            }
            else
            {
                bool Paided = await GetApplication(id, group.physicalyearId, group.CSyllabus.PhaseID);
                decimal applicationvalue = 0;
                decimal LecturePrice = group.LecturePrice;
                if (Paided == false)
                {
                    applicationvalue = group.CSyllabus.CPhases.ApplicationValue;
                    ViewData["mssg1"] = applicationvalue.ToString() + " قيمة الأستمارة  ";
                }
                ViewData["mssg2"] = LecturePrice.ToString() + " ثمن الحصة الأولى  ";

                bool checkPhaseApplication = await CheckPhaseApplication(id, group.physicalyearId, group.CSyllabus.PhaseID, group.TeacherId, group.SyllabusID);
                decimal applicationPhaseValue = 0;
                if (checkPhaseApplication == false)
                {
                    var teachersyllabuapp = await _context.TeacherSyllabus
                        .Where(a => a.TeacherId == group.TeacherId && a.SyllabusID == group.CSyllabus.SyllabusID)
                        .FirstOrDefaultAsync();
                    ViewData["mssg3"] = teachersyllabuapp.ApplicationValue.ToString() + " ثمن استمارة مادة " + teachersyllabuapp.SyllabusName + " مع " + teachersyllabuapp.TeacherName;
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
        public async Task<bool> GetApplication(int? studentId, int PhysicalyearId, int phaseid)
        {
            int result = 0;
            var app = await _context.StudentApplications
                .Where(a => a.StudentId == studentId && a.PhysicalyearId == PhysicalyearId && a.PhaseId == phaseid)
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

        public async Task<IActionResult> ShowGroups(int id)
        {
            // .Where(g => g.CSyllabus.PhaseID == user.PhaseId)
            var user = await _context.Users.FindAsync(id);
            if (user.UserTypeId != 7)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["FullName"] = user.FullName;
            ViewData["StudentName"] = user.FullName;
            List<CGroups> groups = await _context.CGroups

                .ToListAsync();
            return View(groups);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reservation(int id, int GroupId)
        {
            // Get Group By id
            CGroups? group = await _context.CGroups
                    .Where(g => g.GroupId == GroupId)
                    .Include(s => s.TeacherGroups)
                    .Include(s => s.ResourceDays)
                    .Include(s => s.CRooms)
                    .Include(s => s.CRooms.CBranch)
                    .Include(s => s.CSyllabus.CPhases)
                    .Include(s => s.PhysicalYear)
                    .FirstOrDefaultAsync();
            var room = await _context.CRooms.FindAsync(group.RoomId);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            int uid = Convert.ToInt32(userId);
            if (user.online == 0)
            {
                return RedirectToRoute(new { area = "scp", controller = "Branch", action = "LogInBranch" });
            }
            // Get student 
            var student = await _context.Users.FindAsync(id);
            if (student.UserTypeId != 7)
            {
                return RedirectToAction(nameof(Index));
            }
            var userbalance = await _context.UsersBalance.FindAsync(student.AccountID);
            ViewData["balance"] = userbalance.Balance * -1;
            decimal balance = userbalance.Balance * -1;
            // Test If Student had Application Or Not
            bool paided = await GetApplication(id, group.physicalyearId, group.CSyllabus.PhaseID);



            if (paided == false)
            {
                // If Not Buy One
                var studentApplications = await _context.StudentApplications
                     .Where(a => a.StudentId == id && a.PhysicalyearId == group.physicalyearId)
                     .Include(s => s.CPhases)
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
                    BranchId = user.online,
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
                    BranchId = user.online,
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

            bool checkPhaseApplication = await CheckPhaseApplication(id, group.physicalyearId, group.CSyllabus.PhaseID, group.TeacherId, group.SyllabusID);
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
                    BranchId = user.online,
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
                    BranchId = user.online,
                    CreateId = uid,
                    CreatedDate = DateTime.Now,
                };
                _context.Add(JournalEntryLinephaseApplication);
                await _context.SaveChangesAsync();

                StudentPhaseApplications studentPhaseApplications = new StudentPhaseApplications()
                {
                    Active = true,
                    ApplicationValue = applicationPhaseValue,
                    StudentId = id,
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
                BranchId = user.online,
                CreateId = uid,
                CreatedDate = DateTime.Now,
            };
            _context.Add(DocumentGroup);
            await _context.SaveChangesAsync();
            //

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
                BranchId = user.online,
                CreateId = uid,
                CreatedDate = DateTime.Now,
            };
            _context.Add(JournalEntryLineGroup);
            await _context.SaveChangesAsync();

            // StudentGroup
            StudentGroup studentGroup = new StudentGroup
            {
                GroupId = group.GroupId,
                StudentId = id,
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

            return RedirectToRoute(new { controller = "Students", action = "Schedule", area = "scp", id = id });
        }

        public async Task<IActionResult> Schedule(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["id"] = id;
            var user = await _context.Users.FindAsync(id);
            if (user.UserTypeId != 7)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["FullName"] = user.FullName;
            ViewData["StudentName"] = user.FullName;
            var groups = await _context.StudentGroup
                .Where(g => g.StudentId == id)
                .Include(g => g.CGroups)
                .Include(g => g.CGroups.ResourceDays)
                .Include(g => g.CGroups.CRooms)
                .ToListAsync();
            return View(groups);
        }

        public async Task<IActionResult> Balance(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["id"] = id;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            int uid = Convert.ToInt32(userId);
            bool returnValue = await Isaccessright(user.Id, 10);
            if (returnValue)
            {
                ViewData["returnValue"] = "true";
            }
            else
            {
                ViewData["returnValue"] = "false";
            }

            if (user.online == 0)
            {
                return RedirectToRoute(new { area = "scp", controller = "Branch", action = "LogInBranch" });
            }
            ApplicationUser student = await _userManager.FindByIdAsync(id.ToString());
            if (student.UserTypeId != 7)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserName"] = student.FristName + " " + student.SecondName;
            var balance = await _context.UsersBalance.FindAsync(student.AccountID);
            ViewData["balance"] = balance.Balance * -1;
            ViewData["FullName"] = student.FullName;
            var journalEntryLine = await _context.FinancialJournalEntryLine
           .Where(f => f.AccountID == student.AccountID)
           .Include(f => f.FinancialDocuments)
           .OrderByDescending(j => j.JournalEntryDetilsID)
           .ToListAsync();

            var financialJournalEntryLineViewModel = await _context.FinancialJournalEntryLineViewModel
                   .Where(f => f.AccountID == student.AccountID)
                 .OrderByDescending(d => d.FinancialDocumentId)
                 .ToListAsync();


            int iapp = journalEntryLine.Count();
            if (iapp == 0)
                ViewData["mssg"] = "  ليس هناك حركة على هذا الحساب يلزم بالشحن على الهواء أو بكارت لأمكانية اشتراك في مجموعة ";
            return View(financialJournalEntryLineViewModel);
        }

        public async Task<IActionResult> BalanceByAccount(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            int uid = Convert.ToInt32(userId);
            bool returnValue = await Isaccessright(user.Id, 10);
            if (returnValue)
            {
                ViewData["returnValue"] = "true";
            }
            else
            {
                ViewData["returnValue"] = "false";
            }
            ApplicationUser student = await _context.Users.Where(u => u.AccountID == id).FirstOrDefaultAsync();
            if (student.UserTypeId != 7)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["id"] = student.Id;
            ViewData["UserName"] = student.FristName + " " + student.SecondName;
            var balance = await _context.UsersBalance.FindAsync(id);
            ViewData["balance"] = balance.Balance * -1;
            ViewData["FullName"] = student.FullName;
            var journalEntryLine = await _context.FinancialJournalEntryLine
           .Where(f => f.AccountID == student.AccountID)
           .Include(f => f.FinancialDocuments)
           .OrderByDescending(j => j.JournalEntryDetilsID)
           .ToListAsync();

            var financialJournalEntryLineViewModel = await _context.FinancialJournalEntryLineViewModel
                   .Where(f => f.AccountID == student.AccountID)
                 .OrderByDescending(d => d.FinancialDocumentId)
                 .ToListAsync();


            int iapp = journalEntryLine.Count();
            if (iapp == 0)
                ViewData["mssg"] = "  ليس هناك حركة على هذا الحساب يلزم بالشحن على الهواء أو بكارت لأمكانية اشتراك في مجموعة ";
            return View(financialJournalEntryLineViewModel);
        }

        public async Task<IActionResult> OldBalance(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["id"] = id;

            ApplicationUser student = await _userManager.FindByIdAsync(id.ToString());
            if (student.UserTypeId != 7)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserName"] = student.FristName + " " + student.SecondName;
            ViewData["FullName"] = student.FullName;

            decimal oldBalance = 0;
            //if (student.ParentId != null && student.ParentId != 0)
            //{
            //    var studentstatement = await _secondcontext.Studentstatement
            //        .Where(s => s.StudentID == student.ParentId)
            //        .ToListAsync();
            //    oldBalance = (decimal)(studentstatement.Sum(b => b.Depit) - studentstatement.Sum(b => b.Credit));
            //    int iapp = studentstatement.Count();
            //    ViewData["OldBalance"] = oldBalance;
            //    if (iapp == 0)
            //        ViewData["mssg"] = "  ليس هناك حركة قديمة على هذا الحساب   ";
            //    return View(studentstatement);
            //}
            ViewData["OldBalance"] = oldBalance;


            //var studentstatementnull = await _secondcontext.Studentstatement
            //        .Where(s => s.StudentID == 0)
            //        .ToListAsync();

            return View();
        }
        public async Task<IActionResult> PendingBalance(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["id"] = id;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            int uid = Convert.ToInt32(userId);
            if (user.online == 0)
            {
                return RedirectToRoute(new { area = "scp", controller = "Branch", action = "LogInBranch" });
            }
            ApplicationUser student = await _userManager.FindByIdAsync(id.ToString());
            if (student.UserTypeId != 7)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserName"] = student.FristName + " " + student.SecondName;

            var pending = await _context.StudentBalancePendingViewModel
                  .Where(p => p.AccountID == student.AccountID && p.balance > 0)
                  .ToListAsync();
            decimal pendingbalance = 0;
            if (pending != null)
            {
                pendingbalance = pending.Sum(p => p.balance);
            }
            ViewData["Pending"] = pendingbalance;
            ViewData["FullName"] = student.FullName;
            var model = await _context.StudentBalancePendingList
           .Where(f => f.AccountID == student.AccountID && f.Balance > 0)
           .ToListAsync();

            //  int iapp = model.Count();
            if (pendingbalance == 0)
                ViewData["mssg"] = "  لا يوجد رصيد محجوز ";
            return View(pending);
        }

        public async Task<IActionResult> RetunPanding(int AccountID, int GroupId, int TeacherId)
        {
            var pending = await _context.StudentBalancePendingList
             .Where(p => p.AccountID == AccountID && p.GroupId == GroupId && p.TeacherId == TeacherId)
             .FirstOrDefaultAsync();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            int uid = Convert.ToInt32(userId);
            if (user.online == 0)
            {
                return RedirectToRoute(new { area = "scp", controller = "Branch", action = "LogInBranch" });
            }
            decimal pendingbalance = 0;
            if (pending != null)
            {
                pendingbalance = pending.Balance;
                StudentBalancePending studentBalance = new StudentBalancePending()
                {
                    AccountID = AccountID,
                    Debit = pendingbalance,
                    Credit = 0,
                    GroupId = GroupId,
                    TeacherId = TeacherId,
                };
                _context.Add(studentBalance);
                await _context.SaveChangesAsync();
            }
            var student = await _context.Users.Where(s => s.AccountID == AccountID).FirstOrDefaultAsync();

            CGroups? group = await _context.CGroups
                    .Where(g => g.GroupId == GroupId)
                    .Include(s => s.TeacherGroups)
                    .Include(s => s.ResourceDays)
                    .Include(s => s.CRooms)
                    .Include(s => s.CRooms.CBranch)
                    .Include(s => s.CSyllabus.CPhases)
                    .Include(s => s.PhysicalYear)
                    .FirstOrDefaultAsync();
            var room = await _context.CRooms.FindAsync(group.RoomId);
            //  Financial Documents 
            FinancialDocuments DocumentGroup = new FinancialDocuments
            {
                JournalEntryDate = System.DateTime.Today,
                Notes = " استرجاع رصيد محجوز" + group.GroupName,
                TreasuryID = 0,
                AccountID = student.AccountID,
                Value = pendingbalance,
                MovementTypeId = 7,
                physicalyearId = group.physicalyearId,
                Active = true,
                BranchId = user.online,
                Approve = 0,
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
                Debit = 0,
                Credit = DocumentGroup.Value,
                JournalEntryDate = DocumentGroup.JournalEntryDate,
                FinancialDocumentId = DocumentGroup.FinancialDocumentId,
                Active = true,
                BranchId = user.online,
                CreateId = uid,
                CreatedDate = DateTime.Now,
            };
            _context.Add(JournalEntryLineGroup);
            await _context.SaveChangesAsync();

            return RedirectToRoute(new { controller = "Students", action = "PendingBalance", area = "scp", id = student.Id });
        }

        public async Task<IActionResult> Applications(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["id"] = id;
            var user = await _context.Users.FindAsync(id);
            if (user.UserTypeId != 7)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["FullName"] = user.FullName;
            ViewData["StudentName"] = user.FullName;
            var apps = await _context.StudentApplications
                .Where(a => a.StudentId == id)
                .Include(a => a.PhysicalYear)
                .Include(a => a.CPhases)
                .ToListAsync();
            return View(apps);
        }

        public async Task<IActionResult> Subscribe(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["BranchId"] = new SelectList(_context.CBranch, "BranchId", "BranchName");
            ViewData["id"] = id;
            var user = await _context.Users.FindAsync(id);
            if (user.UserTypeId != 7)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["FullName"] = user.FullName;
            ViewData["PhysicalyearId"] = new SelectList(_context.PhysicalYear.Where(p => p.Active == true), "PhysicalyearId", "PhysicalyearName");
            ViewData["PhaseId"] = new SelectList(_context.CPhases.Where(p => p.Parent > 0 && p.Active == true), "PhaseId", "PhaseName");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Subscribe([Bind("StudentId,PhysicalyearId,PhaseId")] StudentApplications application)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            application.Paided = false;
            application.DocNo = 0;
            if (ModelState.IsValid)
            {
                var sub = await _context.StudentApplications
                    .Where(a => a.StudentId == application.StudentId && a.PhaseId == application.PhaseId)
                    .FirstOrDefaultAsync();
                if (sub == null)
                {
                    _context.Add(application);
                    await _context.SaveChangesAsync();
                    return RedirectToRoute(new { controller = "Students", action = "Applications", area = "scp", id = application.StudentId });
                }
                else
                {
                    var user = await _context.Users.FindAsync(application.StudentId);
                    if (user.UserTypeId != 7)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    ViewData["FullName"] = user.FullName;
                    ViewData["PhysicalyearId"] = new SelectList(_context.PhysicalYear.Where(p => p.Active == true), "PhysicalyearId", "PhysicalyearName", application.PhysicalyearId);
                    ViewData["PhaseId"] = new SelectList(_context.CPhases.Where(p => p.Parent > 0 && p.Active == true), "PhaseId", "PhaseName", application.PhaseId);
                    ViewData["message"] = "الطالب مسجل في في هذه المرحلة";
                    return View(application);
                }

            }
            return View();
        }

        // Lecture 
        public async Task<IActionResult> Lectures(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["id"] = id;

            var user = await _context.Users.FindAsync(id);
            if (user.UserTypeId != 7)
            {
                return RedirectToAction(nameof(Index));
            }
            string mssg = string.Empty;
            int departmentId = user.DepartmentId;
            ViewData["PhaseId"] = departmentId;
            if (departmentId == 0)
            {
                mssg = "  أختار سنة دراسية  ";
            }
            StudentsLecturesAndCountAndPhases LecturesAndPhases = new StudentsLecturesAndCountAndPhases();
            LecturesAndPhases.StudentApplications = await _context.StudentApplications
              .Where(a => a.StudentId == id)
              .Include(a => a.CPhases)
              .ToListAsync();
            //&& a.PhaseId == departmentId
            var app = await _context.StudentApplications
              .Where(a => a.StudentId == id && a.PhaseId == departmentId)
              .OrderByDescending(a => a.ApplicationId)
              .FirstOrDefaultAsync();



            int PhaseId = 0;

            if (app != null)
            {
                PhaseId = app.PhaseId;
                if (app.Paided == false)
                {
                    mssg = "لم يسدد الاستمارة";
                }
            }
            else
            {
                mssg = "لم يشترك في سنة دراسية";
            }



            ViewData["mssg"] = mssg;
            ViewData["FullName"] = user.FullName;
            ViewData["date"] = DateTime.Today;
            // var customerIds = deserializedCustomers.Select(x => x.CustomerID).ToList();
            // var customers = context.Customers.Where(x => customerIds.Contains(x.CustomerID)).ToList();

            var studentlectures = await _context.StudentLecture
                .Where(s => s.StudentID == id && s.LectureType == 0)
                .Select(s => s.scheduleId)
                .ToListAsync();

            LecturesAndPhases.StudentsLecturesAndCount = await _context.StudentsLecturesAndCount
                .Where(f => f.LectureDate == DateTime.Today && f.PhaseID == PhaseId && f.Closed == false)
                .Where(f => !studentlectures.Contains(f.GroupscheduleId))
                .ToListAsync();
            return View(LecturesAndPhases);
        }

        // Lecture By Date
        public async Task<IActionResult> LecturesByDate([Bind("id,date")] IdDateViewModel model)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["id"] = model.id;
            ViewData["date"] = model.date;

            var user = await _context.Users.FindAsync(model.id);
            if (user.UserTypeId != 7)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["FullName"] = user.FullName;
            string mssg = string.Empty;
            int departmentId = user.DepartmentId;
            ViewData["PhaseId"] = departmentId;
            if (departmentId == 0)
            {
                mssg = "  أختار سنة دراسية  ";
            }
            StudentsLecturesAndCountAndPhases LecturesAndPhases = new StudentsLecturesAndCountAndPhases();
            LecturesAndPhases.StudentApplications = await _context.StudentApplications
              .Where(a => a.StudentId == model.id)
              .Include(a => a.CPhases)
              .ToListAsync();
            //&& a.PhaseId == departmentId
            var app = await _context.StudentApplications
              .Where(a => a.StudentId == model.id && a.PhaseId == departmentId)
              .OrderByDescending(a => a.ApplicationId)
              .FirstOrDefaultAsync();


            int PhaseId = 0;
            if (app != null)
            {
                PhaseId = app.PhaseId;
                if (app.Paided == false)
                {
                    mssg = "لم يسدد الاستمارة";
                }
            }
            else
            {
                mssg = "لم يشترك في سنة دراسية";
            }
            ViewData["mssg"] = mssg;
            var studentlectures = await _context.StudentLecture
              .Where(s => s.StudentID == model.id && s.LectureType == 0)
              .Select(s => s.scheduleId)
              .ToListAsync();

            LecturesAndPhases.StudentsLecturesAndCount = await _context.StudentsLecturesAndCount
                .Where(f => f.LectureDate == model.date && f.PhaseID == PhaseId && f.Closed == false)
                .Where(f => !studentlectures.Contains(f.GroupscheduleId))
                .ToListAsync();
            return View(LecturesAndPhases);
        }

        // Lecture By Word
        public async Task<IActionResult> LecturesByWord([Bind("id,word")] IdWordViewModel model)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["id"] = model.id;

            var user = await _context.Users.FindAsync(model.id);
            if (user.UserTypeId != 7)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["FullName"] = user.FullName;
            string mssg = string.Empty;
            int departmentId = user.DepartmentId;
            ViewData["PhaseId"] = departmentId;
            if (departmentId == 0)
            {
                mssg = "  أختار سنة دراسية  ";
            }
            StudentsLecturesAndCountAndPhases LecturesAndPhases = new StudentsLecturesAndCountAndPhases();
            LecturesAndPhases.StudentApplications = await _context.StudentApplications
              .Where(a => a.StudentId == model.id)
              .Include(a => a.CPhases)
              .ToListAsync();
            //&& a.PhaseId == departmentId
            var app = await _context.StudentApplications
              .Where(a => a.StudentId == model.id && a.PhaseId == departmentId)
              .OrderByDescending(a => a.ApplicationId)
              .FirstOrDefaultAsync();

            int PhaseId = 0;
            if (app != null)
            {
                PhaseId = app.PhaseId;
                if (app.Paided == false)
                {
                    mssg = "لم يسدد الاستمارة";
                }
            }
            else
            {
                mssg = "لم يشترك في سنة دراسية";
            }
            ViewData["mssg"] = mssg;

            var studentlectures = await _context.StudentLecture
           .Where(s => s.StudentID == model.id && s.LectureType == 0)
           .Select(s => s.scheduleId)
           .ToListAsync();

            LecturesAndPhases.StudentsLecturesAndCount = await _context.StudentsLecturesAndCount
                .Where(f => f.LectureDate == DateTime.Today && f.GroupName.Contains(model.word) && f.PhaseID == PhaseId && f.Closed == false)
                .Where(f => !studentlectures.Contains(f.GroupscheduleId))
                .ToListAsync();
            return View(LecturesAndPhases);
        }

        // Lecture By Date and word
        public async Task<IActionResult> LecturesByDateAndWord([Bind("id,word,date")] IdWordDateViewModel model)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["id"] = model.id;
            ViewData["date"] = model.date;
            ViewData["word"] = model.word;

            var user = await _context.Users.FindAsync(model.id);
            if (user.UserTypeId != 7)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["FullName"] = user.FullName;
            string mssg = string.Empty;
            int departmentId = user.DepartmentId;
            ViewData["PhaseId"] = departmentId;
            if (departmentId == 0)
            {
                mssg = "  أختار سنة دراسية  ";
            }
            StudentsLecturesAndCountAndPhases LecturesAndPhases = new StudentsLecturesAndCountAndPhases();
            LecturesAndPhases.StudentApplications = await _context.StudentApplications
              .Where(a => a.StudentId == model.id)
              .Include(a => a.CPhases)
              .ToListAsync();
            //&& a.PhaseId == departmentId
            var app = await _context.StudentApplications
              .Where(a => a.StudentId == model.id && a.PhaseId == departmentId)
              .OrderByDescending(a => a.ApplicationId)
              .FirstOrDefaultAsync();
            int PhaseId = 0;
            if (app != null)
            {
                PhaseId = app.PhaseId;
                if (app.Paided == false)
                {
                    mssg = "لم يسدد الاستمارة";
                }
            }
            else
            {
                mssg = "لم يشترك في سنة دراسية";
            }
            ViewData["mssg"] = mssg;

            var studentlectures = await _context.StudentLecture
           .Where(s => s.StudentID == model.id && s.LectureType == 0)
           .Select(s => s.scheduleId)
           .ToListAsync();

            LecturesAndPhases.StudentsLecturesAndCount = await _context.StudentsLecturesAndCount
                .Where(f => f.LectureDate == model.date && f.GroupName.Contains(model.word) && f.PhaseID == PhaseId && f.Closed == false)
                .Where(f => !studentlectures.Contains(f.GroupscheduleId))
                .ToListAsync();
            return View(LecturesAndPhases);
        }


        // Reviews 
        public async Task<IActionResult> Reviews(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["id"] = id;

            var user = await _context.Users.FindAsync(id);
            if (user.UserTypeId != 7)
            {
                return RedirectToAction(nameof(Index));
            }
            string mssg = string.Empty;
            int departmentId = user.DepartmentId;
            ViewData["PhaseId"] = departmentId;
            if (departmentId == 0)
            {
                mssg = "  أختار سنة دراسية  ";
            }
            ReviewsScheduleAndCountViewModel LecturesAndPhases = new ReviewsScheduleAndCountViewModel();
            LecturesAndPhases.StudentApplications = await _context.StudentApplications
              .Where(a => a.StudentId == id)
              .Include(a => a.CPhases)
              .ToListAsync();
            //&& a.PhaseId == departmentId
            var app = await _context.StudentApplications
              .Where(a => a.StudentId == id && a.PhaseId == departmentId)
              .OrderByDescending(a => a.ApplicationId)
              .FirstOrDefaultAsync();
            int PhaseId = 0;
            if (app != null)
            {
                PhaseId = app.PhaseId;
                if (app.Paided == false)
                {
                    mssg = "لم يسدد الاستمارة";
                }
            }
            else
            {
                mssg = "لم يشترك في سنة دراسية";
            }
            ViewData["FullName"] = user.FullName;
            ViewData["date"] = DateTime.Today;
            //.Where(f => f.ReviewDate == DateTime.Today && f.ReviewTypeId == 1 )  
            var studentlectures = await _context.StudentLecture
                .Where(s => s.StudentID == id && s.LectureType == 1)
                .Select(s => s.scheduleId)
                .ToListAsync();

            LecturesAndPhases.ReviewsSchedule = await _context.ReviewsSchedule
                .Where(f => f.ReviewDate == DateTime.Today && f.ReviewTypeId == 1 && f.CSyllabus.PhaseID == PhaseId && f.Closed == false)
                .Where(f => !studentlectures.Contains(f.ReviewsScheduleId))
                .Include(f => f.CSyllabus)
                .Include(f => f.CRooms)
                .ToListAsync();

            //var lectures = await _context.StudentLectureReviwCount
            return View(LecturesAndPhases);
        }

        // Reviews By Date 
        public async Task<IActionResult> ReviewsByDate(int id, DateTime date)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["id"] = id;

            var user = await _context.Users.FindAsync(id);
            if (user.UserTypeId != 7)
            {
                return RedirectToAction(nameof(Index));
            }
            string mssg = string.Empty;
            int departmentId = user.DepartmentId;
            ViewData["PhaseId"] = departmentId;
            if (departmentId == 0)
            {
                mssg = "  أختار سنة دراسية  ";
            }
            ReviewsScheduleAndCountViewModel LecturesAndPhases = new ReviewsScheduleAndCountViewModel();
            LecturesAndPhases.StudentApplications = await _context.StudentApplications
              .Where(a => a.StudentId == id)
              .Include(a => a.CPhases)
              .ToListAsync();
            //&& a.PhaseId == departmentId
            var app = await _context.StudentApplications
              .Where(a => a.StudentId == id && a.PhaseId == departmentId)
              .OrderByDescending(a => a.ApplicationId)
              .FirstOrDefaultAsync();
            int PhaseId = 0;
            if (app != null)
            {
                PhaseId = app.PhaseId;
                if (app.Paided == false)
                {
                    mssg = "لم يسدد الاستمارة";
                }
            }
            else
            {
                mssg = "لم يشترك في سنة دراسية";
            }
            ViewData["FullName"] = user.FullName;
            var studentlectures = await _context.StudentLecture
               .Where(s => s.StudentID == id && s.LectureType == 1)
               .Select(s => s.scheduleId)
               .ToListAsync();

            LecturesAndPhases.ReviewsSchedule = await _context.ReviewsSchedule
                .Where(f => f.ReviewDate == date && f.ReviewTypeId == 1 && f.CSyllabus.PhaseID == PhaseId && f.Closed == false)
                .Where(f => !studentlectures.Contains(f.ReviewsScheduleId))
                .Include(f => f.CSyllabus)
                .Include(f => f.CRooms)
                .ToListAsync();
            return View(LecturesAndPhases);
        }

        // Reviews 
        public async Task<IActionResult> Exams(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["id"] = id;

            var user = await _context.Users.FindAsync(id);
            if (user.UserTypeId != 7)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["FullName"] = user.FullName;
            string mssg = string.Empty;
            int departmentId = user.DepartmentId;
            ViewData["PhaseId"] = departmentId;
            if (departmentId == 0)
            {
                mssg = "  أختار سنة دراسية  ";
            }
            ReviewsScheduleAndCountViewModel LecturesAndPhases = new ReviewsScheduleAndCountViewModel();
            LecturesAndPhases.StudentApplications = await _context.StudentApplications
              .Where(a => a.StudentId == id)
              .Include(a => a.CPhases)
              .ToListAsync();
            //&& a.PhaseId == departmentId
            var app = await _context.StudentApplications
              .Where(a => a.StudentId == id && a.PhaseId == departmentId)
              .OrderByDescending(a => a.ApplicationId)
              .FirstOrDefaultAsync();
            int PhaseId = 0;
            if (app != null)
            {
                PhaseId = app.PhaseId;
                if (app.Paided == false)
                {
                    mssg = "لم يسدد الاستمارة";
                }
            }
            else
            {
                mssg = "لم يشترك في سنة دراسية";
            }
            var studentlectures = await _context.StudentLecture
             .Where(s => s.StudentID == id && s.LectureType == 2)
             .Select(s => s.scheduleId)
             .ToListAsync();

            LecturesAndPhases.ReviewsSchedule = await _context.ReviewsSchedule
                .Where(f => f.ReviewDate == DateTime.Today && f.ReviewTypeId == 2 && f.CSyllabus.PhaseID == PhaseId && f.Closed == false)
                 .Where(f => !studentlectures.Contains(f.ReviewsScheduleId))
                .Include(f => f.CSyllabus)
                .Include(f => f.CRooms)
                .ToListAsync();
            return View(LecturesAndPhases);
        }

        // Reviews By Date 
        public async Task<IActionResult> ExamsByDate(int id, DateTime date)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["id"] = id;

            var user = await _context.Users.FindAsync(id);
            if (user.UserTypeId != 7)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["FullName"] = user.FullName;
            string mssg = string.Empty;
            int departmentId = user.DepartmentId;
            ViewData["PhaseId"] = departmentId;
            if (departmentId == 0)
            {
                mssg = "  أختار سنة دراسية  ";
            }
            ReviewsScheduleAndCountViewModel LecturesAndPhases = new ReviewsScheduleAndCountViewModel();
            LecturesAndPhases.StudentApplications = await _context.StudentApplications
              .Where(a => a.StudentId == id)
              .Include(a => a.CPhases)
              .ToListAsync();
            //&& a.PhaseId == departmentId
            var app = await _context.StudentApplications
              .Where(a => a.StudentId == id && a.PhaseId == departmentId)
              .OrderByDescending(a => a.ApplicationId)
              .FirstOrDefaultAsync();
            int PhaseId = 0;
            if (app != null)
            {
                PhaseId = app.PhaseId;
                if (app.Paided == false)
                {
                    mssg = "لم يسدد الاستمارة";
                }
            }
            else
            {
                mssg = "لم يشترك في سنة دراسية";
            }
            var studentlectures = await _context.StudentLecture
           .Where(s => s.StudentID == id && s.LectureType == 2)
           .Select(s => s.scheduleId)
           .ToListAsync();

            LecturesAndPhases.ReviewsSchedule = await _context.ReviewsSchedule
                .Where(f => f.ReviewDate == date && f.ReviewTypeId == 2 && f.CSyllabus.PhaseID == PhaseId && f.Closed == false)
                .Where(f => !studentlectures.Contains(f.ReviewsScheduleId))
                .Include(f => f.CSyllabus)
                .Include(f => f.CRooms)
                .ToListAsync();
            return View(LecturesAndPhases);
        }


        // ChangePhaseViewModel

        [HttpPost]
        public async Task<IActionResult> ChangePhase(ChangePhaseViewModel model)
        {

            var user = await _context.Users.FindAsync(model.StudentId);
            if (user.UserTypeId != 7)
            {
                return RedirectToAction(nameof(Index));
            }


            user.DepartmentId = model.PhaseId;
            _context.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToRoute(new { action = model.action, id = model.StudentId });
        }

        /// Discount - AddDiscount - DeleteDiscount
        public async Task<IActionResult> Discount(int id)
        {


            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var student = await _context.Users.FindAsync(id);
            if (student.UserTypeId != 7)
            {
                return RedirectToAction(nameof(Index));
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            bool Discountisaccess = await Isaccessright(user.Id, 7);
            if (Discountisaccess)
            {
                ViewData["Discount"] = "true";
            }
            else
            {
                ViewData["Discount"] = "false";
            }

            ViewData["FullName"] = student.FullName;
            FinancialAccount financialAccount = await _context.FinancialAccount.FindAsync(student.AccountID);

            ViewData["id"] = id;
            ViewData["StudentId"] = student.AccountID;
            ViewData["physicalyearId"] = new SelectList(_context.PhysicalYear.Where(p => p.Active == true), "PhysicalyearId", "PhysicalyearName");
            ViewData["TeacherId"] = new SelectList(_context.Users.Where(s => s.UserTypeId == 5), "Id", "FullName");
            var Discount = await _context.StudentDiscount
                .Where(g => g.StudentId == student.AccountID)
                .Include(t => t.Teacher)
                .ToListAsync();
            return View(Discount);
        }

        // StudentsDiscount
        public async Task<IActionResult> StudentsDiscount(int? page)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            int pagesize = 25;
            //  int? takepage = 0;
            List<StudentDiscount> Discount = new List<StudentDiscount>();
            if (page == null || page == 1 || page == 0)
            {
                Discount = await _context.StudentDiscount
                            .Include(t => t.FinancialAccount)
                            .Include(t => t.Teacher)
                            .OrderByDescending(t => t.StudentDiscountId)
                            .Take(pagesize)
                            .ToListAsync();
                ViewData["nxtpag"] = 2;
                ViewData["prvpag"] = "0";
                ViewData["page"] = 1;

            }
            else if (page > 1)   // 4 <= 30
            {
                // 4*50= 200  - (4-1) 3*50 =150
                // takepage = (page * 50) - ((page - 1) * 50);

                Discount = await _context.StudentDiscount
                              .Include(t => t.FinancialAccount)
                             .Include(t => t.Teacher)
                             .OrderByDescending(t => t.StudentDiscountId)
                             .Skip((int)(page - 1) * pagesize)
                             .Take(pagesize)
                             .ToListAsync();
                ViewData["nxtpag"] = page + 1;
                ViewData["prvpag"] = page - 1;
                ViewData["page"] = page;

            }
            return View(Discount);
        }

        [HttpPost]
        public async Task<IActionResult> AddDiscount([Bind("LectureType,StudentId,TeacherId,CenterDiscount,teacherDiscount,physicalyearId,Notes")] StudentDiscount studentDiscount)
        {
            var student = await _context.Users.Where(u => u.AccountID == studentDiscount.StudentId).FirstAsync();
            int id = student.Id;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            bool active = true;
            if (studentDiscount.CenterDiscount != 0)
            {
                active = false;
            }
            if (ModelState.IsValid)
            {
                studentDiscount.Active = active;
                studentDiscount.CreateId = uid;
                studentDiscount.UpdateId = 0;
                studentDiscount.CreatedDate = DateTime.Now;
                studentDiscount.Discount = studentDiscount.CenterDiscount + studentDiscount.teacherDiscount;
                _context.Add(studentDiscount);
                await _context.SaveChangesAsync();
                return RedirectToRoute(new { action = "Discount", id = id });
            }
            return RedirectToRoute(new { action = "Discount", id = id });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            var discount = _context.StudentDiscount.Find(id);
            int AccountId = discount.StudentId;
            var student = await _context.Users.Where(s => s.AccountID == AccountId).FirstOrDefaultAsync();
            int studentid = student.Id;
            _context.Remove(discount);
            await _context.SaveChangesAsync();
            return RedirectToRoute(new { controller = "Students", action = "Discount", id = studentid });
        }


        // GroupInsertPrint - ReviewsInsertPrint - ExamsInsertPrint - PrintCopon
        public async Task<IActionResult> GroupInsertPrint(int id, int GroupscheduleId)
        {

            //  162 ? GroupscheduleId = 1089
            ViewData["id"] = id;
            ViewData["url"] = "/scp/Students/Details/" + id.ToString();
            //var Lecture = await _context.StudentLecture
            //     .Where(d => d.scheduleId == GroupscheduleId && d.StudentID == id && d.LectureType == 0)
            //       .FirstOrDefaultAsync();
            //if (Lecture != null)
            //{
            //    return RedirectToRoute(new { controller = "Students", action = "PrintCopon", area = "scp", id = Lecture.StudentLectureId });
            //}


            var groupSchedule = await _context.CGroupSchedule.FindAsync(GroupscheduleId);
            var group = await _context.CGroups.FindAsync(groupSchedule.GroupId);
            var room = await _context.CRooms.FindAsync(group.RoomId);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var user = await _userManager.FindByIdAsync(userId);
            if (user.online == 0)
            {
                return RedirectToRoute(new { area = "scp", controller = "Branch", action = "LogInBranch" });
            }
            //  int treasuryID = user.AccountID;
            var student = await _userManager.FindByIdAsync(id.ToString());
            int accountID = student.AccountID;
            var Pending = await _context.StudentBalancePendingViewModel
              .Where(p => p.AccountID == accountID && p.GroupId == group.GroupId)
               .FirstOrDefaultAsync();
            var studentDiscount = await _context.StudentDiscount
                .Where(d => d.TeacherId == group.TeacherId && d.StudentId == accountID && d.LectureType == 0 && d.physicalyearId == group.physicalyearId && d.Active == true)
                .FirstOrDefaultAsync();
            decimal iDiscount = 0;
            decimal iCenterDiscount = 0;
            decimal iteacherDiscount = 0;
            if (studentDiscount != null)
            {
                iDiscount = studentDiscount.Discount;
                iteacherDiscount = studentDiscount.teacherDiscount;
                iCenterDiscount = studentDiscount.CenterDiscount;
            }
            var balance = await _context.UsersBalance.FindAsync(accountID);
            decimal BalanceValue = balance.Balance * -1;
            decimal DocumentValue = group.LecturePrice - iDiscount;
            //if (DocumentValue > BalanceValue)
            //{
            //    return RedirectToRoute(new { controller = "Students", action = "Details", area = "scp", id = id, messageid = 4, charched = DocumentValue });
            //}

            if (Pending == null)
            {
                if (DocumentValue > BalanceValue)
                {
                    return RedirectToRoute(new { controller = "Students", action = "Details", area = "scp", id = id, messageid = 4, charched = DocumentValue });
                }

                FinancialDocuments financialDocuments = new FinancialDocuments()
                {
                    JournalEntryDate = DateTime.Today,
                    Notes = group.GroupName + " - " + groupSchedule.LectureDate.ToString("MM-dd"),
                    TreasuryID = 0,
                    AccountID = accountID,
                    Value = DocumentValue,
                    MovementTypeId = 8,
                    physicalyearId = group.physicalyearId,
                    Active = true,
                    CreateId = uid,
                    CreatedDate = DateTime.Now,
                    GroupscheduleId = GroupscheduleId,
                    BranchId = user.online,
                };

                _context.Add(financialDocuments);
                await _context.SaveChangesAsync();

                // Document Line Group
                FinancialJournalEntryLine journalEntryLine1 = new FinancialJournalEntryLine()
                {
                    FinancialDocumentId = financialDocuments.FinancialDocumentId,
                    physicalyearId = financialDocuments.physicalyearId,
                    JournalEntryDate = financialDocuments.JournalEntryDate,
                    AccountID = financialDocuments.AccountID,
                    Debit = financialDocuments.Value,
                    Credit = 0,
                    CreateId = uid,
                    CreatedDate = DateTime.Now,
                    Active = true,
                    BranchId = user.online,
                };

                _context.Add(journalEntryLine1);
                await _context.SaveChangesAsync();

            }
            else if (Pending.balance == 0)
            {
                if (DocumentValue > BalanceValue)
                {
                    return RedirectToRoute(new { controller = "Students", action = "Details", area = "scp", id = id, messageid = 4, charched = DocumentValue });
                }

                FinancialDocuments financialDocuments = new FinancialDocuments()
                {
                    JournalEntryDate = DateTime.Today,
                    Notes = group.GroupName + " - " + groupSchedule.LectureDate.ToString("MM-dd"),
                    TreasuryID = 0,
                    AccountID = accountID,
                    Value = DocumentValue,
                    MovementTypeId = 8,
                    physicalyearId = group.physicalyearId,
                    Active = true,
                    CreateId = uid,
                    CreatedDate = DateTime.Now,
                    GroupscheduleId = GroupscheduleId,
                    BranchId = user.online,
                };

                _context.Add(financialDocuments);
                await _context.SaveChangesAsync();

                // Document Line Group
                FinancialJournalEntryLine journalEntryLine1 = new FinancialJournalEntryLine()
                {
                    FinancialDocumentId = financialDocuments.FinancialDocumentId,
                    physicalyearId = financialDocuments.physicalyearId,
                    JournalEntryDate = financialDocuments.JournalEntryDate,
                    AccountID = financialDocuments.AccountID,
                    Debit = financialDocuments.Value,
                    Credit = 0,
                    CreateId = uid,
                    CreatedDate = DateTime.Now,
                    Active = true,
                    BranchId = user.online,
                };

                _context.Add(journalEntryLine1);
                await _context.SaveChangesAsync();

            }
            else
            {
                if (DocumentValue > Pending.balance)
                {
                    return RedirectToRoute(new { controller = "Students", action = "Details", area = "scp", id = id, messageid = 4, charched = DocumentValue });
                }

                StudentBalancePending studentBalancePending = new StudentBalancePending()
                {
                    AccountID = accountID,
                    TeacherId = group.TeacherId,
                    Credit = 0,
                    GroupId = group.GroupId,
                    Debit = DocumentValue,
                };
                _context.Add(studentBalancePending);
                await _context.SaveChangesAsync();


                //FinancialDocuments financialDocuments = new FinancialDocuments()
                //{
                //    JournalEntryDate = DateTime.Today,
                //    Notes = group.GroupName + " - " + groupSchedule.LectureDate.ToString("MM-dd"),
                //    TreasuryID = 0,
                //    AccountID = accountID,
                //    Value = DocumentValue,
                //    MovementTypeId = 9,
                //    physicalyearId = group.physicalyearId,
                //    Active = true,
                //    CreateId = uid,
                //    CreatedDate = DateTime.Now,
                //};
                //_context.Add(financialDocuments);
                //await _context.SaveChangesAsync();

                //// Document Line Group
                //FinancialJournalEntryLine journalEntryLine1 = new FinancialJournalEntryLine()
                //{
                //    FinancialDocumentId = financialDocuments.FinancialDocumentId,
                //    physicalyearId = financialDocuments.physicalyearId,
                //    JournalEntryDate = financialDocuments.JournalEntryDate,
                //    AccountID = financialDocuments.AccountID,
                //    Debit = financialDocuments.Value,
                //    Credit = 0,
                //    CreateId = uid,
                //    CreatedDate = DateTime.Now,
                //    Active = true,
                //};

                //_context.Add(journalEntryLine1);
                //await _context.SaveChangesAsync();

            }

            var query = _context.StudentLecture
                .Where(x => x.scheduleId == groupSchedule.GroupscheduleId);

            var itemsexist = await query.AnyAsync();
            int maxorderindex = 0;

            if (itemsexist)
            {
                maxorderindex = await query.MaxAsync(x => x.StudentIndex);
            }

            //var index = await _context.StudentLecture
            //    .Where(l => l.scheduleId == groupSchedule.GroupscheduleId)
            //    .DefaultIfEmpty()
            //    .MaxAsync(l => l.StudentIndex );



            StudentLecture studentLecture = new StudentLecture()
            {
                scheduleId = groupSchedule.GroupscheduleId,
                LectureType = 0,
                LectureDate = groupSchedule.LectureDate,
                ReservationDate = DateTime.Today.Date,
                StudentID = id,
                StudentIndex = maxorderindex + 1,
                Printed = 0,
                StudentType = 0,
                CenterFee = group.CenterFee,
                ServiceFee = group.ServiceFee,
                TeacherFee = group.TeacherFee,
                LecturePrice = group.LecturePrice,
                Discount = iDiscount,
                CenterDiscount = iCenterDiscount,
                teacherDiscount = iteacherDiscount,
                CenterValue = group.CenterFee - iCenterDiscount,
                TeacherValue = group.TeacherFee - iteacherDiscount,
                Paided = group.LecturePrice - iDiscount,
                CreateId = uid,
                CreatedDate = DateTime.UtcNow,
                Active = true,
                BranchId = user.online,
            };
            _context.Add(studentLecture);
            await _context.SaveChangesAsync();

            return RedirectToRoute(new { controller = "Students", action = "PrintCopon", area = "scp", id = studentLecture.StudentLectureId });

        }
        public async Task<IActionResult> ReviewsInsertPrint(int id, int ReviewsScheduleId)
        {
            ViewData["id"] = id;
            //var Lecture = await _context.StudentLecture
            //     .Where(d => d.scheduleId == ReviewsScheduleId && d.StudentID == id && d.LectureType == 1)
            //       .FirstOrDefaultAsync();
            //if (Lecture != null)
            //{
            //    return RedirectToRoute(new { controller = "Students", action = "PrintCopon", area = "scp", id = Lecture.StudentLectureId });
            //}
            var reviewSchedule = await _context.ReviewsSchedule.FindAsync(ReviewsScheduleId);
            var room = await _context.CRooms.FindAsync(reviewSchedule.RoomId);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var user = await _userManager.FindByIdAsync(userId);
            if (user.online == 0)
            {
                return RedirectToRoute(new { area = "scp", controller = "Branch", action = "LogInBranch" });
            }
            int treasuryID = user.AccountID;
            var student = await _userManager.FindByIdAsync(id.ToString());
            int accountID = student.AccountID;
            var studentDiscount = await _context.StudentDiscount
                .Where(d => d.TeacherId == reviewSchedule.TeacherId && d.StudentId == accountID && d.LectureType == 1 && d.physicalyearId == reviewSchedule.physicalyearId && d.Active == true)
                .FirstOrDefaultAsync();
            decimal iDiscount = 0;
            decimal iCenterDiscount = 0;
            decimal iteacherDiscount = 0;
            if (studentDiscount != null)
            {
                iDiscount = studentDiscount.Discount;
                iteacherDiscount = studentDiscount.teacherDiscount;
                iCenterDiscount = studentDiscount.CenterDiscount;
            }
            var balance = await _context.UsersBalance.FindAsync(accountID);
            decimal BalanceValue = balance.Balance * -1;
            decimal DocumentValue = reviewSchedule.LecturePrice - iDiscount;
            if (DocumentValue > BalanceValue)
            {
                return RedirectToRoute(new { controller = "Students", action = "Details", area = "scp", id = id, messageid = 4, charched = DocumentValue });
            }
            FinancialDocuments financialDocuments = new FinancialDocuments()
            {
                JournalEntryDate = DateTime.Today,
                Notes = reviewSchedule.ReviewsScheduleName + " - " + reviewSchedule.ReviewDate.ToString("MM-dd"),
                TreasuryID = 0,
                AccountID = accountID,
                Value = reviewSchedule.LecturePrice - iDiscount,
                MovementTypeId = 9,
                physicalyearId = reviewSchedule.physicalyearId,
                Active = true,
                CreateId = uid,
                CreatedDate = DateTime.Now,
                GroupscheduleId = ReviewsScheduleId,
                BranchId = user.online,
            };

            _context.Add(financialDocuments);
            await _context.SaveChangesAsync();
            FinancialJournalEntryLine journalEntryLine1 = new FinancialJournalEntryLine()
            {
                FinancialDocumentId = financialDocuments.FinancialDocumentId,
                physicalyearId = financialDocuments.physicalyearId,
                JournalEntryDate = financialDocuments.JournalEntryDate,
                AccountID = financialDocuments.AccountID,
                Debit = financialDocuments.Value,
                Credit = 0,
                CreateId = uid,
                CreatedDate = DateTime.Now,
                Active = true,
                BranchId = user.online,
            };

            _context.Add(journalEntryLine1);
            await _context.SaveChangesAsync();

            var query = _context.StudentLecture
               .Where(x => x.scheduleId == reviewSchedule.ReviewsScheduleId);

            var itemsexist = await query.AnyAsync();
            int maxorderindex = 0;

            if (itemsexist)
            {
                maxorderindex = await query.MaxAsync(x => x.StudentIndex);
            }


            StudentLecture studentLecture = new StudentLecture()
            {
                scheduleId = reviewSchedule.ReviewsScheduleId,
                LectureType = reviewSchedule.ReviewTypeId,
                LectureDate = reviewSchedule.ReviewDate,
                ReservationDate = DateTime.Today.Date,
                StudentID = id,
                StudentIndex = maxorderindex + 1,
                Printed = 0,
                StudentType = 0,
                CenterFee = reviewSchedule.CenterFee,
                ServiceFee = reviewSchedule.ServiceFee,
                TeacherFee = reviewSchedule.TeacherFee,
                LecturePrice = reviewSchedule.LecturePrice,
                Discount = iDiscount,
                CenterDiscount = iCenterDiscount,
                teacherDiscount = iteacherDiscount,
                CenterValue = reviewSchedule.CenterFee - iCenterDiscount,
                TeacherValue = reviewSchedule.TeacherFee - iteacherDiscount,
                Paided = financialDocuments.Value,
                CreateId = uid,
                CreatedDate = DateTime.Now,
                Active = true,
                BranchId = user.online,
            };
            _context.Add(studentLecture);
            await _context.SaveChangesAsync();


            return RedirectToRoute(new { controller = "Students", action = "PrintCopon", area = "scp", id = studentLecture.StudentLectureId });

        }
        public async Task<IActionResult> ExamsInsertPrint(int id, int ReviewsScheduleId)
        {
            ViewData["id"] = id;
            //var Lecture = await _context.StudentLecture
            //     .Where(d => d.scheduleId == ReviewsScheduleId && d.StudentID == id && d.LectureType == 2)
            //       .FirstOrDefaultAsync();
            //if (Lecture != null)
            //{
            //    return RedirectToRoute(new { controller = "Students", action = "PrintCopon", area = "scp", id = Lecture.StudentLectureId });
            //}
            var reviewSchedule = await _context.ReviewsSchedule.FindAsync(ReviewsScheduleId);
            var room = await _context.CRooms.FindAsync(reviewSchedule.RoomId);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);
            var user = await _userManager.FindByIdAsync(userId);
            if (user.online == 0)
            {
                return RedirectToRoute(new { area = "scp", controller = "Branch", action = "LogInBranch" });
            }
            int treasuryID = user.AccountID;
            var student = await _userManager.FindByIdAsync(id.ToString());
            int accountID = student.AccountID;
            var studentDiscount = await _context.StudentDiscount
                .Where(d => d.TeacherId == reviewSchedule.TeacherId && d.StudentId == accountID && d.LectureType == 2 && d.physicalyearId == reviewSchedule.physicalyearId && d.Active == true)
                .FirstOrDefaultAsync();
            decimal iDiscount = 0;
            decimal iCenterDiscount = 0;
            decimal iteacherDiscount = 0;
            if (studentDiscount != null)
            {
                iDiscount = studentDiscount.Discount;
                iteacherDiscount = studentDiscount.teacherDiscount;
                iCenterDiscount = studentDiscount.CenterDiscount;
            }
            var balance = await _context.UsersBalance.FindAsync(accountID);
            decimal BalanceValue = balance.Balance * -1;
            decimal DocumentValue = reviewSchedule.LecturePrice - iDiscount;
            if (DocumentValue > BalanceValue)
            {
                return RedirectToRoute(new { controller = "Students", action = "Details", area = "scp", id = id, messageid = 4, charched = DocumentValue });
            }
            FinancialDocuments financialDocuments = new FinancialDocuments()
            {
                JournalEntryDate = DateTime.Today,
                Notes = reviewSchedule.ReviewsScheduleName + " - " + reviewSchedule.ReviewDate.ToString("MM-dd"),
                TreasuryID = 0,
                AccountID = accountID,
                Value = reviewSchedule.LecturePrice - iDiscount,
                MovementTypeId = 10,
                physicalyearId = reviewSchedule.physicalyearId,
                Active = true,
                CreateId = uid,
                CreatedDate = DateTime.Now,
                GroupscheduleId = ReviewsScheduleId,
                BranchId = user.online,
            };

            _context.Add(financialDocuments);
            await _context.SaveChangesAsync();
            FinancialJournalEntryLine journalEntryLine1 = new FinancialJournalEntryLine()
            {
                FinancialDocumentId = financialDocuments.FinancialDocumentId,
                physicalyearId = financialDocuments.physicalyearId,
                JournalEntryDate = financialDocuments.JournalEntryDate,
                AccountID = financialDocuments.AccountID,
                Debit = financialDocuments.Value,
                Credit = 0,
                CreateId = uid,
                CreatedDate = DateTime.Now,
                Active = true,
                BranchId = user.online,
            };

            _context.Add(journalEntryLine1);
            await _context.SaveChangesAsync();

            var query = _context.StudentLecture
              .Where(x => x.scheduleId == reviewSchedule.ReviewsScheduleId);

            var itemsexist = await query.AnyAsync();
            int maxorderindex = 0;

            if (itemsexist)
            {
                maxorderindex = await query.MaxAsync(x => x.StudentIndex);
            }

            StudentLecture studentLecture = new StudentLecture()
            {
                scheduleId = reviewSchedule.ReviewsScheduleId,
                LectureType = reviewSchedule.ReviewTypeId,
                LectureDate = reviewSchedule.ReviewDate,
                ReservationDate = DateTime.Today.Date,
                StudentID = id,
                StudentIndex = maxorderindex + 1,
                Printed = 0,
                StudentType = 0,
                CenterFee = reviewSchedule.CenterFee,
                ServiceFee = reviewSchedule.ServiceFee,
                TeacherFee = reviewSchedule.TeacherFee,
                LecturePrice = reviewSchedule.LecturePrice,
                Discount = iDiscount,
                CenterDiscount = iCenterDiscount,
                teacherDiscount = iteacherDiscount,
                CenterValue = reviewSchedule.CenterFee - iCenterDiscount,
                TeacherValue = reviewSchedule.TeacherFee - iteacherDiscount,
                Paided = financialDocuments.Value,
                CreateId = uid,
                CreatedDate = DateTime.Now,
                Active = true,
                BranchId = user.online,

            };
            _context.Add(studentLecture);
            await _context.SaveChangesAsync();


            return RedirectToRoute(new { controller = "Students", action = "PrintCopon", area = "scp", id = studentLecture.StudentLectureId });

        }
        public async Task<IActionResult> PrintCopon(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            var model = await _context.StudentLecture
                .Where(f => f.StudentLectureId == id)
                .FirstOrDefaultAsync();
            model.Printed += 1;
            model.UpdateId = user.Id;
            model.UpdatedDate = DateTime.Now;
            var student = await _context.Users.FindAsync(model.StudentID);
            ViewData["id"] = student.Id;
            ViewData["StudentName"] = student.FullName;
            ViewData["BarCode"] = "*" + model.StudentLectureId + "*";
            if (model.LectureType == 0)
            {
                var groupSchedule = await _context.CGroupSchedule.Where(l => l.GroupscheduleId == model.scheduleId)
                .Include(l => l.CGroups)
                .Include(l => l.CGroups.CRooms)
                 .Include(l => l.CGroups.CSyllabus)
                   .Include(l => l.CGroups.CSyllabus.CPhases)
                .FirstOrDefaultAsync();

                ViewData["GropNam"] = groupSchedule.CGroups.GroupName;
                ViewData["roomid"] = groupSchedule.CGroups.CRooms.RoomName;
                ViewData["LectNo"] = groupSchedule.LectureNo;
                ViewData["lbldate"] = groupSchedule.LectureDate.ToString("yyyy-MM-dd");
                ViewData["time"] = groupSchedule.CGroups.SessionStart.ToString("T") + " - " + groupSchedule.CGroups.SessionEnd.ToString("T");
                ViewData["CS"] = groupSchedule.CGroups.CSyllabus.CPhases.PhaseName;
            }
            else
            {
                var reviewsSchedule = await _context.ReviewsSchedule.Where(l => l.ReviewsScheduleId == model.scheduleId)
                .Include(l => l.CRooms)
                  .Include(l => l.CSyllabus)
                   .Include(l => l.CSyllabus.CPhases)
                .FirstOrDefaultAsync();

                ViewData["GropNam"] = reviewsSchedule.ReviewsScheduleName;
                ViewData["roomid"] = reviewsSchedule.CRooms.RoomName;
                ViewData["lbldate"] = reviewsSchedule.ReviewDate.ToString("yyyy-MM-dd");
                ViewData["time"] = reviewsSchedule.SessionStart.ToString("T") + " - " + reviewsSchedule.SessionEnd.ToString("T");
                ViewData["CS"] = reviewsSchedule.CSyllabus.CPhases.PhaseName;
            }


            _context.Update(model);
            await _context.SaveChangesAsync();

            ViewData["StudentIndex"] = model.StudentIndex;

            if (model.Discount > 0)
            {
                ViewData["disc"] = "*";
            }

            ViewData["printchk"] = model.Printed;
            return View(model);
        }


        public async Task createFinancialJournalEntryLine(FinancialDocuments financialDocuments)
        {

            FinancialJournalEntryLine journalEntryLine1 = new FinancialJournalEntryLine()
            {
                FinancialDocumentId = financialDocuments.FinancialDocumentId,
                physicalyearId = financialDocuments.physicalyearId,
                JournalEntryDate = financialDocuments.JournalEntryDate,
                Active = true,
                BranchId = financialDocuments.BranchId,
            };

            if (financialDocuments.MovementTypeId == 2)
            {
                journalEntryLine1.AccountID = financialDocuments.AccountID;
                journalEntryLine1.Debit = financialDocuments.Value;
                journalEntryLine1.Credit = 0;
            }
            else if (financialDocuments.MovementTypeId == 3)
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
                Active = true,
                BranchId = financialDocuments.BranchId,
            };

            if (financialDocuments.MovementTypeId == 2)
            {
                journalEntryLine2.AccountID = financialDocuments.TreasuryID;
                journalEntryLine2.Debit = 0;
                journalEntryLine2.Credit = financialDocuments.Value;
            }
            else if (financialDocuments.MovementTypeId == 3)
            {
                journalEntryLine2.AccountID = financialDocuments.AccountID;
                journalEntryLine2.Debit = financialDocuments.Value;
                journalEntryLine2.Credit = 0;
            }

            _context.Add(journalEntryLine2);
            await _context.SaveChangesAsync();

        }

        // ReplyApplication - DeleteApplication - DeleteGroup 
        public async Task<IActionResult> ReplyApplication(int id)
        {
            ViewData["id"] = id;

            ApplicationUser student = await _userManager.FindByIdAsync(id.ToString());
            if (student.UserTypeId != 7)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserName"] = student.FristName + " " + student.SecondName;
            var balance = await _context.UsersBalance.FindAsync(student.AccountID);
            ViewData["balance"] = balance.Balance * -1;
            ViewData["FullName"] = student.FullName;
            var applications = await _context.StudentApplications
                .Where(p => p.StudentId == id)
                .Include(p => p.CPhases)
                .Include(p => p.PhysicalYear)
                .ToListAsync();
            return View(applications);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            var application = _context.StudentApplications.Find(id);
            int studentid = application.StudentId;
            _context.Remove(application);
            await _context.SaveChangesAsync();
            return RedirectToRoute(new { controller = "Students", action = "ReplyApplication", id = studentid });
        }


        //  ReplyGroup - ReplyLecture - ReplyExam - ReplyReview - DeleteLecture
        public async Task<IActionResult> ReplyGroup(int id)
        {
            ViewData["id"] = id;

            ApplicationUser student = await _userManager.FindByIdAsync(id.ToString());
            if (student.UserTypeId != 7)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserName"] = student.FristName + " " + student.SecondName;
            var balance = await _context.UsersBalance.FindAsync(student.AccountID);
            ViewData["balance"] = balance.Balance * -1;
            ViewData["FullName"] = student.FullName;
            var Groups = await _context.StudentGroup
                .Where(p => p.StudentId == id)
                .Include(p => p.CGroups)
                .ToListAsync();
            return View(Groups);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            var studentGroup = await _context.StudentGroup.FindAsync(id);
            var group = await _context.CGroups.FindAsync(studentGroup.GroupId);
            var room = await _context.CRooms.FindAsync(group.RoomId);
            int techerid = group.TeacherId;
            int studentid = studentGroup.StudentId;

            var student = await _context.Users.FindAsync(studentid);
            int uid = Convert.ToInt32(userId);
            int accountID = student.AccountID;
            _context.Remove(studentGroup);
            await _context.SaveChangesAsync();

            var Pending = await _context.StudentBalancePendingViewModel
           .Where(p => p.AccountID == accountID && p.GroupId == group.GroupId)
            .FirstOrDefaultAsync();

            if (Pending != null)
            {
                if (Pending.balance > 0)
                {
                    StudentBalancePending studentBalancePending = new StudentBalancePending()
                    {
                        AccountID = accountID,
                        TeacherId = group.TeacherId,
                        Credit = 0,
                        GroupId = group.GroupId,
                        Debit = Pending.balance,
                    };
                    _context.Add(studentBalancePending);
                    await _context.SaveChangesAsync();

                    FinancialDocuments financialDocuments = new FinancialDocuments()
                    {
                        JournalEntryDate = System.DateTime.Today,
                        Notes = "أسترجاع مجموعة  - " + group.GroupName,
                        TreasuryID = 0,
                        AccountID = accountID,
                        Value = Pending.balance,
                        MovementTypeId = 7,
                        physicalyearId = group.physicalyearId,
                        Approve = 0,
                        Active = true,
                        CreateId = uid,
                        CreatedDate = DateTime.Now,
                        BranchId = room.BranchId,
                    };
                    _context.Add(financialDocuments);
                    await _context.SaveChangesAsync();

                    // Document Line Group
                    FinancialJournalEntryLine journalEntryLine1 = new FinancialJournalEntryLine()
                    {
                        FinancialDocumentId = financialDocuments.FinancialDocumentId,
                        physicalyearId = financialDocuments.physicalyearId,
                        JournalEntryDate = financialDocuments.JournalEntryDate,
                        AccountID = financialDocuments.AccountID,
                        Debit = 0,
                        Credit = financialDocuments.Value,
                        CreateId = uid,
                        CreatedDate = DateTime.Now,
                        Active = true,
                        BranchId = room.BranchId,
                    };

                    _context.Add(journalEntryLine1);
                    await _context.SaveChangesAsync();

                }
            }
            return RedirectToRoute(new { controller = "Students", action = "ReplyGroup", id = studentGroup.StudentId });
        }

        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser")]
        public async Task<IActionResult> ReplyLecture(int id)
        {
            ViewData["id"] = id;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            bool ReplyLectureisaccess = await Isaccessright(user.Id, 6);
            if (ReplyLectureisaccess)
            {
                ViewData["ReplyLecture"] = "true";
            }
            else
            {
                ViewData["ReplyLecture"] = "false";
            }
            ApplicationUser student = await _userManager.FindByIdAsync(id.ToString());
            if (student.UserTypeId != 7)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserName"] = student.FristName + " " + student.SecondName;
            var balance = await _context.UsersBalance.FindAsync(student.AccountID);
            ViewData["balance"] = balance.Balance * -1;

            ViewData["FullName"] = student.FullName;
            var studentLecture = await _context.StudentLectureViewModel
           .Where(f => f.StudentID == id)
           .OrderByDescending(l => l.StudentLectureId)
           .ToListAsync();
            return View(studentLecture);
        }
        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser")]
        public async Task<IActionResult> ReplyExam(int id)
        {
            ViewData["id"] = id;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            bool ReplyLectureisaccess = await Isaccessright(user.Id, 6);
            if (ReplyLectureisaccess)
            {
                ViewData["ReplyLecture"] = "true";
            }
            else
            {
                ViewData["ReplyLecture"] = "false";
            }
            ApplicationUser student = await _userManager.FindByIdAsync(id.ToString());
            if (student.UserTypeId != 7)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserName"] = student.FristName + " " + student.SecondName;
            var balance = await _context.UsersBalance.FindAsync(student.AccountID);
            ViewData["balance"] = balance.Balance * -1;

            ViewData["FullName"] = student.FullName;
            var studentLecture = await _context.StudentExamViewModel
           .Where(f => f.StudentID == id)
           .OrderByDescending(l => l.StudentLectureId)
           .ToListAsync();
            return View(studentLecture);
        }
        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser")]
        public async Task<IActionResult> ReplyReview(int id)
        {
            ViewData["id"] = id;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            bool ReplyLectureisaccess = await Isaccessright(user.Id, 6);
            if (ReplyLectureisaccess)
            {
                ViewData["ReplyLecture"] = "true";
            }
            else
            {
                ViewData["ReplyLecture"] = "false";
            }
            ApplicationUser student = await _userManager.FindByIdAsync(id.ToString());
            if (student.UserTypeId != 7)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserName"] = student.FristName + " " + student.SecondName;
            var balance = await _context.UsersBalance.FindAsync(student.AccountID);
            ViewData["balance"] = balance.Balance * -1;

            ViewData["FullName"] = student.FullName;
            var studentLecture = await _context.StudentReviewViewModel
          .Where(f => f.StudentID == id)
          .OrderByDescending(l => l.StudentLectureId)
          .ToListAsync();
            return View(studentLecture);
        }

        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser")]
        public async Task<IActionResult> DeleteLecture(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            int uid = Convert.ToInt32(userId);
            var studentLectureViewModel = await _context.StudentLectureViewModel.FindAsync(id);
            var student = await _context.Users.Where(u => u.Id == studentLectureViewModel.StudentID)
                   .FirstOrDefaultAsync();
            var studentLecture1 = await _context.StudentLecture
              .Where(l => l.StudentLectureId == studentLectureViewModel.StudentLectureId)
              .FirstOrDefaultAsync();
            if (studentLecture1.Deleted == 1)
            {
                return RedirectToRoute(new { controller = "Students", action = "ReplyLecture", id = student.Id });
            }
            else
            {
                studentLecture1.Deleted = 1;
                studentLecture1.DeleteId = uid;
                studentLecture1.DeletedDate = DateTime.Now;
                _context.Update(studentLecture1);
                await _context.SaveChangesAsync();

                int branchid = studentLecture1.BranchId;


                // Document Group
                FinancialDocuments financialDocuments = new FinancialDocuments()
                {
                    JournalEntryDate = DateTime.Today,
                    Notes = " استرجاع " + studentLectureViewModel.GroupName,
                    TreasuryID = user.AccountID,
                    AccountID = student.AccountID,
                    Value = studentLectureViewModel.Paided,
                    MovementTypeId = 7,
                    physicalyearId = studentLectureViewModel.physicalyearId,
                    Active = true,
                    Approve = 0,
                    CreateId = uid,
                    CreatedDate = DateTime.Now,
                    GroupscheduleId = studentLectureViewModel.scheduleId,
                    BranchId = branchid,
                };

                _context.Add(financialDocuments);
                await _context.SaveChangesAsync();

                var studentLecture = await _context.StudentLecture
                 .Where(l => l.StudentLectureId == studentLectureViewModel.StudentLectureId)
                 .FirstOrDefaultAsync();

                _context.Remove(studentLecture);
                await _context.SaveChangesAsync();


                FinancialJournalEntryLine journalEntryLine2 = new FinancialJournalEntryLine()
                {
                    FinancialDocumentId = financialDocuments.FinancialDocumentId,
                    physicalyearId = financialDocuments.physicalyearId,
                    JournalEntryDate = financialDocuments.JournalEntryDate,
                    AccountID = financialDocuments.AccountID,
                    Debit = 0,
                    Credit = financialDocuments.Value,
                    CreateId = uid,
                    CreatedDate = DateTime.Now,
                    Active = true,
                    BranchId = branchid,
                };

                _context.Add(journalEntryLine2);
                await _context.SaveChangesAsync();
            }


            //var student = await _context.Users
            //    .Where(s => s.AccountID == journalEntryLine2.AccountID)
            //    .FirstOrDefaultAsync();
            return RedirectToRoute(new { controller = "Students", action = "ReplyLecture", id = student.Id });
        }

        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            int uid = Convert.ToInt32(userId);

            var StudentReviewViewModel = await _context.StudentReviewViewModel.FindAsync(id);
            var student = await _context.Users.Where(u => u.Id == StudentReviewViewModel.StudentID)
              .FirstOrDefaultAsync();

            var studentLecture1 = await _context.StudentLecture
              .Where(l => l.StudentLectureId == StudentReviewViewModel.StudentLectureId)
              .FirstOrDefaultAsync();
            if (studentLecture1.Deleted == 1)
            {
                return RedirectToRoute(new { controller = "Students", action = "ReplyReview", id = student.Id });
            }
            else
            {
                studentLecture1.Deleted = 1;
                studentLecture1.DeleteId = uid;
                studentLecture1.DeletedDate = DateTime.Now;
                _context.Update(studentLecture1);
                await _context.SaveChangesAsync();
                int branchid = studentLecture1.BranchId;

                // Document Group
                FinancialDocuments financialDocuments = new FinancialDocuments()
                {
                    JournalEntryDate = DateTime.Today,
                    Notes = " استرجاع " + StudentReviewViewModel.ReviewsScheduleName,
                    TreasuryID = user.AccountID,
                    AccountID = student.AccountID,
                    Value = StudentReviewViewModel.Paided,
                    MovementTypeId = 7,
                    physicalyearId = StudentReviewViewModel.physicalyearId,
                    Active = true,
                    Approve = 0,
                    CreateId = uid,
                    CreatedDate = DateTime.Now,
                    GroupscheduleId = StudentReviewViewModel.scheduleId,
                    BranchId = branchid,
                };

                _context.Add(financialDocuments);
                await _context.SaveChangesAsync();


                var studentLecture = await _context.StudentLecture
                    .Where(l => l.StudentLectureId == StudentReviewViewModel.StudentLectureId)
                    .FirstOrDefaultAsync();

                _context.Remove(studentLecture);
                await _context.SaveChangesAsync();


                FinancialJournalEntryLine journalEntryLine2 = new FinancialJournalEntryLine()
                {
                    FinancialDocumentId = financialDocuments.FinancialDocumentId,
                    physicalyearId = financialDocuments.physicalyearId,
                    JournalEntryDate = financialDocuments.JournalEntryDate,
                    AccountID = financialDocuments.AccountID,
                    Debit = 0,
                    Credit = financialDocuments.Value,
                    CreateId = uid,
                    CreatedDate = DateTime.Now,
                    Active = true,
                    BranchId = branchid,
                };

                _context.Add(journalEntryLine2);
                await _context.SaveChangesAsync();
            }

            return RedirectToRoute(new { controller = "Students", action = "ReplyReview", id = student.Id });
        }

        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser")]
        public async Task<IActionResult> DeleteExam(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            int uid = Convert.ToInt32(userId);
            var StudentExamViewModel = await _context.StudentExamViewModel.FindAsync(id);
            var student = await _context.Users.Where(u => u.Id == StudentExamViewModel.StudentID)
              .FirstOrDefaultAsync();
            var studentLecture1 = await _context.StudentLecture
              .Where(l => l.StudentLectureId == StudentExamViewModel.StudentLectureId)
              .FirstOrDefaultAsync();
            if (studentLecture1.Deleted == 1)
            {
                return RedirectToRoute(new { controller = "Students", action = "ReplyExam", id = student.Id });
            }
            else
            {
                studentLecture1.Deleted = 1;
                studentLecture1.DeleteId = uid;
                studentLecture1.DeletedDate = DateTime.Now;
                _context.Update(studentLecture1);
                await _context.SaveChangesAsync();
                int branchid = studentLecture1.BranchId;


                // Document Group
                FinancialDocuments financialDocuments = new FinancialDocuments()
                {
                    JournalEntryDate = DateTime.Today,
                    Notes = " استرجاع " + StudentExamViewModel.ReviewsScheduleName,
                    TreasuryID = user.AccountID,
                    AccountID = student.AccountID,
                    Value = StudentExamViewModel.Paided,
                    MovementTypeId = 7,
                    physicalyearId = StudentExamViewModel.physicalyearId,
                    Active = true,
                    Approve = 0,
                    CreateId = uid,
                    CreatedDate = DateTime.Now,
                    GroupscheduleId = StudentExamViewModel.scheduleId,
                    BranchId = branchid,
                };

                _context.Add(financialDocuments);
                await _context.SaveChangesAsync();


                var studentLecture = await _context.StudentLecture
                    .Where(l => l.StudentLectureId == StudentExamViewModel.StudentLectureId)
                    .FirstOrDefaultAsync();

                _context.Remove(studentLecture);
                await _context.SaveChangesAsync();


                FinancialJournalEntryLine journalEntryLine2 = new FinancialJournalEntryLine()
                {
                    FinancialDocumentId = financialDocuments.FinancialDocumentId,
                    physicalyearId = financialDocuments.physicalyearId,
                    JournalEntryDate = financialDocuments.JournalEntryDate,
                    AccountID = financialDocuments.AccountID,
                    Debit = 0,
                    Credit = financialDocuments.Value,
                    CreateId = uid,
                    CreatedDate = DateTime.Now,
                    Active = true,
                    BranchId = branchid,
                };

                _context.Add(journalEntryLine2);
                await _context.SaveChangesAsync();
            }
            return RedirectToRoute(new { controller = "Students", action = "ReplyExam", id = student.Id });
        }

        // UpdateImage  - UpdateImage
        [HttpGet]
        public async Task<IActionResult> UpdateImage(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var employee = await _context.Users.FindAsync(id);
            if (employee.UserTypeId != 7)
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

            if (employee.UserTypeId != 7)
                return RedirectToAction(nameof(Index));

            string imgurlpath = "images/Students/default.jpg";
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

            var result = _userManager.UpdateAsync(employee).Result;


            return RedirectToAction(nameof(Index));
        }


        // Newimages
        [HttpGet]
        public async Task<IActionResult> Newimages()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var employee = await _context.Users.Where(u => u.imageint == 1 && u.UserTypeId == 7).ToListAsync();

            return View(employee);
        }

        // ApproveImage
        [HttpPost]
        public async Task<IActionResult> ApproveImage(int id)
        {
            ApplicationUser employee = await _userManager.FindByIdAsync(id.ToString());
            employee.imageint = 2;
            var result = _userManager.UpdateAsync(employee).Result;
            return RedirectToRoute(new { controller = "Students", action = "Newimages" });
        }

        // RejectImage
        //  string imgurlpath = "images/Students/default.jpg";
        [HttpPost]
        public async Task<IActionResult> RejectImage(int id)
        {
            ApplicationUser employee = await _userManager.FindByIdAsync(id.ToString());
            employee.imageint = 3;
            employee.imgurl = "images/Students/Rejected.jpg";
            var result = _userManager.UpdateAsync(employee).Result;
            return RedirectToRoute(new { controller = "Students", action = "Newimages" });
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
