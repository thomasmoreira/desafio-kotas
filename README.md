# Pokemon API

A **Pokemon API** é uma aplicação desenvolvida em ASP.NET Core que fornece endpoints para gerenciamento e consulta de dados de Pokémon. 
Com uma arquitetura modular, o projeto adota as melhores práticas de desenvolvimento, utilizando o FluentValidation para garantir validações robustas – inclusive com regras assíncronas – e filtros personalizados para padronizar o retorno de erros.

---

## Funcionalidades

- **CRUD de Pokémons:** Criação, leitura, atualização e exclusão de registros.
- **Validação Personalizada:** Uso do FluentValidation para validar as requisições, com suporte para regras assíncronas.
- **Retorno Padronizado de Erros:** Filtros customizados que formatam as mensagens de erro de forma consistente.
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
git clone https://github.com/seu-usuario/Pokemon.Api.git
cd Pokemon.Api
