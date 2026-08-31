# 💈 Site Barbearia

Sistema web para gerenciamento de uma barbearia, desenvolvido com **ASP.NET Core Web API**, **Entity Framework Core**, **SQL Server**, **JWT** e um frontend desenvolvido com **HTML, CSS e JavaScript**.

O projeto permite realizar o gerenciamento de clientes, barbeiros, serviços e agendamentos, além de possuir sistema de autenticação e controle de acesso.

## 📋 Sobre o Projeto

O **Site Barbearia** foi desenvolvido com o objetivo de criar uma aplicação completa para gerenciamento de uma barbearia.

A aplicação é dividida em duas partes principais:

* **Backend:** API REST desenvolvida em C# com ASP.NET Core.
* **Frontend:** Interface web desenvolvida com HTML, CSS e JavaScript.

A API é responsável pelo processamento das informações, autenticação dos usuários, comunicação com o banco de dados e disponibilização dos endpoints utilizados pelo frontend.

O frontend realiza as requisições HTTP para a API e apresenta as informações ao usuário.

## 🚀 Tecnologias Utilizadas

### Backend

* **C#**
* **.NET 8**
* **ASP.NET Core Web API**
* **Entity Framework Core**
* **SQL Server**
* **JWT (JSON Web Token)**
* **Swagger / OpenAPI**
* **REST API**
* **DTOs**

O projeto utiliza .NET 8 e possui referências para autenticação JWT, Entity Framework Core para SQL Server e Swagger.

### Frontend

* **HTML5**
* **CSS3**
* **JavaScript**
* **Fetch API**
* **JSON**

O frontend está organizado nas pastas `Html`, `Css`, `Js` e `Image`.

## 🏗️ Estrutura do Projeto

```text
SiteBarbearia/
│
├── BarbeariaAPI/
│   ├── Controllers/
│   │   ├── AgendamentosController.cs
│   │   ├── AutenticacaoController.cs
│   │   ├── BarbeirosController.cs
│   │   ├── ClientesController.cs
│   │   └── ServicosController.cs
│   │
│   ├── DTOs/
│   │   ├── AgendamentoDTO.cs
│   │   ├── BarbeiroDTO.cs
│   │   ├── CadastroDTO.cs
│   │   ├── ClienteDTO.cs
│   │   ├── LoginDTO.cs
│   │   └── ServicoDTO.cs
│   │
│   ├── Data/
│   │
│   ├── Migrations/
│   │
│   ├── Models/
│   │
│   ├── Properties/
│   │
│   ├── wwwroot/
│   │
│   ├── BarbeariaAPI.csproj
│   ├── Program.cs
│   ├── appsettings.json
│   └── appsettings.Development.json
│
├── FrontEnd/
│   ├── Css/
│   ├── Html/
│   ├── Image/
│   └── Js/
│
├── Deploy/
│   ├── Publish/
│   ├── PublicacaoRunAsp-20260831/
│   └── BarbeariaDB.sql
│
├── SiteBarbearia.sln
└── .gitignore
```

A estrutura acima corresponde à organização atualmente presente no repositório.

## ⚙️ Funcionalidades

### 👤 Clientes

O sistema possui funcionalidades relacionadas ao gerenciamento de clientes, incluindo operações através da API.

Entre as operações disponíveis estão:

* Cadastro de clientes
* Consulta de clientes
* Atualização de informações
* Exclusão de clientes
* Gerenciamento de permissões administrativas

### 💈 Barbeiros

Permite realizar o gerenciamento dos profissionais da barbearia.

Funcionalidades:

* Cadastro de barbeiros
* Consulta de barbeiros
* Atualização de informações
* Exclusão de barbeiros
* Associação com agendamentos

### ✂️ Serviços

Permite cadastrar e gerenciar os serviços oferecidos pela barbearia.

Exemplos:

* Corte de cabelo
* Barba
* Sobrancelha
* Combos de serviços
* Outros serviços oferecidos pela barbearia

### 📅 Agendamentos

O sistema permite realizar o gerenciamento dos horários agendados.

Os agendamentos possuem relacionamento com informações como:

* Cliente
* Barbeiro
* Serviço
* Data
* Horário

A aplicação possui um `AgendamentosController` responsável pelos endpoints relacionados aos agendamentos.

### 🔐 Autenticação

A API utiliza **JWT (JSON Web Token)** para autenticação.

O projeto possui um controller específico para autenticação:

```text
AutenticacaoController.cs
```

O JWT é configurado no `Program.cs`, incluindo validação de:

