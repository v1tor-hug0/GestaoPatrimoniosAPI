using GestaoPatrimonio.Applications.Regras;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.DTOs.BairroDto;
using GestaoPatrimonio.DTOs.TipoPatrimonioDto;
using GestaoPatrimonio.Exceptions;
using GestaoPatrimonio.Interfaces;

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

            if (tipo == null)
            {
                throw new DomainException("Tipo de patrimonio não encontrado.");
            }

            return new ListarTipoPatrimonioDto
            {
                TipoPatrimonioID = tipo.TipoPatrimonioID,
                NomeTipo = tipo.NomeTipo
            };
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

        public void Atualizar(Guid id, CriarTipoPatrimonioDto dto)
        {
            Validar.ValidarNome(dto.NomeTipo);

            TipoPatrimonio tipoExiste = _repository.BuscarPorNome(dto.NomeTipo);

            if(tipoExiste != null)
            {
                throw new DomainException("Já existe um tipo de patrimônio com esse nome.");
            }

            TipoPatrimonio tipoBanco = _repository.BuscarPorId(id);
            if(tipoBanco == null)
            {
                throw new DomainException("Tipo de patrimônio não encontrado.");
            }

            tipoBanco.NomeTipo = dto.NomeTipo;
            _repository.Atualizar(tipoBanco);
        }
    }
}
