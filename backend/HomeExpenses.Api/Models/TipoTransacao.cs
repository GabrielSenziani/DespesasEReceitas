namespace HomeExpenses.Api.Models;

public enum TipoTransacao 
    { 
       Despesa,
       Receita
    }
//utilizei o enum inves de string porque o enum alem de impedir o digito de um valor invalido, fica mais facil de compara e usar no if/switch
//também foi criado outro arquivo separado para ele porque ele pode ser utilizado por outras classes