* Issuer
* Audience
* Lifetime
* Signing Key

Também é utilizado `PasswordHasher` para o tratamento das senhas dos clientes.

### 👑 Controle de acesso

A API utiliza autorização baseada em autenticação para proteger determinadas operações.

Isso permite diferenciar usuários comuns de usuários com permissões administrativas.

## 🗄️ Banco de Dados

O projeto utiliza **Microsoft SQL Server** como banco de dados.

A comunicação entre a API e o banco é realizada através do:

```text
Entity Framework Core
```

O `BarbeariaContext` é registrado no `Program.cs` utilizando o provedor SQL Server.

O projeto também possui uma pasta de migrations:

```text
BarbeariaAPI/
└── Migrations/
```

Além disso, existe um script SQL para criação/configuração do banco dentro da pasta `Deploy`:

```text
Deploy/
└── BarbeariaDB.sql
```

## 📦 Dependências

As principais dependências utilizadas pelo backend são:

```text
Microsoft.AspNetCore.Authentication.JwtBearer
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.SqlServer
Swashbuckle.AspNetCore
```

O projeto está configurado para utilizar o **.NET 8.0**.

## 🔧 Pré-requisitos

Antes de executar o projeto, é necessário possuir instalado:

* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* Microsoft SQL Server
* SQL Server Management Studio (opcional)
* Git
* Navegador web
* Editor de código, como Visual Studio ou VS Code

## 📥 Instalação

### 1. Clonar o repositório

```bash
git clone https://github.com/miguelmcruz6-coder/SiteBarbearia.git
```

Entre na pasta:

```bash
cd SiteBarbearia
```

### 2. Restaurar as dependências

Entre na pasta da API:

```bash
cd BarbeariaAPI
```

Execute:

```bash
dotnet restore
```

### 3. Configurar o banco de dados

Configure a connection string do SQL Server no arquivo:

```text
BarbeariaAPI/appsettings.json
```

Exemplo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=BarbeariaDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

> ⚠️ Não utilize credenciais reais diretamente no repositório. Para ambientes de produção, prefira variáveis de ambiente ou mecanismos seguros de configuração.

### 4. Configurar o JWT

A aplicação espera configurações para:

```text
Jwt:Key
Jwt:Issuer
Jwt:Audience
```

Exemplo:

```json
{
  "Jwt": {
    "Key": "SUA_CHAVE_BASE64",
    "Issuer": "BarbeariaAPI",
    "Audience": "BarbeariaFrontend"
  }
}
```

A API valida essas informações durante a autenticação JWT.

### 5. Executar as migrations

Caso o banco ainda não esteja criado, execute:

```bash
dotnet ef database update
```

Caso o comando `dotnet ef` não esteja instalado:

```bash
dotnet tool install --global dotnet-ef
```

Depois:

```bash
dotnet ef database update
```

Também é possível utilizar o script disponível em:

```text
Deploy/BarbeariaDB.sql
```

## ▶️ Executando o projeto

Na pasta:

```text
BarbeariaAPI/
```

execute:

```bash
dotnet run
```

A API será iniciada e ficará disponível na URL indicada pelo terminal.

Durante o desenvolvimento, o projeto também disponibiliza o **Swagger**, permitindo testar os endpoints da API.

## 📖 Swagger

Com a API executando em modo de desenvolvimento, acesse:

```text
/swagger
```

O Swagger permite visualizar e testar os endpoints disponibilizados pela API.

Como o sistema utiliza JWT, endpoints protegidos podem ser testados informando o token através do botão **Authorize**.

Formato:

```text
Bearer SEU_TOKEN
```

## ❤️ Health Check

A API possui um endpoint para verificar se o servidor está funcionando:

```http
GET /health
```

Resposta esperada:

```json
{
  "status": "ok"
}
```

Esse endpoint é útil para verificar rapidamente o estado da aplicação.

## 🔗 Principais Endpoints

A API utiliza o padrão:

```text
/api/[controller]
```

Entre os principais controllers estão:

| Controller               | Responsabilidade              |
| ------------------------ | ----------------------------- |
| `AutenticacaoController` | Login e autenticação          |
| `ClientesController`     | Gerenciamento de clientes     |
| `BarbeirosController`    | Gerenciamento de barbeiros    |
| `ServicosController`     | Gerenciamento de serviços     |
| `AgendamentosController` | Gerenciamento de agendamentos |

Esses controllers estão presentes atualmente no projeto.

### Exemplo

