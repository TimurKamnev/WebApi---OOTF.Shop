using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOTF.Shopping.Context;
using OOTF.Shopping.Models;
using System.Data;

namespace OOTF.Shopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;

        public ShopController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shop>>> GetShops()
        {
            return await _context.Shop.ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Shop>> GetShop(int id)
        {
            var shop = await _context.Shop.FindAsync(id);

            if (shop == null)
            {
                return NotFound();
            }

            return shop;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShop(int id, Shop shop)
        {
            if (id != shop.Id)
            {
                return BadRequest();
            }

            _context.Entry(shop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShopExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Shop>> PostShop(Shop shop)
        {
            _context.Shop.Add(shop);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShop), new { id = shop.Id }, shop);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteShop(int id)
        {
            var shop = await _context.Shop.FindAsync(id);
            if (shop == null)
            {
                return NotFound();
            }

            _context.Shop.Remove(shop);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShopExists(int id)
        {
            return _context.Shop.Any(e => e.Id == id);
        }
    }
}
