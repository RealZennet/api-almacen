﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ApiAlmacen.Models
{
    public class DestinationModel : DatabaseConnector
    {
        public int IDDestination { get; set; }
        public string DestinationLink { get; set; }
        public DateTime EstimatedDate { get; set; }
        public bool ActivedDestination { get; set; }

        public List<DestinationModel> GetDestinations()
        {
            List<DestinationModel> destinations = new List<DestinationModel>();

            try
            {
                this.Command.CommandText = "SELECT * FROM destino";
                this.Reader = this.Command.ExecuteReader();

                while (this.Reader.Read())
                {
                    DestinationModel destination = new DestinationModel
                    {
                        IDDestination = Convert.ToInt32(this.Reader["id_des"]),
                        DestinationLink = this.Reader["link_des"].ToString(),
                        EstimatedDate = Convert.ToDateTime(this.Reader["fech_esti"].ToString()),
                        ActivedDestination = Convert.ToBoolean(this.Reader["bajalogica"])
                    };

                    destinations.Add(destination);
                }

                this.Reader.Close();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error: " + ex.Message);
            }

            return destinations;
        }
    }
}