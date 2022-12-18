using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentWebAPI.Model
{
	public class StudentCreateDTO
	{
		
		[Required]
		public string Name { get; set; }

		public int Class { get; set; }

		public string Address { get; set; }
	}
}
