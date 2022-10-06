using BookStore.API.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;

        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<List<Books>> GetAllBooksAsync()
        {
            var records =await _context.Books.ToListAsync();
            return records;
        }

        public async Task<Books> GetBookByIdAsync(int id)
        {
            var record = await _context.Books.Where(x => x.Id == id).FirstOrDefaultAsync();
            return record;
        }

        public async Task<int> AddBookAsync(string title, string desc)
        {
            try
            {
                var book = new Books();
                book.Title = title;
                book.Description = desc;
                
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                return book.Id;
            }
            catch (Exception)
            {
                return 0;
            }
            
        }
        public async Task<bool> UpdateBookByIdAsync(int id, Books bookObj)
        {
            //var book = await _context.Books.FindAsync(id);
            //if (book != null)
            //{
            //    book.Title = bookObj.Title;
            //    book.Description = bookObj.Description;
            //    await _context.SaveChangesAsync();
            //}

            var book = new Books()
            {
                Id = id,
                Title = bookObj.Title,
                Description = bookObj.Description
            };
            if (book != null)
            {
                _context.Books.Update(book);
                await _context.SaveChangesAsync();
                return true;
            }
            return false; 

        }

        //public async Task<bool> UpdateBookPatchAsync (int id, Books bookObj)
        //{
        //    var book =await _context.Books.FindAsync(id);
        //    if(book != null)
        //    {
        //        bookObj.ApplyTo(book);
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    return false;
        //}

        public async Task<bool> DeleteBookAync(int id)
        {
            try
            {
                var book = new Books() { Id = id };
                _context.Remove(book);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}
