using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BarbeariaAPI.Data;
using BarbeariaAPI.Models;

namespace BarbeariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentosController : ControllerBase
    {
        private readonly BarbeariaContext _context;

        public AgendamentosController(BarbeariaContext context)
        {
            _context = context;
        }

        // GET: api/agendamentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agendamento>>> GetAgendamentos()
        {
            return await _context.Agendamentos
                .Include(a => a.Cliente)
                .Include(a => a.Barbeiro)
                .Include(a => a.Servico)
                .ToListAsync();
        }

        // GET: api/agendamentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Agendamento>> GetAgendamento(int id)
        {
            var agendamento = await _context.Agendamentos
                .Include(a => a.Cliente)
                .Include(a => a.Barbeiro)
                .Include(a => a.Servico)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (agendamento == null)
            {
                return NotFound();
            }

            return agendamento;
        }

        // POST: api/agendamentos
        [HttpPost]
        public async Task<ActionResult<Agendamento>> PostAgendamento(
            Agendamento agendamento)
        {
            var clienteExiste = await _context.Clientes
                .AnyAsync(c => c.Id == agendamento.ClienteId);

            if (!clienteExiste)
            {
                return BadRequest("Cliente não encontrado.");
            }

            var barbeiroExiste = await _context.Barbeiros
                .AnyAsync(b => b.Id == agendamento.BarbeiroId);

            if (!barbeiroExiste)
            {
                return BadRequest("Barbeiro não encontrado.");
            }

            var servicoExiste = await _context.Servicos
                .AnyAsync(s => s.Id == agendamento.ServicoId);

            if (!servicoExiste)
            {
                return BadRequest("Serviço não encontrado.");
            }

            // Verifica se o barbeiro já possui agendamento
            var horarioOcupado = await _context.Agendamentos
                .AnyAsync(a =>
                    a.BarbeiroId == agendamento.BarbeiroId &&
                    a.Data == agendamento.Data &&
                    a.Horario == agendamento.Horario &&
                    a.Status != "Cancelado"
                );

            if (horarioOcupado)
            {
                return Conflict("Este horário já está ocupado.");
            }

            agendamento.Status = "Agendado";

            _context.Agendamentos.Add(agendamento);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAgendamento),
                new { id = agendamento.Id },
                agendamento
            );
        }

        // PUT: api/agendamentos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgendamento(
            int id,
            Agendamento agendamento)
        {
            if (id != agendamento.Id)
            {
                return BadRequest();
            }

            _context.Entry(agendamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgendamentoExiste(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // DELETE: api/agendamentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgendamento(int id)
        {
            var agendamento = await _context.Agendamentos
                .FindAsync(id);

            if (agendamento == null)
            {
                return NotFound();
            }

            _context.Agendamentos.Remove(agendamento);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/agendamentos/5/cancelar
        [HttpPut("{id}/cancelar")]
        public async Task<IActionResult> CancelarAgendamento(int id)
        {
            var agendamento = await _context.Agendamentos
                .FindAsync(id);

            if (agendamento == null)
            {
                return NotFound();
            }

            agendamento.Status = "Cancelado";

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensagem = "Agendamento cancelado com sucesso."
            });
        }

        private bool AgendamentoExiste(int id)
        {
            return _context.Agendamentos
                .Any(e => e.Id == id);
        }
    }
}