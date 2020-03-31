using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace IASystemWeb.Models.UserLog
{
    public class UserLog
    {
        public DateTime ADate { get; set; }
        public int MachineNO { get; set; }
        public int EmpNumber { get; set; }
        public int VerifyMethod { get; set; }
        public string UniqueID { get; set; }
        public DateTime PunchTime { get; set; }       

    }
    public class LogCollection : List<UserLog>, IEnumerable<SqlDataRecord>
    {
        IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
        {
            var sqlRow = new SqlDataRecord(
                  new SqlMetaData("ADate", SqlDbType.DateTime),
                  new SqlMetaData("MachineNO", SqlDbType.Int),
                  new SqlMetaData("EmpNumber", SqlDbType.Int),
                  new SqlMetaData("VerifyMethod", SqlDbType.Int),
                  new SqlMetaData("UniqueID", SqlDbType.Text),
                  new SqlMetaData("PunchTime", SqlDbType.DateTime)
                  );

            foreach (UserLog log in this)
            {
                sqlRow.SetDateTime(0, log.ADate);
                sqlRow.SetInt32(1, log.MachineNO);
                sqlRow.SetInt32(2, log.EmpNumber);
                sqlRow.SetInt32(3, log.VerifyMethod);
                sqlRow.SetSqlString(4, log.UniqueID);
                sqlRow.SetDateTime(5, log.PunchTime);
                yield return sqlRow;
            }
        }
    } 
}