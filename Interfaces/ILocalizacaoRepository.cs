

using GestaoPatrimonio.Domains;

namespace GestaoPatrimonio.Interfaces
{
    public interface ILocalizacaoRepository
    {
        List<Localizacao> Listar();
        Localizacao BuscarPorId(Guid localizacaoId);
        void Adicionar(Localizacao localizacao);
        void Atualizar(Localizacao localizacao);
        bool AreaExiste(Guid areaId);

        
    }
}
