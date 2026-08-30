using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BarbeariaAPI.DTOs
{
    public class CadastroDTO
    {
        [Required]
        [StringLength(120, MinimumLength = 2)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Informe os 11 dígitos do CPF.")]
        public string CPF { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Informe um telefone válido.")]
        public string Telefone { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Senha { get; set; } = string.Empty;
    }
}
