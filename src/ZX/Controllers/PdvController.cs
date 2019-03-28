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
    /// <summary>
    /// Controlador dos PDVs (Pontos de Venda)
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PdvController : ControllerBase
    {

        private readonly Service.IPDV pdvService;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="pdvService"></param>
        public PdvController(Service.IPDV pdvService)
        {
            this.pdvService = pdvService;
        }

        /// <summary>
        /// Realiza a busca do PDV pelo Cnpj
        /// </summary>
        /// <param name="cnpj">Cnpj do PDV</param>
        /// <returns></returns>
        [HttpGet()]
        [Route("documento/{cnpj}")]
        public ActionResult<Model.Api.PdvRaw> GetByDocumento(string cnpj)
        {
            var pdv = pdvService.GetByDocument(cnpj);

            if (pdv != null)
                return Ok(pdv);
            else
                return NotFound($"Documento não encontrado {cnpj}");
        }

        /// <summary>
        /// Retorna o PDV pelo Id 
        /// </summary>
        /// <param name="id">Id do PDV</param>
        /// <returns>Retorna um PDV válido se encontrado</returns>
        [HttpGet()]
        [Route("{id}")]
        public ActionResult<Model.Api.PdvRaw> GetById(int id)
        {
            var pdv = pdvService.GetById(id);

            if (pdv != null)
                return Ok(pdv);
            else
                return NotFound($"PDV não encontrado {id}");
        }

        /// <summary>
        /// Realiza a busca do PDV mais próximo que atenda o ponto informado
        /// </summary>
        /// <param name="lat">Latitude</param>
        /// <param name="lng">Longitude</param>
        /// <returns>Retorna um PDV válido se encontrado</returns>
        [HttpGet()]
        [Route("LatLng")]
        public ActionResult<Model.Api.PdvRaw> GetByLatLng(double lat, double lng)
        {
            var pdv = pdvService.GetByLatLng(lat, lng);

            if (pdv != null)
                // Retorna o PDV encontrado
                return Ok(pdv);
            else
                // Nenhum PDV atende o ponto informado
                return NotFound();
        }

        /// <summary>
        /// Cria um novo PDV se o Id não existir 
        /// </summary>
        /// <param name="pdvRaw">Modelo Canonico do PDV</param>
        [HttpPost]
        public void Post([FromBody] Model.Api.PdvRaw pdvRaw)
        {
            pdvService.Create(pdvRaw);
        }

        /// <summary>
        /// Realiza a criação de vários Pdvs numa única chamada
        /// </summary>
        /// <param name="pdvs">Collections de PDV</param>
        /// <returns></returns>
        [HttpPost()]
        [Route("Importar")]
        public ActionResult<string> Importar(Model.Api.PdvRawCollection pdvs)
        {
            foreach (var pdv in pdvs.pdvs)
                try
                {
                    pdvService.Create(pdv);
                }
                catch
                {
                    // Logar o erro (não implementado) e processar o próximo
                }

            return Ok("Ok");
        }
    }
}