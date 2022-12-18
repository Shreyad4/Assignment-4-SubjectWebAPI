using Microsoft.AspNetCore.Mvc;
using StudentWebAPI.Model;
using StudentWebAPI.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using StudentWebAPI.Repository.IRepository;
using Microsoft.VisualBasic;
using System.Net;

namespace StudentWebAPI.Controllers
{
    [Route("api/Student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
       
        protected APIResponse _response;

        private readonly IStudentRepository _dbStudent;
        private readonly IMapper _mapper;

        public StudentController(IStudentRepository dbStudent, IMapper mapper)
        {
            _dbStudent = dbStudent;
            _mapper = mapper;
            this._response = new();
        }

        //GetAllData
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>>GetStudents()
        {
            try
            {
                IEnumerable<Student> studentList = await _dbStudent.GetAllAsync();
                _response.Result = _mapper.Map<List<StudentDTO>>(studentList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess= false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        
        //GET
        [HttpGet("{id:int}",Name="GetStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
       
        public async Task<ActionResult<APIResponse>> GetStudent(int id)
        {
            try
            {
                if (id == 0)
                {
                 //_logger.LogError("Get Student data error with ID" + id);
                    _response.StatusCode=HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var student = await _dbStudent.GetAsync(u => u.StudID == id);
                if (student == null)
                {
					_response.StatusCode = HttpStatusCode.NotFound;
					return NotFound(_response);
                }
                //return Ok(student);
                //return Ok(_mapper.Map<StudentDTO>(student));

                _response.Result = _mapper.Map<StudentDTO>(student);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages
					= new List<string>() { ex.ToString() };
			}
			return _response;


		}

		//CREATE
		[HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateStudentData([FromBody]StudentCreateDTO createDTO)
        {
            try
            {
                if (await _dbStudent.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("CustomError", "Already Exits!!");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest(createDTO);

                }

                Student student = _mapper.Map<Student>(createDTO);
                await _dbStudent.CreateAsync(student);

                _response.Result = _mapper.Map<StudentDTO>(student);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetStudent", new { id = student.StudID }, student);
            }
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages
					= new List<string>() { ex.ToString() };
			}
			return _response;
		}

        //DELETE
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteStudent")]

        public async Task<ActionResult<APIResponse>> DeleteStudent(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();

                }
                var student = await _dbStudent.GetAsync(u => u.StudID == id);
                if (student == null)
                {
                    return NotFound();
                }
              
                await _dbStudent.RemoveAsync(student);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages
					= new List<string>() { ex.ToString() };
			}
			return _response;
		}

        //UPDATE-PUT
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:int}",Name="UpdateStudent")]
        public async Task<ActionResult<APIResponse>> UpdateStudent(int id, [FromBody]StudentUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.StudID)
                {
                    return BadRequest();
                }
               Student model = _mapper.Map<Student>(updateDTO);

                await _dbStudent.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages
					= new List<string>() { ex.ToString() };
			}
			return _response;
		}
    }
}
