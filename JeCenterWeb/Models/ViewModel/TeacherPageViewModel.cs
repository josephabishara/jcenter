namespace JeCenterWeb.Models.ViewModel
{
    public class TeacherPageViewModel
    {
        public ApplicationUser? Teacher { get; set; }
        public IEnumerable<CommentsViewModel>? CommentsViewModel { get; set; }
        public decimal TeacherTotal { get; set; }
        public int TeacherGroup { get; set; }
        public int TeacherStudent { get; set; }
        public int TeacherCards { get; set; }
        public decimal TeacherCardsAmount { get; set; }
        public int TeacherSyllabus { get; set; }
        public int TeacherAssistant { get; set; }
        public int TeacherDiscount { get; set; }
        public int TeacherReviews { get; set; }
        public int TeacherExams { get; set; }
        public int TeacherVideos { get; set; }


    }
}
