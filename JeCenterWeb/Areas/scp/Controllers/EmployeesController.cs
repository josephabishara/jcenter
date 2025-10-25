using JeCenterWeb.Data;
using JeCenterWeb.Models;
using JeCenterWeb.Models.Second;
using JeCenterWeb.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace JeCenterWeb.Areas.scp.Controllers
{
    [Area("scp")]
    [Authorize(Roles = "AdminSettings")]
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<TeachersController> _logger;
        string ControllerName = "المستخدمين";
        string AppName = "الأدمن";
        public EmployeesController(ApplicationDbContext context,
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

        public async Task<IActionResult> Index(int? id)
        {
            ViewData["AppName"] = AppName;
            if (id == 1 || id == null)
                ViewData["ControllerName"] = " سوبر أدمن ";
            else if (id == 2)
                ViewData["ControllerName"] = " أدمن ";
            else if (id == 3)
                ViewData["ControllerName"] = " مشرفين  ";
            else if (id == 4)
                ViewData["ControllerName"] = " خدمة طلبة ";
            else if (id == 5)
                ViewData["ControllerName"] = "مستخدمين غير مفعلين";

            if (id == 1 || id == 2 || id == 3 || id == 4)
            {
                List<ApplicationUser> users = await _userManager.Users
               .Where(t => t.UserTypeId == id)
               .Where(t => t.Active == true)
               .ToListAsync();
                return View(users);
            }
            else if (id == 5)
            {
                List<ApplicationUser> users = await _userManager.Users
                  .Where(t => t.UserTypeId < 5)
                  .Where(t => t.Active == false)
                  .ToListAsync();
                return View(users);
            }
            else if (id == null || id == 0)
            {
                List<ApplicationUser> users = await _userManager.Users
               .Where(t => t.UserTypeId == 4)
               .Where(t => t.Active == true)
               .ToListAsync();
                return View(users);
            }

            return View();
        }

        public async Task<IActionResult> SearchBy(string word)
        {
            ViewData["CurrentFilter"] = word;
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = "بحث عن " + word;
            List<ApplicationUser> users = await _userManager.Users
                 .Where(t => t.UserTypeId < 5 && t.Active == true && t.FullName.Contains(word)).ToListAsync();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            //  ViewData["Gender"] = new SelectList(_context.ResourceGender, "GenderId", "GenderName");
            ViewData["Password"] = CreateRandomPassword(6);
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentUserViewModel studentViewModel)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int uid = Convert.ToInt32(userId);

            if (ModelState.IsValid)
            {
               

                string imgurlpath = "images/StudentUsers/default.png";
                if (studentViewModel.imgurl != null)
                {
                    string folder = "images/StudentUsers/";
                    folder += Guid.NewGuid().ToString() + "-" + studentViewModel.imgurl.FileName;
                    imgurlpath = folder;
                    string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                    await studentViewModel.imgurl.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
                }
                var StudentUser = new ApplicationUser();

                StudentUser.Email = "e"+ studentViewModel.Mobile + "@elra3ycenter.com";
                StudentUser.UserName = studentViewModel.Mobile;
                StudentUser.FullName = studentViewModel.FullName;
                StudentUser.PhoneNumber = studentViewModel.Mobile;
                StudentUser.Mobile = studentViewModel.Mobile;
                StudentUser.imgurl = imgurlpath;
                StudentUser.Pwd = studentViewModel.Password;
                StudentUser.UserTypeId = studentViewModel.UserType;
                StudentUser.DepartmentId = studentViewModel.UserType;
                StudentUser.Active = true;
                StudentUser.AccountID = 0;
                StudentUser.AdvancedPermission = studentViewModel.AdvancedPermission;

                var result = await _userManager.CreateAsync(StudentUser, studentViewModel.Password);
                if (result.Succeeded)
                {
                    // "AdminSettings" = 3, "SuperUser" = 2, "StudentService" = 1 
                    List<string> role = new List<string>();
                    if (studentViewModel.UserType == 1)
                    {
                        role.Add("AdminSettings");
                    }
                    if (studentViewModel.UserType == 2)
                    {
                        role.Add("PowerUser");
                    }
                    if (studentViewModel.UserType == 3)
                    {
                        role.Add("SuperUser");
                    }
                    if (studentViewModel.UserType == 4)
                    {
                        role.Add("User");
                    }

                    await _userManager.AddToRolesAsync(StudentUser, role);


                    FinancialAccount financialAccount = new FinancialAccount
                    {
                        AccountName = studentViewModel.FullName,
                        AccountTypeId = studentViewModel.UserType,
                        Active = true,
                        CreateId = uid,
                        CreatedDate = DateTime.Now,
                        AdvancedPermission = true,
                    };

                    _context.Add(financialAccount);
                    await _context.SaveChangesAsync();
                    int AccountId = financialAccount.FinancialAccountId;

                    var user = await _context.Users.FindAsync(StudentUser.Id);
                    user.AccountID = AccountId;

                    _context.Update(user);
                    await _context.SaveChangesAsync();


                    return RedirectToAction("Index", "Employees");
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
                return View(studentViewModel);
            }
            ViewData["Password"] = CreateRandomPassword(8);
            //ViewData["Gender"] = new SelectList(_context.ResourceGender, "GenderId", "GenderName", addUserViewModel.Gender);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ChangeState(int id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id.ToString());
            FinancialAccount financialAccount = await _context.FinancialAccount.FindAsync(user.AccountID);
            if (user.Active == false)
            {
                user.Active = true;
                financialAccount.Active=true;
            }
            else if (user.Active == true)
            {
                user.Active = false;
                financialAccount.Active=false;
            }
            await _userManager.UpdateAsync(user);

            _context.Update(financialAccount);
            await _context.SaveChangesAsync();

            return RedirectToRoute(new { action = "Index", id = user.UserTypeId });

        }

        [HttpGet]
        public async Task<IActionResult> ChangeRole(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var employee = await _context.Users.FindAsync(id);
            if (employee.UserTypeId == 0)
                return RedirectToAction(nameof(Index));
            else if (employee.UserTypeId > 4)
                return RedirectToAction(nameof(Index));

            ViewData["FullName"] = employee.FullName;
           

            UsersRolesViewModel usersRoles  = new UsersRolesViewModel();
            usersRoles.UserId = id;
            usersRoles.OldRoleId = employee.UserTypeId;
            usersRoles.RoleId = employee.UserTypeId;
            usersRoles.AdvancedPermission = employee.AdvancedPermission;
            return View(usersRoles);
        }

        // Aaccessright

        [HttpPost]
        public async Task<IActionResult> ChangeRole([Bind("UserId,RoleId,AdvancedPermission")] UsersRolesViewModel usersRoles)
        {
            //    StudentUser.AdvancedPermission = studentViewModel.AdvancedPermission;
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ApplicationUser employee = await _userManager.FindByIdAsync(usersRoles.UserId.ToString());
            if (employee.UserTypeId == 0)
                return RedirectToAction(nameof(Index));
            
            employee.UserTypeId = usersRoles.RoleId;
            employee.DepartmentId = usersRoles.RoleId;
            employee.AdvancedPermission= usersRoles.AdvancedPermission;
            var result = _userManager.UpdateAsync(employee).Result;

            if(result.Succeeded)
            {
                var oldrole = await _context.UserRoles.Where(r =>r.UserId ==  usersRoles.UserId).ToListAsync();
                foreach (var item in oldrole)
                {
                    _context.Remove(item);
                    await _context.SaveChangesAsync();
                }
                
                List<string> role = new List<string>();
                if (usersRoles.RoleId == 1)
                {
                    role.Add("AdminSettings");
                }
                if (usersRoles.RoleId == 2)
                {
                    role.Add("PowerUser");
                }
                if (usersRoles.RoleId == 3)
                {
                    role.Add("SuperUser");
                }
                if (usersRoles.RoleId == 4)
                {
                    role.Add("User");
                }

                await _userManager.AddToRolesAsync(employee, role);
            }
           



            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Aaccessright(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var employee = await _context.Users.FindAsync(id);
            //if (employee.UserTypeId != 2)
            //    return RedirectToAction(nameof(Index));

            ViewData["FullName"] = employee.FullName;
            ViewData["UserId"] = employee.Id;
            ViewData["Accessrightid"] = new SelectList(_context.AccessRight, "AccessId", "AccessName");

            var userAccessRight = await _context.UserAccessRight
                .Where(ar => ar.UserId == id)
                .Include(ar => ar.AccessRight)
                .ToListAsync();

            return View(userAccessRight);
        }

        [HttpPost]
        public async Task<IActionResult> AddAccessRight([Bind("UserId,AccessId")] UserAccessRight userAccessRight)
        {
            var accessRight = await _context.UserAccessRight
                .Where(ar=>ar.UserId == userAccessRight.UserId && ar.AccessId == userAccessRight.AccessId)
                .FirstOrDefaultAsync();

            if (accessRight == null)
                {
                accessRight = new UserAccessRight
                {
                    UserId = userAccessRight.UserId,
                    AccessId = userAccessRight.AccessId
                };
                _context.UserAccessRight.Add(accessRight);
                await _context.SaveChangesAsync();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "هذا الصلاحية مضافة بالفعل");
            }

            return RedirectToRoute(new { area = "scp", controller = "Employees", action = "Aaccessright", id = userAccessRight.UserId });

        }

        [HttpPost]
        public async Task<IActionResult> RemoveAccessRight(int id)
        {
            
            var userAccessRight = await _context.UserAccessRight.FindAsync(id);
            int employeeId = userAccessRight.UserId;
            if (userAccessRight != null)
            {
                _context.UserAccessRight.Remove(userAccessRight);
                await _context.SaveChangesAsync();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "هذا الصلاحية غير موجودة");
            }
            return RedirectToRoute(new { area = "scp", controller = "Employees", action = "Aaccessright", id = employeeId });
        }




        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var employee = await _context.Users.FindAsync(id);
            if (employee.UserTypeId == 0)
                return RedirectToAction(nameof(Index));
            else if (employee.UserTypeId > 4)
                return RedirectToAction(nameof(Index));

            ViewData["FullName"] = employee.FullName;
            ViewData["Id"] = employee.Id;
            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var employee = await _context.Users.FindAsync(id);
            if (employee.UserTypeId > 4)
                return RedirectToAction(nameof(Index));
            EditStudentUserViewModel model = new EditStudentUserViewModel
            {
                Id = id,
                Email = employee.Email,
                FullName = employee.FullName,
                Mobile = employee.Mobile
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,Mobile")] EditStudentUserViewModel model)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ApplicationUser employee = await _userManager.FindByIdAsync(id.ToString());
            var acc = await _context.FinancialAccount.FindAsync(employee.AccountID);



            if (employee.UserTypeId > 4)
                return RedirectToAction(nameof(Index));

            employee.Email = model.Email;
            employee.FullName = model.FullName;
            employee.Mobile = model.Mobile;

            var result = _userManager.UpdateAsync(employee).Result;
            if (result.Succeeded)
            {
                acc.AccountName = model.FullName;
                _context.Update(acc);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateImage(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            var employee = await _context.Users.FindAsync(id);
            if (employee.UserTypeId > 4)
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

            if (employee.UserTypeId > 4)
                return RedirectToAction(nameof(Index));

            string imgurlpath = "images/StudentUsers/default.png";
            if (model.imgurl != null)
            {
                string folder = "images/StudentUsers/";
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

        [HttpGet]
        public async Task<IActionResult> ChangePassword(int id)
        {
            ViewData["AppName"] = AppName;
            ViewData["ControllerName"] = ControllerName;
            ViewData["Password"] = CreateRandomPassword(8);
            var employee = await _context.Users.FindAsync(id);
            if (employee.UserTypeId > 4)
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

            if (employee.UserTypeId > 4)
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
    }
}
