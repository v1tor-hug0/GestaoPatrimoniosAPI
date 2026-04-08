using GestaoPatrimonio.Domains;

namespace GestaoPatrimonio.Interfaces
{
    public interface ISolicitacaoTransferenciaRepository
    {
        List<SolicitacaoTransferencia> Listar();
        SolicitacaoTransferencia BuscarPorId(Guid transferenciaId);
        bool ExisteSolicitacaoPendente(Guid patrimonioId);
        bool UsuarioResponsavelDaLocalizacao(Guid usuarioId, Guid localizacaoId);
        StatusTransferencia BuscarStatusTranferenciaPorNome(string nomeStatus);
        void Adicionar(SolicitacaoTransferencia solicitacaoTransferencia);
        bool LocalizacaoExiste(Guid localizacaoId);
        Patrimonio BuscarPorPatrimonioId(Guid patrimonioId);
    }
}
