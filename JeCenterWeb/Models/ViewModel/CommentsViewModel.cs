namespace JeCenterWeb.Models.ViewModel
{
    public class CommentsViewModel
    {
        public int Id { get; set; }
        public string? CreateName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? imgurl { get; set; }
        public string? Note { get; set; }
        public int ParentId { get; set; } = 0;
    }
}
