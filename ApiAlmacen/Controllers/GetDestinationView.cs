using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAlmacen.Controllers
{
    public class GetDestinationView
    {
        public int IDDestination { get; set; }
        public string DestinationLink { get; set; }
        public DateTime EstimatedDate { get; set; }
        public bool ActivedDestination { get; set; }
    }
}