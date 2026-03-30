using GestaoPatrimonio.Domains;

namespace GestaoPatrimonio.Interfaces
{
    public interface IBairroRepository
    {
        List<Bairro> Listar();
        Bairro BuscarPorId(Guid bairroId);
        void Adicionar(Bairro bairro);
        void Atualizar(Bairro bairro);
        Bairro BuscarPorNome(string nomeBairro, Guid cidadeId);
        bool CidadeExiste(Guid cidadeId);
    }
}