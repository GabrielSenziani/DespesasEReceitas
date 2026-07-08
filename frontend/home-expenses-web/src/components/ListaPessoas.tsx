import { useState, useEffect } from "react";
import api from "../services/api";
import type { Pessoa } from "../types/Pessoa";

function ListaPessoas () {
    const [pessoas, setPessoas] = useState<Pessoa[]>([]); // aqui foi tipado pois pessoas é uma lista de objetos do tipo Pessoa
    const [nome, setNome] = useState(""); // o useState() vai guardar os dados que mudam, mesma coisa para pessoas e idade
    const [idade, setIdade] = useState(0);

    useEffect(() => {
      async function carregarPessoas() {
        const resposta = await api.get<Pessoa[]>("/Pessoas");
        setPessoas(resposta.data);
      }
      carregarPessoas();
    }, []);

    async function handleCriar() {
    await api.post("/Pessoas", {
        nome: nome,
        idade: idade,
    });

    const resposta = await api.get<Pessoa[]>("/Pessoas");
    setPessoas(resposta.data);
}

   async function handleDeletar(id: number) {
    await api.delete(`/Pessoas/${id}`);

    const resposta = await api.get<Pessoa[]>("/Pessoas");
     setPessoas(resposta.data);
   }

    return (
    <div>
        <div>
    <input
        type="text"
        placeholder="Nome"
        value={nome}
        onChange={(e) => setNome(e.target.value)}
    />
    <input
        type="number"
        placeholder="Idade"
        value={idade}
        onChange={(e) => setIdade(Number(e.target.value))}
    />
    <button onClick={handleCriar}>Cadastrar</button>
   </div>
        <ul>
            {pessoas.map((pessoa) => (
                <li key={pessoa.id}>
                    {pessoa.nome} — {pessoa.idade} anos
                    <button onClick={() => handleDeletar(pessoa.id)}>Deletar</button>
                </li>
            ))}
        </ul>
    </div>
         
);
}

export default ListaPessoas;