using ApiAlmacen.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ApiAlmacen.Models
{
    public class LotesModels:DatabaseConnector
    {
        public int IDLote { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int CantidadProductosEnLote { get; set; } //select by countdown a pertenece.

        public void Save()
        {
            this.Command.CommandText = $"INSERT INTO lote (fech_Crea, cant_Prod_Lote) VALUES " +
                $"('{this.FechaCreacion.ToString("yyyy-MM-dd HH:mm:ss")}', {this.CantidadProductosEnLote})";
            this.Command.ExecuteNonQuery();

            // se obtiene el ID del lote recién insertado
            this.Command.CommandText = "SELECT last_insert_id()";
            this.IDLote = Convert.ToInt32(this.Command.ExecuteScalar());

            
        }


        public List<LotesModels> GetAllLots()
        {
            this.Command.CommandText = $"SELECT * FROM lote"; 
            this.Reader = this.Command.ExecuteReader();

            List<LotesModels> result = new List<LotesModels>();
            while (this.Reader.Read())
            {
                LotesModels lote = new LotesModels();
                lote.IDLote = Int32.Parse(this.Reader["id_Lote"].ToString());
                lote.FechaCreacion = DateTime.Parse(this.Reader["fech_Crea"].ToString());
                lote.CantidadProductosEnLote = Int32.Parse(this.Reader["cant_Prod_Lote"].ToString());
                result.Add(lote);
            }
            return result;
        }
        public void DeleteLots()
        {
            this.Command.CommandText = $"DELETE FROM lote WHERE id_Lote = {this.IDLote}";
            this.Command.ExecuteNonQuery();
        }

        public void Edit()
        {
            this.Command.CommandText = $"UPDATE lote SET " +
                $"cant_Prod_Lote = '{this.CantidadProductosEnLote}' " +
                $"WHERE id_Lote = {this.IDLote}";
            this.Command.ExecuteNonQuery();
        }


    }
}