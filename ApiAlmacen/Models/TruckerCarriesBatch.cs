using ApiAlmacen.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAlmacen.Models
{
    public class TruckerCarriesBatch  : DatabaseConnector
    {
        public int IDTruck { get; set; }
        public int IDBatch { get; set; }

        public void Save()
        {
            this.Command.CommandText = $"INSERT INTO llevan( ) = ";
            this.Command.ExecuteNonQuery();
        }

    }
}