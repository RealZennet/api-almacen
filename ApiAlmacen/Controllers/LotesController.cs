using ApiAlmacen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ApiAlmacen.Controllers
{
    public class LotesController:ApiController
    {
        public static List<LotesModels> DatosLotes = new List<LotesModels>();
        LotesModels lotes = new LotesModels();


        [Route("api/lotes")]
        public IHttpActionResult Post([FromBody] LotesModels lote)
        {
            if (!ModelState.IsValid || lote == null)
            {
                return BadRequest("Error en el ingreso de datos");
            }
            lote.FechaCreacion = DateTime.Now;
            DatosLotes.Add(lote);
            return Ok(lote);
        }

        [Route("api/lotes")]
        public IHttpActionResult Get([FromBody] LotesModels lote)
        {
            var ListaLotes = DatosLotes.ToList();
            return Ok(ListaLotes);
        }

        [Route("api/lotes/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            LotesModels LoteABuscar = DatosLotes.Find(x => x.IDLote == id); 
            if (LoteABuscar == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(LoteABuscar);
            }
        }
        [Route("api/lotes/{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            LotesModels LoteABuscar = DatosLotes.Find(x => x.IDLote == id);
            if (LoteABuscar == null)
            {
                return NotFound();
            }
            else
            {
                DatosLotes.Remove(LoteABuscar);
                return Ok($" El lote {LoteABuscar.IDLote} ha sido eliminado");
            }
        }

    }
}