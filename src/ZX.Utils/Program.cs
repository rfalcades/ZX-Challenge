using System;
using System.Collections.Generic;

namespace ZX.Utils
{
    class Program
    {
        static void Main(string[] args)
        {
            ImportarArquivo();
        }

        private static void ImportarArquivo()
        {
            var json = System.IO.File.ReadAllText(@"D:\Work\ZX-Challenge\src\ZX.Utils\pdvs.json");

            var pdvs = Newtonsoft.Json.JsonConvert.DeserializeObject<ZX.Model.Api.PdvRawCollection>(json);

            var connectionString = "mongodb+srv://dbUsert:db123_A@cluster0-lciu8.azure.mongodb.net/ZD?retryWrites=true";
            var b = new ZX.Service.PDV(connectionString);

            foreach (var pdv in pdvs.pdvs)
            {
                Console.WriteLine($"Gravando {pdv.id}");
                b.Create(pdv);
            }
        }
    }
}
