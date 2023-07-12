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
        [Route("api/lotes")]
        public IHttpActionResult Post([FromBody] LotesModels lote)
        {
            if (!ModelState.IsValid || lote == null)
            {
                return BadRequest("Error en el ingreso de datos");
            }
            lote.FechaCreacion = DateTime.Now;
            lote.Save();
            return Ok($"Lote {lote.IDLote} fue guardado con exito");
        }

        [Route("api/lotes")]
        public IHttpActionResult Get()
        {
            LotesModels lotes = new LotesModels();
            var listaLotes = lotes.GetAllLots();
            var lotsView = listaLotes.Select(everyLot => new GetLotsView
            {
                IDLote = everyLot.IDLote,
                FechaCreacion = everyLot.FechaCreacion,
                CantidadProductosEnLote = everyLot.CantidadProductosEnLote
            });
            return Ok(lotsView);
        }


        [Route("api/lotes/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            LotesModels lotes = new LotesModels();
            var listaLotes = lotes.GetAllLots();
            var lote = listaLotes.FirstOrDefault(everyLot => everyLot.IDLote == id);
            if (lote == null)
            {
                return NotFound();
            }
            else
            {
                var lotsView = new GetLotsView
                {
                    IDLote = lote.IDLote,
                    FechaCreacion = lote.FechaCreacion,
                    CantidadProductosEnLote = lote.CantidadProductosEnLote
                };
                return Ok(lotsView);
            }
        }
        [Route("api/lotes/{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            LotesModels lotes = new LotesModels();
            var listaLotes = lotes.GetAllLots();
            var lote = listaLotes.FirstOrDefault(everyLot => everyLot.IDLote == id);
            if (lote == null)
            {
                return NotFound();
            }
            else
            {
                lote.DeleteLots();
                return Ok($"El lote con el id {lote.IDLote} fue eliminado con exito");
            }
        }

        [Route("api/lotes/{id:int}")]
        public IHttpActionResult Put(int id, [FromBody] LotesModels lote)
        {
            if (!ModelState.IsValid || lote == null)
            {
                return BadRequest("Error en el ingreso de datos");
            }

            lote.IDLote = id;
            lote.Edit();

            return Ok($"Producto {lote.IDLote} editado con éxito");
        }

    }
}