# 🛒 Store System (ADO.NET Study)

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)

Este projeto é um sistema de gerenciamento de loja desenvolvido para consolidar conhecimentos em **C#** e persistência de dados utilizando **ADO.NET**. Ao contrário de usar frameworks automáticos, este sistema foca na manipulação direta de comandos SQL via código.

## 🚀 O que este projeto explora?

O foco principal aqui foi aprender como o C# se comunica com bancos de dados relacionais de forma bruta e performática, utilizando a biblioteca `Microsoft.Data.SqlClient`.

### 🛠 Tecnologias e Bibliotecas
- **Linguagem:** C#
- **Driver de Dados:** `Microsoft.Data.SqlClient`
- **Banco de Dados:** SQL Server
- **Arquitetura:** Aplicação de Console

---

## 📋 Funcionalidades Implementadas

O sistema realiza operações de **CRUD** (Create, Read, Update, Delete) diretamente no banco de dados:
- [x] **Cadastro:** Inserção de produtos usando `SqlCommand`.
- [x] **Consulta:** Leitura de dados com `SqlDataReader`.
- [x] **Atualização:** Edição de registros existentes.
- [x] **Exclusão:** Remoção física de itens do banco.

---

## 🧠 Conceitos de Banco de Dados Aplicados

Durante o desenvolvimento, pratiquei conceitos fundamentais de conectividade:
1. **Connection Strings:** Configuração do caminho e credenciais do banco.
2. **SqlConnection:** Gerenciamento do ciclo de vida da conexão (Open/Close).
3. **Comandos SQL:** Escrita de `INSERT`, `SELECT`, `UPDATE` e `DELETE` dentro do código C#.
4. **Data Readers:** Iteração sobre resultados de consultas para transformar registros em objetos C#.

---

## ⚙️ Como Rodar o Projeto

1. **Pré-requisitos:**
   - Ter o .NET SDK instalado.
   - Ter um banco de dados SQL Server ativo.

2. **Configuração:**
   - Ajuste a **Connection String** no código com as credenciais do seu banco local.

3. **Execução:**
   ```bash
   dotnet restore
   dotnet run
