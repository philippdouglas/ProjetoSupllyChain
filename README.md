# Projeto SupplyChain
![1](https://github.com/philippdouglas/ProjetoSupllyChain/assets/78768376/fb92df88-eb23-4cc8-a2ab-f7fb53fb609c)

## Descrição
O Projeto SupplyChain é um sistema de gerenciamento de mercadorias desenvolvido em C#, .NET, Entity Framework, JQuery, Bootstrap e SQLite. O objetivo deste sistema é permitir o controle de estoque e movimentação de mercadorias, oferecendo funcionalidades para gerenciamento das mercadorias, registro de entradas e saídas, visualização de entradas e saídas por mês em formato de gráfico, e exportação de relatórios mensais contendo todas as movimentações.

![2](https://github.com/philippdouglas/ProjetoSupllyChain/assets/78768376/fb06dfce-6b66-478c-adea-c2664b372689)

## Funcionalidades
- Gerencia as mercadorias disponíveis;
- Registra entradas e saídas de mercadorias;
- Gráfico das entradas e saídas por mês de cada mercadoria;
- Quantidade dos Produtos em estoque;
- Exporta relatórios mensais com todas as entradas e saídas de todas as mercadorias.
- Valida os dados no cliente antes de enviá-los ao servidor, garantindo que campos em branco e caracteres inválidos não sejam submetidos;
- Desenvolvido no sistema .NET MVC, utilizando a linguagem C#;
- Utiliza o Entity Framework para acesso ao banco de dados;
- Utiliza o framework front-end Bootstrap na camada Web;
- Realizar o cadastro de novas mercadorias, salvando nome, número de registro, fabricante, tipo e descrição no banco de dados;
- Cria cadastro de entradas contendo quantidade, data e hora, local e lista (select) de todas as mercadorias já cadastradas, salvando a mercadoria selecionada na tabela de Entrada do banco de dados;
- Registrar saídas contendo quantidade, data e hora, local e uma lista com todas as mercadorias já cadastradas, salvando a mercadoria selecionada na tabela de Saída do banco de dados;
- Banco de dados utilizando SQLite.
  
![3](https://github.com/philippdouglas/ProjetoSupllyChain/assets/78768376/2bcee256-82d2-496d-b415-6dcbe9943582)

## Rodar o Projeto (Vs Code)
1. Abra o terminal no Visual Studio Code.

2. Execute os seguintes comandos no terminal:

```bash
dotnet build
```

```bash
dotnet run
```
![5](https://github.com/philippdouglas/ProjetoSupllyChain/assets/78768376/bb0d8e43-1a1b-4479-9245-d8253e339382)

3. Abra o seu navegador e acesse a URL [https://localhost:5001/Home](https://localhost:5001/Home) ou [https://localhost:5009/Home](https://localhost:5009/Home).

[![C#](https://img.shields.io/badge/-C%23-blue)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![.NET](https://img.shields.io/badge/-.NET-blue)](https://dotnet.microsoft.com/)
[![Entity Framework](https://img.shields.io/badge/-Entity%20Framework-lightgrey)](https://docs.microsoft.com/en-us/ef/)
[![JQuery](https://img.shields.io/badge/-JQuery-blue)](https://jquery.com/)
[![Bootstrap](https://img.shields.io/badge/-Bootstrap-blueviolet)](https://getbootstrap.com/)
[![SQLite](https://img.shields.io/badge/-SQLite-blue)](https://www.sqlite.org/)

## Licença

Este projeto está sob a "**Licença MIT**"


