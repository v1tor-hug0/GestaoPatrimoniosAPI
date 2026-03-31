using GestaoPatrimonio.Domains;

namespace GestaoPatrimonio.Interfaces
{
    public interface ITipoUsuarioRepository
    {
        List<TipoUsuario> Listar();
        TipoUsuario BuscarPorId(Guid tipoUsuarioId);
        void Adicionar(TipoUsuario tipoUsuario);
        void Atualizar(TipoUsuario tipoUsuario);
        TipoUsuario BuscarPorNome(string nome);
    }
}
