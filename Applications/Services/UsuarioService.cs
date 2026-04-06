using GestaoPatrimonio.Applications.Autenticacao;
using GestaoPatrimonio.Applications.Regras;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.DTOs.UsuarioDto;
using GestaoPatrimonio.Exceptions;
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

        public ListarUsuarioDto BuscarPorId(Guid usuarioId)
        {
            Usuario usuario = _repository.BuscarPorId(usuarioId);

            if (usuario == null)
            {
                throw new DomainException("Usuario nao encontrado");
            }

            ListarUsuarioDto usuarioDto = new ListarUsuarioDto
            {
                UsuarioID = usuario.UsuarioID,
                NIF = usuario.NIF,
                Nome = usuario.Nome,
                RG = usuario.RG,
                CPF = usuario.CPF,
                CarteiraTrabalho = usuario.CarteiraTrabalho,
                Email = usuario.Email,
                Ativo = usuario.Ativo,
                PrimeiroAcesso = usuario.PrimeiroAcesso,
                EnderecoID = usuario.EnderecoID,
                CargoID = usuario.CargoID,
                TipoUsuarioID = usuario.TipoUsuarioID
            };

            return usuarioDto;
        }

        public void Adicionar(CriarUsuarioDto usuarioDto)
        {
            Validar.ValidarNome(usuarioDto.Nome);
            Validar.ValidarNIF(usuarioDto.NIF);
            Validar.ValidarCPF(usuarioDto.CPF);
            Validar.ValidarEmail(usuarioDto.Email);

            Usuario usuarioDuplicado = _repository.BuscarDuplicado(usuarioDto.NIF, usuarioDto.CPF, usuarioDto.Email);

            if (usuarioDuplicado != null)
            {
                if (usuarioDuplicado.NIF == usuarioDto.NIF)
                {
                    throw new DomainException("NIF ja cadastrado");
                }
                if (usuarioDuplicado.CPF == usuarioDto.CPF)
                {
                    throw new DomainException("CPF ja cadastrado");
                }
                if (usuarioDuplicado.Email == usuarioDto.Email)
                {
                    throw new DomainException("Email ja cadastrado");
                }
            }

            if (!_repository.EnderecoExiste(usuarioDto.EnderecoID))
            {
                throw new DomainException("Endereco nao existe");
            }
            if (!_repository.CargoExiste(usuarioDto.CargoID))
            {
                throw new DomainException("Cargo nao existe");
            }
            if (!_repository.TipoUsuarioExiste(usuarioDto.TipoUsuarioID))
            {
                throw new DomainException("Tipo de usuario nao exite");
            }

            Usuario usuario = new Usuario
            {
                NIF = usuarioDto.NIF,
                Nome = usuarioDto.Nome,
                RG = usuarioDto.RG,
                CPF = usuarioDto.CPF,
                CarteiraTrabalho = usuarioDto.CarteiraTrabalho,
                Senha = CriptografiaUsuario.CriptografarSenha(usuarioDto.NIF),
                Email = usuarioDto.Email,
                Ativo = true,
                PrimeiroAcesso = true,
                EnderecoID = usuarioDto.EnderecoID,
                CargoID = usuarioDto.CargoID,
                TipoUsuarioID = usuarioDto.TipoUsuarioID
            };

            _repository.Adicionar(usuario);
        }

        public void Atualizar(Guid usuarioId, CriarUsuarioDto dto)
        {
            Validar.ValidarNome(dto.Nome);
            Validar.ValidarNIF(dto.NIF);
            Validar.ValidarCPF(dto.CPF);
            Validar.ValidarEmail(dto.Email);

            Usuario usuarioBanco = _repository.BuscarPorId(usuarioId);

            if (usuarioBanco == null)
            {
                throw new DomainException("Usuario nao encontrado");
            }

            Usuario usuarioDuplicado = _repository.BuscarDuplicado(dto.NIF, dto.CPF, dto.Email, usuarioId);

            if (usuarioDuplicado != null)
            {
                if (usuarioDuplicado.NIF == dto.NIF)
                {
                    throw new DomainException("Ja existe um usuario com esse NIF");
                }

                if (usuarioDuplicado.CPF == dto.CPF)
                {
                    throw new DomainException("Ja existe um usuario com esse CPF");
                }

                if (usuarioDuplicado.Email.ToLower() == dto.Email.ToLower())
                {
                    throw new DomainException("Ja existe um usuario com esse Email");
                }
            }

            if (!_repository.EnderecoExiste(dto.EnderecoID))
            {
                throw new DomainException("Endereco nao existe");
            }
            if (!_repository.CargoExiste(dto.CargoID))
            {
                throw new DomainException("Cargo nao existe");
            }
            if (!_repository.TipoUsuarioExiste(dto.TipoUsuarioID))
            {
                throw new DomainException("Tipo de usuario nao exite");
            }

            usuarioBanco.NIF = dto.NIF;
            usuarioBanco.Nome = dto.Nome;
            usuarioBanco.RG = dto.RG;
            usuarioBanco.CPF = dto.CPF;
            usuarioBanco.CarteiraTrabalho = dto.CarteiraTrabalho;
            usuarioBanco.Email = dto.Email;
            usuarioBanco.EnderecoID = dto.EnderecoID;
            usuarioBanco.CargoID = dto.CargoID;
            usuarioBanco.TipoUsuarioID = dto.TipoUsuarioID;

            _repository.Atualizar(usuarioBanco);

        }

        public void AtualizarStatus(Guid usuarioId, AtualizarStatusUsuarioDto dto)
        {
            Usuario usuarioBanco = _repository.BuscarPorId(usuarioId);

            if(usuarioBanco == null) 
                throw new DomainException("Usuario nao encontrado");

            usuarioBanco.Ativo = dto.Ativo;
            _repository.AtualizarStatus(usuarioBanco);
        }


    }

}

