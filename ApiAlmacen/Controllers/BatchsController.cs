using ApiAlmacen.Controllers.Handlers;
using ApiAlmacen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ApiAlmacen.Controllers
{
    public class BatchsController:ApiController
    {
        private Dictionary<string, string> showResult(string message)
        {
            Dictionary<string, string> resultJson = new Dictionary<string, string>();
            resultJson.Add("Accion realizada con exito: ", message);
            return resultJson;
        }

        [Route("api/v1/lotes")]
        public IHttpActionResult Post([FromBody] BatchModels batch) 
        {
            if (!ModelState.IsValid || batch == null)
            {
                var errorResponse = $"Error, revisa los datos ingresados.";
                return BadRequest(errorResponse.ToString());
            }
            batch.DateOfCreation = DateTime.Now;
            batch.Save();
            return Ok(showResult($"Lote {batch.IDBatch.ToString()} guardado"));
        }

        [Route("api/v1/lotes")]
        public IHttpActionResult Get()
        {
            BatchModels batches = new BatchModels();
            var batchList = batches.GetAllLots();
            var batchView = batchList.Select(everyBatch => new GetBatchsView
            {
                IDBatches = everyBatch.IDBatch,
                DateOfCreation = everyBatch.DateOfCreation,
                ProductsAmountOnBatches = everyBatch.ProductAmountOnBatch
            });
            return Ok(batchView);
        }


        [Route("api/v1/lotes/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            BatchModels batches = new BatchModels();
            var batchList = batches.GetAllLots();
            var batch = batchList.FirstOrDefault(everyBatch => everyBatch.IDBatch == id);
            if (batch == null)
            {
                return NotFound();
            }
            else
            {
                var batchView = new GetBatchsView
                {
                    IDBatches = batch.IDBatch,
                    DateOfCreation = batch.DateOfCreation,
                    ProductsAmountOnBatches = batch.ProductAmountOnBatch
                };
                return Ok(batchView);
            }
        }
        [Route("api/v1/lotes/{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            BatchModels batches = new BatchModels();
            var batchList = batches.GetAllLots();
            var batch = batchList.FirstOrDefault(everyBatch => everyBatch.IDBatch == id);
            if (batch == null)
            {
                return NotFound();
            }
            else
            {
                batch.DeleteLots();
                return Ok(showResult($"El lote con el id {batch.IDBatch} fue eliminado con exito"));
            }
        }

        [Route("api/v1/lotes/{id:int}")]
        public IHttpActionResult Put(int id, [FromBody] BatchModels batch)
        {
            try
            {

            
            if (!ModelState.IsValid || batch == null)
            {
                var errorResponse = $"Error con el lote {batch.IDBatch}";
                return BadRequest(errorResponse.ToString());
            }

            batch.IDBatch = id;
            batch.Edit();

            return Ok(showResult($"Producto {batch.IDBatch} editado con éxito"));
            }
            catch (BatchNotFoundException ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }


    }
}