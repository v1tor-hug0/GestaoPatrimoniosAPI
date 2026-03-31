using GestaoPatrimonio.Domains;
using GestaoPatrimonio.DTOs.UsuarioDto;
using GestaoPatrimonio.Interfaces;

namespace GestaoPatrimonio.Applications.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarUsuarioDto> Listar()
        {
            List<Usuario> usuarios = _repository.Listar();

            List<ListarUsuarioDto> usuariosDto = usuarios.Select(u => new ListarUsuarioDto
            {
                UsuarioID = u.UsuarioID,
                NIF = u.NIF,
                Nome = u.Nome,
                RG = u.RG,
                CPF = u.CPF,
                CarteiraTrabalho = u.CarteiraTrabalho,
                Email = u.Email,
                Ativo = u.Ativo,
                PrimeiroAcesso = u.PrimeiroAcesso,
                EnderecoID = u.EnderecoID,
                CargoID = u.CargoID,
                TipoUsuarioID = u.TipoUsuarioID
            }).ToList();
            return usuariosDto;
        }
    }
}
