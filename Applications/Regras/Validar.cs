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
    }
}
