using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAlmacen.Controllers
{
    public class GetLotsView
    {
        public int IDBatches { get; set; }
        public int ProductsAmountOnBatches { get; set; }
        public DateTime DateOfCreation { get; set; }
        //public int IDCamionAsignado { get; set; }
    }
}