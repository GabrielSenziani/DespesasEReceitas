import { useState, useEffect } from "react";
import api from "../services/api";
import type { Pessoa } from "../types/Pessoa";
import type { Transacao } from "../types/Transacao";

interface TransacoesProps {
    onAtualizar: () => void;
}

function Transacoes({ onAtualizar }: TransacoesProps) {
    const [transacoes, setTransacoes] = useState<Transacao[]>([]);
    const [pessoas, setPessoas] = useState<Pessoa[]>([]);
    const [descricao, setDescricao] = useState("");
    const [valor, setValor] = useState(0);
    const [tipo, setTipo] = useState(0); // 0 = Despesa, 1 = Receita
    const [pessoaId, setPessoaId] = useState(0);

    async function carregarTransacoes() {
        const resposta = await api.get<Transacao[]>("/Transacoes");
        setTransacoes(resposta.data);
    }

    async function carregarPessoas() {
        const resposta = await api.get<Pessoa[]>("/Pessoas");
        setPessoas(resposta.data);
    }

    useEffect(() => {
       async function carregarDados() {
         carregarTransacoes();
         carregarPessoas();
       }
       carregarDados();
    }, []);

    async function handleCriar() {
        await api.post("/Transacoes", {
            descricao: descricao,
            valor: valor,
            tipo: tipo,
            pessoaId: pessoaId,
        });

        await carregarTransacoes();

        // Atualiza o componente Totais
        onAtualizar();

        // limpa o formulário
       setDescricao("");
       setValor(0);
       setTipo(0);
       setPessoaId(0)
    }

    return (
        <div>
            <div>
                <input
                    type="text"
                    placeholder="Descrição"
                    value={descricao}
                    onChange={(e) => setDescricao(e.target.value)}
                />
                <input
                    type="number"
                    placeholder="Valor"
                    value={valor}
                    onChange={(e) => setValor(Number(e.target.value))}
                />
                <select value={tipo} onChange={(e) => setTipo(Number(e.target.value))}>
                    <option value={0}>Despesa</option>
                    <option value={1}>Receita</option>
                </select>
                <select value={pessoaId} onChange={(e) => setPessoaId(Number(e.target.value))}>
                    <option value={0}>Selecione a pessoa</option>
                    {pessoas.map((pessoa) => (
                        <option key={pessoa.id} value={pessoa.id}>
                            {pessoa.nome}
                        </option>
                    ))}
                </select>
                <button onClick={handleCriar}>Cadastrar Transação</button>
            </div>

            <ul>
                {transacoes.map((transacao) => (
                    <li key={transacao.id}>
                        {transacao.descricao} — R$ {transacao.valor} —{" "}
                        {transacao.tipo === 0 ? "Despesa" : "Receita"}
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default Transacoes;