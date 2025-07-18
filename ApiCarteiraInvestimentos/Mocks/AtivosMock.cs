
using ApiCarteiraInvestimentos.Models;

namespace ApiCarteiraInvestimentos.Mocks
{
    public static class AtivosMock
    {
        public static List<AtivoModel> GetAtivos() => new List<AtivoModel>
        {
             new AtivoModel { Codigo = "CDB001", Tipo = "CDB", Nome = "CDB do Banco XP", PrecoUnitario = 100.50m },
             new AtivoModel { Codigo = "FII01", Tipo = "FII", Nome = "Fundo Imobiliário Y", PrecoUnitario = 120.75m },
             new AtivoModel { Codigo = "ACAO01", Tipo = "AÇÃO", Nome = "Ação da Empresa XPTO", PrecoUnitario = 15.25m }
        };              
    }
}