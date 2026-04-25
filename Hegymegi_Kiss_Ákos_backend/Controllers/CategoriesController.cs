using Hegymegi_Kiss_Ákos_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hegymegi_Kiss_Ákos_backend.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly LibrarydbContext _context;

        public CategoriesController(LibrarydbContext context)
        {
            _context = context;
        }

        // ============================================
        // 11. FELADAT: kategóriák + a hozzájuk tartozó könyvek
        // GET https://localhost:7080/api/categories/feladat11
        // ============================================
        [HttpGet("feladat11")]
        public async Task<ActionResult> Get()
        {
            try
            {
                var categories = await _context.Categories
                    .Include(c => c.Books)
                    .ToListAsync();

                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
