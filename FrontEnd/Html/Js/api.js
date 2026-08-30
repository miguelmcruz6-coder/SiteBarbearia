export const API_URL = "http://localhost:5104/api";

export async function requisicao(caminho, opcoes = {}) {
  const resposta = await fetch(`${API_URL}${caminho}`, opcoes);
  const tipo = resposta.headers.get("content-type") || "";
  let conteudo = null;

  if (resposta.status !== 204) {
    conteudo = tipo.includes("application/json")
      ? await resposta.json()
      : await resposta.text();
  }

  if (!resposta.ok) {
    const mensagem =
      typeof conteudo === "string"
        ? conteudo
        : conteudo?.title || "Não foi possível concluir a operação.";

    const erro = new Error(mensagem);
    erro.status = resposta.status;
    throw erro;
  }

  return conteudo;
}
