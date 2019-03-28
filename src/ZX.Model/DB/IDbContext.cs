using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;

namespace ZX.Model.DB
{
    public interface IDbContext
    {
        IMongoCollection<Model.PDV> PDVs { get; }
    }
}
