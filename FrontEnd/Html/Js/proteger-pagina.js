import { exigirLogin } from "./sessao.js";

const sessao = exigirLogin();

if (sessao) {
  document.documentElement.dataset.autenticado = "true";
}
