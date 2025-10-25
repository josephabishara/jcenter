using System.ComponentModel.DataAnnotations.Schema;

namespace JeCenterWeb.Models
{
    public class MasterModel
    {
        public int? Writed { get; set; }
        public int? Deleted { get; set; }
        public bool Active { get; set; }
        public int? CreateId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdateId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? DeleteId { get; set; }
        public DateTime? DeletedDate { get; set; }

        [NotMapped]
        public bool IsSelected { get; set; }
    }
}
