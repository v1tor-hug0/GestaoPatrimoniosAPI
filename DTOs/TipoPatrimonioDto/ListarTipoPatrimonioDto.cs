using GestaoPatrimonio.Domains;

namespace GestaoPatrimonio.DTOs.TipoPatrimonioDto
{
    public class ListarTipoPatrimonioDto
    {
        public Guid TipoPatrimonioID { get; set; }

        public string NomeTipo { get; set; } = string.Empty;

    }
}
