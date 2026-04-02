using GestaoPatrimonio.Applications.Regras;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.DTOs.StatusPatrimonioDto;
using GestaoPatrimonio.Exceptions;
using GestaoPatrimonio.Interfaces;

namespace GestaoPatrimonio.Applications.Services
{
    public class StatusPatrimonioService
    {
        private readonly IStatusPatrimonio _repository;

        public StatusPatrimonioService(IStatusPatrimonio repository)
        {
            _repository = repository;
        }

        public List<ListarStatusPatrimonioDto> Listar()
        {
            List<StatusPatrimonio> statusPatrimonios = _repository.Listar();

            List<ListarStatusPatrimonioDto> statusDto = statusPatrimonios.Select(s => new ListarStatusPatrimonioDto
            {
                StatusPatrimonioID = s.StatusPatrimonioID,
                NomeStatus = s.NomeStatus
            }).ToList();

            return statusDto;
        }

        public ListarStatusPatrimonioDto BuscarPorId(Guid id)
        {
            StatusPatrimonio status = _repository.BuscarPorId(id);

            if (status == null)
            {
                throw new DomainException("Status de Patrimonio nao encontrado");
            }

            ListarStatusPatrimonioDto statusDto = new ListarStatusPatrimonioDto
            {
                StatusPatrimonioID = status.StatusPatrimonioID,
                NomeStatus = status.NomeStatus
            };

            return statusDto;
        }

        public void Adicionar(CriarStatusPatrimonioDto dto)
        {
            Validar.ValidarNome(dto.NomeStatus);

            StatusPatrimonio statusExiste = _repository.BuscarPorNome(dto.NomeStatus);

            if (statusExiste != null)
            {
                throw new DomainException("Status ja existente");
            }

            StatusPatrimonio status = new StatusPatrimonio
            {
                NomeStatus = dto.NomeStatus
            };
            _repository.Adicionar(status);
        }

        public void Atualizar(Guid id, CriarStatusPatrimonioDto dto)
        {
            Validar.ValidarNome(dto.NomeStatus);

            StatusPatrimonio status = _repository.BuscarPorId(id);

            if (status == null)
            {
                throw new DomainException("Status de Patrimonio nao encontrado");
            }

            StatusPatrimonio statusExiste = _repository.BuscarPorNome(dto.NomeStatus);

            if (statusExiste != null)
            {
                throw new DomainException("Status ja existente");
            }

            status.NomeStatus = dto.NomeStatus;
            _repository.Atualizar(status);
        }
    }
}
