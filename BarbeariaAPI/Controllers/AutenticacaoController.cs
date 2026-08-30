using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BarbeariaAPI.Data;
using BarbeariaAPI.DTOs;
using BarbeariaAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BarbeariaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly BarbeariaContext _context;
        private readonly IPasswordHasher<Cliente> _passwordHasher;

        private readonly IConfiguration _configuration;

        public AutenticacaoController(
            BarbeariaContext context,
            IPasswordHasher<Cliente> passwordHasher,
            IConfiguration configuration)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        //POST de cadastro
        [HttpPost("cadastro")]
        public async Task<IActionResult> Cadastro(CadastroDTO cadastro)
        {
            var email = cadastro.Email.Trim().ToLowerInvariant();
            var cpf = cadastro.CPF.Trim();

            var emailExistente = await _context.Clientes
                .AnyAsync(cliente => cliente.Email.ToLower() == email);

            if (emailExistente)
            {
                return Conflict("Já existe alguma conta registrada com esse e-mail.");
            }

            var cpfExiste = await _context.Clientes
                .AnyAsync(cliente => cliente.CPF == cpf);

            if (cpfExiste)
            {
                return Conflict("Já existe alguma conta registrada com esse CPF.");
            }

            var cliente = new Cliente
            {
                Nome = cadastro.Nome.Trim(),
                CPF = cpf,
                Telefone = cadastro.Telefone.Trim(),
                Email = email,
                Admin = false
            };

            cliente.SenhaHash = _passwordHasher.HashPassword(
                cliente,
                cadastro.Senha
            );

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return StatusCode(201, new
            {
                cliente.Id,
                cliente.Nome,
                cliente.Email
            });
        }

        //POST de login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var email = login.Email.Trim().ToLowerInvariant();

            var cliente = await _context.Clientes
                .SingleOrDefaultAsync(c => c.Email.ToLower() == email);

            if (cliente == null || string.IsNullOrWhiteSpace(cliente.SenhaHash))
            {
                return Unauthorized("E-mail ou senha inválidos.");
            }

            var resultado = _passwordHasher.VerifyHashedPassword(
                cliente,
                cliente.SenhaHash,
                login.Senha
            );

            if (resultado == PasswordVerificationResult.Failed)
            {
                return Unauthorized("E-mail ou senha inválidos.");
            }

            if (resultado == PasswordVerificationResult.SuccessRehashNeeded)
            {
                cliente.SenhaHash = _passwordHasher.HashPassword(
                    cliente,
                    login.Senha
                );
                await _context.SaveChangesAsync();
            }

            var expiracao = DateTime.UtcNow.AddHours(2);
            var token = GerarToken(cliente, expiracao);

            return Ok(new
            {
                token,
                expiracao,
                cliente = new
                {
                    cliente.Id,
                    cliente.Nome,
                    cliente.Email
                }
            });
        }

        private string GerarToken(Cliente cliente, DateTime expiracao)
        {
            var jwtKey = _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("Chave JWT não configurada");

            var jwtIssuer = _configuration["Jwt:Issuer"]
                ?? throw new InvalidOperationException("Emissor JWT não configurada");

            var jwtAudience = _configuration["Jwt:Audience"]
                ?? throw new InvalidOperationException("Audiência JWT não configurada");

            var claims = new[]
            {
                new Claim(
                    JwtRegisteredClaimNames.Sub,
                    cliente.Id.ToString()
                ),
                new Claim(
                    ClaimTypes.NameIdentifier,
                    cliente.Id.ToString()
                ),
                new Claim(
                    ClaimTypes.Name,
                    cliente.Nome
                ),
                new Claim(
                    ClaimTypes.Email,
                    cliente.Email
                ),
                new Claim(
                    ClaimTypes.Role,
                    cliente.Admin ? "Admin" : "Cliente"
                )
            };

            var chave = new SymmetricSecurityKey(
                Convert.FromBase64String(jwtKey)
            );

            var credenciais = new SigningCredentials(
                chave,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: expiracao,
                signingCredentials: credenciais
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
