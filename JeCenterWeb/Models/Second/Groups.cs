using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models.Second
{
    public class Groups
    {
        // GroupID, GroupnName, TeacherID, curriculumID, GroupNo, GroupscheduleID, Dayofweek, Timefrom, 
      //  Timeto, roomid, lecturePrice, GroupDate, centerprofit, teacherprofit, centerservice, 
        //                 physicalyearID

        [Key]
        public int GroupID { get; set; }
        public string GroupnName { get; set; }
        public int TeacherID { get; set; }
        public int curriculumID { get; set; }
        public int GroupNo { get; set; }
        public int GroupscheduleID { get; set; }
        public int Dayofweek { get; set; }
        public int Timefrom { get; set; }
        public int Timeto { get; set; }
       
        public int roomid { get; set; }
        public int lectureno { get; set; }
      //  public int countlectuere { get; set; }
        public decimal lecturePrice { get; set; }
        public DateTime GroupDate { get; set; }
        public decimal centerprofit { get; set; }
        public decimal teacherprofit { get; set; }
        public decimal centerservice { get; set; }

        public int physicalyearID { get; set; }
    }
}
