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
        public static List<ProductosModel> DatosProductos = new List<ProductosModel>();
        ProductosModel productos = new ProductosModel();

        [Route("api/productos")]
        public IHttpActionResult Post([FromBody] ProductosModel producto){
           if(!ModelState.IsValid || producto == null)
            {
                return BadRequest("Error en el ingreso de los datos");
            }
            DatosProductos.Add(producto);
            return Ok(producto);
        }

        [Route("api/productos")]
        public IHttpActionResult Get([FromBody] ProductosModel producto)
        {
            var ListaProductos = DatosProductos.ToList();
            return Ok(ListaProductos);
        }

        [Route("api/productos/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            ProductosModel ProductoABuscar = DatosProductos.Find(x => x.IDProducto == id);
            if(ProductoABuscar == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(ProductoABuscar);
            }
        }

        [Route("api/productos/{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            ProductosModel ProductoABuscar = DatosProductos.Find(x => x.IDProducto == id);
            if (ProductoABuscar == null)
            {
                return NotFound();
            }
            else
            {
                DatosProductos.Remove(ProductoABuscar);
                return Ok(ProductoABuscar.NombreProducto + " Ha sido eliminado");
            }
        }

    }
}