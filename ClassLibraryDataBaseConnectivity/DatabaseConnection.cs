using Microsoft.Data.SqlClient;

namespace ClassLibraryDataBaseConnection
{
    public class DatabaseConnection
    {
        public static SqlConnection OpenConnection(string _connString)
        {
            // Declare a variable to hold the SQL connection
            SqlConnection connection = null;
            try
            {

                if (!string.IsNullOrEmpty(_connString))
                {
                    connection = new SqlConnection(_connString);
                    connection.Open();
                }
                return connection;
            }


            catch (SqlException es)
            {
                // Handle SQL exceptions
                Console.WriteLine("OOPs, something went wrong --> Sql Exception Calling");
                Console.WriteLine(es.Message);
                return null;
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                // Print the exception message to the console
                Console.WriteLine("OOPs, something went wrong --> Exception Calling");
                Console.WriteLine(ex.Message);

                // Return null if any other exception occurs
                return null;
            }
        }
    }

}

