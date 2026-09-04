## Simulador de matricula de alunos na escola

## Resumo

API Rest desenvolvida com .NET Framework 4.8, ASP.NET Web API, Dapper com banco de dados SQLServer para 
gerenciamento de alunos 

## Requisitos Técnicos

- Versão do .NET Framework 4.8 com ASP.NET Framework (classico)
- Acesso a dados com Dapper e SQL escrito manualmente
- SQL Server (LocalDB)
- Status HTTP corretos tratatos [200 (OK), 201(Created), 400(Bad Request), 404(Not Found), 409 (Conflict)]
- Organização em camadas (Controller, Service, Repository)

## Principais Funcionalidades

### CRUD de Alunos
- Listagem paginada com filtro por nome
- Obter aluno por ID
- Criar aluno
- Atualizar aluno
- Exclusão lógica (campo Ativo)

### Turmas
- Listagem com vagas disponíveis
- Cache em Memória com possibilidade de implemetar com REDIS (tratado via disponibilidade de recurso)

### Matrícula
- Transações SQL, ou efetiva ou não inclui
- Validações:
  - Turma com vaga disponível
  - Aluno ativo
  - Aluno não matriculado duas vezes na mesma turma

### Relatórios
- Alunos por turma com SQL puro (JOIN, GROUP BY)

### Bônus Implementados
- Cache em Memória com disponibilidade de implementar via Redis
- Tela simples HTML
- Testes unitários (Aluno

## Passo a passo da instalação

### 1. Clone o repositório
```bash
git clone https://github.com/anderson-gracher/EscolaWebSite
````
- Utilizar Visual Studio 2022+ para abrir o projeto (Mas acredito que funcione no visual studio 2019 com algumas adaptações)
- Criar a database local (SQLServer) com o nome `TesteEscola` rodar o script de migration caso o BD ainda esteja vazio
- Instalar os pacotes nugget referentes aos componentes não nativos do .Net
  - Dapper  
  - Unity
  - Unity.WebAPI
  - EntityFramework
  - Microsoft.AspNet.Mvc;
  - Microsoft.AspNet.WebApi
  - Microsoft.AspNet.WebApi.Cors
  - Microsoft.AspNet.WebApi.WebHost
  - System.Web.Http
  - Newtonsoft.Json
  - StackExchange.Redis
  - Moq  
  - MSTest.TestFramework

### 