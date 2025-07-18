using ApiCarteiraInvestimentos.Models;
using ApiCarteiraInvestimentos.Repositories;

namespace ApiCarteiraInvestimentos.Services
{
    public class CarteiraService
    {
        private readonly ICarteiraRepository _carteiraRepository;
        private readonly IAtivoRepository _ativoRepository;

        public CarteiraService(ICarteiraRepository carteiraRepository, IAtivoRepository ativoRepository)
        {
            _carteiraRepository = carteiraRepository;
            _ativoRepository = ativoRepository;
        }
        public virtual CarteiraModel? ObterCarteiraPorClienteId(string clienteId)
        {
            if (string.IsNullOrEmpty(clienteId))
                throw new ArgumentException("O ID do cliente não pode ser nulo ou vazio.", nameof(clienteId));

            return _carteiraRepository.ObterCarteiraPorClienteId(clienteId);
        }
        public string CriarCarteira(CarteiraModel carteira)
        {
            if (carteira == null)
                throw new ArgumentNullException(nameof(carteira), "Carteira não pode ser nula.");
            if (carteira.Ativos == null || !carteira.Ativos.Any())
                throw new ArgumentException("A carteira deve conter pelo menos um ativo.", nameof(carteira.Ativos));

            return _carteiraRepository.CriarCarteira(carteira);
        }
        public decimal CalcularValorTotalCarteira(CarteiraModel carteira)
        {
            var ativosMock = _ativoRepository.ListarAtivos();

            return carteira.Ativos.Sum(ativoCarteira =>
            {
                var precoUnitario = ativosMock
                    .FirstOrDefault(ativo => ativo.Codigo == ativoCarteira.Codigo)
                    ?.PrecoUnitario ?? 0;

                return precoUnitario * ativoCarteira.Quantidade;
            });
        }
    }
}