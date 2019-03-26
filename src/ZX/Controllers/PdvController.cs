using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ZX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdvController : ControllerBase
    {
        private readonly IConfiguration config;

        public PdvController(IConfiguration config)
        {
            this.config = config;
        }

        [HttpGet()]
        public ActionResult<Model.Api.PdvRaw> Get(string documento)
        {
            var pdvs = new ZX.Service.PDV(config.GetConnectionString("ZXDB"));

            var pdv = pdvs.GetByDocument(documento);

            if (pdv != null)
                return Ok(pdv);
            else
                return NotFound($"Documento não encontrado {documento}");

        }

        // GET api/pdv/5
        [HttpGet()]
        [Route("q")]
        public ActionResult<string> Get(double lat, double lng)
        {
            var pdvs = new ZX.Service.PDV(config.GetConnectionString("ZXDB"));

            var pdv = pdvs.GetByLatLng(lat, lng);

            if (pdv != null)
                // Retorna o PDV encontrado
                return Ok(pdv);
            else
                // Nenhum PDV atende o ponto informado
                return NotFound();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Model.Api.PdvRaw pdvRaw)
        {
            var pdv = new ZX.Service.PDV(config.GetConnectionString("ZXDB"));
            pdv.Create(pdvRaw);
        }

        [HttpPost()]
        [Route("Importar")]
        public ActionResult<string> Importar(Model.Api.PdvRawCollection pdvs)
        {
            try
            {
                var b = new ZX.Service.PDV(config.GetConnectionString("ZXDB"));

                foreach (var pdv in pdvs.pdvs)
                    b.Create(pdv);

                return Ok("Ok");
            }
            catch (Exception ex)
            {
                return Ok(ex.ToString() + " xxxx " + System.IO.Directory.GetCurrentDirectory());
            }
        }

    }

}