using BarbeariaAPI.Data;
using BarbeariaAPI.DTOs;
using BarbeariaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarbeariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicosController : ControllerBase
    {
        private readonly BarbeariaContext _context;

        public ServicosController(BarbeariaContext context)
        {
            _context = context;
        }

        // GET: api/servicos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Servico>>> GetServicos()
        {
            return await _context.Servicos.ToListAsync();
        }

        // GET: api/servicos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Servico>> GetServico(int id)
        {
            var servico = await _context.Servicos.FindAsync(id);

            if (servico == null)
            {
                return NotFound("Serviço não encontrado.");
            }

            return servico;
        }

        // POST: api/servicos
        [HttpPost]
        public async Task<ActionResult<Servico>> PostServico(
            ServicoDTO servicoDTO)
        {
            var servico = new Servico
            {
                Nome = servicoDTO.Nome,
                Descricao = servicoDTO.Descricao,
                Preco = servicoDTO.Preco,
                DuracaoMinutos = servicoDTO.DuracaoMinutos
            };

            _context.Servicos.Add(servico);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetServico),
                new { id = servico.Id },
                servico
            );
        }

        // PUT: api/servicos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServico(
            int id,
            ServicoDTO servicoDTO)
        {
            var servico = await _context.Servicos.FindAsync(id);

            if (servico == null)
            {
                return NotFound("Serviço não encontrado.");
            }

            servico.Nome = servicoDTO.Nome;
            servico.Descricao = servicoDTO.Descricao;
            servico.Preco = servicoDTO.Preco;
            servico.DuracaoMinutos = servicoDTO.DuracaoMinutos;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/servicos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServico(int id)
        {
            var servico = await _context.Servicos.FindAsync(id);

            if (servico == null)
            {
                return NotFound("Serviço não encontrado.");
            }

            _context.Servicos.Remove(servico);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}