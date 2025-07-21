using ApiCarteiraInvestimentos.Models;
using ApiCarteiraInvestimentos.Repositories;
using ApiCarteiraInvestimentos.Services;
using Moq;

namespace ApiCarteiraInvestimentos.Tests
{
    public class CarteiraServiceTests
    {
        [Fact]
        public async Task DeveCalcularValorTotalCorretamenteAsync()
        {
            // Arrange
            var mockCarteiraRepository = new Mock<ICarteiraRepository>();
            var mockAtivoRepository = new Mock<IAtivoRepository>();

            mockAtivoRepository
                .Setup(repo => repo.ListarAtivosAsync())
                .ReturnsAsync(new List<AtivoModel>
                {
                    new AtivoModel { Codigo = "CDB001", PrecoUnitario = 100.0m },
                    new AtivoModel { Codigo = "FII001", PrecoUnitario = 50.0m }
                });

            var carteira = new CarteiraModel
            {
                ClienteId = "123",
                Ativos = new List<AtivoCarteiraModel>
                {
                    new AtivoCarteiraModel { Codigo = "CDB001", Quantidade = 10 },
                    new AtivoCarteiraModel { Codigo = "FII001", Quantidade = 5 }
                }
            };

            var carteiraService = new CarteiraService(mockCarteiraRepository.Object, mockAtivoRepository.Object);

            // Act
            var valorTotal = await carteiraService.CalcularValorTotalCarteiraAsync(carteira);

            // Assert
            Assert.Equal(1250.0m, valorTotal);
        }

        [Fact]
        public async Task DeveRetornarZeroQuandoNaoHaAtivosNaCarteiraAsync()
        {
            // Arrange
            var mockCarteiraRepository = new Mock<ICarteiraRepository>();
            var mockAtivoRepository = new Mock<IAtivoRepository>();

            mockAtivoRepository
                .Setup(repo => repo.ListarAtivosAsync())
                .ReturnsAsync(new List<AtivoModel>());

            var carteira = new CarteiraModel
            {
                ClienteId = "123",
                Ativos = new List<AtivoCarteiraModel>()
            };

            var carteiraService = new CarteiraService(mockCarteiraRepository.Object, mockAtivoRepository.Object);

            // Act
            var valorTotal = await carteiraService.CalcularValorTotalCarteiraAsync(carteira);

            // Assert
            Assert.Equal(0.0m, valorTotal);
        }
        [Fact]
        public async Task DeveCriarCarteiraComAtivosValidosAsync()
        {
            // Arrange
            var mockCarteiraRepository = new Mock<ICarteiraRepository>();

            mockCarteiraRepository
                .Setup(repo => repo.CriarCarteiraAsync(It.IsAny<CarteiraModel>()))
                .ReturnsAsync("123");

            var mockAtivoRepository = new Mock<IAtivoRepository>();
            var carteiraService = new CarteiraService(mockCarteiraRepository.Object, mockAtivoRepository.Object);

            var novaCarteira = new CarteiraModel
            {
                ClienteId = "123",
                Ativos = new List<AtivoCarteiraModel>
                {
                    new AtivoCarteiraModel { Codigo = "CDB001", Quantidade = 10 }
                }
            };

            // Act
            var idCarteira = await carteiraService.CriarCarteiraAsync(novaCarteira);

            // Assert
            Assert.NotNull(idCarteira);
            Assert.Equal("123", idCarteira);
        }

        [Fact]
        public async Task DeveRejeitarCarteiraComAtivoInvalidoAsync()
        {
            // Arrange
            var mockCarteiraRepository = new Mock<ICarteiraRepository>();

            var mockAtivoRepository = new Mock<IAtivoRepository>();
            var carteiraService = new CarteiraService(mockCarteiraRepository.Object, mockAtivoRepository.Object);
            var novaCarteira = new CarteiraModel
            {
                ClienteId = "123",
                Ativos = new List<AtivoCarteiraModel>
                {
                    new AtivoCarteiraModel { Codigo = "CDB001", Quantidade = 0 } // Quantidade inválida
                }
            };

            // Assert & Act
            await Assert.ThrowsAsync<ArgumentException>(async () =>
                await carteiraService.CriarCarteiraAsync(novaCarteira)
            );
        }

        [Fact]
        public async Task DeveRejeitarCarteiraSemClienteIdAsync()
        {
            // Arrange
            var mockCarteiraRepository = new Mock<ICarteiraRepository>();

            var mockAtivoRepository = new Mock<IAtivoRepository>();
            var carteiraService = new CarteiraService(mockCarteiraRepository.Object, mockAtivoRepository.Object);

            var novaCarteira = new CarteiraModel
            {
                ClienteId = string.Empty, 
                Ativos = new List<AtivoCarteiraModel>
                {
                    new AtivoCarteiraModel { Codigo = "CDB001", Quantidade = 10 }
                }
            };

            // Assert & Act
            await Assert.ThrowsAsync<ArgumentException>(async () =>
                await carteiraService.CriarCarteiraAsync(novaCarteira)
            );
        }
    }
}