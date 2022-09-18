using Api.Repository.BookRepository;
using Api.Repository.BorrowingRepository;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/borrowing")]
    [ApiController]
    public class BorrowingController : Controller
    {
        private readonly IBorrowingRepository _BorrowingRepository;
        private readonly IBookRepository _BookRepository;


        // GET: BorrowingController
        public BorrowingController(IBorrowingRepository BorrowingRepository,IBookRepository BookRepository)
        {
            _BorrowingRepository = BorrowingRepository;
            _BookRepository = BookRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetBorrowings()
        {
            try
            {
                return Ok(await _BorrowingRepository.GetBorrowings());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }

        [HttpGet("GetBorrowings/{bookId}")]
        //[HttpGet("{bookId:int}")]
        public async Task<ActionResult<Borrowings>> GetBorrowings(int bookId)
        {
            try
            {
                var result = await _BorrowingRepository.GetBorrowings(bookId); // sid -> 16
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
        public async Task<ActionResult> AddBorrowings(Borrowings borrowing)
        {
            try
            {
                if (borrowing == null)
                {
                    return BadRequest();
                }
                var CreateBorrowing = await _BorrowingRepository.AddBorrowings(borrowing);
                Books books = new Books();
                books = await _BookRepository.GetBooks(borrowing.BookId);
                books.Quantity -= 1;
                var bookUpdated = await _BookRepository.UpdateBookQuantity(books);
                //return CreatedAtAction(nameof(AddBorrowings), new { id = CreateBorrowing.Id }, CreateBorrowing);
                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }


        [HttpPut("UpdateBorrowings/{bookId}")]
        public async Task<ActionResult<Borrowings>> UpdateBorrowings(int bookId, Borrowings borrowing)
        {
            try
            {
            
                var borrowingtUpdated = await _BorrowingRepository.GetBorrowings(borrowing.BookId); // 16
                Books books = new Books();
                books = await _BookRepository.GetBooks(borrowing.BookId);
                books.Quantity += 1;

                borrowingtUpdated.SubmitDate = borrowing.SubmitDate;
                var bookUpdated = await _BookRepository.UpdateBookQuantity(books);
                if (borrowingtUpdated == null)
                {
                    return NotFound($"Borrowing {borrowing.BookId} is not found");
                }
                //borrowingtUpdated.SubmitDate = borrowing.SubmitDate;
                var UpdatedBorrowing = await _BorrowingRepository.UpdateBorrowings(borrowing);
                return UpdatedBorrowing;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }

        [HttpDelete("DeleteBorrowings/{sid}/{bookid}")]
        public async Task<ActionResult<Borrowings>> DeleteBorrowings(int sid,int bookid)
        {
            try
            {
                var borrowing = await _BorrowingRepository.DeleteBorrowings(sid, bookid);
                Books books = new Books();
                books = await _BookRepository.GetBooks(borrowing.BookId);
                //books.Quantity += 1;
                var bookUpdated = await _BookRepository.UpdateBookQuantity(books);
                return borrowing;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
            //try
            //{
            //    if (borrowings == null)
            //    {
            //        return BadRequest();
            //    }
            //    var CreateBorrowing = await _BorrowingRepository.DeleteBorrowings(sid,bookid);
            //    Books books = new Books();
            //    books = await _BookRepository.GetBooks(borrowings.BookId);
            //    books.Quantity += 1;
            //    var bookUpdated = await _BookRepository.UpdateBookQuantity(books);
            //    //return CreatedAtAction(nameof(AddBorrowings), new { id = CreateBorrowing.Id }, CreateBorrowing);
            //    return Ok();
            //}
            //catch (Exception ex)
            //{

            //    return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            //}
        }
       

    }
}
