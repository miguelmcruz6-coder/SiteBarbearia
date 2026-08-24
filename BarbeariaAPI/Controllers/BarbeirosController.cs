using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BarbeariaAPI.Data;
using BarbeariaAPI.Models;

namespace BarbeariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarbeirosController : ControllerBase
    {
        private readonly BarbeariaContext _context;

        public BarbeirosController(BarbeariaContext context)
        {
            _context = context;
        }

        // GET: api/barbeiros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Barbeiro>>> GetBarbeiros()
        {
            return await _context.Barbeiros.ToListAsync();
        }

        // GET: api/barbeiros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Barbeiro>> GetBarbeiro(int id)
        {
            var barbeiro = await _context.Barbeiros.FindAsync(id);

            if (barbeiro == null)
            {
                return NotFound();
            }

            return barbeiro;
        }

        // POST: api/barbeiros
        [HttpPost]
        public async Task<ActionResult<Barbeiro>> PostBarbeiro(Barbeiro barbeiro)
        {
            _context.Barbeiros.Add(barbeiro);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetBarbeiro),
                new { id = barbeiro.Id },
                barbeiro
            );
        }

        // PUT: api/barbeiros/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBarbeiro(
            int id,
            Barbeiro barbeiro)
        {
            if (id != barbeiro.Id)
            {
                return BadRequest();
            }

            _context.Entry(barbeiro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BarbeiroExiste(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // DELETE: api/barbeiros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBarbeiro(int id)
        {
            var barbeiro = await _context.Barbeiros.FindAsync(id);

            if (barbeiro == null)
            {
                return NotFound();
            }

            _context.Barbeiros.Remove(barbeiro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BarbeiroExiste(int id)
        {
            return _context.Barbeiros.Any(e => e.Id == id);
        }
    }
}