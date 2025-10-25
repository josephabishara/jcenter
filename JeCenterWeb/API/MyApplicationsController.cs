using JeCenterWeb.Data;
using JeCenterWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JeCenterWeb.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyApplicationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public MyApplicationsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetMyApplications()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int studentId = Convert.ToInt32(userId);
            var apps = await _context.StudentApplications
                .Where(a => a.StudentId == studentId)
                .Include(a => a.PhysicalYear)
                .Include(a => a.CPhases)
                .ToListAsync();
            return Ok(apps);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationDetails(int id)
        {
            var application = await _context.StudentApplications
                .Include(a => a.PhysicalYear)
                .Include(a => a.CPhases)
                .FirstOrDefaultAsync(a => a.ApplicationId == id);
            if (application == null)
            {
                return NotFound();
            }
            return Ok(application);
        }
        [HttpPost]
        public async Task<IActionResult> Subscribe([FromBody] StudentApplications application)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int studentId = Convert.ToInt32(userId);
                application.StudentId = studentId;
                _context.StudentApplications.Add(application);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetApplicationDetails), new { id = application.ApplicationId }, application);
            }
            return BadRequest(ModelState);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApplication(int id, [FromBody] StudentApplications application)
        {
            if (id != application.ApplicationId)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                _context.Entry(application).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            var application = await _context.StudentApplications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            _context.StudentApplications.Remove(application);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("balance")]
        public async Task<IActionResult> GetUserBalance()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser student = await _userManager.FindByIdAsync(userId);
            var userbalance = await _context.UsersBalance.FindAsync(student.AccountID);
            if (userbalance == null)
            {
                return NotFound();
            }
            return Ok(userbalance.Balance * -1);
        }
        [HttpGet("user")]
        public async Task<IActionResult> GetUserName()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser student = await _userManager.FindByIdAsync(userId);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student.FristName);
        }
        [HttpGet("branches")]
        public async Task<IActionResult> GetBranches()
        {
            var branches = await _context.CBranch.ToListAsync();
            if (branches == null)
            {
                return NotFound();
            }
            return Ok(branches);
        }

        [HttpGet("phases")]
        public async Task<IActionResult> GetPhases()
        {
            var phases = await _context.CPhases.Where(p => p.Parent > 0 && p.Active == true).ToListAsync();
            if (phases == null)
            {
                return NotFound();
            }
            return Ok(phases);
        }


        [HttpGet("years")]
        public async Task<IActionResult> GetYears()
        {
            var years = await _context.PhysicalYear.Where(p => p.Active == true).ToListAsync();
            if (years == null)
            {
                return NotFound();
            }
            return Ok(years);
        }

        [HttpGet("groups")]
        public async Task<IActionResult> GetGroupsById(int Id)
        {
            var groups = await _context.CGroups.Where(g=>g.GroupId == Id).ToListAsync();
            if (groups == null)
            {
                return NotFound();
            }
            return Ok(groups);
        }

        [HttpGet("groups/{id}")]
        public async Task<IActionResult> GetGroupsBySyllabusId(int id)
        {
            var groups = await _context.CGroups.Where(g => g.SyllabusID == id).ToListAsync();
            if (groups == null)
            {
                return NotFound();
            }
            return Ok(groups);
        }

        [HttpGet("syllabus")]
        public async Task<IActionResult> GetSyllabusById(int Id)
        {
            var syllabus = await _context.CSyllabus.Where(s => s.SyllabusID == Id).ToListAsync();
            if (syllabus == null)
            {
                return NotFound();
            }
            return Ok(syllabus);
        }
        [HttpGet("syllabus/{id}")]
        public async Task<IActionResult> GetSyllabusByPhaseId(int id)
        {
            var syllabus = await _context.CSyllabus.Where(s => s.PhaseID == id).ToListAsync();
            if (syllabus == null)
            {
                return NotFound();
            }
            return Ok(syllabus);
        }



    }
}
