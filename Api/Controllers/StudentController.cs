using Api.Repository.StudentRepository;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _StudentRepository;
        public StudentController(IStudentRepository StudentRepository)
        {
            _StudentRepository = StudentRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetStudents()
        {
            try
            {
                return Ok(await _StudentRepository.GetStudents());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Students>> GetStudents(int id)
        {
            try
            {
                var result = await _StudentRepository.GetStudents(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }
        [HttpPost]
        public async Task<ActionResult> AddStudents(Students student)
        {
            try
            {
                if (student == null)
                {
                    return BadRequest();
                }
                var CreateStudent = await _StudentRepository.AddStudents(student);
                return CreatedAtAction(nameof(AddStudents), new { id = CreateStudent.Id }, CreateStudent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Students>> UpdateStudents(int id,Students student)
        {
            try
            {
                if (id != student.Id)
                {
                    return BadRequest("Id is not match");
                }
                var studentUpdated = await _StudentRepository.GetStudents(id);
                if (studentUpdated == null)
                {
                    return NotFound($"Student {id} is not found");
                }
                var UpdatedStudent = await _StudentRepository.UpdateStudents(student);
                return UpdatedStudent;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Students>> DeleteStudents(int id)
        {
            try
            {

                var studentDelete = await _StudentRepository.GetStudents(id);

                if (studentDelete != null)
                {
                    var student = await _StudentRepository.DeleteStudents(id);
                    return student;
                }
                return null;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }
        [HttpGet("{Search}")]
        public async Task<ActionResult<IEnumerable<Students>>> Search(string FirstName)
        {
            try
            {
                var result = await _StudentRepository.Search(FirstName);
                if (result.Any())
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }
    }
}
