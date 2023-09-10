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
        public DateTime ShippDate { get; set; }

        public void Save()
        {
            try 
            {
                this.Command.CommandText = $"INSERT INTO llevan (id_camion, id_Lote, fech_Sal) VALUES (" +
                    $"{this.IDTruck}, " +
                    $"{this.IDBatch}, " +
                    $"'{this.ShippDate.ToString("yyyy-MM-dd")}')";
                this.Command.ExecuteNonQuery();
            }
            catch(Exception)
            {

            }

        }

        public void DeleteCarries()
        {
            this.Command.CommandText = $"DELETE FROM llevan WHERE id_camion = {this.IDTruck}";
            this.Command.ExecuteNonQuery();
        }

        public List<TruckerCarriesBatchModel> GetAllCarries()
        {
            this.Command.CommandText = "SELECT * FROM llevan";
            this.Reader = this.Command.ExecuteReader();

            List<TruckerCarriesBatchModel> result = new List<TruckerCarriesBatchModel>();
            while (this.Reader.Read())
            {
                TruckerCarriesBatchModel carrie = new TruckerCarriesBatchModel();
                carrie.IDTruck = Int32.Parse(this.Reader["id_camion"].ToString());
                carrie.IDBatch = Int32.Parse(this.Reader["id_Lote"].ToString());
                carrie.ShippDate = DateTime.Parse(this.Reader["fech_Sal"].ToString());
                result.Add(carrie);
            }
            this.Reader.Close();
            return result;
        }

    }
}