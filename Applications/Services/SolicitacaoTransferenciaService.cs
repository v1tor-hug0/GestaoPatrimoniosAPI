using GestaoPatrimonio.Applications.Regras;
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

        public void Adicionar(Guid usuarioId, CriarSolicitacaoTransferenciaDto dto)
        {
            Validar.ValidarJustificativa(dto.Justificativa);

            Usuario usuario = _usuarioRepository.BuscarPorId(usuarioId);

            if (usuario == null)
            {
                throw new DomainException("Usuario nao encontrado");
            }

            Patrimonio patrimonio = _repository.BuscarPorPatrimonioId(dto.PatrimonioId);

            if (patrimonio == null)
            {
                throw new DomainException("Patrimonio nao encontrado");
            }

            if (!_repository.LocalizacaoExiste(dto.PatrimonioId))
            {
                throw new DomainException("Localizacao de destino não existe");
            }


            if (patrimonio.LocalizacaoID == dto.LocalizacaoId)
            {
                throw new DomainException("Patrimonio ja esta nessa localizacao");
            }

            if (_repository.ExisteSolicitacaoPendente(dto.PatrimonioId))
            {
                throw new DomainException("Ja existe uma solicitacao pendente para esse patrimonio");
            }

            if (usuario.TipoUsuario.NomeTipo == "Responsável")
            {
                bool usuarioResponsavel = _repository.UsuarioResponsavelDaLocalizacao(usuarioId, patrimonio.LocalizacaoID);

                if (!usuarioResponsavel)
                {
                    throw new DomainException("O responsavel só pode solicitar transferência de patrimônio do ambiente ao qual está vinculado");
                }
            }

            StatusTransferencia statusPendente = _repository.BuscarStatusTranferenciaPorNome("Pendente de aprovação");

            if (statusPendente == null)
            {
                throw new DomainException("Status de transferência pendente não encontrado");
            }

            SolicitacaoTransferencia solicitacao = new SolicitacaoTransferencia
            {
                DataCriacaoSolicitante = DateTime.Now,
                Justificativa = dto.Justificativa,
                StatusTransferenciaID = statusPendente.StatusTransferenciaID,
                UsuarioIDSolicitacao = usuarioId,
                UsuarioIDAprovacao = null,
                PatrimonioID = dto.PatrimonioId,
                LocalizacaoID = dto.LocalizacaoId

            };

            _repository.Adicionar(solicitacao);
        }

        public void Responder(Guid transferenciaId, Guid usuarioId, ResponderSolicitacaoTransferenciaDto dto)
        {
            Usuario usuario = _usuarioRepository.BuscarPorId(usuarioId);

            if (usuario == null)
            {
                throw new DomainException("Usuario nao encontrado");
            }

            SolicitacaoTransferencia solicitacao = _repository.BuscarPorId(transferenciaId);

            if (solicitacao == null)
            {
                throw new DomainException("Solicitacão de transferência não encontrada");
            }

            Patrimonio patrimonio = _repository.BuscarPorPatrimonioId(solicitacao.PatrimonioID);

            if (patrimonio == null)
            {
                throw new DomainException("Patrimônio não encontrado");
            }

            StatusTransferencia statusPendente = _repository.BuscarStatusTranferenciaPorNome("Pendente de aprovação");

            if (statusPendente == null)
            {
                throw new DomainException("Status pendente não encontrado");
            }

            if (solicitacao.StatusTransferenciaID != statusPendente.StatusTransferenciaID)
            {
                throw new DomainException("Essa solicitação ja foi respondida");
            }

            if (usuario.TipoUsuario.NomeTipo == "Responsável")
            {
                bool usuarioResponsavel = _repository.UsuarioResponsavelDaLocalizacao(usuarioId, patrimonio.LocalizacaoID);

                if (!usuarioResponsavel)
                {
                    throw new DomainException("Somente o responsável do ambiente de origem pode aprovar ou rejeitar essa solicitação");
                }
            }

            StatusTransferencia statusResposta;

            if (dto.Aprovado)
            {
                statusResposta = _repository.BuscarStatusTranferenciaPorNome("Aprovado");
            }
            else
            {
                statusResposta = _repository.BuscarStatusTranferenciaPorNome("Recusado");
            }

            if (statusResposta == null)
            {
                throw new DomainException("Status de resposta da transferencia não encontrado");
            }

            solicitacao.StatusTransferenciaID = statusResposta.StatusTransferenciaID;
            solicitacao.DataResposta = DateTime.Now;
            solicitacao.UsuarioIDAprovacao = usuarioId;
            
            _repository.Atualizar(solicitacao);

            if (dto.Aprovado)
            {
                StatusPatrimonio statusTransferido = _repository.BuscarStatusPatrimonioPorNome("Transferido");

                if (statusTransferido == null)
                {
                    throw new DomainException("Status de patrimônio 'Transferido' não encontrado");
                }

                TipoAlteracao tipoAlteracao = _repository.BuscarTipoAlteracaoPorNome("Transferência");
                
                if (tipoAlteracao == null)
                {
                    throw new DomainException("Tipo de alteração 'Transferência' não encontrado");
                }

                patrimonio.LocalizacaoID = solicitacao.LocalizacaoID;
                patrimonio.StatusPatrimonioID = statusTransferido.StatusPatrimonioID;

                _repository.AtualizarPatrimonio(patrimonio);

                LogPatrimonio log = new LogPatrimonio
                {
                    DataTransferencia = DateTime.Now,
                    PatrimonioID = patrimonio.PatrimonioID,
                    TipoAlteracaoID = tipoAlteracao.TipoAlteracaoID,
                    StatusPatrimonioID = statusTransferido.StatusPatrimonioID,
                    UsuarioID = usuarioId,
                    LocalizacaoID = solicitacao.LocalizacaoID
                };

            }

            
            
        }

    }
}
