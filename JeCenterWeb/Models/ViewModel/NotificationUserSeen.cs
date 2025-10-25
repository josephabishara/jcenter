namespace JeCenterWeb.Models.ViewModel
{
    public class NotificationUserSeen
    {
        public int UserId { get; set; } = 0;
        public string? UserName { get; set; }
        public bool Seen { get; set; } = false;
        public DateTime? SeenDate { get; set; }
     
    }
}
