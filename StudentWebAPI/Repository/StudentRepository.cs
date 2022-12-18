using Microsoft.EntityFrameworkCore;
using StudentWebAPI.Data;
using StudentWebAPI.Model;
using StudentWebAPI.Repository.IRepository;

namespace StudentWebAPI.Repository
{
	public class StudentRepository :Repository<Student> ,IStudentRepository
	{
		private readonly AppDBContext _db;
		
		public StudentRepository(AppDBContext db) : base(db)
		{
			_db = db;
		}

		public async Task<Student> UpdateAsync(Student entity)
		{
			_db.Studentdata.Update(entity);
			await _db.SaveChangesAsync();
			return entity;

		}
	}
}
