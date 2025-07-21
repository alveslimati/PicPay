using ApiCarteiraInvestimentos.Models;
using ApiCarteiraInvestimentos.Repositories;
using ApiCarteiraInvestimentos.Mocks;

namespace ApiCarteiraInvestimentos.Tests
{
    public class CarteiraRepositoryTests
    {
        [Fact]
        public async Task DeveAdicionarCarteiraAoMockCorretamenteAsync()
        {
            // Arrange
            var repository = new CarteiraRepository();
            var novaCarteira = new CarteiraModel
            {
                ClienteId = "456",
                Ativos = new List<AtivoCarteiraModel>
                {
                    new AtivoCarteiraModel { Codigo = "CDB001", Quantidade = 10 }
                }
            };

            // Act
            var idCarteira = await repository.CriarCarteiraAsync(novaCarteira);

            // Assert
            Assert.NotNull(idCarteira);
            var carteiraAdicionada = CarteiraMock.Carteiras.Find(c => c.Id == idCarteira);
            Assert.NotNull(carteiraAdicionada);
            Assert.Equal("456", carteiraAdicionada.ClienteId);
        }
    }
}