import { criarAgendamento, buscarAgendamentos, cancelarAgendamento, excluirAgendamento, agendaLivre} from './agendar.js'
import { buscarBarbeiros } from './barbeiros.js'
import { buscarClientes, buscarCliente, cadastrarCliente } from './clientes.js'
import { buscarServicos } from './servicos.js'

let servico = document.getElementById("servico");
let barbeiro = document.getElementById("barbeiro");
let data = document.getElementById("data");
let horario = document.getElementById("horario");

let popup = document.getElementById("mensagem")

const agendamentoBtn = document.getElementById("confirmarAgendamento");

agendamentoBtn.addEventListener('click', () => {
    popup.innerHTML = ""
    if(!servico || !barbeiro || !data || !horario){
        let texto = "Confirme "

        if(!servico){
            texto += "seu serviço, "
        }
        if(!barbeiro){
            texto += "seu barbeiro, "
        }
        if(!data){
            texto += "sua data, "
        }
        if(!horario){
            texto += "seu horario, "
        }

        texto.slice(0, -2)
        popup.innerHTML = texto + "."
        return;
    }
    if(agendaLivre(data, horario)){
        criarAgendamento();
    }
})





