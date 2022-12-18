using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentWebAPI.Model
{
	public class StudentUpdateDTO
	{

		[Required]
		public int StudID { get; set; }

		[Required]

		public string Name { get; set; }
		[Required]
		public int Class { get; set; }
		[Required]
		public string Address { get; set; }
	}
}
