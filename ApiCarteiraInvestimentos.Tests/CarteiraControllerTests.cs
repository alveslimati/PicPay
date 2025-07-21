using ApiCarteiraInvestimentos.Controllers;
using ApiCarteiraInvestimentos.Models;
using ApiCarteiraInvestimentos.Repositories;
using ApiCarteiraInvestimentos.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ApiCarteiraInvestimentos.Tests
{
    public class CarteiraControllerTests
    {
        [Fact]
        public async Task DeveRetornar404ParaClienteInexistente()
        {
            // Arrange
            var mockCarteiraRepository = new Mock<ICarteiraRepository>();
            var mockAtivoRepository = new Mock<IAtivoRepository>();

            mockCarteiraRepository
                .Setup(repo => repo.ObterCarteiraPorClienteIdAsync(It.IsAny<string>()))
                .ReturnsAsync((CarteiraModel?)null);

            var carteiraService = new CarteiraService(mockCarteiraRepository.Object, mockAtivoRepository.Object);
            var controller = new CarteirasController(carteiraService);

            // Act
            var resultado = await controller.ConsultarCarteira("999xx"); 
            var resultadoNotFound = resultado as NotFoundObjectResult;

            // Assert
            Assert.NotNull(resultadoNotFound); 
            Assert.Equal(404, resultadoNotFound.StatusCode); 
        }
        
        [Fact]
        public async Task DeveRetornarCarteiraVaziaParaClienteSemAtivos()
        {
            // Arrange
            var mockCarteiraRepository = new Mock<ICarteiraRepository>();
            var mockAtivoRepository = new Mock<IAtivoRepository>();

            mockCarteiraRepository
                .Setup(repo => repo.ObterCarteiraPorClienteIdAsync("456"))
                .ReturnsAsync(new CarteiraModel
                {
                    ClienteId = "456",
                    Ativos = new List<AtivoCarteiraModel>() 
                });

            var carteiraService = new CarteiraService(mockCarteiraRepository.Object, mockAtivoRepository.Object);
            var controller = new CarteirasController(carteiraService);

            // Act
            var resultado = await controller.ConsultarCarteira("456") as OkObjectResult;

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(200, resultado.StatusCode);

            var response = resultado.Value as CarteiraModel;
            Assert.NotNull(response);

            Assert.Equal("456", response.ClienteId);
            Assert.Equal(0.0m, response.ValorTotal); 
            Assert.Empty(response.Ativos); 
        }

        [Fact]
        public async Task DeveRetornarNuloParaCarteiraInexistente()
        {
            // Arrange
            var repository = new CarteiraRepository();

            // Act
            var carteira = await repository.ObterCarteiraPorClienteIdAsync("999"); 

            // Assert
            Assert.Null(carteira);
        }
    }
}