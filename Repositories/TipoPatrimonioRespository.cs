using GestaoPatrimonio.Contexts;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.Interfaces;

namespace GestaoPatrimonio.Repositories
{
    public class TipoPatrimonioRespository : ITipoPatrimonioRepository
    {

        private readonly GestaoPatrimoniosContext _context;
        public TipoPatrimonioRespository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<TipoPatrimonio> Listar()
        {
            return _context.TipoPatrimonio.OrderBy(p => p.NomeTipo).ToList();
        }

        public TipoPatrimonio BuscarPorId(Guid tipoPatrimonioId)
        {
            return _context.TipoPatrimonio.Find(tipoPatrimonioId);
        }

        public TipoPatrimonio BuscarPorNome(string nomeTipo)
        {
            return _context.TipoPatrimonio.FirstOrDefault(p => p.NomeTipo == nomeTipo);
        }

        public void Adicionar(TipoPatrimonio tipoPatrimonio)
        {
            _context.TipoPatrimonio.Add(tipoPatrimonio);
            _context.SaveChanges();
        }

        public void Atualizar(TipoPatrimonio tipoPatrimonio)
        {
            if(tipoPatrimonio == null)
                return;

            TipoPatrimonio tipoPatrimonioExiste = _context.TipoPatrimonio.Find(tipoPatrimonio.TipoPatrimonioID);
            if (tipoPatrimonioExiste != null)
                return;

            tipoPatrimonioExiste.NomeTipo = tipoPatrimonio.NomeTipo;
            _context.TipoPatrimonio.Update(tipoPatrimonioExiste);
            _context.SaveChanges();
        }
    }
}
