using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ApiCarteiraInvestimentos.Models
{
    public class CarteiraModel
{
    public string? Id { get; set; } 

    public required string ClienteId { get; set; } 

    public required List<AtivoCarteiraModel> Ativos { get; set; }

    [BindNever]
    public decimal ValorTotal { get; set; }
}

public class AtivoCarteiraModel
{
    public required string Codigo { get; set; }
    public int Quantidade { get; set; }
}
}