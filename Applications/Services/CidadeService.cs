using GestaoPatrimonio.Applications.Regras;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.DTOs.AreaDto;
using GestaoPatrimonio.DTOs.CidadeDto;
using GestaoPatrimonio.Exceptions;
using GestaoPatrimonio.Interfaces;
using GestaoPatrimonio.Repositories;

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

        public ListarCidadeDto BuscarPorNomeEEstado(string nomeCidade, string nomeEstado)
        {

        }
    }
}
