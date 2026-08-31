var botaoAgendamento = document.getElementById("irParaAgendamento");
var mensagemSobre = document.getElementById("mensagemSobre");

botaoAgendamento.addEventListener("click", function () {
  mensagemSobre.textContent = "Abrindo a página de agendamento...";

  setTimeout(function () {
    window.location.href = "agendar.html";
  }, 400);
});
