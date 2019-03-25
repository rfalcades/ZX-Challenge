using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace ZX.Model
{
    public class PDV
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int IdAux { get; set; }
        public string TradingName { get; set; }
        public string OwnerName { get; set; }
        public string Document { get; set; } // Cnpj

        public GeoJsonPolygon<GeoJson2DGeographicCoordinates> CoverageArea { get; set; }

        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Address { get; set; }
    }
}
