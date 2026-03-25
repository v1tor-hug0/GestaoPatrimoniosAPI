using GestaoPatrimonio.Contexts;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.Interfaces;

namespace GestaoPatrimonio.Repositories
{
    public class AreaRepository : IAreaRepository
    {
        private readonly GestaoPatrimoniosContext _context;  

        public AreaRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }
        

        public List<Area> Listar()
        {
            return _context.Area.OrderBy(area => area.NomeArea).ToList();
        }

        public Area BuscarPorID(Guid areaId)
        {
            return _context.Area.Find(areaId);
        }

        public Area BuscarPorNome(string nomeArea)
        {
            return _context.Area.FirstOrDefault(area => area.NomeArea == nomeArea.ToLower());
        }

        public void Adicionar(Area area)
        {
            _context.Area.Add(area);
            _context.SaveChanges();
        }

        public void Atualizar(Area area)
        {

            if (area == null)
            {
                return;
            }

            Area areaBanco = _context.Area.Find(area.AreaID);

            areaBanco.NomeArea = area.NomeArea;
            _context.SaveChanges();
        }
    }
}
