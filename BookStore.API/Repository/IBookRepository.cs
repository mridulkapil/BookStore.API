using BookStore.API.Data;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Repository
{
    public interface IBookRepository
    {
        Task<List<Books>> GetAllBooksAsync();
        Task<Books> GetBookByIdAsync(int id);
        Task<int> AddBookAsync(string title, string desc);
        Task<bool> UpdateBookByIdAsync(int id, Books bookObj);
        Task<bool> DeleteBookAync(int id);
    }
}
