﻿## API EnqueteOnline

### Features

- [x] Documentação da API com Scalar
- [x] Autenticação e Autorização com JWT com HTTP-only cookies
- [x] Refresh tokens com 1 semana de expiração e job com hangfire para limpar tokens revogados/expirados 
- [x] Login Google ou Facebook
- [x] Cadastro de Enquetes
- [x] Votar nas enquetes
- [x] Acompanhar votação da enquete em tempo real(Websocket com SignalR)
- [x] Editar/Excluir enquete apenas se for o criador da enquete 


### 🛠 Tecnologias

As seguintes ferramentas foram usadas na construção do projeto:
- [.NET](https://dotnet.microsoft.com/en-us/)
- [Postgres](https://www.postgresql.org/)
- Carter
- Hangfire
- Refit

### 🛠 Padrões Utilizados

As seguintes padrões foram usados na construção do projeto:
- DDD (Domain-Driven Design)
- CQRS (Command Query Responsibility Segregation)
- SOLID
- UnitOfWork
- Repository

### Pré-requisitos

Antes de começar, você vai precisar ter instalado em sua máquina as seguintes ferramentas:
[Git](https://git-scm.com), [.NET](https://dotnet.microsoft.com/en-us/).
[Postgres](https://www.postgresql.org/) ou subir container utilizando o [Docker](https://www.docker.com/).
Também é preciso configurar connectionString, apiKey riot, informações oauth do Discord, secret JTW no arquivo `enquete-online-app/src/EnqueteOnline.API/appsettings.Development.json`.
Além disto é bom ter um editor para trabalhar com o código como [Visual Studio](https://visualstudio.microsoft.com/pt-br/downloads/).


### 🎲 Rodando o Back End (servidor)

#### Rodando EnqueteOnline.API

```bash
# Clone este repositório
$ git clone <https://github.com/henriquesan14/enquete-online-app.git>

# Acesse a pasta do projeto no terminal/cmd
$ cd enquete-online-app

# Vá para a pasta da EnqueteOnline.API
$ cd src/EnqueteOnline.API

# Execute a aplicação com o comando do dotnet
$ dotnet run

# A API iniciará na porta:5258 com HTTP
```

### Autor
---

<a href="https://www.linkedin.com/in/henrique-san/">
 <img style="border-radius: 50%;" src="https://avatars.githubusercontent.com/u/33522361?v=4" width="100px;" alt=""/>
 <br />
 <sub><b>Henrique Santos</b></sub></a> <a href="https://www.linkedin.com/in/henrique-san/">🚀</a>


Feito com ❤️ por Henrique Santos 👋🏽 Entre em contato!

[![Linkedin Badge](https://img.shields.io/badge/-Henrique-blue?style=flat-square&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/henrique-san/)](https://www.linkedin.com/in/henrique-san/) 
