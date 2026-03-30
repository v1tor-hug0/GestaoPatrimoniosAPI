namespace GestaoPatrimonio.DTOs.EnderecoDto
{
    public class ListarEnderecoDto
    {
        public Guid EnderecoId { get; set; }
        public string Logradouro { get; set; } = string.Empty;
        public int? Numero { get; set; }
        public string? Complemento { get; set; }
        public string? CEP { get; set; }
        public Guid BairroId { get; set; }
        public string Bairro { get; set; } = string.Empty;

    }
}
