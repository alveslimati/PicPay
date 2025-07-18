using ApiCarteiraInvestimentos.Models;
using ApiCarteiraInvestimentos.Mocks;

namespace ApiCarteiraInvestimentos.Repositories
{
    public class AtivoRepository: IAtivoRepository
    {
        public List<AtivoModel> ListarAtivos()
        {
            return AtivosMock.GetAtivos();
        }
    }
}