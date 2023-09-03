using ApiAlmacen.Controllers.Handlers;
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
        private Dictionary<string, string> showResult(string message)
        {
            Dictionary<string, string> resultJson = new Dictionary<string, string>();
            resultJson.Add("Accion realizada con exito: ", message);
            return resultJson;
        }

        [Route("api/v1/productos")]
        public IHttpActionResult Post([FromBody] ProductModel product)
        {
            if (!ModelState.IsValid || product == null)
            {
                var errorResponse = $"Error en el ingreso de datos con {product.ProductName}.";
                return BadRequest(errorResponse.ToString());
            }
            product.Save();
            return Ok(showResult(product.ProductName));
        }

        [Route("api/v1/productos")]
        public IHttpActionResult Get()
        {
            ProductModel products = new ProductModel();
            var listproducts = products.GetAllProducts();
            var productosView = listproducts.Select(everyProduct => new GetProductsView
            {
                IDProduct = everyProduct.IDProduct,
                ProductName = everyProduct.ProductName,
                ProductWeight = everyProduct.ProductWeight,
                ActivatedProduct = everyProduct.ActivatedProduct,
                ProductDescription = everyProduct.ProductDescription,
                Volume = everyProduct.Volume
            }
            ).ToList();

            return Ok(productosView);
        }

        [Route("api/v1/productos/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            ProductModel product = new ProductModel();
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
                    ActivatedProduct = selectedProduct.ActivatedProduct,
                    ProductDescription = selectedProduct.ProductDescription,
                    Volume = selectedProduct.Volume
                };

                return Ok(productoView);
            }
        }
        [Route("api/v1/productos/{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            ProductModel product = new ProductModel();
            var productList = product.GetAllProducts();
            var selectedProduct = productList.FirstOrDefault(everyProduct => everyProduct.IDProduct == id);
            if (selectedProduct == null)
            {
                return NotFound();
            }
            else
            {
                selectedProduct.DeleteProduct();
                return Ok(showResult(id.ToString()));
            }
        }

        [Route("api/v1/productos/{id:int}")]
        public IHttpActionResult Put(int id, [FromBody] ProductModel product)
        {
            try
            {
                if (!ModelState.IsValid || product == null)
                {
                    var errorResponse = $"Error en el ingreso de datos con {id}.";
                    return BadRequest(errorResponse.ToString());
                }

                product.IDProduct = id;
                product.Edit();

                return Ok(showResult(id.ToString()));
            }
            catch (ProductNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}