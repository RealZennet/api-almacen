using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ApiAlmacen.Models
{
    public class LotesModels
    {
        public int IDLote { get; set; }
        public int CantidadProductos { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IDCamionAsignado { get; set; }
    }
}