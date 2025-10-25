using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models
{
    public class TeacherAssistant : MasterModel
    {
        [Key]
        public int TeacherAssistantId { get; set; }
        public int TeacherId { get; set; }
        public int AssistantId { get; set; }
    }
}
