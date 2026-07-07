using Microsoft.EntityFrameworkCore;
using HomeExpenses.Api.Models;

namespace HomeExpenses.Api.Data;

public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

 
public DbSet<Pessoa> TabelaPessoa { get; set; }
public DbSet<Transacao> TabelaTransacao { get; set; }
}

//dei sinal de que existem duas tabelas ao EF Core dentro da class AppDbContext