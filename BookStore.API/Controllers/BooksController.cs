using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.API.Data;
using BookStore.API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository )
        {
            _bookRepository = bookRepository;
        }
        [Authorize]
        [HttpGet("")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books =await _bookRepository.GetAllBooksAsync();
            return Ok(books);
        }
        //Comment7 and added featur1 branch
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            try
            {
                var book = await _bookRepository.GetBookByIdAsync(id);
                if (book == null)
                {
                    return NotFound();
                }
                return Ok(book);
            }
            catch (Exception)
            {
                return NotFound("Catch Block");
            }  
        }

        [HttpPost("addbook")]
        public async Task<IActionResult> AddBook(Books bookObj)
        {
            try
            {
                var id =await _bookRepository.AddBookAsync(bookObj.Title, bookObj.Description);
                return CreatedAtAction(nameof(GetBookById), new { id = id, Controller = "books" }, id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookPatch([FromBody]Books bookObj, [FromRoute] int id)
        {
            try
            {
                bool result = await _bookRepository.UpdateBookByIdAsync(id, bookObj);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[HttpPatch("{id}")]
        //public async Task<IActionResult> UpdateBook([FromBody]Books bookObj, [FromRoute] int id)
        //{
        //    try
        //    {
        //        bool result = await _bookRepository.UpdateBookByIdAsync(id, bookObj);
        //        return Ok(result);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            try
            {
                bool result = await _bookRepository.DeleteBookAync(id);
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}