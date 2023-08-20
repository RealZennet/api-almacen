using ApiAlmacen.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApiAlmacen.Models
{
    public class ProductModel : DatabaseConnector
    {

        public int IDProduct { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        public int ProductWeight { get; set; }
        [Required]
        public int ProductAmount { get; set; }

        public void Save()
        {
            this.Command.CommandText = $"INSERT INTO producto(nom_Prod, peso_Prod, cant_Prod, desc_Prod) VALUES(" +
               $"'{this.ProductName}', " +
               $"{this.ProductWeight}, " +
               $"{this.ProductAmount}, " +
               $"'{this.ProductDescription}')";
            this.Command.ExecuteNonQuery(); //Controlar excepciones, como nombres muy largos o numeros, etc.
        }

        public void Edit()
        {
            this.Command.CommandText = $"UPDATE producto SET " +
                $"nom_Prod = '{this.ProductName}', " +
                $"peso_Prod = {this.ProductWeight}, " +
                $"cant_Prod = {this.ProductAmount}, " +
                $"desc_Prod = '{this.ProductDescription}' " +
                $"WHERE id_Prod = {this.IDProduct}";
            this.Command.ExecuteNonQuery();
        }


        public List<ProductModel> GetAllProducts()
        {
            this.Command.CommandText = "SELECT * FROM producto";
            this.Reader = this.Command.ExecuteReader();

            List<ProductModel> result = new List<ProductModel>();
            while (this.Reader.Read())
            {
                ProductModel product = new ProductModel();
                product.IDProduct = Int32.Parse(this.Reader["id_Prod"].ToString());
                product.ProductName = this.Reader["nom_Prod"].ToString();
                product.ProductWeight = Int32.Parse(this.Reader["peso_Prod"].ToString());
                product.ProductAmount = Int32.Parse(this.Reader["cant_Prod"].ToString());
                product.ProductDescription = this.Reader["desc_Prod"].ToString();
                result.Add(product);
            }
            this.Reader.Close();
            return result;
        }
        public void DeleteProduct()
        {
            this.Command.CommandText = $"DELETE FROM producto WHERE id_Prod = {this.IDProduct}";
            this.Command.ExecuteNonQuery();
        }
    }
}