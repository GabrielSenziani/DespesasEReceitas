namespace HomeExpenses.Api.DTOs;

public class TotalGeralDto 
{
    public List<PessoaTotalDto> Pessoas { get; set; } = new(); // O new() é uma forma resumida de new List<PessoaTotalDto>(), então o compilador ja sabe o tipo pelo contexto, é bom colocar isso porque o List<PessoaTotalDto> Pessoas é um tipo de referencia, então o compilador poderia dar um warning sobre a possibilidade dela ficar nula, então serve mais pra evitar essa poluição de warnings 
    public decimal TotalDeReceitas { get; set; }
    public decimal TotalDeDespesas { get; set; }
    public decimal SaldoGeralTotal { get; set; }
}
