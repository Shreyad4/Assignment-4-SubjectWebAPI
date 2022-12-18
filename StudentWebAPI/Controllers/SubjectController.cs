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
    [Route("api/Subject")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ISubjectRepository _dbSubject;
        private readonly IStudentRepository _dbStudent;
        private readonly IMapper _mapper;

        public SubjectController(ISubjectRepository dbSubject, IMapper mapper,IStudentRepository dbStudent)
        {
            _dbSubject = dbSubject;
            _mapper = mapper;
            this._response = new();
            _dbStudent = dbStudent;
        }

        //GetAllData
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>>GetSubjects()
        {
            try
            {
                // _logger.LogInformation("Getting all Student data");
                IEnumerable<Subject> subjectList = await _dbSubject.GetAllAsync();
                _response.Result = _mapper.Map<List<SubjectDTO>>(subjectList);
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
        [HttpGet("{id:int}",Name="GetSubject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
    
        public async Task<ActionResult<APIResponse>> GetSubject(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode=HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var subject = await _dbSubject.GetAsync(u => u.SubID == id);
                if (subject == null)
                {
					_response.StatusCode = HttpStatusCode.NotFound;
					return NotFound(_response);
                }
           
                _response.Result = _mapper.Map<SubjectDTO>(subject);
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
        public async Task<ActionResult<APIResponse>> CreateSubjectData([FromBody]SubjectCreateDTO createDTO)
        {
            try
            {
               
                if (await _dbSubject.GetAsync(u => u.SubName.ToLower() == createDTO.SubName.ToLower()) != null)
                {
                    ModelState.AddModelError("CustomError", "Already Exits!!");
                    return BadRequest(ModelState);
                }
                if(await _dbStudent.GetAsync(u=>u.StudID==createDTO.StudID)== null)
                {

					ModelState.AddModelError("CustomError", "StudID Invalid!!");
					return BadRequest(ModelState);
				}
                if (createDTO == null)
                {
                    return BadRequest(createDTO);

                }
           
                Subject subject = _mapper.Map<Subject>(createDTO);

                await _dbSubject.CreateAsync(subject);

                _response.Result = _mapper.Map<SubjectDTO>(subject);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetSubject", new { id = subject.SubID }, subject);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteSubject")]

        public async Task<ActionResult<APIResponse>> DeleteSubject(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();

                }
                var subject = await _dbSubject.GetAsync(u => u.SubID == id);
                if (subject == null)
                {
                    return NotFound();
                }
              
                await _dbSubject.RemoveAsync(subject);
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
        [HttpPut("{id:int}",Name="UpdateSubject")]
        public async Task<ActionResult<APIResponse>> UpdateSubject(int id, [FromBody]SubjectUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.SubID)
                {
                    return BadRequest();
                }

				if (await _dbStudent.GetAsync(u => u.StudID == updateDTO.StudID) == null)
				{

					ModelState.AddModelError("CustomError", "StudID Invalid!!");
					return BadRequest(ModelState);
				}
				
				Subject model = _mapper.Map<Subject>(updateDTO);
                await _dbSubject.UpdateAsync(model);
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
