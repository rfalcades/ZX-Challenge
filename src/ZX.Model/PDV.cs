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

        public double DistanceFrom(double longitude, double latitude)
        {
            return DistanceUtil.DistanceBetweenPlaces(longitude, latitude, this.Address.Coordinates.Longitude, this.Address.Coordinates.Latitude);
        }
    }

    public class DistanceUtil
    {
        internal static double DistanceBetweenPlaces(double lon1, double lat1, double lon2, double lat2)
        {
            double R = 6371; // km            

            double sLat1 = Math.Sin(Radians(lat1));
            double sLat2 = Math.Sin(Radians(lat2));
            double cLat1 = Math.Cos(Radians(lat1));
            double cLat2 = Math.Cos(Radians(lat2));
            double cLon = Math.Cos(Radians(lon1) - Radians(lon2));

            double cosD = sLat1 * sLat2 + cLat1 * cLat2 * cLon;

            double d = Math.Acos(cosD);

            double dist = R * d;

            return dist;
        }

        internal static double Radians(double x)
        {
            return x * Math.PI / 180;
        }
    }
}
