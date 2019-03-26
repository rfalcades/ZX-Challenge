using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using System.Linq;

namespace ZX.Service
{
    public class PDV
    {
        private readonly string connectionString;

        public PDV(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private ZX.Model.DB.ZDContext zdContext = null;

        /// <summary>
        /// Realiza a inclusão de um novo PDV
        /// </summary>
        public void Create(Model.Api.PdvRaw pdvRaw)
        {
            var pdv = ZX.Service.Utils.ConvertFromRaw(pdvRaw);
            zdContext = new Model.DB.ZDContext(connectionString);

            // Verificar se id já existe
            var aux = zdContext.PDVs.Find(d => d.IdAux == pdv.IdAux).SingleOrDefault();

            if (aux != null)
                throw new ApplicationException($"Id {pdv.IdAux} já existente. Inclusão não permitida!");

            aux = zdContext.PDVs.Find(d => d.Document == pdv.Document).SingleOrDefault();

            if (aux != null)
                throw new ApplicationException($"Documento {pdv.Document} já existente. Inclusão não permitida!");


            zdContext.PDVs.InsertOne(pdv);
        }

        /// <summary>
        /// Faz a busca pelo CNPJ do PDV e retorna se encontrado
        /// </summary>
        /// <param name="document">Numero do CNPJ</param>
        /// <returns></returns>
        public Model.Api.PdvRaw GetByDocument(string document)
        {
            zdContext = new Model.DB.ZDContext(connectionString);

            var pdv = zdContext.PDVs.Find(d => d.Document == document.Trim()).SingleOrDefault();

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
            zdContext = new Model.DB.ZDContext(connectionString);

            // Define o ponto de referência
            var p = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(new GeoJson2DGeographicCoordinates(lng, lat));

            // Monta o filtro pra retornar os PDVs que atendem o ponto informado
            var q = Builders<Model.PDV>.Filter.GeoIntersects(_ => _.CoverageArea, p);

            // Retorna todos os PDVs cujo ponto informado é atendido
            var pdvs = zdContext.PDVs.Find(q).ToList();

            Model.Api.PdvRaw pdvRaw = null;

            if (pdvs != null && pdvs.Count > 0)
            {
                var pdvMaisPerto = pdvs.OrderBy(_ => _.DistanceFrom(lng, lat)).ToList()[0];

                // Transforma pro modelo de apresentação
                pdvRaw = Utils.ConvertToRaw(pdvMaisPerto);
            }

            return pdvRaw;
        }
    }
}
