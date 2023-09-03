using ApiAlmacen.Controllers;
using ApiAlmacen.Controllers.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ApiAlmacen.Models
{
    public class BatchModels:DatabaseConnector
    {
        public int IDBatch { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime ShippingDate { get; set; }
        public int IDShipp { get; set; }

        public void Save()
        {
            this.Command.CommandText = $"INSERT INTO lote (fech_Crea, fech_Entre, id_Des) VALUES " +
                $"('{this.DateOfCreation.ToString("yyyy-MM-dd")}', " +
                $"'{this.ShippingDate.ToString("yyyy-MM-dd")}'," +
                $"{this.IDShipp})"; 
            this.Command.ExecuteNonQuery(); //crashea cuando la foreign key no se encuentra

            this.Command.CommandText = "SELECT last_insert_id()";
            this.IDBatch = Convert.ToInt32(this.Command.ExecuteScalar());
        }

        public List<BatchModels> GetAllLots()
        {
            this.Command.CommandText = $"SELECT * FROM lote"; 
            this.Reader = this.Command.ExecuteReader();

            List<BatchModels> result = new List<BatchModels>();
            while (this.Reader.Read())
            {
                BatchModels lote = new BatchModels();
                lote.IDBatch = Int32.Parse(this.Reader["id_Lote"].ToString());
                lote.DateOfCreation = DateTime.Parse(this.Reader["fech_Crea"].ToString());
                lote.ShippingDate = DateTime.Parse(this.Reader["fech_Entre"].ToString());
                lote.IDShipp = Int32.Parse(this.Reader["id_Des"].ToString());
                result.Add(lote);
            }
            this.Reader.Close();
            return result;
        }
        public void DeleteLots()
        {
            this.Command.CommandText = $"DELETE FROM lote WHERE id_Lote = {this.IDBatch}"; 
            this.Command.ExecuteNonQuery();
        }

        public bool CheckIfBatchExists(int id)
        {
            this.Command.CommandText = $"SELECT COUNT(*) FROM lote WHERE id_Lote = {id}";
            object result = this.Command.ExecuteScalar();

            if(result != null && result != DBNull.Value)
            {
                if (int.TryParse(result.ToString(), out int rowCount))
                {
                    return rowCount > 0;
                }
            }
            return false;
        }


    }
}