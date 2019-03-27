# ZX Ventures Backend Challenge

## Stack utilizada

* .NET Core 2.2 (Visual Studio 2017 Community / Visual Studio Code)
* MongoDb 4.0.6 
* Azure Websites as Hosting Service
* MongoDb Atlas

## Voc� pode conferir a API em
* [Documenta��o] (https://zx20190326101938.azurewebsites.net/swagger/index.html)
* [API] (https://zx20190326101938.azurewebsites.net/api)
* Obs.: N�o fazer teste de carga nesse ambiente

## Pr� Requisitos (Executar localmente)

### Para compilar

* Baixe o SDK do .net core em [download] (https://dotnet.microsoft.com/download) e siga as instru��es de instala��o de acordo com a plataforma
* Clone esse reposit�rio ou fa�a download do pacote e expanda numa pasta local
* Acesse a pasta `src` na pasta de destino e rode o comando: `dotnet build` 

### Para executar

* Acesse a pasta `src` na pasta de destino e rode o comando: `dotnet run --project .\zx\zx.csproj`
* A api estar� dispon�vel em `https://localhost:5001`

### Instalar o mongodb

* Acesse [mongo download](https://www.mongodb.com/download-center/community) e fa�a o download ou siga as instru�oes de instala��o
* Inicie uma inst�ncia do mongo com o comando `mongod`
* Acesse o shell do mongo com o comando `mongo` e execute o script:
	* use ZD
	* db.PDV.createIndex( { "IdAux": 1 }, { unique: true } )
	* db.PDV.createIndex({ CoverageArea: "2dsphere" })
	* db.PDV.createIndex({ Address: "2dsphere" })

### API

* Acesse a documenta��o em (https://localhost:5001/swagger)


