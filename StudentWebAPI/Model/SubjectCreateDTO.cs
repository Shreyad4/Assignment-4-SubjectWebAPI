using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentWebAPI.Model
{
	public class SubjectCreateDTO
	{

		[Required]
		public int SubID { get; set; }
		[Required]
		public int StudID { get; set; }
		public string SubName { get; set; }
		public int SubMaxMarks { get; set; }
		public int SubMarks1 { get; set; }
		public int SubMarks2 { get; set; }
		public int SubMarks3 { get; set; }
	}
}
