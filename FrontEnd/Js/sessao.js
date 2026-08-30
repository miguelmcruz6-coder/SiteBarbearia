const CHAVE_SESSAO = "barbearia_sessao";

export function salvarSessao(dados) {
  sessionStorage.setItem(CHAVE_SESSAO, JSON.stringify(dados));
}

export function obterSessao() {
  const valor = sessionStorage.getItem(CHAVE_SESSAO);
  if (!valor) return null;

  try {
    const sessao = JSON.parse(valor);
    if (!sessao.token || new Date(sessao.expiracao) <= new Date()) {
      encerrarSessao();
      return null;
    }
    return sessao;
  } catch {
    encerrarSessao();
    return null;
  }
}

export function cabecalhoAutenticado() {
  const sessao = obterSessao();
  return sessao
    ? { Authorization: `Bearer ${sessao.token}` }
    : {};
}

export function exigirLogin() {
  const sessao = obterSessao();
  if (!sessao) {
    const retorno = encodeURIComponent(location.pathname.split("/").pop());
    location.href = `login.html?retorno=${retorno}`;
    return null;
  }
  return sessao;
}

export function encerrarSessao() {
  sessionStorage.removeItem(CHAVE_SESSAO);
}
