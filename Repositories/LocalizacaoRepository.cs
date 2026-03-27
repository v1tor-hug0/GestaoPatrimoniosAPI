using GestaoPatrimonio.Domains;
using GestaoPatrimonio.Interfaces;
using Microsoft.EntityFrameworkCore;
using GestaoPatrimonio.Contexts;

namespace GestaoPatrimonio.Repositories
{
    public class LocalizacaoRepository : ILocalizacaoRepository
    {

        private readonly GestaoPatrimoniosContext _context;


        public LocalizacaoRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<Localizacao> Listar()
        {
            return _context.Localizacao.OrderBy(local => local.NomeLocal).ToList();
        }

        public Localizacao BuscarPorId(Guid localizacaoId)
        {
            return _context.Localizacao.Find(localizacaoId);
        }

        public void Adicionar(Localizacao localizacao)
        {
            _context.Localizacao.Add(localizacao);
            _context.SaveChanges();
        }

        public bool AreaExiste(Guid areaId)
        {
            return _context.Area.Any(a => a.AreaID == areaId);
        }

        public void Atualizar(Localizacao localizacao)
        {

            if (localizacao == null)
            {
                return;
            }

            Localizacao localizacaoBanco = _context.Localizacao.Find(localizacao.LocalizacaoID);

            if (localizacaoBanco == null)
            {
                return;
            }

            localizacaoBanco.NomeLocal = localizacao.NomeLocal;
            localizacaoBanco.LocalSAP = localizacao.LocalSAP;
            localizacaoBanco.DescricaoSAP = localizacao.DescricaoSAP;
            localizacaoBanco.AreaID = localizacao.AreaID;

            _context.SaveChanges();
        }

        public Localizacao BuscarPorNome(string nomeLocal, Guid areaId)
        {
            return _context.Localizacao.FirstOrDefault(l => l.NomeLocal.ToLower() == nomeLocal.ToLower() && l.AreaID == areaId);
        }
    }
}
