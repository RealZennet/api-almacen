using ApiAlmacen.Controllers.Handlers;
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
                $"{this.HouseNumber}," +
                $"'{this.Corner}'," +
                $"'{this.Customer}')";
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
            this.Command.CommandText = $"DELETE FROM paquete WHERE id_paq = {this.IDPackage}";
            this.Command.ExecuteNonQuery();
        }


        public bool CheckIfPackageExists(int id)
        {
            this.Command.CommandText = $"SELECT COUNT(*) FROM paquete WHERE id_paq = {id}";
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

            bool packageExists = CheckIfPackageExists(this.IDPackage);

            if (packageExists == true)
            {
                this.Command.CommandText = $"UPDATE paquete SET " +
                    $"calle = '{this.Street}', " +
                    $"num = {this.HouseNumber}, " +
                    $"esq = '{this.Corner}', " +
                    $"cliente = '{this.Customer}' " +
                    $"WHERE id_paq = {this.IDPackage}";
                this.Command.ExecuteNonQuery();
            }
            else
            {
                throw new ErrorHandlerPackageNotFound(this.IDPackage);
            }
        }

    }
}