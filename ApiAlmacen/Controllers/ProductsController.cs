using ApiAlmacen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ApiAlmacen.Controllers
{
    public class ProductsController : ApiController
    {
        [Route("api/v1/productos")]
        public IHttpActionResult Post([FromBody] ProductosModel producto)
        {
            if (!ModelState.IsValid || producto == null)
            {
                return BadRequest("Error en el ingreso de datos");
            }
            producto.Save();
            return Ok($"Producto {producto.ProductName} guardado con exito");
        }

        [Route("api/v1/productos")]
        public IHttpActionResult Get()
        {
            ProductosModel productos = new ProductosModel();
            var listaproductos = productos.GetAllProducts();
            var productosView = listaproductos.Select(everyProduct => new GetProductsView
            {
                IDProduct = everyProduct.IDProduct,
                ProductName = everyProduct.ProductName,
                ProductWeight = everyProduct.ProductWeight,
                ProductAmount = everyProduct.ProductAmount,
                ProductDescription = everyProduct.ProductDescription
            }
            ).ToList();

            return Ok(productosView);
        }

        [Route("api/v1/productos/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            ProductosModel product = new ProductosModel();
            var productList = product.GetAllProducts();
            var selectedProduct = productList.FirstOrDefault(p => p.IDProduct == id);

            if (selectedProduct == null)
            {
                return NotFound();
            }
            else
            {
                var productoView = new GetProductsView
                {
                    IDProduct = selectedProduct.IDProduct,
                    ProductName = selectedProduct.ProductName,
                    ProductWeight = selectedProduct.ProductWeight,
                    ProductAmount = selectedProduct.ProductAmount,
                    ProductDescription = selectedProduct.ProductDescription
                };

                return Ok(productoView);
            }
        }
        [Route("api/v1/productos/{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            ProductosModel product = new ProductosModel();
            var productList = product.GetAllProducts();
            var selectedProduct = productList.FirstOrDefault(everyProduct => everyProduct.IDProduct == id);
            if (selectedProduct == null)
            {
                return NotFound();
            }
            else
            {
                selectedProduct.DeleteProduct();
                return Ok($"Producto con ID {id} eliminado con éxito");
            }
        }

        [Route("api/v1/productos/{id:int}")]
        public IHttpActionResult Put(int id, [FromBody] ProductosModel product)
        {
            if (!ModelState.IsValid || product == null)
            {
                return BadRequest("Error en el ingreso de datos");
            }
            product.IDProduct = id;
            product.Edit();

            return Ok($"Producto {product.ProductName} editado con éxito");
        }


    }
}