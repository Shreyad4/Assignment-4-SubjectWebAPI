using StudentWebAPI.Model;

namespace StudentWebAPI.Repository.IRepository
{
	public interface IStudentRepository : IRepository<Student>
	{
		Task<Student> UpdateAsync(Student entity);
	}
}
