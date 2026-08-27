using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarbeariaAPI.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public bool Admin { get; set; } = false;
        public string Nome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}