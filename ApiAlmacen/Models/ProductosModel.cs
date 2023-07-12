using ApiAlmacen.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApiAlmacen.Models
{
    public class ProductosModel : DatabaseConnector
    {
        public int IDProducto { get; set; }
        public string NombreProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public int PesoProducto { get; set; }
        public int CantidadProducto { get; set; }

        public void Save()
        {
            this.Command.CommandText = $"INSERT INTO producto(nom_Prod, peso_Prod, cant_Prod, desc_Prod) VALUES(" +
               $"'{this.NombreProducto}', " +
               $"{this.PesoProducto}, " +
               $"{this.CantidadProducto}, " +
               $"'{this.DescripcionProducto}')";
            this.Command.ExecuteNonQuery(); //Controlar excepciones, como nombres muy largos o numeros, etc.
        }

        public void Edit()
        {
            this.Command.CommandText = $"UPDATE producto SET " +
                $"nom_Prod = '{this.NombreProducto}', " +
                $"peso_Prod = {this.PesoProducto}, " +
                $"cant_Prod = {this.CantidadProducto}, " +
                $"desc_Prod = '{this.DescripcionProducto}' " +
                $"WHERE id_Prod = {this.IDProducto}";
            this.Command.ExecuteNonQuery();
        }


        public List<ProductosModel> GetAllProducts()
        {
            this.Command.CommandText = "SELECT * FROM producto";
            this.Reader = this.Command.ExecuteReader();

            List<ProductosModel> result = new List<ProductosModel>();
            while (this.Reader.Read())
            {
                ProductosModel product = new ProductosModel();
                product.IDProducto = Int32.Parse(this.Reader["id_Prod"].ToString());
                product.NombreProducto = this.Reader["nom_Prod"].ToString();
                product.PesoProducto = Int32.Parse(this.Reader["peso_Prod"].ToString());
                product.CantidadProducto = Int32.Parse(this.Reader["cant_Prod"].ToString());
                product.DescripcionProducto = this.Reader["desc_Prod"].ToString();
                result.Add(product);
            }
            this.Reader.Close();
            return result;
        }
        public void DeleteProduct()
        {
            this.Command.CommandText = $"DELETE FROM producto WHERE id_Prod = {this.IDProducto}";
            this.Command.ExecuteNonQuery();
        }
    }
}