using JeCenterWeb.Data;
using Microsoft.EntityFrameworkCore;

namespace JeCenterWeb.Models.Repository
{
    public class StudentApplicationsRepository
    {
        private static ApplicationDbContext _context;
        public StudentApplicationsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public static async Task<int> GetApplication(int studentId, int PhysicalyearId)
        {
            int result = 0;
            var app = await _context.StudentApplications
                .Where(a => a.StudentId == studentId && a.PhysicalyearId == PhysicalyearId)
                .AnyAsync();
            if (app == null)
                result = 0;
            else
                result = 1;
            return result;
        }
    }
}
