using StudentWebAPI.Model;

namespace StudentWebAPI.Repository.IRepository
{
	public interface ISubjectRepository : IRepository<Subject>
	{
		Task<Subject> UpdateAsync(Subject entity);
	}
}
