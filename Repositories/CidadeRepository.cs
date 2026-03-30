using GestaoPatrimonio.Contexts;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.Interfaces;

namespace GestaoPatrimonio.Repositories
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly GestaoPatrimoniosContext _context;
        public CidadeRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }
        public List<Cidade> Listar()
        {
            return _context.Cidade.OrderBy(c => c.NomeCidade).ToList();
        }

        public Cidade BuscarPorId(Guid cidadeId)
        {
            return _context.Cidade.Find(cidadeId);
        }

        public Cidade BuscarPorNomeEEstado(string nomeCidade, string nomeEstado)
        {
                return _context.Cidade.FirstOrDefault(c => c.NomeCidade == nomeCidade.ToLower() && c.Estado == nomeEstado.ToLower());
        }

        public void Adicionar(Cidade cidade)
        {
            _context.Cidade.Add(cidade);
            _context.SaveChanges();
        }

        public void Atualizar(Cidade cidade)
        {
            if (cidade == null)
            {
                return;
            }

            Cidade cidadeBanco = _context.Cidade.Find(cidade.CidadeID);

            if (cidadeBanco == null)
            {
                return;
            }

            cidadeBanco.NomeCidade = cidade.NomeCidade;
            cidadeBanco.Estado = cidade.Estado;

            _context.SaveChanges();
        }
    }
}
