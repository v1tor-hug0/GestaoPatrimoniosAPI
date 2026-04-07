using GestaoPatrimonio.Domains;

namespace GestaoPatrimonio.Interfaces
{
    public interface ILogPatrimonioRepository
    {
        List<LogPatrimonio> Listar();
        List<LogPatrimonio> BuscarPorPatrimonio(Guid patrimonioId);
    }
}
