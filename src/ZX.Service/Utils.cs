using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver.GeoJsonObjectModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace ZX.Service
{
    public static class Utils
    {

        public static Model.Api.PdvRaw ConvertToRaw(Model.PDV pdv)
        {
            Model.Api.PdvRaw pdvRaw = new Model.Api.PdvRaw();

            pdvRaw.id = pdv.IdAux.ToString();
            pdvRaw.tradingName = pdv.TradingName;
            pdvRaw.ownerName = pdv.OwnerName;
            pdvRaw.document = pdv.Document;

            pdvRaw.address.type = "Point";
            pdvRaw.address.coordinates = new List<double>() { pdv.Address.Coordinates.Longitude, pdv.Address.Coordinates.Latitude };

            pdvRaw.coverageArea.type = "MultiPolygon";
            pdvRaw.coverageArea.coordinates = new List<List<List<List<double>>>>();
            pdvRaw.coverageArea.coordinates.Add(new List<List<List<double>>>());
            pdvRaw.coverageArea.coordinates[0].Add(new List<List<double>>());

            foreach (var p in pdv.CoverageArea.Coordinates.Exterior.Positions)
                pdvRaw.coverageArea.coordinates[0][0].Add(new List<double>() { p.Longitude, p.Latitude });

            return pdvRaw;
        }

        public static Model.PDV ConvertFromRaw(Model.Api.PdvRaw pdvRaw)
        {
            var pdv = new Model.PDV();

            pdv.IdAux = int.Parse(pdvRaw.id);
            pdv.TradingName = pdvRaw.tradingName;
            pdv.OwnerName = pdvRaw.ownerName;
            pdv.Document = pdvRaw.document;

            pdv.Address = GeoJson.Point(new GeoJson2DGeographicCoordinates(pdvRaw.address.coordinates[0], pdvRaw.address.coordinates[1]));

            var coverageArea = new List<GeoJson2DGeographicCoordinates>();
            foreach (var p in pdvRaw.coverageArea.coordinates)
                foreach (var p1 in p)
                    foreach (var p2 in p1)
                        coverageArea.Add(GeoJson.Geographic(p2[0], p2[1]));

            pdv.CoverageArea = GeoJson.Polygon(coverageArea.ToArray());

            return pdv;
        }

        public static Model.PDV ConvertFromText(string jsonPdv)
        {
            dynamic o = JsonConvert.DeserializeObject(jsonPdv);

            var pdv = new Model.PDV();

            pdv.Id = o.id;
            pdv.TradingName = o.tradingName;
            pdv.OwnerName = o.ownerName;
            pdv.Document = o.document;

            pdv.Address = GeoJson.Point(new GeoJson2DGeographicCoordinates(Double.Parse(o.address.coordinates[1].ToString()), Double.Parse(o.address.coordinates[0].ToString())));

            var coverageArea = new List<GeoJson2DGeographicCoordinates>();

            foreach (var p in o.coverageArea.coordinates)
                foreach (var p1 in p)
                    foreach (var p2 in p1)
                        coverageArea.Add(GeoJson.Geographic(Double.Parse(p2[1].ToString()), Double.Parse(p2[0].ToString())));

            pdv.CoverageArea = GeoJson.Polygon(coverageArea.ToArray());

            return pdv;
        }
    }
}
