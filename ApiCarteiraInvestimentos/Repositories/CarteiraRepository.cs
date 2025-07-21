using ApiCarteiraInvestimentos.Mocks;
using ApiCarteiraInvestimentos.Models;

namespace ApiCarteiraInvestimentos.Repositories
{
    public class CarteiraRepository : ICarteiraRepository
    {
        public async Task<CarteiraModel?> ObterCarteiraPorClienteIdAsync(string clienteId)
        {
            return await Task.FromResult(CarteiraMock.Carteiras
                .FirstOrDefault(c => c.ClienteId == clienteId));
        }

        public async Task<string> CriarCarteiraAsync(CarteiraModel carteira)
        {
            var id = Guid.NewGuid().ToString();
            carteira.Id = id;
            CarteiraMock.Carteiras.Add(carteira);
            return await Task.FromResult(id);
        }
    }
}