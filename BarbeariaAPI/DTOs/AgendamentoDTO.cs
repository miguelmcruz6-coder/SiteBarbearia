using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarbeariaAPI.DTOs
{
    public class AgendamentoDTO
    {
        public int ClienteId { get; set; }
        public int BarbeiroId { get; set; }
        public int ServicoId { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Horario { get; set; }
    }
}
