using GestaoPatrimonio.Domains;

namespace GestaoPatrimonio.Interfaces
{
    public interface IAreaRepository
    {
        List<Area> Listar();
        Area BuscarPorID(Guid areaId);
        Area BuscarPorNome(string nomeArea);

        void Adicionar(Area area);
        void Atualizar(Area area);
    }
}
