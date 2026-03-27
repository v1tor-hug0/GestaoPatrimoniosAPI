using Microsoft.AspNetCore.Routing.Constraints;

namespace GestaoPatrimonio.DTOs.CidadeDto
{
    public class ListarCidadeDto
    {
        public Guid CidadeId { get; set; }
        public string NomeCidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public Guid BairroId { get; set; }
    }
}
