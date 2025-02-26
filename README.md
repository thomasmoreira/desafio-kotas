# Pokemon API

A **Pokemon API** é uma aplicação desenvolvida em ASP.NET Core, utilizando como base a API https://pokeapi.co/,  para um desafio by [Coodesh](https://coodesh.com/).
Com uma arquitetura modular, o projeto adota as melhores práticas de desenvolvimento. utilizando o FluentValidation para garantir validações robustas – inclusive com regras assíncronas – e filtros personalizados para padronizar o retorno de erros.

---

## Funcionalidades

- **Consulta de Pokémons**, **Cadastro e Consulta de Chefes Pokémons**, **Registro e Consulta de Captura de Pokémons**
- **Validação Personalizada:** Uso do FluentValidation para validar as requisições, com suporte para regras assíncronas.
- **Retorno Padronizado de Erros:** Filtros customizados que formatam as mensagens de erro de forma consistente.
- **MediatR:** Implementação do MediatR para desacoplar a lógica de negócio dos controllers, organizando comandos e queries de forma limpa e escalável.
- **Injeção de Dependências:** Estrutura desacoplada para facilitar testes e manutenção.
- **Extensibilidade:** Arquitetura modular que permite a adição de novas funcionalidades e endpoints facilmente.

---

## Tecnologia

- [.NET 9 ](https://dotnet.microsoft.com/download)
- [SQLite]
- [Visual Studio / VS Code](https://visualstudio.microsoft.com/) ou outra IDE de sua preferência

---

## Como Executar

### 1. Clonando o Repositório

```bash
git clone https://github.com/thomasmoreira/desafio-kotas.git
cd desafio-kotas/Pokemon.Api
dotnet run
