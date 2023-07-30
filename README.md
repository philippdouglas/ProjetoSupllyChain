Projeto SupplyChain

--Descrição

Projeto de gerenciamento de mercadorias utilizando C#, .NET, Entity Framework, JQuery, Bootstrap e SQLite. O objetivo deste sistema é permitir o controle de estoque e movimentação de mercadorias, com funcionalidades para gerenciamento das mercadorias, entrada e saída, visualização de entradas e saídas por mês em formato de gráfico, além de exportação de relatórios mensais com todas as movimentações.

--Funcionalidades

•	Gerenciar as mercadorias disponíveis;
•	Registrar entradas e saídas de mercadorias;
•	Visualizar gráficos das entradas e saídas por mês de cada mercadoria;
•	Exportar relatórios mensais com todas as entradas e saídas de todas as mercadorias.
•	Valida os dados no cliente antes de enviá-los ao servidor, garantindo que campos em branco e caracteres inválidos não sejam submetidos;
•	Desenvolvido no sistema .NET MVC, utilizando a linguagem C#;
•	Utiliza o Entity Framework para acesso ao banco de dados;
•	Utiliza o framework front-end Bootstrap na camada Web;
•	Realizar o cadastro de novas mercadorias, salvando nome, número de registro, fabricante, tipo e descrição no banco de dados;
•	Cria cadastro de entradas contendo quantidade, data e hora, local e lista (select) de todas as mercadorias já cadastradas, salvando a mercadoria selecionada na tabela de Entrada do banco de dados;
•	Registrar saídas contendo quantidade, data e hora, local e uma lista com todas as mercadorias já cadastradas, salvando a mercadoria selecionada na tabela de Saída do banco de dados;
•	Banco de dados utilizando SQLite.

--Licença
Este projeto está sob a licença MIT License.

