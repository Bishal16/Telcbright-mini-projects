using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConnectMySql
{
    class Program
    {
        static void Main()
        {
            // Connection string
            string connectionString = "Server=YourServerName;Database=YourDatabaseName;User Id=YourUsername;Password=YourPassword;";

            // Create a new SqlConnection object
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();
                    Console.WriteLine("Connected to the database.");

                    // Perform database operations here...

                    // Close the connection
                    connection.Close();
                    Console.WriteLine("Connection closed.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            Console.ReadLine();
        }
    }
}
