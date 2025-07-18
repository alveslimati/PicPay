using ApiCarteiraInvestimentos.Models;
using System.Collections.Generic;

namespace ApiCarteiraInvestimentos.Repositories
{
    public interface IAtivoRepository
    {
        List<AtivoModel> ListarAtivos();
    }
}