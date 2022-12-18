using Microsoft.EntityFrameworkCore;
using StudentWebAPI.Data;
using StudentWebAPI.Model;
using StudentWebAPI.Repository.IRepository;

namespace StudentWebAPI.Repository
{
	public class SubjectRepository :Repository<Subject> ,ISubjectRepository
	{
		private readonly AppDBContext _db;
		
		public SubjectRepository(AppDBContext db) : base(db)
		{
			_db = db;
		}

		public async Task<Subject> UpdateAsync(Subject entity)
		{
			_db.Subjectdata.Update(entity);
			await _db.SaveChangesAsync();
			return entity;

		}
	}
}
