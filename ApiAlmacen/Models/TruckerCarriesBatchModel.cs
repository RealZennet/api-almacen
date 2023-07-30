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
            try // Controlar excepcion para evitar duplicados
            {
                this.Command.CommandText = $"INSERT INTO llevan (id_Cam, id_Lote) VALUES (" +
                    $"{this.IDTruck}, " +
                    $"{this.IDBatch} )";
                this.Command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {

            }

        }

        public void DeleteCarries()
        {
            this.Command.CommandText = $"DELETE FROM llevan WHERE id_Cam = {this.IDTruck}";
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
                carrie.IDTruck = Int32.Parse(this.Reader["id_Cam"].ToString());
                carrie.IDBatch = Int32.Parse(this.Reader["id_Lote"].ToString());
                result.Add(carrie);
            }
            this.Reader.Close();
            return result;
        }

    }
}