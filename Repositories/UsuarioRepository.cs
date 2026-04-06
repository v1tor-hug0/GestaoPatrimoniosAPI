using GestaoPatrimonio.Contexts;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestaoPatrimonio.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public UsuarioRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<Usuario> Listar()
        {
            return _context.Usuario.OrderBy(u => u.Nome).ToList();
        }

        public Usuario BuscarPorId(Guid usuarioId)
        {
            return _context.Usuario.Find(usuarioId);
        }

        public Usuario BuscarDuplicado(string nif, string cpf, string email, Guid? usuarioId = null)
        {
            var consulta = _context.Usuario.AsQueryable();

            if (usuarioId.HasValue)
            {
                consulta = consulta.Where(u => u.UsuarioID != usuarioId.Value);
            }

            return consulta.FirstOrDefault(u => u.NIF == nif || u.CPF == cpf || u.Email.ToLower() == email.ToLower());
        }

        public bool EnderecoExiste(Guid enderecoId)
        {
            return _context.Endereco.Any(e => e.EnderecoID == enderecoId);
        }

        public bool CargoExiste(Guid cargoId)
        {
            return _context.Cargo.Any(c => c.CargoID == cargoId);
        }

        public bool TipoUsuarioExiste(Guid tipoUsuarioId)
        {
            return _context.TipoUsuario.Any(t => t.TipoUsuarioID == tipoUsuarioId);
        }

        public void Adicionar(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            _context.SaveChanges();
        }

        public void Atualizar(Usuario usuario)
        {
            if (usuario == null)
            {
                return;
            }

            Usuario usuarioBanco = _context.Usuario.Find(usuario.UsuarioID);

            if (usuarioBanco == null)
            {
                return;
            }

            usuarioBanco.NIF = usuario.NIF;
            usuarioBanco.Nome = usuario.Nome;
            usuarioBanco.RG = usuario.RG;
            usuarioBanco.CPF = usuario.CPF;
            usuarioBanco.CarteiraTrabalho = usuario.CarteiraTrabalho;
            usuarioBanco.Senha = usuario.Senha;
            usuarioBanco.Email = usuario.Email;
            usuarioBanco.EnderecoID = usuario.EnderecoID;
            usuarioBanco.CargoID = usuario.CargoID;
            usuarioBanco.TipoUsuarioID = usuario.TipoUsuarioID;

            _context.SaveChanges();
        }

        public void AtualizarStatus(Usuario usuario)
        {
            if (usuario == null)
            {
                return;
            }
            Usuario usuarioBanco = _context.Usuario.Find(usuario.UsuarioID);
            if (usuarioBanco == null)
            {
                return;
            }
            usuarioBanco.Ativo = usuario.Ativo;
            _context.SaveChanges();
        }

        public Usuario ObterPorNIFComTipoUsuario(string nif)
        {
            return _context.Usuario.Include(usuario => usuario.TipoUsuario).FirstOrDefault(usuario => usuario.NIF == nif);
        }

        public void AtualizarSenha(Usuario usuario)
        {
            if (usuario == null) return;


            Usuario usuarioBanco = _context.Usuario.Find(usuario.UsuarioID);

            if (usuarioBanco == null) return;


            usuarioBanco.Senha = usuario.Senha;
            _context.SaveChanges();
        }

        public void AtualizarPrimeiroAcesso(Usuario usuario)
        {
            if (usuario == null) return;

            Usuario usuarioBanco = _context.Usuario.Find(usuario.UsuarioID);

            if (usuarioBanco == null) return;


            usuarioBanco.PrimeiroAcesso = usuario.PrimeiroAcesso;
            _context.SaveChanges();
        }

    }
}
