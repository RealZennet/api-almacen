using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAlmacen.Controllers
{
    public class GetProductsView 
    {
        public int IDProduct { get; set; }
        public string ProductName { get; set; }
        public int ProductWeight { get; set; }
        public int ProductAmount { get; set; }
        public string ProductDescription { get; set; }
    }
}