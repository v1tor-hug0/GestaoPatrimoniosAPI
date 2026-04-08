namespace GestaoPatrimonio.DTOs.SolicitacaoTransferenciaDto
{
    public class CriarSolicitacaoTransferenciaDto
    {
        public string Justificativa { get; set; } = string.Empty;
        public Guid PatrimonioId { get; set; }
        public Guid LocalizacaoId  { get; set; }
    }
}
