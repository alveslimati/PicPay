using ApiCarteiraInvestimentos.Models;

namespace ApiCarteiraInvestimentos.Repositories
{
    public interface ICarteiraRepository
    {
        CarteiraModel? ObterCarteiraPorClienteId(string clienteId);
        string CriarCarteira(CarteiraModel carteira);
    }
}