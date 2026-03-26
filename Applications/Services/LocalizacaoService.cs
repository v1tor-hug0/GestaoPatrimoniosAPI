using GestaoPatrimonio.Domains;
using GestaoPatrimonio.DTOs.LocalizacaoDto;
using GestaoPatrimonio.Interfaces;
using GestaoPatrimonio.Exceptions;
using GestaoPatrimonio.Applications.Regras;

namespace GestaoPatrimonio.Applications.Services
{
    public class LocalizacaoService 
    {
        private readonly ILocalizacaoRepository _repository;

        public LocalizacaoService(ILocalizacaoRepository repository)
        {
            _repository = repository;
        }


        public List<ListarLocalizacaoDto> Listar()
        {
            List<Localizacao> localizacoes = _repository.Listar();
            List<ListarLocalizacaoDto> localizacoesDto = localizacoes.Select(localizacao => new ListarLocalizacaoDto
            {
                LocalizacaoID = localizacao.LocalizacaoID,
                NomeLocal = localizacao.NomeLocal,
                LocalSAP = localizacao.LocalSAP,
                DescricaoSAP = localizacao.DescricaoSAP,
                AreaId = localizacao.AreaID
            }).ToList();
            return localizacoesDto;
        }

        public ListarLocalizacaoDto BuscarPorId(Guid localizacaoId)
        {
            Localizacao localizacao = _repository.BuscarPorId(localizacaoId);
            if (localizacao == null)
            {
                throw new DomainException("Localização não encontrada.");
            }

            ListarLocalizacaoDto localizacaoDto = new ListarLocalizacaoDto
            {
                LocalizacaoID = localizacao.LocalizacaoID,
                NomeLocal = localizacao.NomeLocal,
                LocalSAP = localizacao.LocalSAP,
                DescricaoSAP = localizacao.DescricaoSAP,
                AreaId = localizacao.AreaID
            };
            return localizacaoDto;
        }

        public void Adicionar(CriarLocalizacaoDto dto)
        {
            Validar.ValidarNome(dto.NomeLocal);

            if (!_repository.AreaExiste(dto.AreaId))
            {
                throw new DomainException("Área associada não encontrada.");
            }

            Localizacao localizacao = new Localizacao
            {
                NomeLocal = dto.NomeLocal,
                LocalSAP = dto.LocalSAP,
                DescricaoSAP = dto.DescricaoSAP,
                AreaID = dto.AreaId
            };

            _repository.Adicionar(localizacao);
        }

        public void Atualizar(Guid localizacaoId, CriarLocalizacaoDto dto)
        {
            Validar.ValidarNome(dto.NomeLocal);

            Localizacao localizacaoBanco = _repository.BuscarPorId(localizacaoId);

            if (localizacaoBanco == null)
            {
                throw new DomainException("Localização não encontrada.");
            }

            if(!_repository.AreaExiste(dto.AreaId))
                throw new DomainException("Área informada não encontrada.");

            localizacaoBanco.NomeLocal = dto.NomeLocal;
            localizacaoBanco.LocalSAP = dto.LocalSAP;
            localizacaoBanco.DescricaoSAP = dto.DescricaoSAP;
            localizacaoBanco.AreaID = dto.AreaId;

            _repository.Atualizar(localizacaoBanco);
        }
    }
}