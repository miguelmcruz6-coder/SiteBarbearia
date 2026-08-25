const API_URL = "https://localhost:5104/api";

export async function criarAgendamento(agendamento) {
  try {
    const resposta = await fetch(`${API_URL}/agendamentos`, {
      method: "POST",

      headers: {
        "Content-Type": "application/json",
      },

      body: JSON.stringify(agendamento),
    });

    if (!resposta.ok) {
      const mensagem = await resposta.text();

      throw new Error(mensagem);
    }

    const resultado = await resposta.json();

    console.log("Agendamento criado:", resultado);

    return resultado;
  } catch (erro) {
    console.error("Erro:", erro);
  }
}

export async function buscarAgendamentos() {
  try {
    const resposta = await fetch(`${API_URL}/agendamentos`);

    if (!resposta.ok) {
      throw new Error("Erro ao buscar agendamentos.");
    }

    const agendamentos = await resposta.json();

    console.log(agendamentos);

    return agendamentos;
  } catch (erro) {
    console.error(erro);
  }
}

export async function cancelarAgendamento(id) {
  try {
    const resposta = await fetch(`${API_URL}/agendamentos/${id}/cancelar`, {
      method: "PUT",
    });

    if (!resposta.ok) {
      throw new Error("Erro ao cancelar agendamento.");
    }

    const resultado = await resposta.json();

    console.log(resultado);

    return resultado;
  } catch (erro) {
    console.error(erro);
  }
}

export async function excluirAgendamento(id) {
  try {
    const resposta = await fetch(`${API_URL}/agendamentos/${id}`, {
      method: "DELETE",
    });

    if (!resposta.ok) {
      throw new Error("Erro ao excluir agendamento.");
    }

    console.log("Agendamento excluído.");
  } catch (erro) {
    console.error(erro);
  }
}

//////////////////////////////////////////////////////////////////////////////

export async function agendaLivre(data, horario) {
  let agenda = await buscarAgendamentos();
  const agendamento = agenda.find(
    (n) => n.data === data,
    n.horario === horario
  );
  if (agendamento === null) {
    return true;
  } else {
    return false;
  }
}
