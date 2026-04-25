using Hegymegi_Kiss_Ákos_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Hegymegi_Kiss_Ákos_backend.Controllers
{
    public class BooksController
    {
        [Route("api/books")]
        [ApiController]
        public class BooksController : ControllerBase
        {
            private readonly LibrarydbContext _context;

            public BooksController(LibrarydbContext context)
            {
                _context = context;
            }

            [HttpGet("feladat10")]
            public async Task<ActionResult> Get()
            {
                var books = await _context.Books.ToListAsync();

                if (books != null)
                {
                    return Ok(books);
                }

                Exception e = new();
                return BadRequest(e.Message);
            }

            [HttpPost("feladat13")]
            public async Task<ActionResult> AddNewBook(string id, Book book)
            {
                var builder = WebApplication.CreateBuilder();
                string uid = builder.Configuration.GetValue<string>("Code");

                if (uid == id)
                {
                    var bk = new Book
                    {

                        BookId = book.BookId,
                        Title = book.Title,
                        PublishDate = book.PublishDate,
                        AuthorId = book.AuthorId,
                        CategoryId = book.CategoryId
                    };

                    if (bk != null)
                    {
                        await _context.Books.AddAsync(bk);
                        await _context.SaveChangesAsync();
                        return StatusCode(201, "Könyv hozzáadása sikeresen megtörtént.");
                    }

                    Exception e = new();
                    return BadRequest(e.Message);
                }

                return StatusCode(401, "Nincs jogusltsága új könyv felviteléhez.");
            }

        }

    }
}
