using ApiAlmacen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ApiAlmacen.Controllers
{
    public class TruckerCarriesBatchController : ApiController
    {
        [Route("api/v1/camionllevalotes")]
        public IHttpActionResult Post([FromBody] TruckerCarriesBatchModel truckercarriesbatch)
        {
            if (!ModelState.IsValid || truckercarriesbatch == null)
            {
                return BadRequest("Error en el ingreso de datos");
            }
            truckercarriesbatch.Save();
            return Ok($"El camion {truckercarriesbatch.IDTruck} llevara el lote {truckercarriesbatch.IDBatch}");
        }

        [Route("api/v1/camionllevalotes")]
        public IHttpActionResult Get()
        {
            TruckerCarriesBatchModel truckercarriesbatch = new TruckerCarriesBatchModel();
            var listcarries = truckercarriesbatch.GetAllCarries();
            var carriesView = listcarries.Select(everyCarrie => new GetTruckersCarriesBatchView
            {
                IDTruck = everyCarrie.IDTruck,
                IDBatch = everyCarrie.IDBatch
            }
            ).ToList();

            return Ok(carriesView);
        }

    }
}