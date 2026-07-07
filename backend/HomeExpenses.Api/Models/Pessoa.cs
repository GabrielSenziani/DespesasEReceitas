namespace HomeExpenses.Api.Models;

public class Pessoa {
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty; //evitar que dê um warning na propriedade string Nome por conta do .csproj que vem habilitado com <Nullable>enable</Nullable> (avisa quando uma propridade ta nula/vazia sem querer)
    public int Idade { get; set; }
    public bool IsMinor => Idade < 18; // aqui vai definir a diferemnça de menores, é uma pratica que vai facilitar na constrção do codigo inves de colocar Idade < 18 em varios lugares do codigo
}