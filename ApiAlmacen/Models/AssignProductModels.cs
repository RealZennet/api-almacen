using ApiAlmacen.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAlmacen.Models
{
    public class AssignProductModels:DatabaseConnector
    {
        public int IDLote { get; set; }
        public int IDProduct { get; set; }

        public void Save()
        {
            this.Command.CommandText = $"INSERT INTO pertenece (id_Prod, id_Lote) VALUES ({this.IDProduct}, {this.IDLote})";
            this.Command.ExecuteNonQuery();
        }


        public List<AssignProductModels> getAllAsignedProducts()
        {
            this.Command.CommandText = $"SELECT * FROM pertenece";
            this.Reader = this.Command.ExecuteReader();
            List<AssignProductModels> result = new List<AssignProductModels>();
            while (this.Reader.Read())
            {
                AssignProductModels assignedProduct = new AssignProductModels();
                assignedProduct.IDProduct = Int32.Parse(this.Reader["id_Prod"].ToString());
                assignedProduct.IDLote = Int32.Parse(this.Reader["id_Lote"].ToString());
                result.Add(assignedProduct);
            }
            return result;
        }

        public void Delete()
        {
            this.Command.CommandText = $"DELETE FROM pertenece WHERE id_Lote = {this.IDLote}";
            this.Command.ExecuteNonQuery();
        }


    }
}