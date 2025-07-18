using ApiCarteiraInvestimentos.Models;
using ApiCarteiraInvestimentos.Repositories;
using ApiCarteiraInvestimentos.Services;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ApiCarteiraInvestimentos.Tests
{
    public class CarteiraServiceTests
    {
        [Fact]
        public void DeveCalcularValorTotalCorretamente()
        {
            // Arrange
            var mockCarteiraRepository = new Mock<ICarteiraRepository>(); // Mock da interface
            var mockAtivoRepository = new Mock<IAtivoRepository>(); // Mock da interface

            // Simular a lista de ativos
            mockAtivoRepository
                .Setup(repo => repo.ListarAtivos())
                .Returns(new List<AtivoModel>
                {
                    new AtivoModel { Codigo = "CDB001", PrecoUnitario = 100.0m },
                    new AtivoModel { Codigo = "FII001", PrecoUnitario = 50.0m }
                });

            // Criar uma carteira mockada para o teste
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
            var valorTotal = carteiraService.CalcularValorTotalCarteira(carteira);

            // Assert
            Assert.Equal(1250.0m, valorTotal); // 10 * 100 + 5 * 50 = 1250
        }
        [Fact]
        public void DeveRetornarZeroQuandoNaoHaAtivosNaCarteira()
        {
            // Arrange
            var mockCarteiraRepository = new Mock<ICarteiraRepository>();
            var mockAtivoRepository = new Mock<IAtivoRepository>();

            // Simular que não existem ativos cadastrados no repositório
            mockAtivoRepository
                .Setup(repo => repo.ListarAtivos())
                .Returns(new List<AtivoModel>()); // Lista vazia

            // Criar uma carteira sem ativos
            var carteira = new CarteiraModel
            {
                ClienteId = "123",
                Ativos = new List<AtivoCarteiraModel>() // Sem ativos
            };

            var carteiraService = new CarteiraService(mockCarteiraRepository.Object, mockAtivoRepository.Object);

            // Act
            var valorTotal = carteiraService.CalcularValorTotalCarteira(carteira);

            // Assert
            Assert.Equal(0.0m, valorTotal);
        }
    }
}