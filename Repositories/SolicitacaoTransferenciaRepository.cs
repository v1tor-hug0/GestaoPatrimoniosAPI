using GestaoPatrimonio.Contexts;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.Interfaces;

namespace GestaoPatrimonio.Repositories
{
    public class SolicitacaoTransferenciaRepository : ISolicitacaoTransferenciaRepository
    {

        private readonly GestaoPatrimoniosContext _context;

        public SolicitacaoTransferenciaRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }


        public List<SolicitacaoTransferencia> Listar()
        {
            return _context.SolicitacaoTransferencia.OrderByDescending(s => s.DataCriacaoSolicitante).ToList();
        }


        public SolicitacaoTransferencia BuscarPorId(Guid transferenciaId)
        {
            return _context.SolicitacaoTransferencia.Find(transferenciaId);
        }


        public StatusTransferencia BuscarStatusTranferenciaPorNome(string nomeStatus)
        {
            return _context.StatusTransferencia.FirstOrDefault(s => s.NomeStatus.ToLower() == nomeStatus.ToLower());
        }

        public bool ExisteSolicitacaoPendente(Guid patrimonioId)
        {
            StatusTransferencia statusPendente = BuscarStatusTranferenciaPorNome("Pendente de aprovação");
            if (statusPendente == null)  return false;

            return _context.SolicitacaoTransferencia.Any(s => s.PatrimonioID == patrimonioId && s.StatusTransferenciaID == statusPendente.StatusTransferenciaID);
        }


        public bool UsuarioResponsavelDaLocalizacao(Guid usuarioId, Guid localizacaoId)
        {
            return _context.Usuario.Any(u => u.UsuarioID == usuarioId && u.Localizacao.Any(l => l.LocalizacaoID == localizacaoId));

        }

        

        public void Adicionar(SolicitacaoTransferencia solicitacaoTransferencia)
        {
            _context.SolicitacaoTransferencia.Add(solicitacaoTransferencia);
            _context.SaveChanges();
        }

        public bool LocalizacaoExiste(Guid localizacaoId)
        {
            return _context.Localizacao.Any(l => l.LocalizacaoID == localizacaoId);
        }

        public Patrimonio BuscarPorPatrimonioId(Guid patrimonioId)
        {
            return _context.Patrimonio.Find(patrimonioId);
        }

        public StatusPatrimonio BuscarStatusPatrimonioPorNome(string nomeStatus)
        {
            return _context.StatusPatrimonio.FirstOrDefault(status => status.NomeStatus.ToLower() == nomeStatus.ToLower());
        }

       

        public TipoAlteracao BuscarTipoAlteracaoPorNome(string nomeTipo)
        {
            return _context.TipoAlteracao.FirstOrDefault(tipo => tipo.NomeTipo.ToLower() == nomeTipo.ToLower());
        }

        public void Atualizar(SolicitacaoTransferencia solicitacaoTransferencia)
        {
            if (solicitacaoTransferencia == null)
            {
                return;
            }

            SolicitacaoTransferencia solicitacaoBanco = _context.SolicitacaoTransferencia.Find(solicitacaoTransferencia.TransferenciaID);

            if (solicitacaoBanco == null)
            {
                return;
            }

            solicitacaoBanco.DataResposta = solicitacaoTransferencia.DataResposta;
            solicitacaoBanco.StatusTransferenciaID = solicitacaoTransferencia.StatusTransferenciaID;
            solicitacaoBanco.UsuarioIDAprovacao = solicitacaoTransferencia.UsuarioIDAprovacao;

            _context.SaveChanges();
        }

        public void AtualizarPatrimonio(Patrimonio patrimonio)
        {
            if (patrimonio == null)
            {
                return;
            }

            Patrimonio patrimonioBanco = _context.Patrimonio.Find(patrimonio.PatrimonioID);
            
            if (patrimonioBanco == null)
            {
                return;
            }

            patrimonioBanco.LocalizacaoID = patrimonio.LocalizacaoID;
            patrimonioBanco.StatusPatrimonioID = patrimonio.StatusPatrimonioID;

            _context.SaveChanges();

        }

        public void AdicionarLog(LogPatrimonio logPatrimonio)
        {
            _context.LogPatrimonio.Add(logPatrimonio);
            _context.SaveChanges();
        }
    }
}
