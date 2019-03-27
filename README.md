# ZX Ventures Backend Challenge

## Stack utilizada

* .NET Core 2.2 (Visual Studio 2017 Community / Visual Studio Code)
* MongoDb 4.0.6 
* Azure Websites as Hosting Service
* MongoDb Atlas

## Você pode conferir a API em
* [Documentação] (https://zx20190326101938.azurewebsites.net/swagger/index.html)
* [API] (https://zx20190326101938.azurewebsites.net/api)
* Obs.: Não fazer teste de carga nesse ambiente

## Pré Requisitos (Executar localmente)

### Para compilar

* Baixe o SDK do .net core em [download] (https://dotnet.microsoft.com/download) e siga as instruções de instalação de acordo com a plataforma
* Clone esse repositório ou faça download do pacote e expanda numa pasta local
* Acesse a pasta `src` na pasta de destino e rode o comando: `dotnet build` 

### Para executar

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

### API

* Acesse a documentação em (https://localhost:5001/swagger)


