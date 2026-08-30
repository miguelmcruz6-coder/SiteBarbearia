using BarbeariaAPI.Data;
using BarbeariaAPI.DTOs;
using BarbeariaAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Barbeiro>>> GetBarbeiros()
        {
            return await _context.Barbeiros.ToListAsync();
        }

        // GET: api/barbeiros/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Barbeiro>> GetBarbeiro(int id)
        {
            var barbeiro = await _context.Barbeiros.FindAsync(id);

            if (barbeiro == null)
            {
                return NotFound("Barbeiro não encontrado.");
            }

            return barbeiro;
        }

        // POST: api/barbeiros
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Barbeiro>> PostBarbeiro(
            BarbeiroDTO barbeiroDTO)
        {
            var barbeiro = new Barbeiro
            {
                Nome = barbeiroDTO.Nome
            };

            _context.Barbeiros.Add(barbeiro);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetBarbeiro),
                new { id = barbeiro.Id },
                barbeiro
            );
        }

        // PUT: api/barbeiros/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBarbeiro(
            int id,
            BarbeiroDTO barbeiroDTO)
        {
            var barbeiro = await _context.Barbeiros.FindAsync(id);

            if (barbeiro == null)
            {
                return NotFound("Barbeiro não encontrado.");
            }

            barbeiro.Nome = barbeiroDTO.Nome;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/barbeiros/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBarbeiro(int id)
        {
            var barbeiro = await _context.Barbeiros.FindAsync(id);

            if (barbeiro == null)
            {
                return NotFound("Barbeiro não encontrado.");
            }

            _context.Barbeiros.Remove(barbeiro);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
