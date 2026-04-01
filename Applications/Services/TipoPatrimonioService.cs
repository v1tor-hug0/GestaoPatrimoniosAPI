using GestaoPatrimonio.Domains;
using GestaoPatrimonio.DTOs.TipoPatrimonioDto;
using GestaoPatrimonio.Interfaces;
using GestaoPatrimonio.Exceptions;
using GestaoPatrimonio.Applications.Regras;

namespace GestaoPatrimonio.Applications.Services
{
    public class TipoPatrimonioService
    {
        private readonly ITipoPatrimonioRepository _repository;

        public TipoPatrimonioService(ITipoPatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarTipoPatrimonioDto> Listar()
        {
            List<TipoPatrimonio> tipos = _repository.Listar();

            List<ListarTipoPatrimonioDto> tipoDto = tipos.Select(t => new ListarTipoPatrimonioDto
            {
                TipoPatrimonioID = t.TipoPatrimonioID,
                NomeTipo = t.NomeTipo
            }).ToList();
            return tipoDto;
        }

        public ListarTipoPatrimonioDto BuscarPorId(Guid tipoPatrimonioId)
        {
            TipoPatrimonio tipo = _repository.BuscarPorId(tipoPatrimonioId);

            if(tipo == null)
            {
                throw new DomainException("Tipo de patrimônio não encontrado.");
            }

            ListarTipoPatrimonioDto tipoDto = new ListarTipoPatrimonioDto
            {
                TipoPatrimonioID = tipo.TipoPatrimonioID,
                NomeTipo = tipo.NomeTipo
            };

            return tipoDto;
        }

        public void Adicionar(CriarTipoPatrimonioDto dto)
        {
            Validar.ValidarNome(dto.NomeTipo);

            TipoPatrimonio tipos = _repository.BuscarPorNome(dto.NomeTipo);

            if(tipos != null)
            {
                throw new DomainException("Já existe um tipo de patrimônio com esse nome.");
            }

            TipoPatrimonio tipo = new TipoPatrimonio()
            {
                NomeTipo = dto.NomeTipo
            };

            _repository.Adicionar(tipo);
        }

        public void Atualizar(CriarTipoPatrimonioDto dto)
        {
            Validar.ValidarNome(dto.NomeTipo);

            TipoPatrimonio tipoExiste = _repository.BuscarPorNome(dto.NomeTipo);


        }
    }
}
