using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;

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

        public Model.Api.PdvRaw GetByDocument(string document)
        {
            zdContext = new Model.DB.ZDContext(connectionString);

            var pdv = zdContext.PDVs.Find(d => d.Document == document.Trim()).SingleOrDefault();

            Model.Api.PdvRaw pdvRaw = null;
            if (pdv != null)
                pdvRaw = Utils.ConvertToRaw(pdv);

            return pdvRaw;
        }

        public Model.Api.PdvRawCollection GetByLatLng(double lat, double lng)
        {
            zdContext = new Model.DB.ZDContext(connectionString);

            //var geoNearOptions = new BsonDocument {
            //            { "nearSphere", new BsonDocument {
            //                { "type", "Point" },
            //                { "coordinates", new BsonArray { lng, lat} },
            //            } },
            //            //{ "distanceField", "dist.calculated" },
            //            // { "maxDistance", 100 },
            //            // { "includeLocs", "dist.location" },
            //            // { "num", 5 },
            //            // { "spherical" , true }
            //    };

            //var q = new BsonDocument { { "$geoNear", geoNearOptions } };

            var q = new BsonDocument { {"CoverageArea" ,
                new BsonDocument { {"$nearSphere",
                new BsonDocument { {"$geometry",
                new BsonDocument {
                            { "type", "Point" },
                            { "coordinates", new BsonArray { lng, lat} },
                        } } } } } } };

            var pdvs = zdContext.PDVs.Find(q).ToList();

            Model.Api.PdvRawCollection pdvsRaw = new Model.Api.PdvRawCollection();

            if (pdvs != null)
                foreach (var pdv in pdvs)
                    pdvsRaw.Add(Utils.ConvertToRaw(pdv));

            return pdvsRaw;
        }

    }
}
