export interface PessoaTotal {
    id: number;
    nome: string;
    totalReceitas: number;
    totalDespesas: number;
    saldo: number;
}

export interface TotalGeral {
    pessoas: PessoaTotal[]; //representa o objeto retornado dentro do array de totais
    totalDeReceitas: number;
    totalDeDespesas: number;
    saldoGeralTotal: number;
}