using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAlmacen.Models
{
    public class PackageModel : DatabaseConnector
    {
        public int IDPackage { get; set; }
        public string Street { get; set; }
        public int HouseNumber {get; set;}
        public string Corner { get; set; }
        public string Customer { get; set; }

        public void Save()
        {
            this.Command.CommandText = $"INSERT INTO paquete (calle, num, esq, cliente)" +
                $" VALUES " +
                $"('{this.Street}', " +
                $"{this.HouseNumber})," +
                $"'{this.Corner}'," +
                $"'{this.Customer}'";
            this.Command.ExecuteNonQuery();
        }


        public List<PackageModel> getAllPackages()
        {
            this.Command.CommandText = $"SELECT * FROM paquete";
            this.Reader = this.Command.ExecuteReader();
            List<PackageModel> result = new List<PackageModel>();
            while (this.Reader.Read())
            {
                PackageModel package = new PackageModel();
                package.IDPackage = Int32.Parse(this.Reader["id_paq"].ToString());
                package.Street = this.Reader["calle"].ToString();
                package.HouseNumber = Int32.Parse(this.Reader["num"].ToString());
                package.Corner = this.Reader["esq"].ToString();
                package.Customer = this.Reader["cliente"].ToString();
                result.Add(package);
            }
            return result;
        }

        public void Delete()
        {
            this.Command.CommandText = $"DELETE FROM paquete WHERE id_Lote = {this.IDPackage}";
            this.Command.ExecuteNonQuery();
        }

    }
}