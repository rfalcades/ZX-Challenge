# ZX Ventures Backend Challenge

## Stack utilizada

* .NET Core 2.2 (Visual Studio 2017 Community / Visual Studio Code)
* MongoDb 4.0.6 
* Azure Websites as Hosting Service
* MongoDb Atlas

### Você pode conferir a API funcionando em
* [Documentação](https://zx20190326101938.azurewebsites.net/swagger/index.html)
* [API](https://zx20190326101938.azurewebsites.net/api)

### Exemplo

Buscar um PDV na [Barra Funda](https://zx20190326101938.azurewebsites.net/api/pdv/latlng?lat=-23.528184&lng=-46.656427)

Obs.: Não fazer teste de carga nesse ambiente

## Pré Requisitos (Executar localmente)

### Compilar

* Baixe o SDK do .net core em [download](https://dotnet.microsoft.com/download) e siga as instruções de instalação de acordo com a plataforma
* Clone esse repositório ou faça download do pacote e expanda numa pasta local
* Acesse a pasta `src` na pasta de destino e rode o comando: `dotnet build` 

### Rodar testes 

* Acesse a pasta `src` na pasta de destino e rode o comando: `dotnet test`

Obs: Pode ser que o teste falhe por causa da restrição de firewall no banco de dados

### Executar localmente

* Acesse a pasta `src` na pasta de destino e rode o comando: `dotnet run --project .\zx\zx.csproj`
* A api estará disponível em `https://localhost:5001`

### Instalar o mongodb

* Acesse [mongo download](https://www.mongodb.com/download-center/community) e faça o download ou siga as instruçoes de instalação
* Inicie uma instância do mongo com o comando `mongod`
* Acesse o shell do mongo com o comando `mongo` e execute o script:
	* use ZD
	* db.PDV.createIndex( { "IdAux": 1 }, { unique: true } )
	* db.PDV.createIndex({ CoverageArea: "2dsphere" })
	* db.PDV.createIndex({ Address: "2dsphere" })

* Configurar a string de conexão com o banco de dados em appsettings.json 

```
  "ConnectionStrings": {
    "ZXDB": "mongodb://localhost:27017/ZD?retryWrites=true"
  }
```
	   
### API

* Acesse a [documentação local](https://localhost:5001/swagger)

### Organização do Código

O código está dividido assim:

* ZX - Ponto de entrada de API onde está o executável principal (WebApi)
* ZX.Model - Modelo de Classes e acesso ao banco
* ZX.Service - Ou worker, é onde estão as regras que servem tanto a WebApi quanto a CLI
* ZX.CLI - Ou Command Line Interface, é uma aplicação de apoio para executar linhas de comando
* ZX.UnitTest - Projeto de testes unitários



