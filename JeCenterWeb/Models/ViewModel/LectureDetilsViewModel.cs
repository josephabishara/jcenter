namespace JeCenterWeb.Models.ViewModel
{
    public class LectureDetilsViewModel
    {
        public int LectureId { get; set; }
        public int AttendCount { get; set; }
        public int AbsentCount { get; set; }
        public int DiscountCount { get; set; }
        public  bool  Closed { get; set; }
        public bool Paided { get; set; }
    }
}
