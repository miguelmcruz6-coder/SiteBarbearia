using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarbeariaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BarbeariaAPI.Data
{
    public class BarbeariaContext : DbContext
    {
        public BarbeariaContext(DbContextOptions<BarbeariaContext> options) : base(options){}
        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<Barbeiro> Barbeiros { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Servico> Servicos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Servico>()
                .Property(servico => servico.Preco)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Barbeiro>().HasData(
                new Barbeiro { Id = 1, Nome = "Vinicius Silva Lima" },
                new Barbeiro { Id = 2, Nome = "Miguel Miyaki da Cruz" }
            );

            modelBuilder.Entity<Servico>().HasData(
                new Servico
                {
                    Id = 1,
                    Nome = "Cabelo completo",
                    Descricao = "Corte de cabelo completo",
                    Preco = 0m,
                    DuracaoMinutos = 60
                },
                new Servico
                {
                    Id = 2,
                    Nome = "Barba completa",
                    Descricao = "Serviço completo de barba",
                    Preco = 0m,
                    DuracaoMinutos = 30
                },
                new Servico
                {
                    Id = 3,
                    Nome = "Sobrancelha",
                    Descricao = "Design de sobrancelha",
                    Preco = 0m,
                    DuracaoMinutos = 15
                },
                new Servico
                {
                    Id = 4,
                    Nome = "Máquina",
                    Descricao = "Corte feito com máquina",
                    Preco = 0m,
                    DuracaoMinutos = 30
                },
                new Servico
                {
                    Id = 5,
                    Nome = "Cabelo completo + Hidratação",
                    Descricao = "Corte completo com hidratação",
                    Preco = 0m,
                    DuracaoMinutos = 90
                },
                new Servico
                {
                    Id = 6,
                    Nome = "Cabelo completo + Barba + Sobrancelha",
                    Descricao = "Combo de cabelo, barba e sobrancelha",
                    Preco = 0m,
                    DuracaoMinutos = 90
                },
                new Servico
                {
                    Id = 7,
                    Nome = "Depilação a cera do nariz",
                    Descricao = "Depilação nasal com cera",
                    Preco = 0m,
                    DuracaoMinutos = 15
                },
                new Servico
                {
                    Id = 8,
                    Nome = "Depilação a cera da sobrancelha",
                    Descricao = "Depilação da sobrancelha com cera",
                    Preco = 0m,
                    DuracaoMinutos = 15
                }
            );
        }
    }
}
