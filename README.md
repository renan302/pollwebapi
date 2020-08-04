<h1>Iniciar o Projeto PollWebApi</h1>

- Instalar o docker. Basta acessar a página https://docs.docker.com/engine/install/ubuntu/ e seguir as instruções para a instalação em seu sistema operacional;

- Instalar o docker-compose. Basta acessar a página https://docs.docker.com/compose/install/ e seguir as instruções para a instalação em seu sistema operacional;

- Instalar a SDK .NET Core https://dotnet.microsoft.com/download;

- Acessar o arquivo "./PollWebApi/appsettings.json";

- Alterar o valor "MEU_IP" da propriedade "pollmysqldev", para seu ip em ambiente de desenvolvimento;

- Alterar o valor "MEU_IP" da propriedade "pollmysqlprod", para seu ip em ambiente de produção;

- Alterar o valor "MEU_IP" da propriedade "conexaoredisdev", para seu ip em ambiente de desenvolvimento;

- Alterar o valor "MEU_IP" da propriedade "conexaoredisprod", para seu ip em ambiente de produção;

- Na pasta raiz do projeto, onde fica localizado o arquivo docker-compose.yml, execute o comando "docker-compose up -d";

- Na pasta raiz do projeto, Execute o comando "dotnet ef database update", para iniciar um novo banco de dados;

- Na pasta raiz do projeto, Execute o comando "dotnet test", para executar os testes de integração;

- Agora sua aplicação deve estar rodando no endereço "http://localhost:8010/" ou "http://127.0.0.1:8010"

- Swagger API: "http://localhost:8010/doc" ou "http://127.0.0.1:8010/doc"
