using Microsoft.AspNetCore.Routing.Constraints;

namespace GestaoPatrimonio.DTOs.LocalizacaoDto
{
    public class ListarLocalizacaoDto
    {
        public Guid LocalizacaoID { get; set; }
        public string NomeLocal { get; set; } = string.Empty;
        public int? LocalSAP { get; set; }
        public string DescricaoSAP { get; set; }
        public Guid AreaId { get; set; }
    }
}
