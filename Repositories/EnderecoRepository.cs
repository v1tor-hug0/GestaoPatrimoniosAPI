using GestaoPatrimonio.Applications.Regras;
using GestaoPatrimonio.Contexts;
using GestaoPatrimonio.Domains;
using GestaoPatrimonio.Interfaces;

namespace GestaoPatrimonio.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly GestaoPatrimoniosContext _context;
        public EnderecoRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<Endereco> Listar()
        {
            return _context.Endereco.OrderBy(e => e.Logradouro).ToList();
        }

        public Endereco BuscarPorId(Guid enderecoId)
        {
            return _context.Endereco.Find(enderecoId);
        }

        public Endereco BuscarPorLogradouroENumero(string logradouro, int? numero, Guid bairroId)
        {
            return _context.Endereco.FirstOrDefault(e => e.Logradouro == logradouro && e.Numero == numero && e.BairroID == bairroId);
        }

        public bool BairroExiste(Guid bairroId)
        {
            return _context.Bairro.Any(b => b.BairroID == bairroId);
        }

        public void Adicionar(Endereco endereco)
        {
            _context.Endereco.Add(endereco);
            _context.SaveChanges();
        }

        public void Atualizar(Endereco endereco)
        {
            if (endereco == null)
                return;

            Endereco enderecoBanco = _context.Endereco.Find(endereco.EnderecoID);
            if(enderecoBanco == null)
                return;

            enderecoBanco.Logradouro = endereco.Logradouro;
            enderecoBanco.Numero = endereco.Numero;
            enderecoBanco.Complemento = endereco.Complemento;
            enderecoBanco.CEP = endereco.CEP;
            enderecoBanco.BairroID = endereco.BairroID;
            enderecoBanco.Bairro = endereco.Bairro;

            _context.Endereco.Update(enderecoBanco);
            _context.SaveChanges();

        }
    }
}
