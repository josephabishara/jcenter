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

    public class CardsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        string ControllerName = "الفروع";
        string AppName = "الأدمن";

        public CardsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Authorize(Roles = "AdminSettings,PowerUser")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "AdminSettings,PowerUser")]
        [HttpPost]
        public async Task<IActionResult> AddChargingCard(Cardwrite cardwrite)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cardwrite);
                await _context.SaveChangesAsync();
                await createcards(cardwrite.Cardwriteid, cardwrite.Cardvalue, cardwrite.CardCount);
                return RedirectToAction(nameof(Index));
            }
            return View(cardwrite);
        }
        [Authorize(Roles = "AdminSettings,PowerUser")]
        public async Task createcards(int id, int val, int cnt)
        {
            string code;
            for (int i = 0; i < cnt; i++)
            {
                code = RandomPassword(14);
                ChargingCard? old = await _context.ChargingCard
                    .Where(c => c.CardCode == code)
                    .FirstOrDefaultAsync();
                if (old != null)
                {
                    i--;
                }
                else
                {
                    ChargingCard chargingCard = new ChargingCard
                    {
                        CardCode = code,
                        CardValue = val,
                        State = false,

                        StudentID = 0,
                        Cardwriteid = id,
                    };
                    _context.Add(chargingCard);
                    await _context.SaveChangesAsync();
                }

            }
        }

        public static string RandomPassword(int PasswordLength)
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

        // Checkcard
        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser,User")]
        public async Task<IActionResult> NewCheckcard()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = "Check Card";

            return View();
        }

        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser,User")]
        public async Task<IActionResult> Checkcard(string word)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = "Check Card";
            ChargingCard? card = await _context.ChargingCard.Where(c => c.CardCode == word).FirstOrDefaultAsync();
            string ilabel = "callout callout-warning";
            string iheader = "الكارت غير موجود";
            string iparagraph = "";
            if (card != null)
            {
                if (card.State == false)
                {
                    ilabel = "callout callout-success";
                    iheader = "الكارت غير مشحون  ";
                }
                else if (card.State == true)
                {
                    var student = await _context.Users.FindAsync(155);
                    var Cardwrite = await _context.Users.FindAsync(156);
                    ilabel = "callout callout-danger";
                    iheader = "الكارت مشحون من قبل";
                    iparagraph = " قيمة الكارت :  " + card.CardValue.ToString() + "<br>"
                        + " تم شحنه في : " + card.ChargingDate.ToShortDateString() + "<br>"
                        + " للطالب : " + student.FullName + "<br>"
                        + " شُحن عن طريق : " + Cardwrite.FullName;
                }
            }
            ViewData["ilabel"] = ilabel;
            ViewData["iheader"] = iheader;
            ViewData["iparagraph"] = iparagraph;
            return View(card);
        }


        // Checkcard
        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser,User")]
        public async Task<IActionResult> NewCheckCoupon()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = "   فحص كوبون";

            return View();
        }

        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser,User")]
        public async Task<IActionResult> CheckCoupon(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = "   فحص كوبون";
            StudentLecture? coupon = await _context.StudentLecture.FindAsync(id);
            CheckCouponViewModel model = new CheckCouponViewModel();
            var iuser = await _context.Users.FindAsync(coupon.CreateId);
            string ilabel = "callout callout-warning";
            string iheader = "الكوبون غير موجود";

            if (coupon != null)
            {
                ilabel = "callout callout-success";
                model.StudentIndex = coupon.StudentIndex;
                model.CenterDiscount = coupon.CenterDiscount;
                model.CenterFee = coupon.CenterFee;
                model.CenterValue = coupon.CenterValue;
                model.Discount = coupon.Discount;
                model.LecturePrice = coupon.LecturePrice;
                model.Paided = coupon.Paided;
                model.Printed = coupon.Printed;
                model.ServiceFee = coupon.ServiceFee;
                model.teacherDiscount = coupon.teacherDiscount;
                model.TeacherFee = coupon.TeacherFee;
                model.TeacherValue = coupon.TeacherValue;

                var student = await _context.Users.FindAsync(coupon.StudentID);
                model.StudentName = student.FullName;
                model.Studentmobile = student.UserName;

                if (coupon.LectureType == 0)
                {
                    var schedule = await _context.CGroupSchedule.FindAsync(coupon.scheduleId);
                    var group = await _context.CGroups.FindAsync(schedule.GroupId);
                    model.LectureName = group.GroupName;
                    model.TeacherName = group.TeacherName;
                    iheader = group.GroupName;
                    model.LectureType = "مجموعة";
                }
                else
                {
                    if (coupon.LectureType == 1)
                    {
                        model.LectureType = "مراجعة";
                    }
                    else if (coupon.LectureType == 2)
                    {
                        model.LectureType = "أمتحان";
                    }
                    var schedule = await _context.ReviewsSchedule.FindAsync(coupon.scheduleId);
                    var teacher = await _context.Users.FindAsync(schedule.TeacherId);
               
                    model.LectureName = schedule.ReviewsScheduleName;
                    model.TeacherName = teacher.FullName;
                    iheader = schedule.ReviewsScheduleName; 
                }

            }
            ViewData["ilabel"] = ilabel;
            ViewData["iheader"] = iheader;
            ViewData["iuser"] = iuser.FullName;
            if(coupon.Deleted== null)
            {
                ViewData["Approved"] = 0;
            }
            else
            {
                ViewData["Approved"] = coupon.Deleted;
            }
            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
          

            return View(model);
        }



        // Checkcard
        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser")]
        public async Task<IActionResult> ApproveCoupon()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = " اعتماد كوبون ";

            return View();
        }

        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser")]
        public async Task<IActionResult> ApprovedCoupon(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = "   اعتماد كوبون";
            StudentLecture? coupon = await _context.StudentLecture.FindAsync(id);
            CheckCouponViewModel model = new CheckCouponViewModel();
            var iuser = await _context.Users.FindAsync(coupon.CreateId);
            string ilabel = "callout callout-warning";
            string iheader = "الكوبون غير موجود";

            if (coupon != null)
            {
                ilabel = "callout callout-success";
                model.StudentIndex = coupon.StudentIndex;
                model.CenterDiscount = coupon.CenterDiscount;
                model.CenterFee = coupon.CenterFee;
                model.CenterValue = coupon.CenterValue;
                model.Discount = coupon.Discount;
                model.LecturePrice = coupon.LecturePrice;
                model.Paided = coupon.Paided;
                model.Printed = coupon.Printed;
                model.ServiceFee = coupon.ServiceFee;
                model.teacherDiscount = coupon.teacherDiscount;
                model.TeacherFee = coupon.TeacherFee;
                model.TeacherValue = coupon.TeacherValue;

                var student = await _context.Users.FindAsync(coupon.StudentID);
                model.StudentName = student.FullName;
                model.Studentmobile = student.UserName;

                if (coupon.LectureType == 0)
                {
                    var schedule = await _context.CGroupSchedule.FindAsync(coupon.scheduleId);
                    var group = await _context.CGroups.FindAsync(schedule.GroupId);
                    model.LectureName = group.GroupName;
                    model.TeacherName = group.TeacherName;
                    iheader = group.GroupName;
                    model.LectureType = "مجموعة";
                }
                else
                {
                    if (coupon.LectureType == 1)
                    {
                        model.LectureType = "مراجعة";
                    }
                    else if (coupon.LectureType == 2)
                    {
                        model.LectureType = "أمتحان";
                    }
                    var schedule = await _context.ReviewsSchedule.FindAsync(coupon.scheduleId);
                    var teacher = await _context.Users.FindAsync(schedule.TeacherId);

                    model.LectureName = schedule.ReviewsScheduleName;
                    model.TeacherName = teacher.FullName;
                    iheader = schedule.ReviewsScheduleName;
                }

            }
            ViewData["ilabel"] = ilabel;
            ViewData["iheader"] = iheader;
            ViewData["iuser"] = iuser.FullName;
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
            ViewData["Approved"] = coupon.Deleted;
            return View(model);
        }

        // CancelApproveCoupon

        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser")]
        public async Task<IActionResult> CancelApproveCoupon()
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = "إلغاء اعتماد كوبون ";
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            bool isaccess = await Isaccessright(user.Id, 1);
            if (!isaccess)
            {
                return RedirectToRoute(new { area = "scp", controller = "Dashboard", action = "Index" });
            }

            return View();
        }

        [Authorize(Roles = "AdminSettings,PowerUser,SuperUser")]
        public async Task<IActionResult> CanceledApproveCoupon(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = "   اعتماد كوبون";
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            bool isaccess = await Isaccessright(user.Id, 1);
            if (!isaccess)
            {
                return RedirectToRoute(new { area = "scp", controller = "Dashboard", action = "Index" });
            }
            StudentLecture? coupon = await _context.StudentLecture.FindAsync(id);
            CheckCouponViewModel model = new CheckCouponViewModel();
            var iuser = await _context.Users.FindAsync(coupon.CreateId);
            string ilabel = "callout callout-warning";
            string iheader = "الكوبون غير موجود";

            if (coupon != null)
            {
                ilabel = "callout callout-danger";
                model.StudentIndex = coupon.StudentIndex;
                model.CenterDiscount = coupon.CenterDiscount;
                model.CenterFee = coupon.CenterFee;
                model.CenterValue = coupon.CenterValue;
                model.Discount = coupon.Discount;
                model.LecturePrice = coupon.LecturePrice;
                model.Paided = coupon.Paided;
                model.Printed = coupon.Printed;
                model.ServiceFee = coupon.ServiceFee;
                model.teacherDiscount = coupon.teacherDiscount;
                model.TeacherFee = coupon.TeacherFee;
                model.TeacherValue = coupon.TeacherValue;

                var student = await _context.Users.FindAsync(coupon.StudentID);
                model.StudentName = student.FullName;
                model.Studentmobile = student.UserName;

                if (coupon.LectureType == 0)
                {
                    var schedule = await _context.CGroupSchedule.FindAsync(coupon.scheduleId);
                    var group = await _context.CGroups.FindAsync(schedule.GroupId);
                    model.LectureName = group.GroupName;
                    model.TeacherName = group.TeacherName;
                    iheader = group.GroupName;
                    model.LectureType = "مجموعة";
                }
                else
                {
                    if (coupon.LectureType == 1)
                    {
                        model.LectureType = "مراجعة";
                    }
                    else if (coupon.LectureType == 2)
                    {
                        model.LectureType = "أمتحان";
                    }
                    var schedule = await _context.ReviewsSchedule.FindAsync(coupon.scheduleId);
                    var teacher = await _context.Users.FindAsync(schedule.TeacherId);

                    model.LectureName = schedule.ReviewsScheduleName;
                    model.TeacherName = teacher.FullName;
                    iheader = schedule.ReviewsScheduleName;
                }

            }
            ViewData["ilabel"] = ilabel;
            ViewData["iheader"] = iheader;
            ViewData["iuser"] = iuser.FullName;
          

            if (coupon.Deleted != 0)
            {
                coupon.Deleted = 0;
                coupon.DeleteId = user.Id;
                coupon.DeletedDate = DateTime.Now;
                _context.Update(coupon);
                await _context.SaveChangesAsync();
            }
            ViewData["Approved"] = coupon.Deleted;
            return View(model);
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
