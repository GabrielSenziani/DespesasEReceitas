using Microsoft.AspNetCore.Mvc;
using HomeExpenses.Api.Data;
using HomeExpenses.Api.Models;
using HomeExpenses.Api.DTOs;

namespace HomeExpenses.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoasController : ControllerBase
{
    private readonly AppDbContext _context;

    public PessoasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Listar()
    {
        return Ok(_context.TabelaPessoa.ToList()); //O metodo ToList() vai retornar tudo que esta dentro da tabela
    }

    [HttpGet("totais")]
    public IActionResult Totais() 
    {
    var resultado = new List<PessoaTotalDto>();

    foreach (var pessoa in _context.TabelaPessoa.ToList()) // para cada pesoa na tabela de pessoas, vai executar o bloco abaixo, chamando essa pessoa de "pessoa"
    {
     var totalReceitas = _context.TabelaTransacao
    .Where(t => t.PessoaId == pessoa.Id && t.Tipo == TipoTransacao.Receita)
    .ToList() // o SQLite tinha uma limitação de não saber fazer o sum diretamente com numeros decimais, então sem o metodo ToList(), o EF Core tenta traduzr tudo para uma unica query SQL, mas se incluir o ToList(), o EF Core executa o so Where() no banco 
    .Sum(t => t.Valor); // Calcula o total de receitas registrados no id da pessoa

    var totalDespesas = _context.TabelaTransacao
    .Where(t => t.PessoaId == pessoa.Id && t.Tipo == TipoTransacao.Despesa)
    .ToList()
    .Sum(t => t.Valor); //Calcula o total de Despesas da pessoa de acordo com o que tem registrado no id dela
    
    resultado.Add(new PessoaTotalDto
{
    Id = pessoa.Id,
    Nome = pessoa.Nome,
    TotalReceitas = totalReceitas,
    TotalDespesas = totalDespesas,
    Saldo = totalReceitas - totalDespesas
}); //Usando o metodo Add(), vai adicionar um novo Dto, na qual vai retornar o quando saiu do saldo da pessoa, que vai ser baseado na subtração dos gastos com despesas e receitas
    }

    var totalGeralReceitas = resultado.Sum(p => p.TotalReceitas);
    var totalGeralDespesas = resultado.Sum(p => p.TotalDespesas);
    var saldoGeralLiquido = totalGeralReceitas - totalGeralDespesas;
    var totalGeral = new TotalGeralDto 
    {
        Pessoas = resultado,
        TotalDeReceitas = totalGeralReceitas,
        TotalDeDespesas = totalGeralDespesas,
        SaldoGeralTotal = saldoGeralLiquido
    };

    return Ok(totalGeral);
}
    

    [HttpPost]
    public IActionResult Criar(Pessoa pessoa)
    {
    _context.TabelaPessoa.Add(pessoa); // Add() vai adicionar uma pessoa ao banco de dados, para isso é necessario inserir o body de acordo com o que foi escrito no models Pessoa.cs
    _context.SaveChanges();
    return Ok(pessoa);
    }

    [HttpDelete("{id}")]
    public IActionResult Deletar(int id)
    {
    var pessoa = _context.TabelaPessoa.Find(id); // o metodo Find() vai buscar a pessoa pelo id, se não achar, vai retornar null
    if (pessoa == null) {
      return NotFound(); // Se a busca pelo id da pessoa na tabela for = a null atraves do metodo .Find(), vai retornar um erro comum de não encontrado (NotFound) 
    }

    var transacoesDaPessoa = _context.TabelaTransacao.Where(t => t.PessoaId == id).ToList(); // para cada transação t, vai manter ela na listta se t.pessoa for == ao id que veio na URL

    _context.TabelaTransacao.RemoveRange(transacoesDaPessoa); // o metodo RemoveRange inves de remover apenas uma coisa, vai remover uma lista inteira que está relacionada ao id da pessoa
    _context.TabelaPessoa.Remove(pessoa); // Caso passe pelo erro de NotFound, a rota delete entra em ação com o Remove()
    _context.SaveChanges(); // Salva as mudanças no banco de dados
    return NoContent(); // Retorno comum de http 204
    }
}