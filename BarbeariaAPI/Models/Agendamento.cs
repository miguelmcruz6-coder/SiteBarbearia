using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarbeariaAPI.Models
{
    public class Agendamento
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public int BarbeiroId { get; set; }
        public Barbeiro Barbeiro { get; set; }

        public int ServicoId { get; set; }
        public Servico Servico { get; set; }

        public DateTime Data { get; set; }
        public TimeSpan Horario { get; set; }

        public string Status { get; set; }
    }
}