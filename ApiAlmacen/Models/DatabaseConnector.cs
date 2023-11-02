using System;
using MySqlConnector;

namespace ApiAlmacen.Models
{
    public abstract class DatabaseConnector : IDisposable
    {
        private string dbip = "localhost";
        private string dbUser = "root";
        private string dbPassword = "zackquack";
        private string dbDatabaseName = "quickCarryDB";

        protected MySqlConnection Connection;
        protected MySqlCommand Command;
        protected MySqlDataReader Reader;

        public DatabaseConnector()
        {
            InitializeConnection();
        }

        private void InitializeConnection()
        {
            try
            {
                Connection = new MySqlConnection(
                    $"server={dbip};" +
                    $"user={dbUser};" +
                    $"password={dbPassword};" +
                    $"database={dbDatabaseName};");

                Connection.Open();
                Command = new MySqlCommand();
                Command.Connection = Connection;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al abrir la conexión: " + ex.Message);
                // Puedes manejar el error aquí, lanzar una excepción o realizar alguna acción de recuperación según tus necesidades.
            }
        }

        public void Dispose()
        {
            // Cierra la conexión cuando se destruye la instancia
            Connection?.Close();
            Connection?.Dispose();
        }
    }
}
