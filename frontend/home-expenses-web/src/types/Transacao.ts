export interface Transacao {
    id: number;
    descricao: string;
    valor: number;
    tipo: number;  // O tipo é representado como number porque a API retorna o enum como 0 (Despesa) ou 1 (Receita)
    pessoaId: number;
}