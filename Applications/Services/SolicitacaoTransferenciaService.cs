using GestaoPatrimonio.Domains;
using GestaoPatrimonio.DTOs.SolicitacaoTransferenciaDto;
using GestaoPatrimonio.Exceptions;
using GestaoPatrimonio.Interfaces;

namespace GestaoPatrimonio.Applications.Services
{
    public class SolicitacaoTransferenciaService
    {
        private readonly ISolicitacaoTransferenciaRepository _repository;
        private readonly IUsuarioRepository _usuarioRepository;

        public SolicitacaoTransferenciaService(ISolicitacaoTransferenciaRepository repository, IUsuarioRepository usuarioRepository)
        {
            _repository = repository;
            _usuarioRepository = usuarioRepository;
        }

        public List<ListarSolicitacaoTransferenciaDto> Listar()
        {
            List < SolicitacaoTransferencia > solicitacoes = _repository.Listar();

            List<ListarSolicitacaoTransferenciaDto> solicitacoesDto = solicitacoes.Select(s => new ListarSolicitacaoTransferenciaDto
            {
                TransferenciaId = s.TransferenciaID,
                DataCriacaoSolicitante = s.DataCriacaoSolicitante,
                DataResposta = s.DataResposta,
                Justificativa = s.Justificativa,
                StatusTransferenciaId = s.StatusTransferenciaID,
                UsuarioIDSolicitacao = s.UsuarioIDSolicitacao,
                UsuarioIDAprovacao = s.UsuarioIDAprovacao,
                PatrimonioID = s.PatrimonioID,
                LocalizacaoID = s.LocalizacaoID
            }
            ).ToList();

            return solicitacoesDto;
        }

        public ListarSolicitacaoTransferenciaDto BuscarPorId(Guid transferenciaId)
        {
            SolicitacaoTransferencia s = _repository.BuscarPorId(transferenciaId);

            if (s == null) throw new DomainException("Solicitacao de transferecia nao encontrada");

            ListarSolicitacaoTransferenciaDto solicitacaoDto = new ListarSolicitacaoTransferenciaDto
            {
                TransferenciaId = s.TransferenciaID,
                DataCriacaoSolicitante = s.DataCriacaoSolicitante,
                DataResposta = s.DataResposta,
                Justificativa = s.Justificativa,
                StatusTransferenciaId = s.StatusTransferenciaID,
                UsuarioIDSolicitacao = s.UsuarioIDSolicitacao,
                UsuarioIDAprovacao = s.UsuarioIDAprovacao,
                PatrimonioID = s.PatrimonioID,
                LocalizacaoID = s.LocalizacaoID
            };
            return solicitacaoDto;
        }

    }
}
