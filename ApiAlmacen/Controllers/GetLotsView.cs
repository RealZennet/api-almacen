using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAlmacen.Controllers
{
    public class GetLotsView
    {
        public int IDLote { get; set; }
        public int CantidadProductosEnLote { get; set; }
        public DateTime FechaCreacion { get; set; }
        //public int IDCamionAsignado { get; set; }
    }
}