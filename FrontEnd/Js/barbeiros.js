const API_URL = "https://localhost:5104/api";

export async function buscarBarbeiros() {
    try {
        const resposta = await fetch(`${API_URL}/barbeiros`);

        if (!resposta.ok) {
            throw new Error("Erro ao buscar barbeiros.");
        }

        const barbeiros = await resposta.json();

        console.log(barbeiros);

        return barbeiros;
    }
    catch (erro) {
        console.error(erro);
    }
}