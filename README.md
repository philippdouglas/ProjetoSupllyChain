# Projeto SupplyChain

[![C#](https://img.shields.io/badge/-C%23-blue)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![.NET](https://img.shields.io/badge/-.NET-blue)](https://dotnet.microsoft.com/)
[![Entity Framework](https://img.shields.io/badge/-Entity%20Framework-lightgrey)](https://docs.microsoft.com/en-us/ef/)
[![JQuery](https://img.shields.io/badge/-JQuery-blue)](https://jquery.com/)
[![Bootstrap](https://img.shields.io/badge/-Bootstrap-blueviolet)](https://getbootstrap.com/)
[![SQLite](https://img.shields.io/badge/-SQLite-blue)](https://www.sqlite.org/)

## Descrição
O Projeto SupplyChain é um sistema de gerenciamento de mercadorias desenvolvido em C#, .NET, Entity Framework, JQuery, Bootstrap e SQLite. O objetivo deste sistema é permitir o controle de estoque e movimentação de mercadorias, oferecendo funcionalidades para gerenciamento das mercadorias, registro de entradas e saídas, visualização de entradas e saídas por mês em formato de gráfico, e exportação de relatórios mensais contendo todas as movimentações.

## Funcionalidades


<h4 align="center"> 
    :construction:  - Cadastro e autenticação de usuários funcionalidade em construção  :construction:
</h4>

![Autenticação](https://github.com/philippdouglas/ProjetoSupllyChain/assets/78768376/3452c195-403c-4ec1-bd28-7ea8ab1293b4)

- Gerencia as mercadorias;

![5](https://github.com/philippdouglas/ProjetoSupllyChain/assets/78768376/c47fb96c-e2b8-47ee-8cdb-b59f583231b7)  
![excluir](https://github.com/philippdouglas/ProjetoSupllyChain/assets/78768376/431c830d-b307-403d-aa9c-f6c6bb65e2df)

- Registra entradas e saídas de mercadorias;

![2](https://github.com/philippdouglas/ProjetoSupllyChain/assets/78768376/7da2d090-7ef0-4f5e-a6a4-16f3d8fc5ecc)

- Quantidade dos Produtos em estoque;

![Mercadoria](https://github.com/philippdouglas/ProjetoSupllyChain/assets/78768376/72a3bc3d-f20f-44c0-aacf-56e637f1ff1c)

- Exporta relatórios mensais com todas as entradas e saídas de todas as mercadorias.

![relatorio](https://github.com/philippdouglas/ProjetoSupllyChain/assets/78768376/5115ee98-b8a9-40ff-ae73-5c66831dd198)

- Gráfico das entradas e saídas por mês de cada mercadoria;
- Valida os dados no cliente antes de enviá-los ao servidor, garantindo que campos em branco e caracteres inválidos não sejam submetidos;
- Desenvolvido no sistema .NET MVC, utilizando a linguagem C#;
- Utiliza o Entity Framework para acesso ao banco de dados;
- Utiliza o framework front-end Bootstrap na camada Web;
- Realizar o cadastro de novas mercadorias, salvando nome, número de registro, fabricante, tipo e descrição no banco de dados;
- Cria cadastro de entradas contendo quantidade, data e hora, local e lista (select) de todas as mercadorias já cadastradas, salvando a mercadoria selecionada na tabela de Entrada do banco de dados;
- Registrar saídas contendo quantidade, data e hora, local e uma lista com todas as mercadorias já cadastradas, salvando a mercadoria selecionada na tabela de Saída do banco de dados;
- Banco de dados utilizando SQLite.

![db](https://github.com/philippdouglas/ProjetoSupllyChain/assets/78768376/d39e2460-f854-4ad2-b527-cd384db0c065)
  
## Rodar o Projeto (Vs Code)
1. Abra o terminal no Visual Studio Code.

2. Execute os seguintes comandos no terminal:

```bash
dotnet build
```

```bash
dotnet run
```

3. Abra o seu navegador e acesse a URL [https://localhost:5001/Home](https://localhost:5001/Home).

## Licença

Este projeto está sob a "**Licença MIT**"


