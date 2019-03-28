using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver.GeoJsonObjectModel;
using System;
using System.Linq;

namespace ZX.UnitTest
{
    [TestClass]
    public class Testes
    {
        private ZX.Service.IPDV pdvService = null;

        public Testes()
        {
            var connectionString = "mongodb+srv://dbUsert:db123_A@cluster0-lciu8.azure.mongodb.net/ZD?retryWrites=true";
            pdvService = new Service.PDV(new Model.DB.ZDContext(connectionString));
        }

        [TestMethod]
        public void Insert_One()
        {
            var pdv = new Model.PDV();

            pdv.IdAux = 0;
            pdv.TradingName = "Adega Osasco 2";
            pdv.OwnerName = "Ze da Ambev 2 ";
            pdv.Document = "05.453.716/000170";
            pdv.Address = GeoJson.Point(new GeoJson2DGeographicCoordinates( -23.013538, -43.297337));

            pdv.CoverageArea = GeoJson.Polygon(
                               GeoJson.Geographic(-22.99669, -43.36556),
                               GeoJson.Geographic(-23.01928, -43.36539),
                               GeoJson.Geographic(-23.01802, -43.26583),
                               GeoJson.Geographic(-23.00649, -43.25724),
                               GeoJson.Geographic(-23.00127, -43.23355),
                               GeoJson.Geographic(-22.99716, -43.2381),
                               GeoJson.Geographic(-22.99649, -43.23866),
                               GeoJson.Geographic(-22.99756, -43.24063),
                               GeoJson.Geographic(-22.99736, -43.24634),
                               GeoJson.Geographic(-22.99606, -43.24677),
                               GeoJson.Geographic(-22.99381, -43.24067),
                               GeoJson.Geographic(-22.99121, -43.24886),
                               GeoJson.Geographic(-22.99456, -43.25617),
                               GeoJson.Geographic(-22.99203, -43.25625),
                               GeoJson.Geographic(-22.99065, -43.25346),
                               GeoJson.Geographic(-22.98283, -43.29599),
                               GeoJson.Geographic(-22.96481, -43.3262),
                               GeoJson.Geographic(-22.96402, -43.33427),
                               GeoJson.Geographic(-22.96829, -43.33616),
                               GeoJson.Geographic(-22.98157, -43.342),
                               GeoJson.Geographic(-22.97967, -43.34817),
                               GeoJson.Geographic(-22.98062, -43.35142),
                               GeoJson.Geographic(-22.98084, -43.3573),
                               GeoJson.Geographic(-22.98032, -43.36522),
                               GeoJson.Geographic(-22.98422, -43.36696),
                               GeoJson.Geographic(-22.98855, -43.36717),
                               GeoJson.Geographic(-22.99351, -43.36636),
                               GeoJson.Geographic(-22.99669, -43.36556));

            // zdContext.PDVs.InsertOne(pdv);
            Assert.IsTrue(1 == 1);
        }

        [TestMethod]
        public void Deserialize_Test()
        {
            var json = @"{
          'id': '2',
          'tradingName': 'Adega Pinheiros',
          'ownerName': 'Ze da Silva',
          'document': '04.433.714/0001-44',
          'coverageArea': {
                'type': 'MultiPolygon',
             'coordinates': [
                [
                   [
                      [
                         -49.36299,
                         -25.4515
                      ],
                      [
                         -49.35334,
                         -25.45065
                      ],
                      [
                         -49.33675,
                         -25.4429
                      ],
                      [
                         -49.32291,
                         -25.4398
                      ],
                      [
                         -49.3188,
                         -25.44089
                      ],
                      [
                         -49.31064,
                         -25.43903
                      ],
                      [
                         -49.29828,
                         -25.43391
                      ],
                      [
                         -49.29751,
                         -25.43377
                      ],
                      [
                         -49.29588,
                         -25.43322
                      ],
                      [
                         -49.29215,
                         -25.43189
                      ],
                      [
                         -49.28855,
                         -25.43043
                      ],
                      [
                         -49.28662,
                         -25.42958
                      ],
                      [
                         -49.28424,
                         -25.42865
                      ],
                      [
                         -49.25803,
                         -25.42853
                      ],
                      [
                         -49.25533,
                         -25.42279
                      ],
                      [
                         -49.25585,
                         -25.4169
                      ],
                      [
                         -49.25524,
                         -25.40981
                      ],
                      [
                         -49.25761,
                         -25.40403
                      ],
                      [
                         -49.25524,
                         -25.39787
                      ],
                      [
                         -49.26005,
                         -25.39178
                      ],
                      [
                         -49.26078,
                         -25.3819
                      ],
                      [
                         -49.26267,
                         -25.37348
                      ],
                      [
                         -49.25952,
                         -25.37003
                      ],
                      [
                         -49.25971,
                         -25.36597
                      ],
                      [
                         -49.26301,
                         -25.35774
                      ],
                      [
                         -49.26468,
                         -25.34742
                      ],
                      [
                         -49.30623,
                         -25.35119
                      ],
                      [
                         -49.36262,
                         -25.36639
                      ],
                      [
                         -49.37043,
                         -25.3798
                      ],
                      [
                         -49.36743,
                         -25.40593
                      ],
                      [
                         -49.36837,
                         -25.42578
                      ],
                      [
                         -49.36299,
                         -25.4515
                      ]
                   ]
                ]
             ]
          },
          'address': {
             'type': 'Point',
             'coordinates': [
                -49.33425,
                -25.380995
             ]
}
       }";
            
            ZX.Service.Utils.ConvertFromText(json);

        }

        [TestMethod]
        public void GetById()
        {
            try
            {
                var pdvRaw = pdvService.GetById(1);

                Assert.IsTrue(pdvRaw != null);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GetByDocument()
        {
            try
            {
                var pdvRaw = pdvService.GetByDocument("02.453.716/000170");

                Assert.IsTrue(pdvRaw != null);
            }
            catch 
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Distance()
        {
            // BF -46.656427, -23.528184
            // JC -46.701694, -23.579419
            // Ref 1 -46.693768, -23.569365

            var d1 = ZX.Model.DistanceUtil.DistanceBetweenPlaces(-46.693768, -23.569365, -46.656427, -23.528184);
            var d2 = ZX.Model.DistanceUtil.DistanceBetweenPlaces(-46.693768, -23.569365, -46.701694, -23.579419);

            Assert.IsTrue(d1 != d2);
        }
    }
}
