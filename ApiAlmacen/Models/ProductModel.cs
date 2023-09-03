using ApiAlmacen.Controllers;
using ApiAlmacen.Controllers.Handlers;
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
        [StringLength(30, ErrorMessage = "El nombre del producto debe tener un máximo de 30 caracteres.")]
        public string ProductName { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "La descripcion del producto debe tener un máximo de 100 caracteres.")]
        public string ProductDescription { get; set; }
        [Required]
        public int ProductWeight { get; set; }
        [Required]
        public bool ActivatedProduct { get; set; }

        public void Save()
        {
            try { 
            this.Command.CommandText = $"INSERT INTO producto(nom_Prod, peso_Prod, bajalogica, desc_Prod) VALUES(" +
               $"'{this.ProductName}', " +
               $"{this.ProductWeight}, " +
               $"{this.ActivatedProduct}, " +
               $"'{this.ProductDescription}')";
            this.Command.ExecuteNonQuery();
            }
            catch(Exception){}
        }


        public bool CheckIfProductExists(int id)
        {
            this.Command.CommandText = $"SELECT COUNT(*) FROM producto WHERE id_Prod = {id}";
            object result = this.Command.ExecuteScalar();

            if (result != null && result != DBNull.Value)
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
            
            bool productExists = CheckIfProductExists(this.IDProduct);

            if (productExists == true)
            {
                this.Command.CommandText = $"UPDATE producto SET " +
                    $"nom_Prod = '{this.ProductName}', " +
                    $"peso_Prod = {this.ProductWeight}, " +
                    $"cant_Prod = {this.ActivatedProduct}, " +
                    $"desc_Prod = '{this.ProductDescription}' " +
                    $"WHERE id_Prod = {this.IDProduct}";
                this.Command.ExecuteNonQuery();
            }
            else
            {
                throw new ProductNotFoundException(this.IDProduct);
            }
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
                product.ActivatedProduct = Convert.ToBoolean(this.Reader["bajalogica"].ToString());
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