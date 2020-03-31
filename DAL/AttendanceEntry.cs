using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using System.Data;
using System.Data.SqlClient;

namespace IASystem.DAL
{
    public class AttendanceEntry
    {
        public static string s = System.Configuration.ConfigurationManager.ConnectionStrings["InvogueEntities"].ConnectionString;
        public static EntityConnectionStringBuilder e = new EntityConnectionStringBuilder(s);
        public static string ProviderConnectionString = e.ProviderConnectionString;
        public static SqlConnection cnConnection = new SqlConnection(ProviderConnectionString);

        public String ExecuteSP(SqlCommand sqlCommand)
        {
            var value = (string)null;
            if (cnConnection.State == ConnectionState.Closed)
            {
                cnConnection.Open();
            }
            try
            {
                sqlCommand.Connection = cnConnection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.ExecuteNonQuery();
                value = sqlCommand.Parameters["@rMessage"].Value.ToString();
            }
            catch (Exception ex)
            {
                value = ex.Message;
            }
            finally
            {
                cnConnection.Close();
            }
            return value;
        }
    }
}
