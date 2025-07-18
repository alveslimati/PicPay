using ApiCarteiraInvestimentos.Models;

namespace ApiCarteiraInvestimentos.Mocks
{
    public static class CarteiraMock
    {
        public static List<CarteiraModel> Carteiras { get; } = new List<CarteiraModel>
        {
            new CarteiraModel
            {
                Id = "1",
                ClienteId = "123",
                Ativos = new List<AtivoCarteiraModel> 
                {
                    new AtivoCarteiraModel { Codigo = "CDB001", Quantidade = 10 },
                    new AtivoCarteiraModel { Codigo = "VALE3", Quantidade = 5 },
                    new AtivoCarteiraModel { Codigo = "SBF3", Quantidade = 5 },
                    new AtivoCarteiraModel { Codigo = "ABEV3", Quantidade = 15 },
                    new AtivoCarteiraModel { Codigo = "ACAO01", Quantidade = 8 }
                }
            },
            new CarteiraModel
            {
                Id = "2",
                ClienteId = "456",
                Ativos = new List<AtivoCarteiraModel> 
                {
                    new AtivoCarteiraModel { Codigo = "CDB001", Quantidade = 20 },
                    new AtivoCarteiraModel { Codigo = "BBDC3", Quantidade = 10 },
                    new AtivoCarteiraModel { Codigo = "FII01", Quantidade = 10 },
                    new AtivoCarteiraModel { Codigo = "SBF3", Quantidade = 10 },
                    new AtivoCarteiraModel { Codigo = "VALE3", Quantidade = 8 },
                    new AtivoCarteiraModel { Codigo = "ACAO01", Quantidade = 12 },
                    new AtivoCarteiraModel { Codigo = "MGLU3", Quantidade = 5 },
                    new AtivoCarteiraModel { Codigo = "LREN3", Quantidade = 7 },
                    new AtivoCarteiraModel { Codigo = "BBDC3", Quantidade = 6 }
                }
            }
        };
        public static string AdicionarCarteira(CarteiraModel carteira)
        {
            carteira.Id = Guid.NewGuid().ToString();
            Carteiras.Add(carteira);
            return carteira.Id;
        }
    }
}