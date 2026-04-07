using GestaoPatrimonio.Domains;
using GestaoPatrimonio.DTOs.LogPatrimonioDto;
using GestaoPatrimonio.Exceptions;
using GestaoPatrimonio.Interfaces;
using System.Diagnostics.Eventing.Reader;

namespace GestaoPatrimonio.Applications.Services
{
    public class LogPatrimonioService
    {
        private readonly ILogPatrimonioRepository _repository;

        public LogPatrimonioService(ILogPatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarLogPatrimonioDto> Listar()
        {
            List<LogPatrimonio> logs = _repository.Listar();

            List<ListarLogPatrimonioDto> logsDto = logs.Select(log => new ListarLogPatrimonioDto
            {
                LogPatrimonioId = log.LogPatrimonioID,
                DataTransferencia = log.DataTransferencia,
                PatrimonioId = log.PatrimonioID,
                DenominacaoPatrimonio = log.Patrimonio.Denominacao,
                TipoAlteracao = log.TipoAlteracao.NomeTipo,
                StatusPatrimonio = log.StatusPatrimonio.NomeStatus,
                Usuario = log.Usuario.Nome,
                Localizacao = log.Localizacao.NomeLocal
            }).ToList();

            return logsDto;
        }

        public List<ListarLogPatrimonioDto> BuscarPorPatrimonio(Guid patrimonioId)
        {
            List<LogPatrimonio> logs = _repository.BuscarPorPatrimonio(patrimonioId);

            if (logs == null)
            {
                throw new DomainException("Patrimonio nao encontrado");
            }

            List<ListarLogPatrimonioDto> logsDto = logs.Select(log => new ListarLogPatrimonioDto
            {
                LogPatrimonioId = log.LogPatrimonioID,
                DataTransferencia = log.DataTransferencia,
                PatrimonioId = log.PatrimonioID,
                DenominacaoPatrimonio = log.Patrimonio.Denominacao,
                TipoAlteracao = log.TipoAlteracao.NomeTipo,
                StatusPatrimonio = log.StatusPatrimonio.NomeStatus,
                Usuario = log.Usuario.Nome,
                Localizacao = log.Localizacao.NomeLocal
            }).ToList();

            return logsDto;
        }


    }
}
