import { requisicao } from "./api.js";
import { obterSessao } from "./sessao.js";

if (obterSessao()) {
  location.href = "index.html";
}

const formulario = document.getElementById("formCadastro");
const mensagem = document.getElementById("mensagemCadastro");
const botao = document.getElementById("botao-cadastro");

formulario.addEventListener("submit", async (evento) => {
  evento.preventDefault();
  mensagem.textContent = "";
  botao.disabled = true;

  try {
    await requisicao("/autenticacao/cadastro", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        nome: document.getElementById("nome").value.trim(),
        cpf: document.getElementById("cpf").value.trim(),
        telefone: document.getElementById("telefone").value.trim(),
        email: document.getElementById("email").value.trim(),
        senha: document.getElementById("senha").value
      })
    });

    location.href = "login.html?cadastro=sucesso";
  } catch (erro) {
    mensagem.textContent = erro.message;
  } finally {
    botao.disabled = false;
  }
});
