using Api.Repository.AuthorRepository;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/author")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _AuthorRepository;
        public AuthorController(IAuthorRepository AuthorRepository)
        {
            _AuthorRepository = AuthorRepository;
        }
        // GET: AuthorController
        [HttpGet]
        public async Task<ActionResult> GetBooks()
        {
            try
            {
                return Ok(await _AuthorRepository.GetAuthors());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Authors>> GetAuthors(int id)
        {
            try
            {
                var result = await _AuthorRepository.GetAuthors(id);
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
        public async Task<ActionResult> AddAuthors(Authors author)
        {
            try
            {
                if (author == null)
                {
                    return BadRequest();
                }
                var CreateAuthor = await _AuthorRepository.AddAuthors(author);
                return CreatedAtAction(nameof(AddAuthors), new { id = CreateAuthor.Id }, CreateAuthor);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Authors>> UpdateAuthors(int id,Authors author)
        {
            try
            {
                if (id != author.Id)
                {
                    return BadRequest("Id is not match");
                }
                var authorUpdated = await _AuthorRepository.GetAuthors(id);
                if (authorUpdated == null)
                {
                    return NotFound($"Author {id} is not found");
                }
                var UpdatedAuthor = await _AuthorRepository.UpdateAuthors(author);
                return UpdatedAuthor;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Authors>> DeleteAuthors(int id)
        {
            try
            {

                var authorDelete = await _AuthorRepository.GetAuthors(id);

                if (authorDelete != null)
                {
                    var author = await _AuthorRepository.GetAuthors(id);
                    return author;
                }
                return null;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }
        [HttpGet("{Search}")]
        public async Task<ActionResult<IEnumerable<Authors>>> Search(string Name)
        {
            try
            {
                var result = await _AuthorRepository.Search(Name);
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
