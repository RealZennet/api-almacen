using ApiAlmacen.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApiAlmacen.Models
{
    public class ProductosModel : DatabaseConnector
    {
        public int IDProducto { get; set; }
        public string NombreProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public int PesoProducto { get; set; }
        public int CantidadProducto { get; set; }

    }
}