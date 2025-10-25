using JeCenterWeb.Data;
using JeCenterWeb.Models.ViewModel;
using JeCenterWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using UAParser;
using Microsoft.Identity.Client;

namespace JeCenterWeb.Controllers;

[AllowAnonymous]
public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<AccountController> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public const string SessionKeyName = "_Name";
    public const string SessionKeyPhase = "_phase";
    public const string SessionKeyPhysicalyear = "_physicalyear";
    public const string SessionKeyId = "_id";
    public AccountController(ApplicationDbContext context,
      UserManager<ApplicationUser> userManager,
      SignInManager<ApplicationUser> signInManager,
      ILogger<AccountController> logger ,
        IHttpContextAccessor httpContextAccessor)

    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }



    [AllowAnonymous]
    [Route("Login")]
    [HttpGet]
    public IActionResult Login()
    {
        /// Get Mac Address
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        string sMacAddress = string.Empty;
        string sIpAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
        string sPath = Request.Path;
        string os = Environment.OSVersion.ToString();
        var userAgent = HttpContext.Request.Headers["User-Agent"];
        var uaParser = Parser.GetDefault();
        ClientInfo c = uaParser.Parse(userAgent);
        string browsr = c.UserAgent.Family;
        string device = c.Device.Family;


        //       string Browser = Request.Browser.Browser.ToString();
        //       string Version = Request.Browser.Version.ToString();

        foreach (NetworkInterface adapter in nics)
        {
            if (sMacAddress == String.Empty)// only return MAC Address from first card  
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                sMacAddress = adapter.GetPhysicalAddress().ToString();

            }
        }

        ViewData["Mac"] = sMacAddress;
        ViewData["Ip"] = sIpAddress;
        ViewData["Path"] = sPath;
        ViewData["os"] = os;
        ViewData["browsr"] = browsr;
        ViewData["device"] = device;
        bool isAuthenticated = User.Identity.IsAuthenticated;
        if (isAuthenticated == true)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        //if (ModelState.IsValid)
        //{

        var user = await _context.Users.Where(u=>u.UserName == model.Mobile).FirstOrDefaultAsync();
        if (user != null)
        {
            if (user.Active == false)
            {
                ModelState.AddModelError(string.Empty, " مستخدم غير صالح ");
            }
            else
            {
                var result = await _signInManager.PasswordSignInAsync(model.Mobile, model.Password, true, false);
                if (result.Succeeded)
                {

                    //string returnUrl = HttpContext.Request.Query["returnUrl"];
                    //if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    //    return Redirect(returnUrl);
                    //else
                        return RedirectToAction("signin", "Account");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, " المستخدم أو كلمة المرور غير صحيحة رجاء الاعادة ");
                }
            }
        }
        else
        {
            ModelState.AddModelError(string.Empty, " بيانات المستخدم غير صحيحة رجاء الاعادة ");
        }
           
        //}
        return View(model);
    }

    public async Task<IActionResult> signin()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        ApplicationUser user = await _userManager.FindByIdAsync(userId);
       

        if (user.UserTypeId == 1)
        {
            return RedirectToRoute(new { area = "scp", controller = "Dashboard", action = "Index" });
        }
        else if (user.UserTypeId < 5)
        {
            return RedirectToRoute(new { area = "scp", controller = "Branch", action = "LogInBranch" });
        }
        else if (user.UserTypeId == 7)
        {
            return RedirectToRoute(new { controller = "Dashboard", action = "Index", area = "learning" });
            //if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyPhase)))
            //{
            //    HttpContext.Session.SetInt32(SessionKeyPhase, user.PhaseId);
            //}
            //if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyName)))
            //{
            //    HttpContext.Session.SetString(SessionKeyName, user.FullName);
            //    HttpContext.Session.SetInt32(SessionKeyId, user.Id);
            //    //HttpContext.Session.SetInt32(SessionKeyPhase, user.PhaseId);
            //}
            //string username = HttpContext.Session.GetString(SessionKeyName);
            //var studentapp = await _context.StudentApplications
            //    .Where(s => s.StudentId == user.Id)
            //    .OrderByDescending(s => s.ApplicationId).FirstOrDefaultAsync();
            //if (studentapp != null)
            //{
            //    HttpContext.Session.SetInt32(SessionKeyPhase, studentapp.PhaseId);
            //    HttpContext.Session.SetInt32(SessionKeyPhysicalyear, studentapp.PhysicalyearId);
            //    return RedirectToRoute(new { controller = "Dashboard", action = "Index", area = "learning" });
            //}
            //else
            //{ 
            //    HttpContext.Session.SetInt32(SessionKeyPhase, 0);
            //    HttpContext.Session.SetInt32(SessionKeyPhysicalyear, 0);
            //    // SessionKeyPhysicalyear
            //    return RedirectToRoute(new { controller = "Applications", action = "Index", area = "learning" });
            //}
           
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Route("Register")]
    [AllowAnonymous]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [Route("Register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([Bind("FristName,SecondName,LastName,FamilyName,Mobile,Password,ConfirmPassword,Address,NationalID,Birthdate,School,GenderId,PhaseId,ParentJob,ParentMobile")] RegisterViewModel registerViewModel)
    {
        string imgurl = "images/Students/default.jpg";
        ViewData["PhaseId"] = new SelectList(_context.CPhases.Where(p => p.Parent > 0 && p.Active == true), "PhaseId", "PhaseName");
      
        if (ModelState.IsValid)
        {
            var student = new ApplicationUser();

            student.Email = "s" + registerViewModel.Mobile + "@elra3ycenter.com" ;
            student.UserName = registerViewModel.Mobile;
            student.Mobile = registerViewModel.Mobile;
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
            student.DepartmentId = 0;
            student.UserTypeId = 7;
            student.ParentMobile = registerViewModel.ParentMobile;
            student.Pwd = registerViewModel.Password;
            student.imgurl = imgurl;
            student.Active = true;
            student.CreateId = 0;
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
                    CreateId = student.Id,
                    CreatedDate = DateTime.Now,
                    // UserId = user.Id,
                };

                _context.Add(financialAccount);
                await _context.SaveChangesAsync();
                int FinancialAccountId = financialAccount.FinancialAccountId;

                var user = await _context.Users.FindAsync(student.Id);
                user.AccountID = FinancialAccountId;

                _context.Update(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("logout");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        else
        {
            return View(registerViewModel);
        }
        return View();
    }


    [HttpGet]
    [Route("Logout")]
    public async Task<IActionResult> logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }
    
    

}
