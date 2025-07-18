namespace ApiCarteiraInvestimentos.Models
{
    public class CarteiraModel
    {
        public string? Id { get; set; }
        public string? ClienteId { get; set; }
        public List<AtivoCarteiraModel> Ativos { get; set; } = [];
        public decimal ValorTotal { get; set; }
    }

    public class AtivoCarteiraModel
    {
        public string ?Codigo { get; set; }
        public int Quantidade { get; set; } 
    }
}