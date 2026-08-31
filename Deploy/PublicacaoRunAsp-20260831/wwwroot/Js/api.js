const ambienteLocal = ["localhost", "127.0.0.1"].includes(location.hostname);

export const API_URL =
  globalThis.BARBEARIA_API_URL ??
  (ambienteLocal ? "http://localhost:5104/api" : `${location.origin}/api`);

export async function requisicao(caminho, opcoes = {}) {
  const resposta = await fetch(`${API_URL}${caminho}`, opcoes);
  const tipo = resposta.headers.get("content-type") || "";
  let conteudo = null;

  if (resposta.status !== 204) {
    conteudo = tipo.includes("json")
      ? await resposta.json()
      : await resposta.text();
  }

  if (!resposta.ok) {
    const mensagensDosCampos = {
      CPF: "CPf inválido, informe os 11 números.",
      Telefone: "Telefone inválido, informe 10 ou 11 números.",
    };

    const erros = conteudo?.errors;
    let mensagem = "";

    if (erros) {
      const camposComErro = Object.keys(erros);

      const mensagens = camposComErro.map((campo) => {
        return mensagensDosCampos[campo] || erros[campo][0];
      });

      mensagem = mensagens.join("");
    }

    if (!mensagem) {
      mensagem =
        typeof conteudo === "string"
          ? conteudo
          : conteudo?.title || "Não foi possível concluir a operação.";
    }

    const erro = new Error(mensagem);
    erro.status = resposta.status;
    throw erro;
  }

  return conteudo;
}
