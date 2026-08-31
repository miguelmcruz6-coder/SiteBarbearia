import { requisicao } from "./api.js";
import {
  cabecalhoAutenticado,
  encerrarSessao,
  exigirLogin
} from "./sessao.js";

const sessao = exigirLogin();
const lista = document.getElementById("lista-agendamentos");
const mensagem = document.getElementById("mensagem-agendamentos");
let agendamentos = [];

function dataDoAgendamento(item) {
  const data = item.data.split("T")[0];
  return new Date(`${data}T${item.horario}`);
}

function formatarData(item) {
  return dataDoAgendamento(item).toLocaleString("pt-BR", {
    dateStyle: "short",
    timeStyle: "short"
  });
}

function criarCard(item) {
  const card = document.createElement("article");
  card.className = "card-agendamento";

  const dados = document.createElement("div");
  dados.className = "dados-agendamento";

  const servico = document.createElement("p");
  servico.className = "nome-servico";
  servico.textContent = item.servico.nome;

  const data = document.createElement("p");
  data.className = "detalhe-agendamento";
  data.textContent = formatarData(item);

  const profissional = document.createElement("p");
  profissional.className = "detalhe-agendamento";
  profissional.textContent = `Profissional: ${item.barbeiro.nome}`;

  const status = document.createElement("span");
  status.className = "status-agendamento";
  status.textContent = item.status;

  dados.append(servico, data, profissional);
  card.append(dados, status);

  const podeCancelar =
    item.status !== "Cancelado" && dataDoAgendamento(item) >= new Date();

  if (podeCancelar) {
    const botao = document.createElement("button");
    botao.type = "button";
    botao.className = "botao-cancelar";
    botao.textContent = "Cancelar";
    botao.addEventListener("click", () => cancelar(item.id));
    card.appendChild(botao);
  }

  return card;
}

function renderizar() {
  lista.replaceChildren();
  if (!agendamentos.length) {
    mensagem.textContent = "Nenhum agendamento encontrado.";
    return;
  }

  mensagem.textContent = "";
  agendamentos.forEach((item) => lista.appendChild(criarCard(item)));
}

async function carregar() {
  try {
    agendamentos = await requisicao("/agendamentos/meus", {
      headers: cabecalhoAutenticado()
    });
    renderizar();
  } catch (erro) {
    if (erro.status === 401) {
      encerrarSessao();
      location.href = "login.html?retorno=meus-agendamentos.html";
      return;
    }
    mensagem.textContent = erro.message;
  }
}

async function cancelar(id) {
  if (!confirm("Deseja cancelar este agendamento?")) return;

  try {
    await requisicao(`/agendamentos/${id}/cancelar`, {
      method: "PUT",
      headers: cabecalhoAutenticado()
    });
    await carregar();
  } catch (erro) {
    mensagem.textContent = erro.message;
  }
}

if (sessao) {
  document.getElementById("saudacao-cliente").textContent =
    `Olá, ${sessao.cliente.nome}`;

  document.getElementById("botao-logout").addEventListener("click", () => {
    encerrarSessao();
    location.href = "login.html";
  });

  carregar();
}
