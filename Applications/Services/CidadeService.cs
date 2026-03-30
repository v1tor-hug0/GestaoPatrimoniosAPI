using GestaoPatrimonio.Applications.Regras;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.DTOs.AreaDto;
using GestaoPatrimonio.DTOs.CidadeDto;
using GestaoPatrimonio.Exceptions;
using GestaoPatrimonio.Interfaces;
using GestaoPatrimonio.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GestaoPatrimonio.Applications.Services
{
    public class CidadeService
    {
        private readonly ICidadeRepository _repository;

        public CidadeService(ICidadeRepository repository)
        {
            _repository = repository;
        }

        public List<ListarCidadeDto> Listar()
        {
            List<Cidade> cidades = _repository.Listar();
            List<ListarCidadeDto> cidadeDto = cidades.Select(c => new ListarCidadeDto
            {
                CidadeId = c.CidadeID,
                NomeCidade = c.NomeCidade,
                Estado = c.Estado,
                BairroId = c.Bairro.Select(b => b.BairroID).FirstOrDefault()
            }).ToList();
            return cidadeDto;
        }

        public ListarCidadeDto BuscarPorId(Guid cidadeId)
        {
            Cidade cidade = _repository.BuscarPorId(cidadeId);
            if (cidade == null)
            {
                throw new DomainException("Cidade não encontrada.");
            }

            ListarCidadeDto cidadeDto = new ListarCidadeDto()
            {
                CidadeId = cidade.CidadeID,
                NomeCidade = cidade.NomeCidade,
                Estado = cidade.Estado,
                BairroId = cidade.Bairro.Select(b => b.BairroID).FirstOrDefault()
            };

            return cidadeDto;
        }

        public void Adicionar(CriarCidadeDto dto)
        {
            Validar.ValidarNome(dto.NomeCidade);

            Cidade cidadeExistente = _repository.BuscarPorNomeEEstado(dto.NomeCidade, dto.Estado);

            if (cidadeExistente != null)
            {
                throw new DomainException("Já existe uma cidade com o mesmo nome e estado.");
            }

            Cidade novaCidade = new Cidade
            {
                CidadeID = Guid.NewGuid(),
                NomeCidade = dto.NomeCidade,
                Estado = dto.Estado
            };

            _repository.Adicionar(novaCidade);
        }

        public void Atualizar(Guid cidadeId, CriarCidadeDto dto)
        {
            Validar.ValidarNome(dto.NomeCidade);
            Validar.ValidarEstado(dto.Estado);

            Cidade? cidadeBanco = _repository.BuscarPorId(cidadeId);

            if (cidadeBanco == null)
            {
                throw new DomainException("Cidade não encontrada.");
            }

            Cidade? cidadeExistente = _repository.BuscarPorNomeEEstado(dto.NomeCidade, dto.Estado);

            if (cidadeExistente != null && cidadeExistente.CidadeID != cidadeId)
            {
                throw new DomainException("Já existe uma cidade cadastrada com esse nome nesse estado.");
            }

            cidadeBanco.NomeCidade = dto.NomeCidade;
            cidadeBanco.Estado = dto.Estado;

            _repository.Atualizar(cidadeBanco);
        }
    }
}
