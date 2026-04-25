using Hegymegi_Kiss_Ákos_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hegymegi_Kiss_Ákos_backend.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibrarydbContext _context;
        private readonly IConfiguration _configuration;

        public BooksController(LibrarydbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // ============================================
        // 10. FELADAT: összes könyv
        // GET https://localhost:7080/api/books/feladat10
        // ============================================
        [HttpGet("feladat10")]
        public async Task<ActionResult> Get()
        {
            try
            {
                var books = await _context.Books.ToListAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // ============================================
        // 13. FELADAT: új könyv felvitele UID alapján
        // POST https://localhost:7080/api/books/feladat13?uid=FKB3F4FEA09CE43C
        // Body: { "bookId": 0, "title": "...", "publishDate": "...", "authorId": 0, "categoryId": 0 }
        // ============================================
        [HttpPost("feladat13")]
        public async Task<ActionResult> AddNewBook([FromQuery] string uid, [FromBody] Book book)
        {
            // UID összehasonlítása a főprogramban tárolt értékkel
            string? storedUid = _configuration["UID"];

            if (uid != storedUid)
            {
                return StatusCode(401, new { message = "Nincs jogosultsága új könyv felvételéhez!" });
            }

            try
            {
                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();
                return StatusCode(201, new { message = "Könyv hozzáadása sikeresen megtörtént." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
