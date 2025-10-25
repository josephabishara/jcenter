namespace JeCenterWeb.Models.ViewModel
{
    public class StudentDetailsViewModel
    {
        public ApplicationUser student { get; set; }
        public IEnumerable<CommentsViewModel>? CommentsViewModel { get; set; }
        public IEnumerable<CommentsViewModel>? ParentCommentsViewModel { get; set; }
    }
}
