using AutoMapper;
using StudentWebAPI.Model;

namespace StudentWebAPI
{
	public class MappingConfig : Profile
	{
		public MappingConfig()
		{ 
			CreateMap<Student, StudentDTO>();
			CreateMap<StudentDTO, Student>();

			CreateMap<Student, StudentCreateDTO>().ReverseMap();
			CreateMap<Student, StudentUpdateDTO>().ReverseMap();

			CreateMap<Subject, SubjectDTO>();
			CreateMap<SubjectDTO, Subject>();

			CreateMap<Subject, SubjectCreateDTO>().ReverseMap();
			CreateMap<Subject, SubjectUpdateDTO>().ReverseMap();
		}
	}
}
