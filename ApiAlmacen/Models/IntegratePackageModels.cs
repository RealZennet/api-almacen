using ApiAlmacen.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAlmacen.Models
{
    public class IntegratePackageModels:DatabaseConnector
    {
        public int IDPackage { get; set; }
        public int IDBatch { get; set; }

        public void Save()
        {
            this.Command.CommandText = $"INSERT INTO integra (id_paq, id_lote) VALUES ({this.IDPackage}, {this.IDBatch})";
            this.Command.ExecuteNonQuery();
        }


        public List<IntegratePackageModels> getAllAsignedProducts()
        {
            this.Command.CommandText = $"SELECT * FROM integra";
            this.Reader = this.Command.ExecuteReader();
            List<IntegratePackageModels> result = new List<IntegratePackageModels>();
            while (this.Reader.Read())
            {
                IntegratePackageModels assignedProduct = new IntegratePackageModels();
                assignedProduct.IDPackage = Int32.Parse(this.Reader["id_paq"].ToString());
                assignedProduct.IDBatch = Int32.Parse(this.Reader["id_lote"].ToString());
                result.Add(assignedProduct);
            }
            return result;
        }

        public void Delete()
        {
            this.Command.CommandText = $"DELETE FROM integra WHERE id_paq = {this.IDBatch}";
            this.Command.ExecuteNonQuery();
        }

        //getoneassigned

    }
}