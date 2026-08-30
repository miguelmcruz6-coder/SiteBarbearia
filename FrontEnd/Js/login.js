import { requisicao } from "./api.js";
import { obterSessao, salvarSessao } from "./sessao.js";

const formulario = document.getElementById("formLogin");
const mensagem = document.getElementById("mensagemLogin");
const botao = document.getElementById("botao-login");

if (obterSessao()) {
  location.href = "index.html";
}

const aviso = new URLSearchParams(location.search).get("cadastro");
if (aviso === "sucesso") {
  mensagem.textContent = "Conta criada. Entre com seu e-mail e senha.";
}

formulario.addEventListener("submit", async (evento) => {
  evento.preventDefault();
  mensagem.textContent = "";
  botao.disabled = true;

  try {
    const resultado = await requisicao("/autenticacao/login", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        email: document.getElementById("email").value.trim(),
        senha: document.getElementById("senha").value
      })
    });

    salvarSessao(resultado);

    const retorno = new URLSearchParams(location.search).get("retorno");
    const paginasPermitidas = ["agendar.html", "meus-agendamentos.html"];
    location.href = paginasPermitidas.includes(retorno)
      ? retorno
      : "index.html";
  } catch (erro) {
    mensagem.textContent = erro.message;
  } finally {
    botao.disabled = false;
  }
});
