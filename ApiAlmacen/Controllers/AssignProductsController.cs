using ApiAlmacen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ApiAlmacen.Controllers
{
    public class AssignProductsController:ApiController
    {

        private Dictionary<string, string> showResult(string message)
        {
            Dictionary<string, string> resultJson = new Dictionary<string, string>();
            resultJson.Add("Accion realizada con exito: ", message);
            return resultJson;
        }

        [Route("api/v1/asignacionproductos")]
        public IHttpActionResult Post([FromBody] AssignProductModels AssignedProduct)
        {
            if (!ModelState.IsValid || AssignedProduct == null)
            {
                var errorResponse = $"Error en el ingreso de datos con el id {AssignedProduct.IDBatch} y " +
                    $"{AssignedProduct.IDProduct}";
                return BadRequest(errorResponse.ToString());
            }

            try
            {
                AssignedProduct.Save();
                return Ok(showResult($"Producto {AssignedProduct.IDProduct} fue asignado correctamente al lote {AssignedProduct.IDBatch}"));
            }
            catch (Exception)
            {
                return BadRequest("Error en el ingreso de datos, si el error persiste contacta a un desarrollador");
            }
        }


        [Route("api/v1/asignacionproductos")]
        public IHttpActionResult Get()
        {
            AssignProductModels products = new AssignProductModels();
            var assignedProductsList = products.getAllAsignedProducts();
            if (assignedProductsList == null || !assignedProductsList.Any())
            {
                return NotFound();
            }

            var AssignedProductsView = assignedProductsList.Select(everyProduct => new GetAssignedProductsView
            {
                IDProduct = everyProduct.IDProduct,
                IDBatch = everyProduct.IDBatch,
            }).ToList();

            return Ok(AssignedProductsView);
        }

        [Route("api/v1/asignacionproductos/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            AssignProductModels products = new AssignProductModels();
            var assignedProductsList = products.getAllAsignedProducts();
            var AssignedProductsSearch = assignedProductsList.FirstOrDefault(p => p.IDProduct == id);

            if (AssignedProductsSearch == null)
            {
                return NotFound();
            }
            else
            {
                var AssignedProductsView = new GetAssignedProductsView
                {
                    IDProduct = AssignedProductsSearch.IDProduct,
                    IDBatch = AssignedProductsSearch.IDBatch,
                };

                return Ok(AssignedProductsView);
            }
        }
        [Route("api/v1/asignacionproductos/{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            AssignProductModels products = new AssignProductModels();
            var productList = products.getAllAsignedProducts();
            var productAssigned = productList.FirstOrDefault(everyProduct => everyProduct.IDProduct == id);
            if (productAssigned == null)
            {
                return NotFound();
            }
            else
            {
                productAssigned.Delete();
                return Ok(showResult($"Producto con ID {id} eliminado con éxito"));
            }
        }

    }

}