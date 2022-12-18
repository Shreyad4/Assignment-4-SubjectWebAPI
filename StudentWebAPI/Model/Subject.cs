using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentWebAPI.Model
{
	public class Subject
	{
		[Key]
		public int SubID { get; set; }
		[ForeignKey("Student")]
		public int StudID { get; set; }
		public Student Student { get; set; }

		public string SubName { get; set; }
		public int SubMaxMarks { get; set; }
		public int SubMarks1 { get; set;}
		public int SubMarks2 { get; set; }
		public int SubMarks3 { get; set;}
	}
}
