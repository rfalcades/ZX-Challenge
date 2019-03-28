using System;
using System.Collections.Generic;

namespace ZX.Utils
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        private static void ImportarArquivo(string path)
        {
            var json = System.IO.File.ReadAllText(path);

            var pdvs = Newtonsoft.Json.JsonConvert.DeserializeObject<ZX.Model.Api.PdvRawCollection>(json);

            // var connectionString = "mongodb+srv://dbUsert:db123_A@cluster0-lciu8.azure.mongodb.net/ZD?retryWrites=true";
            // var b = new ZX.Service.PDV(connectionString);

            //foreach (var pdv in pdvs.pdvs)
            //{
            //    Console.WriteLine($"Gravando {pdv.id}");
            //    b.Create(pdv);
            //}
        }
    }
}
