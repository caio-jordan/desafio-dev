# Desafio Técnico API Previsão do Tempo


Fui desafiado a criar uma aplicação que consulta a previsão tanto atual quanto futura utilizando o https://www.weatherapi.com, e também um serviço que rode em background para cada uma hora limpe todo o cache.


[![Build Status](https://app.travis-ci.com/caio-jordan/MinimalApi.API.svg?token=x3m7RGGS2zZfTm7Js9gs)](https://app.travis-ci.com/caio-jordan/MinimalApi.API)

## Referência


 - [HttpClientFactory](https://learn.microsoft.com/pt-br/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests)
 - [Polly - Utilizado Para Resiliência ](https://learn.microsoft.com/pt-br/dotnet/architecture/microservices/implement-resilient-applications/implement-http-call-retries-exponential-backoff-polly)
 - [EntityFrameWork](https://learn.microsoft.com/pt-br/ef/core/get-started/overview/first-app?tabs=netcore-cli)
- [Tarefas Background Hang Fire](https://docs.hangfire.io/en/latest/getting-started/index.html)
- [PostgresSQL](https://www.postgresql.org/docs/)




## Documentação da API


#### Retorna a previsão de hoje


```http
  GET /api/previsao/previsaoatual
```


| Parâmetro   | Tipo   	| Descrição                       	|
| :---------- | :--------- | :---------------------------------- |
| `cidade` | `string` | **Obrigatório**. Cidade onde deseja ver a previsão atual |


#### Retorna a previsão de até 5 dias a partir da data de hoje


```http
  GET /api/previsao/previsaestendida
```


| Parâmetro   | Tipo   	| Descrição                               	|
| :---------- | :--------- | :------------------------------------------ |
| `cidade`  	| `string` | **Obrigatório**. Cidade onde deseja ver a previsão atual |
| `diasPrevisao`  	| `string` | O padrão sempre será o dia atual |




#### Retorna o historico de busca de até uma hora
```http
  GET /api/previsao/historicoprevisao
```


#### Exclui todo o historico de busca
```http
  DELETE /api/previsao
```


## Configurando appSettings

Para rodar esse projeto local, você vai precisar addicionar sua ConnectionStrings no seu appSettings:

`"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;User ID=postgres;Password=**;Database=postgres;"
}`


## Deploy

Será necessário utilizar o Ef Core para criar o dataBase

```bash
  dotnet ef migrations add "initial-migration"
  dotnet ef database update   
```

Subindo o projeto com docker:

Instâncie a imagem:

```bash
  docker build -t desafio-dev    
```

Instâncie o container (opcional: "--name desafio-dev-container" ):
```bash
  docker run desafio-dev --name desafio-dev-container
```
Agora só acessar a rota default http://localhost:8080/swagger

## Aprendizados

Esse desafio foi bastante desafiador, pois atualmente trabalho com outros tipos de tecnologias e até mesmo com outro banco de dados. Para desenvolver esse projeto, foi utilizado o padrão MVC e o Repository Pattern para a configuração dos repositórios. Implementei uma fábrica HTTP para conseguir implementar a política de retry. Como background task, pela primeira vez implementei o HangFire, que, inclusive, é muito simples de utilizar. Por hora, é isso. Dê um git clone aí e aproveite.

