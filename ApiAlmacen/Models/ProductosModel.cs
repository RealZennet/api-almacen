using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApiAlmacen.Models
{
    public class ProductosModel
    {
        public int IDProducto { get; set; }
        [Required(ErrorMessage = "El campo 'NombreProducto' es requerido")]
        public string NombreProducto { get; set; }
        [Required(ErrorMessage = "El campo 'DescripcionProducto' es requerido")]
        public string DescripcionProducto { get; set; }
        [Required(ErrorMessage = "El campo 'PesoProducto' es requerido")]
        public int PesoProducto { get; set; }
        [Required(ErrorMessage = "El campo 'CantidadProducto' es requerido")]
        public int CantidadProducto { get; set; }
    }
}