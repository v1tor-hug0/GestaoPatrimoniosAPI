using GestaoPatrimonio.DTOs.EnderecoDto;
using GestaoPatrimonio.Interfaces;
using GestaoPatrimonio.Applications.Regras;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.Exceptions;
using GestaoPatrimonio.DTOs.BairroDto;

namespace GestaoPatrimonio.Applications.Services
{
    public class EnderecoService
    {

        private readonly IEnderecoRepository _repository;

        public EnderecoService(IEnderecoRepository repository)
        {
            _repository = repository;
        }


        public List<ListarEnderecoDto> Listar()
        {
            List<Endereco> enderecos = _repository.Listar();

            List<ListarEnderecoDto> enderecosDto = enderecos.Select(e => new ListarEnderecoDto
            {
                EnderecoId = e.EnderecoID,
                Logradouro = e.Logradouro,
                Numero = e.Numero,
                Complemento = e.Complemento,
                CEP = e.CEP,
                BairroId = e.BairroID

            }).ToList();
            return enderecosDto;
        }

        public ListarEnderecoDto BuscarPorId(Guid enderecoId)
        {
            Endereco endereco = _repository.BuscarPorId(enderecoId);
            if (endereco == null)
            {
                throw new DomainException("Endereço não encontrado.");
            }

            return new ListarEnderecoDto
            {
                BairroId = endereco.BairroID,
                CEP = endereco.CEP,
                Complemento = endereco.Complemento,
                EnderecoId = endereco.EnderecoID,
                Logradouro = endereco.Logradouro,
                Numero = endereco.Numero
            };
        }

        public void Adicionar(CriarEnderecoDto dto)
        {
            Validar.ValidarNome(dto.Logradouro);

            if (!_repository.BairroExiste(dto.BairroId))
            {
                throw new DomainException("Bairro não encontrado.");
            }

            Endereco enderecoExistente = _repository.BuscarPorLogradouroENumero(dto.Logradouro, dto.Numero, dto.BairroId);

            if (enderecoExistente != null)
            {
                throw new DomainException("Já existe um endereço com o mesmo logradouro, número e bairro.");
            }

            Endereco endereco = new Endereco
            {
                EnderecoID = Guid.NewGuid(),
                Logradouro = dto.Logradouro,
                Numero = dto.Numero,
                Complemento = dto.Complemento,
                CEP = dto.CEP,
                BairroID = dto.BairroId
            };
            _repository.Adicionar(endereco);

        }

        public void Atualizar(Guid enderecoId, CriarEnderecoDto dto)
        {
            Validar.ValidarNome(dto.Logradouro);

            Endereco endereco = _repository.BuscarPorId(enderecoId);
            if (endereco == null)
            {
                throw new DomainException("Endereço não encontrado.");
            }

            if (!_repository.BairroExiste(dto.BairroId))
            {
                throw new DomainException("Bairro não encontrado.");
            }

            Endereco enderecoExiste = _repository.BuscarPorLogradouroENumero(dto.Logradouro, dto.Numero, dto.BairroId);

            if(enderecoExiste != null && enderecoExiste.EnderecoID != enderecoId)
            {
                throw new DomainException("Já existe um endereço com o mesmo logradouro, número e bairro.");
            }

            endereco.Logradouro = dto.Logradouro;
            endereco.Numero = dto.Numero;
            endereco.Complemento = dto.Complemento;
            endereco.CEP = dto.CEP;
            endereco.BairroID = dto.BairroId;
        }
        }
}
