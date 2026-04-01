

using GestaoPatrimonio.Contexts;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.Interfaces;

namespace GestaoPatrimonio.Repositories
{
    public class CargoRepository : ICargoRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public CargoRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public void Adicionar(Cargo cargo)
        {
            _context.Cargo.Add(cargo);
            _context.SaveChanges();
        }

        public void Atualizar(Cargo cargo)
        {
            if (cargo == null)
            {
                return;
            }

            Cargo cargoBanco = _context.Cargo.Find(cargo.CargoID);

            if (cargoBanco == null) return;

            cargoBanco.NomeCargo = cargo.NomeCargo;

            _context.SaveChanges();
        }

        public Cargo BuscarPorId(Guid cargoId)
        {
            return _context.Cargo.Find(cargoId);
        }

        public Cargo BuscarPorNome(string nomeCargo)
        {
            return _context.Cargo.FirstOrDefault(c => c.NomeCargo == nomeCargo);
        }

        public List<Cargo> Listar()
        {
            return _context.Cargo.ToList();
        }


    }
}
