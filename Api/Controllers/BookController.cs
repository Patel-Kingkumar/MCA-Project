using Api.Repository.BookRepository;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Api.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookRepository _BookRepository;
        public BookController(IBookRepository bookRepository)
        {
            _BookRepository = bookRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetBooks()
        {
            try
            {
                return Ok(await _BookRepository.GetBooks());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetBooks(int id)
        {
            try
            {
                var result = await _BookRepository.GetBooks(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }


        [HttpGet("GetMyBooks/{sid}")]
        public async Task<ActionResult> GetMyBooks(int sid)
        {
            try
            {
                var result = await _BookRepository.GetMyBooks(sid);
                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Books>> DeleteMyBook(int id)
        {
            try
            {
                var bookDelete = await _BookRepository.GetBooks(id);
                if (bookDelete != null)
                {
                    var book = await _BookRepository.DeleteMyBooks(id);
                    return book;
                }
                return null;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }


        [HttpPost]
        public async Task<ActionResult> AddBooks(Books book)
        {
            try
            {
                if (book == null)
                {
                    return BadRequest();
                }
                var CreateBook = await _BookRepository.AddBooks(book);
                return CreatedAtAction(nameof(AddBooks), new { id = CreateBook.Id }, CreateBook);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Books>> UpdateBook(int id, Books book)
        {
            try
            {
                if (id != book.Id)
                {
                    return BadRequest("Id is not match");
                }
                var booktUpdated = await _BookRepository.GetBooks(id);
                if (booktUpdated == null)
                {
                    return NotFound($"Book {id} is not found");
                }
                //booktUpdated.Quantity = book.Quantity + 1;
                var UpdatedBook = await _BookRepository.UpdateBooks(book);
                return UpdatedBook;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Books>> DeleteBook(int id)
        {
            try
            {
                var bookDelete = await _BookRepository.GetBooks(id);

                if (bookDelete != null)
                {
                    var book = await _BookRepository.DeleteMyBooks(id);
                    return book;
                }
                return null;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }


        [HttpGet("{Search}")]
        public async Task<ActionResult<IEnumerable<Books>>> Search(string Title)        
        {
            try
            {
                var result = await _BookRepository.Search(Title);
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
        //[HttpGet("{SearchPublisher}")]
        //public async Task<ActionResult<IEnumerable<Books>>> SearchPublisher(string Publisher)
        //{
        //    try
        //    {
        //        var result = await _BookRepository.SearchPublisher(Publisher);
        //        if (result.Any())
        //        {
        //            return Ok(result);
        //        }
        //        return NotFound();
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
        //    }
        //}
    }

}