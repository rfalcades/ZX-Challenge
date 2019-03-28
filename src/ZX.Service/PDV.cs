using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using System.Linq;

namespace ZX.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class PDV : IPDV
    {
        private readonly Model.DB.IDbContext zdContext = null;

        public PDV(Model.DB.IDbContext zdContext) 
        {
            this.zdContext = zdContext;
        }

        /// <summary>
        /// Realiza a inclusão de um novo PDV
        /// </summary>
        public void Create(Model.Api.PdvRaw pdvRaw)
        {
            var pdv = ZX.Service.Utils.ConvertFromRaw(pdvRaw);

            var aux = this.GetByDocument(pdv.Document);

            if (aux != null)
                throw new ApplicationException($"Documento {pdv.Document} já existente. Inclusão não permitida!");

            if (pdv.IdAux == 0)
            {
                pdv.IdAux = this.GetMaxId();
            }
            else
            {
                // Verificar se id já existe
                aux = this.GetById(pdv.IdAux);
            }

            if (aux != null)
                throw new ApplicationException($"Id {pdv.IdAux} já existente. Inclusão não permitida!");


            if (pdv.Address == null || pdv.CoverageArea == null || string.IsNullOrEmpty(pdv.Document) || string.IsNullOrEmpty(pdv.OwnerName) || string.IsNullOrEmpty(pdv.TradingName))
                throw new ApplicationException($"Modelo inválido");

            zdContext.PDVs.InsertOne(pdv);
        }

        /// <summary>
        /// Faz a busca pelo CNPJ do PDV e retorna se encontrado
        /// </summary>
        /// <param name="document">Numero do CNPJ</param>
        /// <returns></returns>
        public Model.Api.PdvRaw GetByDocument(string document)
        {
            var pdv = zdContext.PDVs.Find(d => d.Document == document.Trim()).SingleOrDefault();

            Model.Api.PdvRaw pdvRaw = null;
            if (pdv != null)
                pdvRaw = Utils.ConvertToRaw(pdv);

            return pdvRaw;
        }

        /// <summary>
        /// Retorna o PDV pelo Id
        /// </summary>
        /// <param name="id">Id do PDV</param>
        /// <returns></returns>
        public Model.Api.PdvRaw GetById(int id)
        {
            var pdv = zdContext.PDVs.Find(d => d.IdAux == id).SingleOrDefault();

            Model.Api.PdvRaw pdvRaw = null;

            if (pdv != null)
                pdvRaw = Utils.ConvertToRaw(pdv);

            return pdvRaw;
        }

        /// <summary>
        /// Dado uma latitude e longitude, retorna pelo PDV mais próximo
        /// </summary>
        /// <param name="lat">Latitude</param>
        /// <param name="lng">Longitude</param>
        /// <returns></returns>
        public Model.Api.PdvRaw GetByLatLng(double lat, double lng)
        {
            // Define o ponto de referência
            var p = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(new GeoJson2DGeographicCoordinates(lng, lat));

            // Monta o filtro pra retornar os PDVs que atendem o ponto informado
            var q = Builders<Model.PDV>.Filter.GeoIntersects(_ => _.CoverageArea, p);

            // Retorna todos os PDVs cujo ponto informado é atendido
            var pdvs = zdContext.PDVs.Find(q).ToList();

            Model.Api.PdvRaw pdvRaw = null;

            if (pdvs != null && pdvs.Count > 0)
            {
                // Ordena por distância do PDV e seleciona o primeiro
                var pdvMaisPerto = pdvs.OrderBy(_ => _.DistanceFrom(lng, lat)).ToList()[0];

                // Transforma pro modelo de apresentação
                pdvRaw = Utils.ConvertToRaw(pdvMaisPerto);
            }

            return pdvRaw;
        }

        private int GetMaxId()
        {
            // db.collection.aggregate({ $group: { _id: null, max: { $max: "$age" } } });
            var aggregate = zdContext.PDVs.Aggregate()
                                      .Group( new BsonDocument { { "_id", 0 }, { "max", new BsonDocument("$max", "$IdAux") } })
                                      .Limit(1);

            var result = aggregate.SingleOrDefault();

            if (result is null)
                return 1;

            return result["max"].AsInt32 + 1;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IPDV
    {
        void Create(Model.Api.PdvRaw pdvRaw);
        Model.Api.PdvRaw GetByDocument(string document);
        Model.Api.PdvRaw GetById(int id);
        Model.Api.PdvRaw GetByLatLng(double lat, double lng);
    }
}
