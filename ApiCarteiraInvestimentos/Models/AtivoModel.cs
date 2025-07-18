namespace ApiCarteiraInvestimentos.Models
{
    public class AtivoModel
    {
        public string ?Codigo { get; set; }
        public string ?Tipo { get; set; }
        public string ?Nome { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}