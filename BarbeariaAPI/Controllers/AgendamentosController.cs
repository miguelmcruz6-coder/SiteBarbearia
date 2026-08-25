using BarbeariaAPI.Data;
using BarbeariaAPI.DTOs;
using BarbeariaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                return NotFound("Agendamento não encontrado.");
            }

            return agendamento;
        }

        // POST: api/agendamentos
        [HttpPost]
        public async Task<ActionResult<Agendamento>> PostAgendamento(
            AgendamentoDTO agendamentoDTO)
        {
            // Verificar cliente
            var clienteExiste = await _context.Clientes
                .AnyAsync(c => c.Id == agendamentoDTO.ClienteId);

            if (!clienteExiste)
            {
                return BadRequest("Cliente não encontrado.");
            }

            // Verificar barbeiro
            var barbeiroExiste = await _context.Barbeiros
                .AnyAsync(b => b.Id == agendamentoDTO.BarbeiroId);

            if (!barbeiroExiste)
            {
                return BadRequest("Barbeiro não encontrado.");
            }

            // Verificar serviço
            var servicoExiste = await _context.Servicos
                .AnyAsync(s => s.Id == agendamentoDTO.ServicoId);

            if (!servicoExiste)
            {
                return BadRequest("Serviço não encontrado.");
            }

            // Verificar se o horário está ocupado
            var horarioOcupado = await _context.Agendamentos
                .AnyAsync(a =>
                    a.BarbeiroId == agendamentoDTO.BarbeiroId &&
                    a.Data == agendamentoDTO.Data &&
                    a.Horario == agendamentoDTO.Horario &&
                    a.Status != "Cancelado"
                );

            if (horarioOcupado)
            {
                return Conflict(
                    "Este barbeiro já possui um agendamento neste horário."
                );
            }

            // Criar agendamento
            var agendamento = new Agendamento
            {
                ClienteId = agendamentoDTO.ClienteId,
                BarbeiroId = agendamentoDTO.BarbeiroId,
                ServicoId = agendamentoDTO.ServicoId,
                Data = agendamentoDTO.Data,
                Horario = agendamentoDTO.Horario,
                Status = "Agendado"
            };

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
            AgendamentoDTO agendamentoDTO)
        {
            var agendamento = await _context.Agendamentos
                .FindAsync(id);

            if (agendamento == null)
            {
                return NotFound("Agendamento não encontrado.");
            }

            // Verificar cliente
            var clienteExiste = await _context.Clientes
                .AnyAsync(c => c.Id == agendamentoDTO.ClienteId);

            if (!clienteExiste)
            {
                return BadRequest("Cliente não encontrado.");
            }

            // Verificar barbeiro
            var barbeiroExiste = await _context.Barbeiros
                .AnyAsync(b => b.Id == agendamentoDTO.BarbeiroId);

            if (!barbeiroExiste)
            {
                return BadRequest("Barbeiro não encontrado.");
            }

            // Verificar serviço
            var servicoExiste = await _context.Servicos
                .AnyAsync(s => s.Id == agendamentoDTO.ServicoId);

            if (!servicoExiste)
            {
                return BadRequest("Serviço não encontrado.");
            }

            // Verificar conflito de horário
            var horarioOcupado = await _context.Agendamentos
                .AnyAsync(a =>
                    a.Id != id &&
                    a.BarbeiroId == agendamentoDTO.BarbeiroId &&
                    a.Data == agendamentoDTO.Data &&
                    a.Horario == agendamentoDTO.Horario &&
                    a.Status != "Cancelado"
                );

            if (horarioOcupado)
            {
                return Conflict(
                    "Este barbeiro já possui um agendamento neste horário."
                );
            }

            agendamento.ClienteId = agendamentoDTO.ClienteId;
            agendamento.BarbeiroId = agendamentoDTO.BarbeiroId;
            agendamento.ServicoId = agendamentoDTO.ServicoId;
            agendamento.Data = agendamentoDTO.Data;
            agendamento.Horario = agendamentoDTO.Horario;

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
                return NotFound("Agendamento não encontrado.");
            }

            agendamento.Status = "Cancelado";

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensagem = "Agendamento cancelado com sucesso."
            });
        }

        // DELETE: api/agendamentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgendamento(int id)
        {
            var agendamento = await _context.Agendamentos
                .FindAsync(id);

            if (agendamento == null)
            {
                return NotFound("Agendamento não encontrado.");
            }

            _context.Agendamentos.Remove(agendamento);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}