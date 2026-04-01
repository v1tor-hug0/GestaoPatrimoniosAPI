using GestaoPatrimonio.Domains;
using GestaoPatrimonio.DTOs.CargoDto;
using GestaoPatrimonio.Interfaces;
using GestaoPatrimonio.Exceptions;
using GestaoPatrimonio.Applications.Regras;
using Microsoft.Identity.Client;

namespace GestaoPatrimonio.Applications.Services
{
    public class CargoService
    {
        private readonly ICargoRepository _repository;

        public CargoService(ICargoRepository repository)
        {
            _repository = repository;
        }

        public List<ListarCargoDto> Listar()
        {
            List<Cargo> cargos = _repository.Listar();
            List<ListarCargoDto> cargosDto = cargos.Select(c => new ListarCargoDto
            {
                CargoID = c.CargoID,
                NomeCargo = c.NomeCargo,
            }).ToList();

            return cargosDto;
        }

        public ListarCargoDto BuscarPorId(Guid id)
        {
            Cargo cargo = _repository.BuscarPorId(id);

            if (cargo == null) throw new DomainException("Cargo não encontrado");

            ListarCargoDto cargoDto = new ListarCargoDto
            {
                CargoID = cargo.CargoID,
                NomeCargo = cargo.NomeCargo,
            };

            return cargoDto;
        }

        public void Adicionar(CriarCargoDto cargoDto)
        {
            Cargo cargoExistente = _repository.BuscarPorNome(cargoDto.NomeCargo);

            if (cargoExistente != null) throw new DomainException("Esse cargo já existe");

            Cargo cargo = new Cargo
            {
                NomeCargo = cargoDto.NomeCargo
            };

            _repository.Adicionar(cargo);
        }

        public void Atualizar(CriarCargoDto cargoDto, Guid id)
        {
            Validar.ValidarNome(cargoDto.NomeCargo);

            Cargo cargoBanco = _repository.BuscarPorId(id);

            if (cargoBanco == null) throw new DomainException("Esse cargo não foi encontrado");

            Cargo cargoExistente = _repository.BuscarPorNome(cargoDto.NomeCargo);

            if (cargoExistente != null && id != cargoExistente.CargoID) throw new DomainException("Já existe um cargo com esse nome");

            cargoBanco.NomeCargo = cargoDto.NomeCargo;

            _repository.Atualizar(cargoBanco);
        }
    }
}
