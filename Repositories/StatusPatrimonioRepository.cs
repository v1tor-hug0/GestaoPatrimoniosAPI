using GestaoPatrimonio.Contexts;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.Exceptions;
using GestaoPatrimonio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestaoPatrimonio.Repositories
{
    public class StatusPatrimonioRepository : IStatusPatrimonio
    {
        private readonly GestaoPatrimoniosContext _context;

        public StatusPatrimonioRepository(GestaoPatrimoniosContext context)
        {
            _context  = context;
        }

        public void Adicionar(StatusPatrimonio statusPatrimonio)
        {
            _context.StatusPatrimonio.Add(statusPatrimonio);
            _context.SaveChanges();
        }

        public void Atualizar(StatusPatrimonio statusPatrimonio)
        {
            if (statusPatrimonio == null)
                return;

            StatusPatrimonio statusBanco = _context.StatusPatrimonio.FirstOrDefault(s => s.StatusPatrimonioID == statusPatrimonio.StatusPatrimonioID);

            if (statusBanco == null)
                return;

            statusBanco.NomeStatus = statusPatrimonio.NomeStatus;
            _context.SaveChanges();
        }

        public StatusPatrimonio BuscarPorId(Guid statusPatrimonioId)
        {
            return _context.StatusPatrimonio.Find(statusPatrimonioId);
        }

        public StatusPatrimonio BuscarPorNome(string nomeStatus)
        {
            return _context.StatusPatrimonio.FirstOrDefault(s => s.NomeStatus.ToLower() == nomeStatus.ToLower());
        }

        public List<StatusPatrimonio> Listar()
        {
            return _context.StatusPatrimonio.OrderBy(s => s.NomeStatus).ToList();
        }
    }
}
