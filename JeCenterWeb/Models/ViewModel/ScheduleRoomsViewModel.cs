using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.ViewModel
{
    public class ScheduleRoomsViewModel
    {

        public string TeacherName { get; set; }
        public TimeSpan SessionStart { get; set; }
        public TimeSpan SessionEnd { get; set; }
        public int type { get; set; }
    }
}
