using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

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
    }
}
