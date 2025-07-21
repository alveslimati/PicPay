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

        public async Task<CarteiraModel?> ObterCarteiraPorClienteIdAsync(string clienteId)
        {
            if (string.IsNullOrEmpty(clienteId))
                throw new ArgumentException("O ID do cliente não pode ser nulo ou vazio.", nameof(clienteId));

            return await _carteiraRepository.ObterCarteiraPorClienteIdAsync(clienteId);
        }

        public async Task<string> CriarCarteiraAsync(CarteiraModel carteira)
        {
            if (carteira == null)
                throw new ArgumentNullException(nameof(carteira), "Carteira não pode ser nula.");

            if (string.IsNullOrWhiteSpace(carteira.ClienteId))
                throw new ArgumentException("O ID do cliente é obrigatório.", nameof(carteira.ClienteId));

            if (carteira.Ativos == null)
                carteira.Ativos = new List<AtivoCarteiraModel>();

            foreach (var ativo in carteira.Ativos)
            {
                if (ativo.Quantidade <= 0)
                {
                    throw new ArgumentException($"A quantidade do ativo '{ativo.Codigo}' deve ser maior que zero.", nameof(carteira.Ativos));
                }

                if (string.IsNullOrWhiteSpace(ativo.Codigo))
                {
                    throw new ArgumentException("O ativo deve ter um código válido.", nameof(carteira.Ativos));
                }
            }
            carteira.ValorTotal = 0;
            return await _carteiraRepository.CriarCarteiraAsync(carteira);
        }

        public async Task<decimal> CalcularValorTotalCarteiraAsync(CarteiraModel carteira)
        {
            var ativosMock = await _ativoRepository.ListarAtivosAsync();

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