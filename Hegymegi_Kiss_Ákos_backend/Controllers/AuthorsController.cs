using Hegymegi_Kiss_Ákos_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hegymegi_Kiss_Ákos_backend.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly LibrarydbContext _context;

        public AuthorsController(LibrarydbContext context)
        {
            _context = context;
        }

        // ============================================
        // 9. FELADAT: szerző + könyvei név alapján
        // GET https://localhost:7080/api/authors/feladat9/Linda%20Wilson
        // ============================================
        [HttpGet("feladat9/{name}")]
        public async Task<ActionResult> Get(string name)
        {
            var author = await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.AuthorName == name);

            if (author == null)
            {
                return NotFound(new { message = "A szerző nem található!" });
            }

            return Ok(author);
        }

        // ============================================
        // 12. FELADAT: szerzők száma
        // GET https://localhost:7080/api/authors/feladat12
        // ============================================
        [HttpGet("feladat12")]
        public async Task<ActionResult> NumOfAuthors()
        {
            try
            {
                var num = await _context.Authors.CountAsync();
                return Ok(new { szerzokSzama = num });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
