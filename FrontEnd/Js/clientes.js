const API_URL = "https://localhost:5104/api";

export async function buscarClientes() {
    try {
        const resposta = await fetch(`${API_URL}/clientes`);

        if (!resposta.ok) {
            throw new Error("Erro ao buscar clientes.");
        }

        const clientes = await resposta.json();

        console.log(clientes);

        return clientes;
    }
    catch (erro) {
        console.error(erro);
    }
}

export async function buscarCliente(id) {
    try {
        const resposta = await fetch(`${API_URL}/clientes/${id}`);

        if (!resposta.ok) {
            throw new Error("Cliente não encontrado.");
        }

        const cliente = await resposta.json();

        console.log(cliente);

        return cliente;
    }
    catch (erro) {
        console.error(erro);
    }
}

export async function cadastrarCliente(cliente) {
    try {
        const resposta = await fetch(`${API_URL}/clientes`, {
            method: "POST",

            headers: {
                "Content-Type": "application/json"
            },

            body: JSON.stringify(cliente)
        });

        if (!resposta.ok) {
            throw new Error("Erro ao cadastrar cliente.");
        }

        const clienteCriado = await resposta.json();

        console.log("Cliente criado:", clienteCriado);

        return clienteCriado;
    }
    catch (erro) {
        console.error(erro);
    }
}

