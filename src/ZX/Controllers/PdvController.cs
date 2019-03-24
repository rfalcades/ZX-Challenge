using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return string.Format("Valor {0}", id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Model.Api.PdvRaw pdvRaw)
        {
            var pdv = new ZX.Service.PDV(config.GetConnectionString("ZXDB"));
            pdv.Create(pdvRaw);
        }
    
    }

}