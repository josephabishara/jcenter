using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models
{
    public class ResourceMailbox : MasterModel
    {
        [Key]
        public int MailboxId { get; set; }
        public int MailFrom { get; set; }
        public int MailTo { get; set; }
        public string MailSubject { get; set; }
        public string MailBody { get; set; }
        public DateTime MailDate { get; set; }
        public string AttachUrl { get; set; }
        public bool Sent { get; set; }
        public bool Recived { get; set; }
        public bool SentDeleted { get; set; }
        public bool RecivedDeleted { get; set; }
    }
}
