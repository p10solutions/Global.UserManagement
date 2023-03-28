# Global.UserManagement 
é um microserviço que lida com o dominio de Usuários, utilizando uma Web API .NET 6.
O microserviço <b>Global.UserManagement</b> realiza uma comunicação via RabbitMQ com o microserviço de Aduitoria <b>Global.UserAudit</b> (https://github.com/p10solutions/Global.UserAudit), 
neste microserviço se encontra o docker-compose para subir o servidor do RabbitMQ.

Passo a Passo

1. Execute o <b>docker-compose</b> na raiz deste repositório para que o banco <b>Sql Server</b> seja criado

2. Execute o projeto com o seguinte comando <b>dotnet Global.UserManagement.Api.dll</b>

3. Acesse o endpoint da documentação da api em seu navegador https://localhost:7060/swagger
