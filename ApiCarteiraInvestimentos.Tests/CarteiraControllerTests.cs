using ApiCarteiraInvestimentos.Controllers;
using ApiCarteiraInvestimentos.Models;
using ApiCarteiraInvestimentos.Repositories;
using ApiCarteiraInvestimentos.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ApiCarteiraInvestimentos.Tests
{
    public class CarteiraControllerTests
    {
        [Fact]
        public void DeveRetornar404ParaClienteInexistente()
        {
            // Arrange
            var mockCarteiraRepository = new Mock<ICarteiraRepository>(); // Mock da Interface!
            var mockAtivoRepository = new Mock<AtivoRepository>(); // Mock do AtivoRepository

            // Simular que nenhuma carteira é encontrada
            mockCarteiraRepository
                .Setup(repo => repo.ObterCarteiraPorClienteId(It.IsAny<string>()))
                .Returns((CarteiraModel?)null);

            // Criar o CarteiraService com dependências mockadas
            var carteiraService = new CarteiraService(mockCarteiraRepository.Object, mockAtivoRepository.Object);

            // Criar o controller usando o serviço
            var controller = new CarteirasController(carteiraService);

            // Act
            var resultado = controller.ConsultarCarteira("999xx"); // Cliente inexistente
            var resultadoNotFound = resultado as NotFoundObjectResult;

            // Assert
            Assert.NotNull(resultadoNotFound); // Garantir que o resultado não é nulo
            Assert.Equal(404, resultadoNotFound.StatusCode); // Status 404 esperado
        }
        [Fact]
        public void DeveRetornarCarteiraVaziaParaClienteSemAtivos()
        {
            // Arrange
            var mockCarteiraRepository = new Mock<ICarteiraRepository>();
            var mockAtivoRepository = new Mock<IAtivoRepository>();

            // Configurar o mock para retornar uma carteira sem ativos
            mockCarteiraRepository
                .Setup(repo => repo.ObterCarteiraPorClienteId("456"))
                .Returns(new CarteiraModel
                {
                    ClienteId = "456",
                    Ativos = new List<AtivoCarteiraModel>() // Sem ativos
                });

            var carteiraService = new CarteiraService(mockCarteiraRepository.Object, mockAtivoRepository.Object);
            var controller = new CarteirasController(carteiraService);

            // Act
            var resultado = controller.ConsultarCarteira("456") as OkObjectResult;

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(200, resultado.StatusCode);

            // Verificar se o resultado é do tipo CarteiraResponse
            var response = resultado.Value as CarteiraModel;
            Assert.NotNull(response);

            // Validar o conteúdo do objeto de resposta
            Assert.Equal("456", response.ClienteId);
            Assert.Equal(0.0m, response.ValorTotal); // Valor total deve ser 0
            Assert.Empty(response.Ativos); // Certificar que a lista de ativos está vazia
        }

        [Fact]
        public void DeveRetornarNuloParaCarteiraInexistente()
        {
            // Arrange
            var repository = new CarteiraRepository();

            // Act
            var carteira = repository.ObterCarteiraPorClienteId("999"); // Cliente inexistente

            // Assert
            Assert.Null(carteira); // Deve retornar nulo
        }
    }
}