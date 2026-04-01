using GestaoPatrimonio.Domains;

namespace GestaoPatrimonio.Interfaces
{
    public interface IStatusPatrimonio
    {
        List<StatusPatrimonio> Listar();
        StatusPatrimonio BuscarPorId(Guid statusPatrimonioId);
        StatusPatrimonio BuscarPorNome(string nomeStatus);

        void Adicionar(StatusPatrimonio statusPatrimonio);
        void Atualizar(StatusPatrimonio statusPatrimonio);
    }
}
