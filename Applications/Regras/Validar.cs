using GestaoPatrimonio.Exceptions;

namespace GestaoPatrimonio.Applications.Regras
{
    public class Validar
    {
        public static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("Nome é obrigatorio");

            }
        }
        public static void ValidarEstado(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
            {
                throw new DomainException("Estado é obrigatório.");
            }
        }
    }
}
