import "./App.css";
import { useState } from "react";

import ListaPessoas from "./components/ListaPessoas";
import Transacoes from "./components/Transacoes";
import Totais from "./components/Totais";

function App() {
    const [atualizacao, setAtualizacao] = useState(0);

    function atualizarDados() {
        setAtualizacao((valor) => valor + 1);
    }

    return (
        <>
            <h1>Controle de Gastos Residenciais</h1>

            <ListaPessoas onAtualizar={atualizarDados} />

            <Transacoes onAtualizar={atualizarDados} />

            <Totais atualizacao={atualizacao} />
        </>
    );
}

export default App;