using GestaoPatrimonio.Domains;
using GestaoPatrimonio.DTOs.TipoUsuarioDto;
using GestaoPatrimonio.Interfaces;
using GestaoPatrimonio.Exceptions;
using GestaoPatrimonio.Applications.Regras;

namespace GestaoPatrimonio.Applications.Services
{
    public class TipoUsuarioService 
    {
        private readonly ITipoUsuarioRepository _repository;

        public TipoUsuarioService(ITipoUsuarioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarTipoUsuarioDto> Listar()
        {
            List<TipoUsuario> Tipos = _repository.Listar();

            List<ListarTipoUsuarioDto> TipoDto = Tipos.Select(t => new ListarTipoUsuarioDto
            {
                TipoUsuarioID = t.TipoUsuarioID,
                NomeTipo = t.NomeTipo
            }).ToList();
            return TipoDto;
        }

        public ListarTipoUsuarioDto BuscarPorId(Guid TipoUsuarioID)
        {
            TipoUsuario tipo = _repository.BuscarPorId(TipoUsuarioID);

            if (tipo == null)
            {
                throw new DomainException("Tipo de usuário não encontrado.");
            }

            ListarTipoUsuarioDto tipoDto = new ListarTipoUsuarioDto()
            {
                TipoUsuarioID = tipo.TipoUsuarioID,
                NomeTipo = tipo.NomeTipo
            };
            return tipoDto;
        }

        public void Adicionar(CriarTipoUsuarioDto dto)
        {
            Validar.ValidarNome(dto.NomeTipo);

            TipoUsuario tipoExiste = _repository.BuscarPorNome(dto.NomeTipo);

            if(tipoExiste != null)
            {
                throw new DomainException("Já existe um tipo de usuário com esse nome.");
            }

            TipoUsuario tipo = new TipoUsuario
            {
                NomeTipo = dto.NomeTipo
            };

            _repository.Adicionar(tipo);
        }

        public void Atualizar (Guid id, CriarTipoUsuarioDto dto)
        {
            Validar.ValidarNome(dto.NomeTipo);

            TipoUsuario tipoBanco = _repository.BuscarPorId(id);

            if(tipoBanco == null)
            {
                throw new DomainException("Tipo de usuário não encontrado.");
            }

            TipoUsuario TipoExiste = _repository.BuscarPorNome(dto.NomeTipo);

            if (TipoExiste != null)
            {
                throw new DomainException("Já existe um tipo de usuário com esse nome.");
            }

            tipoBanco.NomeTipo = dto.NomeTipo;
            _repository.Atualizar(tipoBanco);
        }
    }
}
