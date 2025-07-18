using ApiCarteiraInvestimentos.Models;
using ApiCarteiraInvestimentos.Mocks;
using System.Linq;

namespace ApiCarteiraInvestimentos.Repositories
{
    public class CarteiraRepository : ICarteiraRepository
    {
        public string CriarCarteira(CarteiraModel carteira)
        {
            return CarteiraMock.AdicionarCarteira(carteira);
        }

        public CarteiraModel? ObterCarteiraPorClienteId(string clienteId)
        {
            return CarteiraMock.Carteiras.FirstOrDefault(c => c.ClienteId == clienteId);
        }
    }
}