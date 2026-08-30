const API_URL = "https://localhost:5104/api";

export async function buscarServicos() {
    try {
        const resposta = await fetch(`${API_URL}/servicos`);

        if (!resposta.ok) {
            throw new Error("Erro ao buscar serviços.");
        }

        const servicos = await resposta.json();

        console.log(servicos);

        return servicos;
    }
    catch (erro) {
        console.error(erro);
    }
}