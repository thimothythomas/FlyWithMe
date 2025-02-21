using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using FlyWithMe.Utility;
using ClassLibraryDataBaseConnection;
using System.Configuration;

namespace FlyWithMe.Repository
{
    public class AdminRepository : IAdminRepository
    {
        public async Task<bool> ValidateAdminAsync(string username, string password)
        {
            string inConnection = ConfigurationManager.ConnectionStrings["CsWin"].ConnectionString;

            using (SqlConnection conn = DatabaseConnection.OpenConnection(inConnection))
            {
                if (conn == null)
                    return false;

                using (SqlCommand cmd = new SqlCommand("sp_ValidateAdmin", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    int count = (int)await cmd.ExecuteScalarAsync();
                    return count > 0;
                }
            }
        }
    }
}
