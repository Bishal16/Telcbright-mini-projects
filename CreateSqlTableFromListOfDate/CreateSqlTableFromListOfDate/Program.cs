using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;


namespace CreateSqlTableFromListOfDate
{
    class Program
    {  
        static void Main(string[] args)
        {
            string[] dates = {"20230101", "20230102", "20230103", "20230104", "20230105", "20230106", "20230107", "20230108"};
            
            createTable(dates);
            
        }
        static void createTable(string[] dates)
        {
            string connectionString = "server=localhost;user=root;password=;database=testdb;";

            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
                foreach(string date in dates)
                {
                    string sql = $"create table unique_event_{date} (col1 varchar(50), col2 varchar(50), col3 varchar(50));"; // Replace with your actual table name
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                
            }
        }

        
    }
}
