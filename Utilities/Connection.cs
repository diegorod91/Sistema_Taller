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
                //case "D": conexionDB = ConnectionTools.GetConnectionString("RAE", "10.62.1.64\\desarrollo", "sa", "sql", true); break;
                case "D": conexionDB = ConnectionTools.GetConnectionString("Taller", "DESKTOP-KMTVQUV"); break;
                //case "T": conexionDB = ConnectionTools.GetConnectionString("RAE", "10.62.1.64\\testeo", "sa", "SQL01*", true); break;
                case "T": conexionDB = ConnectionTools.GetConnectionString("Taller", "DESKTOP-KMTVQUV"); break;
                //case "P": conexionDB = ConnectionTools.GetConnectionString("RAE", "urano", "sa", "sql", true); break;
                case "P": conexionDB = ConnectionTools.GetConnectionString("Taller", "DESKTOP-KMTVQUV"); break;

            }
            return conexionDB;

        }
    }
}


public static class ConnectionTools
{
    public static string GetConnectionString(
    string initialCatalog = "",
    string dataSource = "")
    {
        string connString = new System.Data.SqlClient.SqlConnectionStringBuilder
        {
            InitialCatalog = initialCatalog,
            DataSource = dataSource,
            IntegratedSecurity = true,
            ConnectTimeout = 60,
            ApplicationName = System.Configuration.ConfigurationManager.AppSettings["ApplicationName"]
        }.ConnectionString;
        return String.Format(connString);
    }
}
