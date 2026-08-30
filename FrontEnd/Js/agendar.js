import { requisicao } from "./api.js";
import {
  cabecalhoAutenticado,
  encerrarSessao,
  exigirLogin
} from "./sessao.js";

const sessao = exigirLogin();
const servico = document.getElementById("servico");
const barbeiro = document.getElementById("barbeiro");
const data = document.getElementById("data");
const horario = document.getElementById("horario");
const mensagem = document.getElementById("mensagem");
const botaoConfirmar = document.getElementById("confirmarAgendamento");
const botaoSair = document.getElementById("botao-sair");

function mostrarMensagem(texto, erro = false) {
  mensagem.textContent = texto;
  mensagem.dataset.tipo = erro ? "erro" : "sucesso";
}

function adicionarOpcoes(select, itens) {
  select.length = 1;

  itens.forEach((item) => {
    const opcao = document.createElement("option");
    opcao.value = item.id;
    opcao.textContent = item.nome;
    select.appendChild(opcao);
  });
}

function limparHorarios(texto = "Escolha o serviço, o profissional e a data") {
  horario.length = 1;
  horario.options[0].textContent = texto;
  horario.disabled = true;
}

function tratarErroAutenticacao(erro) {
  if (erro.status !== 401) return false;

  encerrarSessao();
  location.href = "login.html?retorno=agendar.html";
  return true;
}

async function carregarHorarios() {
  mostrarMensagem("");
  limparHorarios("Carregando horários...");

  if (!servico.value || !barbeiro.value || !data.value) {
    limparHorarios();
    return;
  }

  try {
    const parametros = new URLSearchParams({
      barbeiroId: barbeiro.value,
      servicoId: servico.value,
      data: data.value
    });
    const horarios = await requisicao(
      `/agendamentos/horarios-disponiveis?${parametros}`,
      { headers: cabecalhoAutenticado() }
    );

    horario.length = 1;

    if (!horarios.length) {
      limparHorarios("Nenhum horário disponível");
      return;
    }

    horario.options[0].textContent = "Escolha o horário";
    horarios.forEach((valor) => {
      const opcao = document.createElement("option");
      opcao.value = `${valor}:00`;
      opcao.textContent = valor;
      horario.appendChild(opcao);
    });
    horario.disabled = false;
  } catch (erro) {
    if (tratarErroAutenticacao(erro)) return;
    limparHorarios("Não foi possível carregar os horários");
    mostrarMensagem(erro.message, true);
  }
}

async function carregarDados() {
  try {
    const [servicos, barbeiros] = await Promise.all([
      requisicao("/servicos", { headers: cabecalhoAutenticado() }),
      requisicao("/barbeiros", { headers: cabecalhoAutenticado() })
    ]);

    adicionarOpcoes(servico, servicos);
    adicionarOpcoes(barbeiro, barbeiros);

    if (!servicos.length || !barbeiros.length) {
      mostrarMensagem(
        "É necessário cadastrar serviços e profissionais antes de agendar.",
        true
      );
    }
  } catch (erro) {
    if (tratarErroAutenticacao(erro)) return;
    mostrarMensagem(erro.message, true);
  }
}

async function confirmarAgendamento() {
  mostrarMensagem("");

  if (!servico.value || !barbeiro.value || !data.value || !horario.value) {
    mostrarMensagem("Preencha serviço, profissional, data e horário.", true);
    return;
  }

  botaoConfirmar.disabled = true;

  try {
    await requisicao("/agendamentos", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        ...cabecalhoAutenticado()
      },
      body: JSON.stringify({
        clienteId: 0,
        barbeiroId: Number(barbeiro.value),
        servicoId: Number(servico.value),
        data: `${data.value}T00:00:00`,
        horario: horario.value
      })
    });

    mostrarMensagem("Agendamento confirmado com sucesso.");
    servico.value = "";
    barbeiro.value = "";
    data.value = "";
    horario.value = "";
  } catch (erro) {
    if (tratarErroAutenticacao(erro)) return;
    mostrarMensagem(erro.message, true);
  } finally {
    botaoConfirmar.disabled = false;
  }
}

if (sessao) {
  document.getElementById("nome-usuario").textContent = sessao.cliente.nome;
  const hoje = new Date();
  data.min = [
    hoje.getFullYear(),
    String(hoje.getMonth() + 1).padStart(2, "0"),
    String(hoje.getDate()).padStart(2, "0")
  ].join("-");
  botaoConfirmar.addEventListener("click", confirmarAgendamento);
  servico.addEventListener("change", carregarHorarios);
  barbeiro.addEventListener("change", carregarHorarios);
  data.addEventListener("change", carregarHorarios);
  botaoSair.addEventListener("click", () => {
    encerrarSessao();
    location.href = "login.html";
  });
  carregarDados();
}
