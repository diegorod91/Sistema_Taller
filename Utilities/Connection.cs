using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Utilities
{
    public sealed class Connection
    {
        private Connection() { }

        public static string GetConnection(string workstationID = "")
        {
            string conexionDB = "";
            switch (System.Configuration.ConfigurationManager.AppSettings["SystemState"].ToString())
            {
                case "D": conexionDB = ConnectionTools.GetConnectionString("solucionbase", "10.62.1.64", "sa", "sql", true); break;
                case "T": conexionDB = ConnectionTools.GetConnectionString("solucionbase", "10.62.1.64", "sa", "sql", true); break;
                case "P": conexionDB = ConnectionTools.GetConnectionString("solucionbase", "10.62.1.64", "sa", "sql", true); break;
            }
            return conexionDB;
        }
    }

    public static class ConnectionTools
    {
        public static string GetConnectionString(
            string initialCatalog = "",
            string dataSource = "",
            string userId = "",
            string password = "",
            bool persistSecurity = true, string workstationID = "")
        {

            string connString = new System.Data.SqlClient.SqlConnectionStringBuilder
            {
                InitialCatalog = initialCatalog,
                DataSource = dataSource,
                PersistSecurityInfo = persistSecurity,
                UserID = userId,
                Password = password,
                WorkstationID = workstationID,
                ConnectTimeout = 60,
                ApplicationName = System.Configuration.ConfigurationManager.AppSettings["ApplicationName"]
            }.ConnectionString;
            return String.Format(connString);
        }
    }
}
