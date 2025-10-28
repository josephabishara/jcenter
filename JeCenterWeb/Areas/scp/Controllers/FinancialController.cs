using JeCenterWeb.Data;
using JeCenterWeb.Models;
using JeCenterWeb.Models.Second;
using JeCenterWeb.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Security.Claims;

namespace JeCenterWeb.Areas.scp.Controllers
{
    [Area("scp")]
    [Authorize(Roles = "AdminSettings,PowerUser,SuperUser")]
    public class FinancialController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        string ControllerName = "الحسابات";
        string AppName = "الأدمن";
        public FinancialController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //        cDepartment.CreateId = Convert.ToInt32(userId);
        //        cDepartment.UpdateId = Convert.ToInt32(userId);
        //        cDepartment.DeleteId = Convert.ToInt32(userId);
        //        cDepartment.UpdatedDate = DateTime.Now;
        //        cDepartment.CreatedDate = DateTime.Now;
        //        cDepartment.DeletedDate = DateTime.Now;
        //        cDepartment.Active = true;

        //  iTreasury =  primary =  كشف حساب الخزنة
        //  TreasuryCashing =  info =  مصروفات من الخزنة
        //  TreasuryCashing = secondary =  صرف للمدرسين
        //  TreasuryReceive =  warning =  استلام من خزنة اخري
        //  TreasuryTransform =  success =  توريد لخزنة
        //  TreasuryTransform = danger =  توريد للأدمن

