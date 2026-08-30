using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BarbeariaAPI.DTOs
{
    public class AgendamentoDTO
    {
        public int ClienteId { get; set; }
        [Range(1, int.MaxValue)]
        public int BarbeiroId { get; set; }

        [Range(1, int.MaxValue)]
        public int ServicoId { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Horario { get; set; }
    }
}
