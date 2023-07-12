using ApiAlmacen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ApiAlmacen.Controllers
{
    public class ProductosController : ApiController
    {
        [Route("api/productos")]
        public IHttpActionResult Post([FromBody] ProductosModel producto)
        {
            if (!ModelState.IsValid || producto == null)
            {
                return BadRequest("Error en el ingreso de datos");
            }
            producto.Save();
            return Ok($"Producto {producto.NombreProducto} guardado con exito");
        }

        [Route("api/productos")]
        public IHttpActionResult Get()
        {
            ProductosModel productos = new ProductosModel();
            var listaproductos = productos.GetAllProducts();
            var productosView = listaproductos.Select(everyProduct => new GetProductosView
            {
                IDProducto = everyProduct.IDProducto,
                NombreProducto = everyProduct.NombreProducto,
                PesoProducto = everyProduct.PesoProducto,
                CantidadProducto = everyProduct.CantidadProducto,
                DescripcionProducto = everyProduct.DescripcionProducto
            }
            ).ToList();

            return Ok(productosView);
        }

        [Route("api/productos/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            ProductosModel productos = new ProductosModel();
            var listaproductos = productos.GetAllProducts();
            var producto = listaproductos.FirstOrDefault(p => p.IDProducto == id);

            if (producto == null)
            {
                return NotFound();
            }
            else
            {
                var productoView = new GetProductosView
                {
                    IDProducto = producto.IDProducto,
                    NombreProducto = producto.NombreProducto,
                    PesoProducto = producto.PesoProducto,
                    CantidadProducto = producto.CantidadProducto,
                    DescripcionProducto = producto.DescripcionProducto
                };

                return Ok(productoView);
            }
        }
        [Route("api/productos/{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            ProductosModel productos = new ProductosModel();
            var listaproductos = productos.GetAllProducts();
            var producto = listaproductos.FirstOrDefault(everyProduct => everyProduct.IDProducto == id);
            if (producto == null)
            {
                return NotFound();
            }
            else
            {
                producto.DeleteProduct();
                return Ok($"Producto con ID {id} eliminado con éxito");
            }
        }

        [Route("api/productos/{id:int}")]
        public IHttpActionResult Put(int id, [FromBody] ProductosModel producto)
        {
            if (!ModelState.IsValid || producto == null)
            {
                return BadRequest("Error en el ingreso de datos");
            }
            producto.IDProducto = id;
            producto.Edit();

            return Ok($"Producto {producto.NombreProducto} editado con éxito");
        }


    }
}