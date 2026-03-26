using GestaoPatrimonio.Exceptions;
using GestaoPatrimonio.Applications.Regras;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.DTOs.AreaDto;
using GestaoPatrimonio.Interfaces;
using GestaoPatrimonio.Repositories;

namespace GestaoPatrimonio.Applications.Services
{
    public class AreaService
    {
        private readonly IAreaRepository _repository;

        public AreaService(IAreaRepository repository)
        {
            _repository = repository;
        }

        public List<ListarAreaDto> Listar()
        {
            List<Area> areas = _repository.Listar();
            List<ListarAreaDto> areasDto = areas.Select(area => new ListarAreaDto
            {
                AreaID = area.AreaID,
                NomeArea = area.NomeArea
            }).ToList();

            return areasDto;
        }

        public ListarAreaDto BuscarPorId(Guid areaId)
        {
            Area area = _repository.BuscarPorID(areaId);

            if(area == null)
            {
                throw new DomainException("Área não encontrada.");
            }

            ListarAreaDto areaDto = new ListarAreaDto
            {
                AreaID = area.AreaID,
                NomeArea = area.NomeArea
            };
            return areaDto;
        }

        public void Adicionar(CriarAreaDto dto)
        {
            Validar.ValidarNome(dto.NomeArea);

            Area areaExistente = _repository.BuscarPorNome(dto.NomeArea);

            if (areaExistente != null)
            {
                throw new DomainException("Ja existe uma área com esse nome.");
            }

            Area novaArea = new Area
            {
                //AreaID = Guid.NewGuid(), caso nao fosse automatico
                NomeArea = dto.NomeArea
            };
            _repository.Adicionar(novaArea);
        }
        
        public void Atualizar(Guid areaId, CriarAreaDto dto)
        {
            Validar.ValidarNome(dto.NomeArea);

            Area areaBanco = _repository.BuscarPorID(areaId);

            if (areaBanco == null)
                throw new DomainException("Área não encontrada.");
            

            Area areaExistente = _repository.BuscarPorNome(dto.NomeArea);

            if (areaExistente != null)           
                throw new DomainException("Ja existe uma área com esse nome.");
            

            areaBanco.NomeArea = dto.NomeArea;

            _repository.Atualizar(areaBanco);
        }
    }
}
