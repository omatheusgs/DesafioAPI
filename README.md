Objetivos do Desafio.

Uma rota para cadastro de usuário

Uma rota para autenticação de usuário

Uma rota para consulta de pessoas, que deve retornar uma lista de objeto de pessoas;

Uma rota para consultar uma pessoa pelo seu código;

Uma rota para consultar pessoas de uma determinada UF;

Uma rota de gravar pessoa, que deve retornar o objeto “salvo”;

O método deve ser capaz de validar as informações recebidas;

Uma rota para atualizar os dados de uma pessoa, que deve retorno o objeto atualizado;

Uma rota para excluir uma pessoa;

Descrição: A aplicação foi construída com .NET 8. Para executar é necessário ter essa versão instalada. Você pode realizar o download aqui: https://dotnet.microsoft.com/pt-br/download/dotnet/8.0

A aplicação foi documentada com Swagger, para facilitar a visualização e execução das rotas. Saiba mais em: https://swagger.io/.

A API não possui comunicação com o banco de dados (conforme orientado no pedido do desafio). Todos os dados são persistidos e consumidos em memória (cache) da aplicação.

A aplicação já possui uma pessoa carregada na memória, com o Código '1' e UF definida como "GO", para facilitar a busca.

A aplicação já vem com um usuário carregado, com usuário e senha como 'admin'.

A aplicação está divida em duas camadas:

Web: possui toda a documentação com Swagger e rotas que o usuário pode acessar. Essa é a camada que deve ser executada;

Core: possui todos os serviços que implementam as regras de negócio da aplicação e comunicação com os dados que estão em cache. Também possui algumas funções comuns à toda a aplicação;

Execução:

Instale a versão 8 do .NET Framework;
Clone o repositório;
Abra a solução;
Execute a aplicação (F5);
Crie um novo token acessando a parte de Autenticação;
Copie o token recebido e o coloque na parte de autorização da documentação do Swagger (canto superior direito);
Execute as rotas de pessoa.
