# Clube de Leitura - Sistema de Gerenciamento

## Descrição do Projeto

O Clube de Leitura é um sistema de gerenciamento que permite controlar empréstimos de revistas. A aplicação possibilita o cadastro de amigos, caixas para armazenamento, revistas e o controle de empréstimos, facilitando a gestão do clube.

## Funcionalidades

- **Módulo de Amigos**:

  - Cadastro de amigos com nome, responsável e telefone
  - Edição e exclusão de amigos
  - Verificação de dados duplicados
  - Validação de campos obrigatórios

- **Módulo de Caixas**:

  - Cadastro de caixas com etiqueta, cor e dias de empréstimo
  - Edição e exclusão de caixas
  - Verificação de etiquetas duplicadas
  - Verificação de caixas com revistas vinculadas ao excluir

- **Módulo de Revistas**:

  - Cadastro de revistas com título, edição, ano e caixa de armazenamento
  - Edição e exclusão de revistas
  - Controle de status (disponível, emprestada, reservada)
  - Visualização de revistas por caixa
  - Verificação de títulos e edições duplicados

- **Módulo de Empréstimos**:
  - Registro de empréstimos e devoluções
  - Cálculo automático de datas de devolução baseado na configuração da caixa
  - Controle de atrasos na devolução
  - Visualização de empréstimos por estado (todos, abertos, atrasados)
  - Visualização de empréstimos por amigo

## Como Utilizar

Ao iniciar o programa, você terá acesso a um menu principal com as seguintes opções:

1. **Amigos**:

   - Inserir novo amigo
   - Editar amigo
   - Excluir amigo
   - Visualizar todos os amigos

2. **Caixas**:

   - Inserir nova caixa
   - Editar caixa
   - Excluir caixa
   - Visualizar todas as caixas

3. **Revistas**:

   - Inserir nova revista
   - Editar revista
   - Excluir revista
   - Visualizar todas as revistas
   - Visualizar revistas por caixa

4. **Empréstimos**:

   - Registrar empréstimo
   - Registrar devolução
   - Visualizar todos os empréstimos
   - Visualizar empréstimos em aberto
   - Visualizar empréstimos em atraso
   - Visualizar empréstimos por amigo

5. **Sair** da aplicação

## Regras de Negócio

- Um amigo só pode ter um empréstimo ativo por vez
- Uma revista só pode ser emprestada se estiver disponível
- O prazo de empréstimo é determinado pela configuração da caixa onde a revista está armazenada
- Não é possível excluir amigos com empréstimos vinculados
- Não é possível excluir caixas com revistas vinculadas
- Não é possível excluir revistas com empréstimos ativos
- Não é possível editar revistas que estão emprestadas

## Estrutura do Projeto

O projeto está organizado em quatro módulos principais:

- **ModuloAmigo**: Classes relacionadas à gestão de amigos

  - Amigo.cs: Entidade que representa um amigo
  - RepositorioAmigo.cs: Gerencia operações de CRUD para amigos
  - TelaAmigo.cs: Interface de usuário para o módulo de amigos

- **ModuloCaixa**: Classes relacionadas à gestão de caixas

  - Caixa.cs: Entidade que representa uma caixa
  - RepositorioCaixa.cs: Gerencia operações de CRUD para caixas
  - TelaCaixa.cs: Interface de usuário para o módulo de caixas

- **ModuloRevista**: Classes relacionadas à gestão de revistas

  - Revista.cs: Entidade que representa uma revista
  - RepositorioRevista.cs: Gerencia operações de CRUD para revistas
  - TelaRevista.cs: Interface de usuário para o módulo de revistas

- **ModuloEmprestimo**: Classes relacionadas à gestão de empréstimos

  - Emprestimo.cs: Entidade que representa um empréstimo
  - RepositorioEmprestimo.cs: Gerencia operações de CRUD para empréstimos
  - TelaEmprestimo.cs: Interface de usuário para o módulo de empréstimos

- **Compartilhado**: Classes compartilhadas entre os módulos
  - MenuPrincipal.cs: Gerencia o menu principal e orquestra os módulos

## Requisitos

- .NET 8.0 ou superior
- Sistema operacional compatível com .NET (Windows, macOS, Linux)

## Como utilizar

1. Clone o repositório ou baixe o código fonte.
2. Abra o terminal ou o prompt de comando e navegue até a pasta raiz
3. Utilize o comando abaixo para restaurar as dependências do projeto.

```
dotnet restore
```

4. Em seguida, compile a solução utilizando o comando:

```
dotnet build --configuration Release
```

5. Para executar o projeto compilando em tempo real

```
dotnet run --project ClubeLeitura.ConsoleApp
```

6. Para executar o arquivo compilado, navegue até a pasta `./ClubeLeitura.ConsoleApp/bin/Release/net8.0/` e execute o arquivo:

```
ClubeLeitura.ConsoleApp.exe
```
