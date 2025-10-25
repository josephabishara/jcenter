using System.ComponentModel.DataAnnotations;

namespace JeCenterWeb.Models
{
	public class Branchcolor
	{
		[Key]
        public int BranchcolorId { get; set; }
        public string color { get; set; }
        public string btn { get; set; }
        public string txt { get; set; }
        public string txth { get; set; }
    }
}