        [Authorize(Roles = "AdminSettings")]
        public async Task<IActionResult> Index()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var balance = await _context.UsersBalance.ToListAsync();
            return View(balance);
        }
        [Authorize(Roles = "AdminSettings")]
        public async Task<IActionResult> ChartOfAccounts()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var balance = await _context.FinancialAccount.Where(a => a.AccountTypeId == 0).ToListAsync();
            return View(balance);
        }

        // GET: scp/Account/Create
        [Authorize(Roles = "AdminSettings")]
        public IActionResult CreateAccount()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            return View();
        }

        // POST: scp/Account/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AdminSettings")]
        public async Task<IActionResult> CreateAccount([Bind("FinancialAccountId,AccountName,AdvancedPermission,Writed,Deleted,Active,CreateId,CreatedDate,UpdateId,UpdatedDate,DeleteId,DeletedDate")] FinancialAccount financialAccount)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                financialAccount.CreateId = Convert.ToInt32(userId);
                financialAccount.UpdateId = Convert.ToInt32(userId);
                financialAccount.DeleteId = Convert.ToInt32(userId);
                financialAccount.UpdatedDate = DateTime.Now;
                financialAccount.CreatedDate = DateTime.Now;
                financialAccount.DeletedDate = DateTime.Now;
                financialAccount.Active = true;
                _context.Add(financialAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ChartOfAccounts));
            }
            return View(financialAccount);
        }

        // GET: scp/Account/Edit/5
        [Authorize(Roles = "AdminSettings")]
        public async Task<IActionResult> EditAccount(int? id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;

            var FinancialAccount = await _context.FinancialAccount.FindAsync(id);
            if (FinancialAccount == null)
            {
                return NotFound();
            }
            return View(FinancialAccount);
        }

        // POST: scp/Account/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AdminSettings")]
        public async Task<IActionResult> EditAccount(int id, [Bind("FinancialAccountId,AccountName,AdvancedPermission,Writed,Deleted,Active,CreateId,CreatedDate,UpdateId,UpdatedDate,DeleteId,DeletedDate")] FinancialAccount financialAccount)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;


            if (ModelState.IsValid)
            {

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                financialAccount.UpdateId = Convert.ToInt32(userId);
                financialAccount.UpdatedDate = DateTime.Now;
                financialAccount.Active = true;
                _context.Update(financialAccount);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(ChartOfAccounts));
            }
            return View(financialAccount);
        }



        // GET: cs/Branch
        [Authorize(Roles = "AdminSettings")]
        public async Task<IActionResult> Physicalyears()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;

            var branches = await _context.PhysicalYear
                .ToListAsync();

            return View(branches);
        }

        [Authorize(Roles = "AdminSettings")]
        public async Task<IActionResult> ChangePhysicalYear(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            ApplicationUser employee = await _userManager.FindByIdAsync(user.Id.ToString());
            employee.ParentId = id;
            var result = _userManager.UpdateAsync(employee).Result;

            return RedirectToRoute(new { controller = "Financial", action = "Branches", area = "scp" });
        }

        // GET: cs/Branch
        [Authorize(Roles = "AdminSettings")]
        public async Task<IActionResult> Branches()
        {
            ViewData["AppName"] = AppName;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            var year = await _context.PhysicalYear.FindAsync(user.ParentId);
            ViewData["ControllerName"] = ControllerName + " - " + year.PhysicalyearName;
            var branches = await _context.CBranch
                .ToListAsync();
            DateTime myday = DateTime.Today;
            DateTime journaldate = new DateTime(myday.Year, myday.Month, myday.Day, 0, 0, 0);
            ViewData["date"] = DateTime.Today.ToString("yyyy-MM-dd");
            List<FinancialDashboardViewModel> MyViewModellist = new List<FinancialDashboardViewModel>();
            foreach (var item in branches)
            {
                var financialDashboardReportlist = await _context.FinancialDashboardReport
                    .Where(r => r.BranchId == item.BranchId && r.JournalEntryDate == journaldate && r.physicalyearId == user.ParentId)
                    .ToListAsync();

                var entryLine = await _context.FinancialDocuments
                    .Where(d => d.BranchId == item.BranchId && d.JournalEntryDate >= journaldate && d.physicalyearId == user.ParentId)
                    .ToListAsync();


                FinancialDashboardViewModel viewModel = new FinancialDashboardViewModel();
                viewModel.FinancialDashboardReport = financialDashboardReportlist;

                viewModel.BranchName = item.BranchName;
                viewModel.BranchId = item.BranchId;
                // viewModel.TotalCharged = entryLine.Where(e => e.MovementTypeId == 6)
                //                                     .Sum(e => e.Value);

                //viewModel.TotalPayments = entryLine
                //    .Where(e => e.MovementTypeId == 2)
                //         .Where(e => e.JournalEntryDate >= DateTime.Today && e.JournalEntryDate < DateTime.Today.AddDays(+1))
                //    .Sum(e => e.Value);

                viewModel.TeacherCount = entryLine
               .Where(e => e.MovementTypeId == 3 || e.MovementTypeId == 11 || e.MovementTypeId == 12)
                 .Where(e => e.JournalEntryDate >= DateTime.Today && e.JournalEntryDate < DateTime.Today.AddDays(+1))
               .Sum(e => e.Value);

                viewModel.GroupsCount = await _context.CGroupSchedule
                   .Where(e => e.LectureDate == DateTime.Today && e.CGroups.CRooms.BranchId == item.BranchId)
                   .Include(e => e.CGroups.CRooms)
                   .CountAsync();

                viewModel.ReviewsCount = await _context.ReviewsSchedule
               .Where(e => e.ReviewDate == DateTime.Today && e.ReviewTypeId == 1 && e.CRooms.BranchId == item.BranchId)
               .Include(e => e.CRooms)
               .CountAsync();

                viewModel.ExamsCount = await _context.ReviewsSchedule
               .Where(e => e.ReviewDate == DateTime.Today && e.ReviewTypeId == 2 && e.CRooms.BranchId == item.BranchId)
               .Include(e => e.CRooms)
               .CountAsync();

                //DateTime myday = DateTime.Now;
                //DateTime journaldate = new DateTime(myday.Year, myday.Month, myday.Day, 0, 0, 0);
                //var entryLine = await _context.FinancialDocuments
                //    .Where(l => l.JournalEntryDate > journaldate)
                //    .ToListAsync();
                //var tl = await _context.CGroupSchedule
                //              .Where(e => e.LectureDate == DateTime.Today && e.CGroups.CRooms.BranchId == item.BranchId)
                //              .Include(e => e.CGroups.CRooms)
                //              .GroupBy(e => e.CGroups.TeacherId).ToListAsync();
                //viewModel.TeacherCount = tl.Count();

                var sl = await _context.StudentLectureByBranchToday
                   .Where(e => e.BranchId == item.BranchId && e.ReservationDate == DateTime.Today.Date)
                   .FirstOrDefaultAsync();
                if (sl == null)
                {
                    viewModel.StudentCount = 0;
                }
                else
                {
                    viewModel.StudentCount = sl.StudentCount;
                }


                MyViewModellist.Add(viewModel);

            }

            return View(MyViewModellist);
        }

        [Authorize(Roles = "AdminSettings")]
        public async Task<IActionResult> BranchesByDate(DateTime date)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            var year = await _context.PhysicalYear.FindAsync(user.ParentId);
            ViewData["ControllerName"] = ControllerName + " - " + year.PhysicalyearName;
            var branches = await _context.CBranch
                .ToListAsync();
            DateTime myday = date.AddDays(-1);
            DateTime journaldate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            ViewData["date"] = date.ToString("yyyy-MM-dd");
            List<FinancialDashboardViewModel> MyViewModellist = new List<FinancialDashboardViewModel>();
            foreach (var item in branches)
            {

                var financialDashboardReportlist = await _context.FinancialDashboardReport
                   .Where(r => r.BranchId == item.BranchId && r.JournalEntryDate == journaldate && r.physicalyearId == user.ParentId)
                   .ToListAsync();
                var entryLine = await _context.FinancialDocuments
                    .Where(d => d.BranchId == item.BranchId && d.JournalEntryDate > journaldate && d.physicalyearId == user.ParentId)
                    .ToListAsync();


                FinancialDashboardViewModel viewModel = new FinancialDashboardViewModel();
                viewModel.FinancialDashboardReport = financialDashboardReportlist;
                viewModel.BranchName = item.BranchName;
                viewModel.BranchId = item.BranchId;
                // viewModel.TotalCharged = entryLine.Where(e => e.MovementTypeId == 6)
                //                                     .Sum(e => e.Value);

                // viewModel.TotalPayments = entryLine
                //     .Where(e => e.MovementTypeId == 2)
                //          .Where(e => e.JournalEntryDate >= date && e.JournalEntryDate < date.AddDays(+1))
                //     .Sum(e => e.Value);

                viewModel.TeacherCount = entryLine
               .Where(e => e.MovementTypeId == 3 || e.MovementTypeId == 11 || e.MovementTypeId == 12)
                 .Where(e => e.JournalEntryDate >= date && e.JournalEntryDate < date.AddDays(+1))
               .Sum(e => e.Value);

                viewModel.GroupsCount = await _context.CGroupSchedule
                   .Where(e => e.LectureDate == date && e.CGroups.CRooms.BranchId == item.BranchId && e.CGroups.physicalyearId == user.ParentId)
                   .Include(e => e.CGroups.CRooms)
                   .CountAsync();

                viewModel.ReviewsCount = await _context.ReviewsSchedule
               .Where(e => e.ReviewDate == date && e.ReviewTypeId == 1 && e.CRooms.BranchId == item.BranchId && e.physicalyearId == user.ParentId)
               .Include(e => e.CRooms)
               .CountAsync();

                viewModel.ExamsCount = await _context.ReviewsSchedule
               .Where(e => e.ReviewDate == date && e.ReviewTypeId == 2 && e.CRooms.BranchId == item.BranchId && e.physicalyearId == user.ParentId)
               .Include(e => e.CRooms)
               .CountAsync();

                //DateTime myday = DateTime.Now;
                //DateTime journaldate = new DateTime(myday.Year, myday.Month, myday.Day, 0, 0, 0);
                //var entryLine = await _context.FinancialDocuments
                //    .Where(l => l.JournalEntryDate > journaldate)
                //    .ToListAsync();
                //var tl = await _context.CGroupSchedule
                //              .Where(e => e.LectureDate == DateTime.Today && e.CGroups.CRooms.BranchId == item.BranchId)
                //              .Include(e => e.CGroups.CRooms)
                //              .GroupBy(e => e.CGroups.TeacherId).ToListAsync();
                //viewModel.TeacherCount = tl.Count();

                var sl = await _context.StudentLectureByBranchToday
                  .Where(e => e.BranchId == item.BranchId && e.ReservationDate == date)
                  .FirstOrDefaultAsync();
                if (sl == null)
                {
                    viewModel.StudentCount = 0;
                }
                else
                {
                    viewModel.StudentCount = sl.StudentCount;
                }

                MyViewModellist.Add(viewModel);

            }

            return View(MyViewModellist);
        }
        [Authorize(Roles = "AdminSettings")]
        public async Task<IActionResult> BranchesDetails(int branchId, int movementTypeId, DateTime date)
        {
            DateTime myday = date.AddDays(-1);
            DateTime journaldate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            var brnch = await _context.CBranch.FindAsync(branchId);
            var tp = await _context.MovementType.FindAsync(movementTypeId);

            ViewData["branch"] = brnch.BranchName;
            ViewData["type"] = tp.MovementTypeName;
            ViewData["date"] = journaldate.ToShortDateString();

            var financialJournalEntryLineViewModel = await _context.FinancialJournalEntryLineViewModel
                     .Where(f => f.BranchId == branchId && f.MovementTypeId == movementTypeId && f.JournalEntryDate == journaldate)
                    .OrderByDescending(d => d.FinancialDocumentId)
                    .ToListAsync();
            if (movementTypeId == 7)
            {
                ViewData["sum"] = financialJournalEntryLineViewModel.Sum(s => s.Credit);
            }
            else
            {
                ViewData["sum"] = financialJournalEntryLineViewModel.Sum(s => s.Debit);
            }

            return View(financialJournalEntryLineViewModel);
        }
        [Authorize(Roles = "AdminSettings")]
        public async Task<IActionResult> FinancialAccount()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = "حسابات المصاريف";
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            var year = await _context.PhysicalYear.FindAsync(user.ParentId);
            ViewData["ControllerName"] = "حسابات المصاريف" + " - " + year.PhysicalyearName;
            var balance = await _context.ExpensesBalance
                .Where(a => a.PhysicalyearId == user.ParentId)
                .ToListAsync();
            return View(balance);
        }

        [HttpPost]
        [Authorize(Roles = "AdminSettings")]
        public async Task<IActionResult> CreateFinancialAccount([Bind("AccountName,AccountTypeId")] FinancialAccount financialAccount)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                financialAccount.CreateId = Convert.ToInt32(userId);
                financialAccount.UpdateId = Convert.ToInt32(userId);
                financialAccount.DeleteId = Convert.ToInt32(userId);
                financialAccount.UpdatedDate = DateTime.Now;
                financialAccount.CreatedDate = DateTime.Now;
                financialAccount.DeletedDate = DateTime.Now;
                financialAccount.Active = true;
                _context.Add(financialAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(FinancialAccount));
            }
            return View(financialAccount);

        }

        [Authorize(Roles = "AdminSettings")]
        public async Task<IActionResult> TreasuryBalance()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            var year = await _context.PhysicalYear.FindAsync(user.ParentId);
            ViewData["ControllerName"] = ControllerName + " - " + year.PhysicalyearName;
            var balance = await _context.UsersBalance
                .Where(u => u.UserTypeId == 1 || u.UserTypeId == 2 || u.UserTypeId == 3 || u.UserTypeId == 4)
                .Where(u => u.Balance > 0)
                //.Where(y => y.physicalyearId == user.ParentId)
                .ToListAsync();
            return View(balance);
        }

        [Authorize(Roles = "AdminSettings")]
        public async Task<IActionResult> TeachersBalance()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            var year = await _context.PhysicalYear.FindAsync(user.ParentId);
            ViewData["ControllerName"] = ControllerName + " - " + year.PhysicalyearName;
            var balance = await _context.TeachersBalance
                .Where(u => u.physicalyearId == user.ParentId && u.Balance > 0)
                .OrderByDescending(u => u.Balance)
                .ToListAsync();
            return View(balance);
        }


        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser")]
        public async Task<IActionResult> Teacherbalance()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            bool isaccess =await Isaccessright(user.Id, 5);
            if (!isaccess )
            {
                return RedirectToRoute(new { area = "scp", controller = "Dashboard", action = "Index" });
            }
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["userid"] = new SelectList(_context.Users.Where(t => t.UserTypeId == 5 && t.Active == true), "AccountID", "FullName");
            //  ViewData["userid"] = new SelectList(_context.FinancialAccount.Where(t => t.AccountTypeId == 5 && t.Active == true), "FinancialAccountId", "AccountName");
            return View();
        }

        [Authorize(Roles = "AdminSettings")]
        public async Task<IActionResult> UsersBalance()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            var year = await _context.PhysicalYear.FindAsync(user.ParentId);
            ViewData["ControllerName"] = ControllerName + " - " + year.PhysicalyearName;
            var balance = await _context.UsersBalance
                .Where(u => u.Balance > 0)
                .ToListAsync();
            return View(balance);
        }

        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser")]
        public async Task<IActionResult> Userbalance()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            var year = await _context.PhysicalYear.FindAsync(user.ParentId);

            bool isaccess =await Isaccessright(user.Id, 4);
            if (!isaccess)
            {
                return RedirectToRoute(new { area = "scp", controller = "Dashboard", action = "Index" });
            }
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["userid"] = new SelectList(_context.FinancialAccount.Where(u => u.AccountTypeId < 5 && u.Active == true), "FinancialAccountId", "AccountName");
            return View();
        }

        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser")]
        public async Task<IActionResult> Details([Bind("id")] int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            bool Isaccessright4 = await Isaccessright(user.Id, 4);
            bool Isaccessright5 = await Isaccessright(user.Id, 5);
            if (Isaccessright4 == false && Isaccessright4 == false)
            {
                return RedirectToRoute(new { area = "scp", controller = "Dashboard", action = "Index" });
            }
            var year = await _context.PhysicalYear.FindAsync(user.ParentId);
            var acc = await _context.FinancialAccount.FindAsync(id);
            DateTime myday = DateTime.Now;
            DateTime journaldate = new DateTime(myday.Year, myday.Month, myday.Day, 0, 0, 0);
            if (user.UserTypeId == 1)
            {
                ViewData["ControllerName"] = ControllerName + " - " + year.PhysicalyearName;
            }
            ViewData["FullName"] = acc.AccountName;
            ViewData["id"] = id;
            ViewData["journaldate"] = journaldate.ToShortDateString();
            var balance = await _context.UsersBalance
                .Where(u => u.Id == acc.FinancialAccountId)
                .FirstOrDefaultAsync();
            ViewData["Balance"] = 0;
            var financialJournalEntryLineViewModel = await _context.FinancialJournalEntryLineViewModel
                      .Where(f => f.AccountID == acc.FinancialAccountId && f.physicalyearId == user.ParentId)
                         .Where(p => p.JournalEntryDate.Date == journaldate)
                    .OrderByDescending(d => d.FinancialDocumentId)
                    .ToListAsync();
            return View(financialJournalEntryLineViewModel);
        }

        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser")]
        public async Task<IActionResult> DetailsDay([Bind("id,Fromdate,Todate")] IdTowDateViewModel model)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            bool Isaccessright4 =await Isaccessright(user.Id, 4);
            bool Isaccessright5 =await Isaccessright(user.Id, 5);
            if (Isaccessright4 ==  false  && Isaccessright4 == false) 
            {
                return RedirectToRoute(new { area = "scp", controller = "Dashboard", action = "Index" });
            }
            var year = await _context.PhysicalYear.FindAsync(user.ParentId);
            var acc = await _context.FinancialAccount.FindAsync(model.id);
            DateTime fromday = model.Fromdate;
            DateTime today = model.Todate;
            DateTime journalfromdate = new DateTime(fromday.Year, fromday.Month, fromday.Day, 0, 0, 0);
            DateTime journaltodate = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);
            if (user.UserTypeId == 1)
            {
                ViewData["ControllerName"] = ControllerName + " - " + year.PhysicalyearName;
            }
            ViewData["FullName"] = acc.AccountName;
            ViewData["id"] = model.id;
            //var balance = await _context.UsersBalance
            //    .Where(u => u.Id == acc.FinancialAccountId)
            //    .FirstOrDefaultAsync();

            ViewData["journaldate"] = " من : " + journalfromdate.ToShortDateString() + " - الى : " + journaltodate.ToShortDateString();
            var financialJournalEntryLineViewModel = await _context.FinancialJournalEntryLineViewModel
                      .Where(f => f.AccountID == acc.FinancialAccountId)
                      .Where(p => p.JournalEntryDate.Date >= journalfromdate && p.JournalEntryDate.Date <= journaltodate)
                    .OrderByDescending(d => d.FinancialDocumentId)
                    .ToListAsync();
            ViewData["Balance"] = financialJournalEntryLineViewModel.Sum(a => a.Debit);
            return View(financialJournalEntryLineViewModel);
        }
        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser")]
        public async Task<IActionResult> Detail([Bind("id")] IdViewModel idViewModel)
        {
            return RedirectToRoute(new { action = "Details", id = idViewModel.id });
        }


        // payment 
        [Authorize(Roles = "AdminSettings")]
        public async Task<IActionResult> Payments()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            var year = await _context.PhysicalYear.FindAsync(user.ParentId);
            DateTime myday = DateTime.Now;
            DateTime journaldate = new DateTime(myday.Year, myday.Month, myday.Day, 0, 0, 0);
            ViewData["journaldate"] = journaldate;
            ViewData["ControllerName"] = ControllerName + " - " + year.PhysicalyearName;
            var FinancialJournalEntryViewModel = await _context.FinancialJournalEntryViewModel
                        .Where(e => e.MovementTypeId == 2 && e.physicalyearId == user.ParentId)
                        .Where(p => p.JournalEntryDate.Date == journaldate)
                        .OrderByDescending(d => d.FinancialDocumentId)
                        .ToListAsync();

            return View(FinancialJournalEntryViewModel);
        }

        [Authorize(Roles = "AdminSettings")]
        public async Task<IActionResult> Paymentday(DateTime date)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            var year = await _context.PhysicalYear.FindAsync(user.ParentId);
            ViewData["ControllerName"] = ControllerName + " - " + year.PhysicalyearName;
            DateTime journaldate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            ViewData["journaldate"] = journaldate;
            var FinancialJournalEntryViewModel = await _context.FinancialJournalEntryViewModel
                        .Where(e => e.MovementTypeId == 2 && e.physicalyearId == user.ParentId)
                        .Where(p => p.JournalEntryDate.Date == journaldate)
                        .OrderByDescending(d => d.FinancialDocumentId)
                        .ToListAsync();

            return View(FinancialJournalEntryViewModel);
        }

        //   ApprovePayment
        [HttpPost]
        [Authorize(Roles = "AdminSettings")]
        public async Task<IActionResult> ApprovePayment(int id)
        {
            DateTime day = DateTime.Today;
            var financialDocuments = await _context.FinancialDocuments.FindAsync(id);
            day = financialDocuments.JournalEntryDate;
            financialDocuments.Approve = 1;
            financialDocuments.UpdatedDate = DateTime.Now;
            _context.Update(financialDocuments);
            await _context.SaveChangesAsync();
            await createTransformationrJournalEntryLine(financialDocuments);
            // return RedirectToRoute(new { action = "Payments" });
            return RedirectToRoute(new { action = "Paymentday" , date = day.ToString("yyyy-MM-dd") });
        }

        //   RejectPayment
        [HttpPost]
        [Authorize(Roles = "AdminSettings")]
        public async Task<IActionResult> RejectPayment(int id)
        {
            DateTime day = DateTime.Today;
            var financialDocuments = await _context.FinancialDocuments.FindAsync(id);
            day = financialDocuments.JournalEntryDate;
            financialDocuments.Approve = 2;
            financialDocuments.UpdatedDate = DateTime.Now;
            _context.Update(financialDocuments);
            await _context.SaveChangesAsync();
            // await createTransformationrJournalEntryLine(financialDocuments);
            // return RedirectToRoute(new { action = "Payments" });
            return RedirectToRoute(new { action = "Paymentday", date = day.ToString("yyyy-MM-dd") });
        }



        // Return 
        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser")]
        public async Task<IActionResult> PaymentsReturn()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            var year = await _context.PhysicalYear.FindAsync(user.ParentId);

            bool isaccess = await Isaccessright(user.Id , 2);
            if (!isaccess)
            {
                return RedirectToRoute(new { area = "scp", controller = "Dashboard", action = "Index" });
            }
            if(user.UserTypeId==1)
            {
                ViewData["ControllerName"] = ControllerName + " - " + year.PhysicalyearName;
            }

            DateTime myday = DateTime.Now;
            DateTime journaldate = new DateTime(myday.Year, myday.Month, myday.Day, 0, 0, 0);
            ViewData["journaldate"] = journaldate;
            var FinancialJournalEntryViewModel = await _context.FinancialJournalEntryViewModel
                        .Where(e => e.MovementTypeId == 7)
                        .Where(p => p.JournalEntryDate.Date == journaldate)
                        .OrderByDescending(d => d.FinancialDocumentId)
                        .ToListAsync();

            return View(FinancialJournalEntryViewModel);
        }

        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser")]
        public async Task<IActionResult> PaymentdayReturn(DateTime date)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            var year = await _context.PhysicalYear.FindAsync(user.ParentId);
            bool isaccess = await Isaccessright(user.Id, 2);
            if (!isaccess)
            {
                return RedirectToRoute(new { area = "scp", controller = "Dashboard", action = "Index" });
            }
            if (user.UserTypeId == 1)
            {
                ViewData["ControllerName"] = ControllerName + " - " + year.PhysicalyearName;
            }
            DateTime journaldate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            ViewData["journaldate"] = journaldate;
            var FinancialJournalEntryViewModel = await _context.FinancialJournalEntryViewModel
                        .Where(e => e.MovementTypeId == 7)
                        .Where(p => p.JournalEntryDate.Date == journaldate)
                        .OrderByDescending(d => d.FinancialDocumentId)
                        .ToListAsync();

            return View(FinancialJournalEntryViewModel);
        }

        //   ApprovePaymentReturn
        [HttpPost]
        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser")]
        public async Task<IActionResult> ApprovePaymentReturn(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            bool isaccess = await Isaccessright(user.Id, 2);
            if (!isaccess)
            {
                return RedirectToRoute(new { area = "scp", controller = "Dashboard", action = "Index" });
            }
            var financialDocuments = await _context.FinancialDocuments.FindAsync(id);
            financialDocuments.Approve = 1;
            financialDocuments.UpdatedDate = DateTime.Now;
            _context.Update(financialDocuments);
            await _context.SaveChangesAsync();
            //  await createTransformationrJournalEntryLine(financialDocuments);
            return RedirectToRoute(new { action = "PaymentsReturn" });
        }

        //   RejectPaymentReturn
        [HttpPost]
        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser")]
        public async Task<IActionResult> RejectPaymentReturn(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            bool isaccess = await Isaccessright(user.Id, 2);
            if (!isaccess)
            {
                return RedirectToRoute(new { area = "scp", controller = "Dashboard", action = "Index" });
            }
            var financialDocuments = await _context.FinancialDocuments.FindAsync(id);
            financialDocuments.Approve = 2;
            financialDocuments.UpdatedDate = DateTime.Now;
            _context.Update(financialDocuments);
            await _context.SaveChangesAsync();
            // await createTransformationrJournalEntryLine(financialDocuments);
            return RedirectToRoute(new { action = "PaymentsReturn" });
        }


        // Discount 
        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser")]
        public async Task<IActionResult> Discounts(int? page) // 4
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            //var access =await _context.UserAccessRight
            //   .Where(u => u.UserId == user.Id && u.AccessId == 3)
            //   .FirstOrDefaultAsync();
            //if (access == null)
            //{
            //    return RedirectToRoute(new { area = "scp", controller = "Dashboard", action = "Index" });
            //}
            bool isaccess = await Isaccessright(user.Id, 3);
            if (!isaccess)
            {
                return RedirectToRoute(new { area = "scp", controller = "Dashboard", action = "Index" });
            }
            //bool isaccess = await Isaccessright(user.Id, 3);
            //if (!isaccess)
            //{
            //    return RedirectToRoute(new { area = "scp", controller = "Dashboard", action = "Index" });
            //}

            int pagesize = 25;
            //  int? takepage = 0;
            List<StudentDiscount> Discount = new List<StudentDiscount>();
            List<StudentDiscountViewModel> studentDiscountViewModel = new List<StudentDiscountViewModel>();
            //if (Discount != null)
            //{
            //    pagecount = Discount.Count / 50; //  1500 / 50 = 30
            //}

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

            foreach (var item in Discount)
            {
                var teacher = await _context.Users.FindAsync(item.TeacherId);
                var studen = await _context.Users
                    .Where(s => s.AccountID == item.StudentId)
                    .FirstOrDefaultAsync();
                var Employee = await _context.Users.FindAsync(item.CreateId);

                StudentDiscountViewModel studentDiscount = new StudentDiscountViewModel()
                {
                    StudentDiscountId = item.StudentDiscountId,
                    AccountId = item.StudentId,
                    StudentId = studen.Id,
                    TeacherId = item.TeacherId,
                    UserId = item.CreateId,
                    Discount = item.Discount,
                    CenterDiscount = item.CenterDiscount,
                    teacherDiscount = item.teacherDiscount,
                    physicalyearId = item.physicalyearId,
                    Notes = item.Notes,
                    TeacherName = teacher.FullName,
                    StudentName = studen.FullName,
                    UserName = Employee.FullName,
                    Active = item.Active,
                    UpdateId = item.UpdateId,
                    CreatedDate = item.CreatedDate,
                };

                if (item.LectureType == 0)
                {
                    studentDiscount.LectureType = "مجموعة";
                }
                else if (item.LectureType == 1)
                {
                    studentDiscount.LectureType = "مراجعة";
                }
                else if (item.LectureType == 2)
                {
                    studentDiscount.LectureType = "أمتحان";
                }

                studentDiscountViewModel.Add(studentDiscount);
            }

            return View(studentDiscountViewModel);

        }



        //   ApprovePayment
        [HttpPost]
        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser")]
        public async Task<IActionResult> ApproveDiscount(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            bool isaccess = await Isaccessright(user.Id, 3);
            if (!isaccess)
            {
                return RedirectToRoute(new { area = "scp", controller = "Dashboard", action = "Index" });
            }
            var studentDiscount = await _context.StudentDiscount.FindAsync(id);
            studentDiscount.Active = true;
            studentDiscount.UpdateId = Convert.ToInt32(userId);
            studentDiscount.UpdatedDate = DateTime.Now;
            _context.Update(studentDiscount);
            await _context.SaveChangesAsync();

            return RedirectToRoute(new { action = "Discounts" });
        }

        //   RejectPayment
        [HttpPost]
        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser")]
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            bool isaccess = await Isaccessright(user.Id, 3);
            if (!isaccess)
            {
                return RedirectToRoute(new { area = "scp", controller = "Dashboard", action = "Index" });
            }
            var studentDiscount = await _context.StudentDiscount.FindAsync(id);
            studentDiscount.Active = false;
            studentDiscount.UpdateId = Convert.ToInt32(userId);
            studentDiscount.UpdatedDate = DateTime.Now;
            _context.Update(studentDiscount);
            await _context.SaveChangesAsync();

            return RedirectToRoute(new { action = "Discounts" });
        }

        [Authorize(Roles = "AdminSettings")]
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

        [Authorize(Roles = "AdminSettings")]
        public async Task<int> CreateFinancialJournalEntryLine(FinancialDocuments financialDocuments)
        {
            int result = 0;
            FinancialJournalEntryLine financialJournalEntryLine = new FinancialJournalEntryLine
            {
                AccountID = financialDocuments.AccountID,
                FinancialDocumentId = financialDocuments.FinancialDocumentId,
                physicalyearId = financialDocuments.physicalyearId,
                Debit = financialDocuments.Value,
                Credit = 0,
                JournalEntryDate = financialDocuments.JournalEntryDate,


            };
            _context.Add(financialJournalEntryLine);
            await _context.SaveChangesAsync();
            result += 1;
            financialJournalEntryLine.AccountID = financialDocuments.TreasuryID;
            financialJournalEntryLine.Debit = 0;
            financialJournalEntryLine.Credit = financialDocuments.Value;
            _context.Add(financialJournalEntryLine);
            await _context.SaveChangesAsync();
            result += 1;
            return result;

        }


     
        public async Task<bool> Isaccessright(int userid, int accessid)
        {
            bool result = false;

            var access =await _context.UserAccessRight
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
