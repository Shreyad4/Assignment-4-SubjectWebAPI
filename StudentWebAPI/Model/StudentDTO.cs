using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentWebAPI.Model
{
    public class StudentDTO
    {
        public static IEnumerable<StudentDTO> StudentList { get; internal set; }

        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int StudID { get; set; }
        
        [Required]
        
        public string Name { get; set; }
        
        public int Class { get; set; }
        
        public string Address { get; set; }
    }
}