Criar um agendamento:

```http
POST /api/Agendamentos
```

Exemplo de JSON:

```json
{
  "clienteId": 1,
  "barbeiroId": 1,
  "servicoId": 1,
  "data": "2026-08-31",
  "horario": "10:00"
}
```

> Os campos exatos devem seguir o DTO utilizado pela versão atual da API.

## 📦 DTOs

O projeto utiliza **Data Transfer Objects (DTOs)** para transportar informações entre o frontend e a API.

Atualmente existem DTOs para:

```text
AgendamentoDTO
BarbeiroDTO
CadastroDTO
ClienteDTO
LoginDTO
ServicoDTO
```

Eles estão localizados em:

```text
BarbeariaAPI/DTOs/
```

O uso de DTOs ajuda a evitar que as entidades do banco sejam diretamente expostas pela API e permite definir exatamente quais informações podem ser recebidas ou enviadas.

## 🌐 Frontend

O frontend está localizado em:

```text
FrontEnd/
```

Sua estrutura é dividida em:

```text
FrontEnd/
├── Css/
├── Html/
├── Image/
└── Js/
```

A API também está configurada para incluir os arquivos do `FrontEnd` dentro do `wwwroot` durante a execução/publicação do projeto.

## 🔄 Comunicação Frontend → API

O frontend utiliza JavaScript para realizar requisições HTTP para a API.

Exemplo:

```javascript
fetch(`${API_URL}/api/Agendamentos`, {
    method: "POST",
    headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${token}`
    },
    body: JSON.stringify(agendamento)
});
```

As respostas da API são recebidas em formato JSON e utilizadas para atualizar a interface.

## 🔒 CORS

A API possui uma política de CORS chamada:

```text
Frontend
```

Ela permite definir quais origens podem realizar requisições para a API.

As origens de produção são configuradas através de:

```json
{
  "Cors": {
    "AllowedOrigins": []
  }
}
```

Em ambiente de desenvolvimento, `localhost` e `127.0.0.1` são tratados de maneira específica pela configuração atual.

## 📁 Deploy

O projeto possui uma pasta específica para arquivos relacionados à publicação:

```text
Deploy/
```

Ela contém arquivos como:

```text
BarbeariaDB.sql
Publish/
PublicacaoRunAsp-20260831/
```

Também existem arquivos compactados relacionados à publicação da aplicação.

## 🛡️ Segurança

O projeto utiliza algumas medidas de segurança, como:

* Autenticação JWT
* Validação de Issuer
* Validação de Audience
* Validação de validade do token
* Validação da chave de assinatura
* Hash de senhas
* CORS
* HTTPS
* HSTS em produção

A configuração de autenticação e autorização está centralizada no `Program.cs`.

## 🧪 Testes da API

Para testar a API, recomenda-se utilizar:

* Swagger
* Postman
* Insomnia
* Arquivo `.http` incluído no projeto

O projeto possui:

```text
BarbeariaAPI/BarbeariaAPI.http
```

que pode ser utilizado para realizar requisições HTTP durante o desenvolvimento.

## 🛠️ Desenvolvimento

Para iniciar o desenvolvimento:

```bash
git clone https://github.com/miguelmcruz6-coder/SiteBarbearia.git

cd SiteBarbearia/BarbeariaAPI

dotnet restore

dotnet run
```

Depois, utilize o frontend ou o Swagger para interagir com a API.

## 📌 Melhorias Futuras

Algumas funcionalidades que podem ser adicionadas futuramente:

* [ ] Recuperação de senha
* [ ] Confirmação de agendamento
* [ ] Cancelamento de agendamento pelo cliente
* [ ] Notificações de agendamento
* [ ] Dashboard administrativo
* [ ] Relatórios de atendimentos
* [ ] Controle de horários disponíveis
* [ ] Bloqueio de horários
* [ ] Histórico de agendamentos
* [ ] Sistema de avaliação dos serviços
* [ ] Upload de imagens dos barbeiros
* [ ] Melhorias na responsividade do frontend
* [ ] Testes automatizados
* [ ] Dockerização da aplicação
* [ ] Deploy automatizado

## 👨‍💻 Autor

**Miguel Miyaki da Cruz**

GitHub:

https://github.com/miguelmcruz6-coder

## 📄 Licença

Este projeto foi desenvolvido para fins de estudo e desenvolvimento de uma aplicação web completa utilizando tecnologias de frontend e backend.

---

⭐ Se este projeto foi útil para você, considere deixar uma estrela no repositório!
