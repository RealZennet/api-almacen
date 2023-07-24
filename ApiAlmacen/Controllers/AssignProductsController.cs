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
        [Route("api/v1/asignacionproductos")]
        public IHttpActionResult Post([FromBody] AssignProductModels AssignedProduct)
        {
            if (!ModelState.IsValid || AssignedProduct == null)
            {
                return BadRequest("Error en el ingreso de datos");
            }

            try
            {
                AssignedProduct.Save();
                return Ok($"Producto {AssignedProduct.IDProduct} fue asignado correctamente al lote {AssignedProduct.IDLote}");
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

            var AssignedProductsView = assignedProductsList.Select(everyProduct => new GetAssignedProductsView
            {
                IDProduct = everyProduct.IDProduct,
                IDLote = everyProduct.IDLote,
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
                    IDLote = AssignedProductsSearch.IDLote,
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
                return Ok($"Producto con ID {id} eliminado con éxito");
            }
        }

    }

}