using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace GetIncrementalValue
{
    class TupleIncrementManager
    {
        static void Main(string[] args)
        {
            string tuple = "Modon22";
            int defaultValue = 1000;
            GetIncrementalValue (tuple, defaultValue);
        }

        static void GetIncrementalValue(string tuple, int defaultValue)
        {
            string connectionString = "server=localhost;user=root;password=;database=testdb;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            // search using given tuple
            string searchSQL = $@"  select tuple, lastValue 
                                    from testTable 
                                    where tuple = '{tuple}'";
            MySqlCommand searchCmd = new MySqlCommand(searchSQL, connection);
            MySqlDataReader reader = searchCmd.ExecuteReader();
            reader.Read();
            

            // if tuple already exist then increase lastValue
            if (reader.HasRows) {
            
                int lastValue = reader.GetInt32("lastValue");
                string updateSQL = $@"  update testTable 
                                        set lastValue = {lastValue + 1}
                                        where tuple = '{tuple}'; ";
                MySqlCommand updateCmd = new MySqlCommand(updateSQL, connection);
                reader.Close();
                updateCmd.ExecuteNonQuery();
            }
            // if tuple does not exist then insert new tuple with lastValue = 1
            else{
                reader.Close();
                string insertSQL = $@"  insert into testTable 
                                        values ( '{tuple}', {defaultValue}); ";
                MySqlCommand updateCmd = new MySqlCommand(insertSQL, connection);
                reader.Close();
                updateCmd.ExecuteNonQuery();
            }
        }            
    }
}
