using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAlmacen.Controllers
{
    public class GetProductsView 
    {
        public int IDProducto { get; set; }
        public string NombreProducto { get; set; }
        public int PesoProducto { get; set; }
        public int CantidadProducto { get; set; }
        public string DescripcionProducto { get; set; }
    }
}