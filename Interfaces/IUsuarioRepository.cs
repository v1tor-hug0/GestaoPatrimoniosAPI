using GestaoPatrimonio.Domains;

namespace GestaoPatrimonio.Interfaces
{
    public interface IUsuarioRepository
    {
            List<Usuario> Listar();
            Usuario BuscarPorId(Guid usuarioId);
            void Adicionar(Usuario usuario);
            void Atualizar(Usuario usuario);
            Usuario BuscarPorEmail(string email);
    }
}
