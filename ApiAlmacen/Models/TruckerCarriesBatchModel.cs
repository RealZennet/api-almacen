using ApiAlmacen.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAlmacen.Models
{
    public class TruckerCarriesBatchModel  : DatabaseConnector
    {
        public int IDTruck { get; set; }
        public int IDBatch { get; set; }

        public void Save()
        {
            this.Command.CommandText = $"INSERT INTO llevan(id_Cam, id_Lote) = " +
                $"{this.IDTruck}, " +
                $"{this.IDBatch} ";
            this.Command.ExecuteNonQuery();
        }

        public void DeleteCarrie()
        {
            this.Command.CommandText = $"DELETE FROM llevan WHERE id_Cam = {this.IDTruck}";
        }

    }
}