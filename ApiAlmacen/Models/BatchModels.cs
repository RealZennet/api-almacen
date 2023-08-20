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
        public int ProductAmountOnBatch { get; set; } //select by countdown a pertenece.

        public void Save()
        {
            this.Command.CommandText = $"INSERT INTO lote (fech_Crea, cant_Prod_Lote) VALUES " +
                $"('{this.DateOfCreation.ToString("yyyy-MM-dd HH:mm:ss")}', {this.ProductAmountOnBatch})";
            this.Command.ExecuteNonQuery();

            // se obtiene el ID del lote recién insertado
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
                lote.ProductAmountOnBatch = Int32.Parse(this.Reader["cant_Prod_Lote"].ToString());
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

        public void Edit()
        {
            bool productExists = CheckIfBatchExists(this.IDBatch);

            if(productExists == true) { 
            this.Command.CommandText = $"UPDATE lote SET " +
                $"cant_Prod_Lote = '{this.ProductAmountOnBatch}' " +
                $"WHERE id_Lote = {this.IDBatch}";
            this.Command.ExecuteNonQuery();
            }
            else
            {
                throw new BatchNotFoundException(this.IDBatch);
            }
        }


    }
}