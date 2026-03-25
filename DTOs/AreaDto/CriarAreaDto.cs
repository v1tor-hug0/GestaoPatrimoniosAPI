using System.ComponentModel.DataAnnotations;

namespace GestaoPatrimonio.DTOs.AreaDto
{
    public class CriarAreaDto
    {
        [Required(ErrorMessage = "O nome da área é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome da área deve ter no máximo 50 caracteres.")]
        public string NomeArea { get; set; } = string.Empty;  // string.Empty = null proibido 


    }
}
