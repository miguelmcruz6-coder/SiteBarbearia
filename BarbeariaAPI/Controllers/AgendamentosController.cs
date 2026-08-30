using System.Security.Claims;
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
    public class AgendamentosController : ControllerBase
    {
        private readonly BarbeariaContext _context;

        public AgendamentosController(BarbeariaContext context)
        {
            _context = context;
        }

        // GET: api/agendamentos/horarios-disponiveis?barbeiroId=1&data=2026-08-30
        [Authorize]
        [HttpGet("horarios-disponiveis")]
        public async Task<IActionResult> GetHorariosDisponiveis(
            int barbeiroId,
            DateTime data)
        {
            if (barbeiroId <= 0 || data == default)
            {
                return BadRequest("Informe o profissional e a data.");
            }

            if (data.Date < DateTime.Today)
            {
                return BadRequest("A data não pode estar no passado.");
            }

            var barbeiroExiste = await _context.Barbeiros
                .AnyAsync(b => b.Id == barbeiroId);

            if (!barbeiroExiste)
            {
                return NotFound("Profissional não encontrado.");
            }

            var horariosOcupados = await _context.Agendamentos
                .Where(a =>
                    a.BarbeiroId == barbeiroId &&
                    a.Data.Date == data.Date &&
                    a.Status != "Cancelado")
                .Select(a => a.Horario)
                .ToListAsync();

            var horariosDisponiveis = new List<string>();

            for (var horario = TimeSpan.FromHours(8);
                 horario <= TimeSpan.FromHours(18);
                 horario = horario.Add(TimeSpan.FromMinutes(30)))
            {
                if (!horariosOcupados.Contains(horario))
                {
                    horariosDisponiveis.Add(horario.ToString(@"hh\:mm"));
                }
            }

            return Ok(horariosDisponiveis);
        }

        // GET: api/agendamentos/meus
        [Authorize]
        [HttpGet("meus")]
        public async Task<IActionResult> GetMeusAgendamentos()
        {
            var clienteIdTexto = User.FindFirst(
                ClaimTypes.NameIdentifier
            )?.Value;

            if (!int.TryParse(clienteIdTexto, out var clienteId))
            {
                return Unauthorized("Token inválido.");
            }

            var agendamentos = await _context.Agendamentos
                .Where(a => a.ClienteId == clienteId)
                .OrderByDescending(a => a.Data)
                .ThenByDescending(a => a.Horario)
                .Select(a => new
                {
                    a.Id,
                    a.Data,
                    a.Horario,
                    a.Status,
                    barbeiro = new
                    {
                        a.Barbeiro.Id,
                        a.Barbeiro.Nome
                    },
                    servico = new
                    {
                        a.Servico.Id,
                        a.Servico.Nome,
                        a.Servico.Preco,
                        a.Servico.DuracaoMinutos
                    }
                })
                .ToListAsync();

            return Ok(agendamentos);
        }

        // GET: api/agendamentos
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Agendamento>> PostAgendamento(
            AgendamentoDTO agendamentoDTO)
        {
            var clienteIdTexto = User.FindFirst(
                ClaimTypes.NameIdentifier
            )?.Value;

            if (!int.TryParse(clienteIdTexto, out var clienteId))
            {
                return Unauthorized("Token inválido.");
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
                ClienteId = clienteId,
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
        [Authorize(Roles = "Admin")]
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
        [Authorize]
        [HttpPut("{id}/cancelar")]
        public async Task<IActionResult> CancelarAgendamento(int id)
        {
            var agendamento = await _context.Agendamentos
                .FindAsync(id);

            if (agendamento == null)
            {
                return NotFound("Agendamento não encontrado.");
            }

            var clienteIdTexto = User.FindFirst(
                ClaimTypes.NameIdentifier
            )?.Value;

            if (!int.TryParse(clienteIdTexto, out var clienteId))
            {
                return Unauthorized("Token inválido.");
            }

            if (agendamento.ClienteId != clienteId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            agendamento.Status = "Cancelado";

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensagem = "Agendamento cancelado com sucesso."
            });
        }

        // DELETE: api/agendamentos/5
        [Authorize(Roles = "Admin")]
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
