using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Reflection;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using Utilities;

namespace DAL
{
    public class Controladora:IDisposable
    {
        private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());
        private SqlConnection sqlConnection;
        private SqlTransaction sqlTransaction;
        private SqlParameter[] sqlParameterCollection;
        private bool disposed;
        public Controladora()
        {
            sqlConnection = new SqlConnection(GetConnection());
        }

        private string GetConnection()
        {
           return Connection.GetConnection();
        }
        public static Controladora Current
        {
            get
            {
                if (Contexto.Items != null)
                {
                    if (Contexto.Items["sqlControladora"] == null)
                    {
                        Contexto.Items.Add("sqlControladora", new Controladora());
                       
                    }
                    return (Controladora)Contexto.Items["sqlControladora"];
                }
                else
                {
                    return new Controladora();
                }
            }
        }
        public int SaveUpdate(string spName,params object[] parameterValues)
        {
            SqlParameter[] commandParameters = GetSpParameterSet(GetConnectionForChangeDatabase(), spName,true);
            AssignParameterValues(commandParameters, parameterValues);
            return SaveUpdate(spName, commandParameters);
        }
        public int SaveUpdate(string spName, SqlParameter[] commandParameters)
        {
            return this.ExecuteNonQuery(spName, commandParameters);
        }
        public int Delete(string spName, params object[] parameterValues)
        {
            SqlParameter[] commandParameters = GetSpParameterSet(GetConnectionForChangeDatabase(), spName,true);
            AssignParameterValues(commandParameters, parameterValues);
            return this.Delete(spName, commandParameters);
        }
        public int Delete(string spName, SqlParameter[] commandParameters)
        {
            return this.ExecuteNonQuery(spName, commandParameters);
        }
        public DataSet GetAll(string spName,bool AutoOCConnection)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, sqlConnection, sqlTransaction, CommandType.StoredProcedure, spName, (SqlParameter[])null);
            return this.ExecuteDataSet(cmd);		
        }
        public DataSet GetListByCriteria(string query, bool AutoOCConnection)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, sqlConnection, sqlTransaction, CommandType.Text, query, null);
            return this.ExecuteDataSet(cmd);
        }
        public DataSet GetListByCriteria(string spName, bool AutoOCConnection, params object[] parameterValues)
        {
            SqlParameter[] commandParameters = GetSpParameterSet(GetConnectionForChangeDatabase(), spName,true);
            AssignParameterValues(commandParameters, parameterValues);
            return this.GetListByCriteria(spName, AutoOCConnection, commandParameters);
        }
        public DataSet GetListByCriteria(string spName,bool AutoOCConnection, SqlParameter[] parameterValues)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, sqlConnection, sqlTransaction, CommandType.StoredProcedure, spName, parameterValues);
            return this.ExecuteDataSet(cmd);
        }
        public List<T> GetAll<T>(string spName)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, sqlConnection, sqlTransaction, CommandType.StoredProcedure, spName, (SqlParameter[])null);
            return this.ExecuteXmlReaderList<T>(cmd);
        }
        public List<T> GetListByCriteria<T>(string query)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, sqlConnection, sqlTransaction, CommandType.Text, query, null);
            return this.ExecuteXmlReaderList<T>(cmd);
        }
        public List<T> GetListByCriteria<T>(string spName, params object[] parameterValues)
        {
            SqlParameter[] commandParameters = GetSpParameterSet(GetConnectionForChangeDatabase(), spName, true);
            AssignParameterValues(commandParameters, parameterValues);
            return this.GetListByCriteria<T>(spName, commandParameters);
        }
        public List<T> GetListByCriteria<T>(string spName, SqlParameter[] parameterValues)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, sqlConnection, sqlTransaction, CommandType.StoredProcedure, spName, parameterValues);
            return this.ExecuteXmlReaderList<T>(cmd);
        }
        public T GetObject<T>(string query)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, sqlConnection, sqlTransaction, CommandType.Text, query, null);
            return this.ExecuteXmlReader<T>(cmd);
        }
        public T GetObject<T>(string spName, params object[] parameterValues)
        {
            SqlParameter[] commandParameters = GetSpParameterSet(GetConnectionForChangeDatabase(), spName, true);
            AssignParameterValues(commandParameters, parameterValues);
            return this.GetObject<T>(spName, commandParameters);
        }
        public T GetObject<T>(string spName, SqlParameter[] parameterValues)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, sqlConnection, sqlTransaction, CommandType.StoredProcedure, spName, parameterValues); ;
            return this.ExecuteXmlReader<T>(cmd);
        }
        public T Get<T>(string query)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, sqlConnection, sqlTransaction, CommandType.Text, query, null);
            return this.ExecuteEscalar<T>(cmd);
        }
        public T Get<T>(string spName, params object[] parameterValues)
        {
            SqlParameter[] commandParameters = GetSpParameterSet(GetConnectionForChangeDatabase(), spName, true);
            AssignParameterValues(commandParameters, parameterValues);
            return this.Get<T>(spName, commandParameters);
        }
        public T Get<T>(string spName, SqlParameter[] parameterValues)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, sqlConnection, sqlTransaction, CommandType.StoredProcedure, spName, parameterValues); ;
            return this.ExecuteEscalar<T>(cmd);
        }
        public void BeginTrasaction()
        {
            sqlTransaction = sqlConnection.BeginTransaction();
        }
        public void BeginTrasaction(string transactionName)
        {
            sqlTransaction = sqlConnection.BeginTransaction(transactionName);
        }
        public void CommitTransaction()
        {
            sqlTransaction.Commit();
        }
        public void RollbackTransaction()
        {
            sqlTransaction.Rollback();
        }
        public void RollbackTransaction(string transactionName)
        {
            sqlTransaction.Rollback(transactionName);
        }
        public void SaveTransaction(string savePointName)
        {
            sqlTransaction.Save(savePointName);
        }
        public void ChangeDatabase(string database)
        {
            sqlConnection.ChangeDatabase(database);
        }
        public void ChangeConnectionString(string connectionString)
        {
            sqlConnection.ConnectionString = connectionString;
        }
        public void OpenConnection()
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
        }
        public void CloseConnection()
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        public SqlParameter[] GetSqlParametersResult()
        {
            return this.sqlParameterCollection;
        }
        public T ReturnValue<T>()
        {
            return (T)this.sqlParameterCollection[0].Value;
        }
        #region Metodos Privados
        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            foreach (SqlParameter p in commandParameters)
            {
                //check for derived output value with no value assigned
                //if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
                //{
                //    p.Value = DBNull.Value;
                //}
                if (p.Value == null) p.Value = DBNull.Value;
                command.Parameters.Add(p);
            }
        }
        private static void AssignParameterValues(SqlParameter[] commandParameters, object[] parameterValues)
        {
            if ((commandParameters == null) || (parameterValues == null))
            {
                return;
            }

            if (commandParameters[0].Direction == ParameterDirection.ReturnValue)
            {
                if (commandParameters.Length - 1 != parameterValues.Length)
                {
                    throw new ArgumentException("La cantidad de valores no coinciden con la cantidad de parametros.");
                }
                commandParameters[0].Value = int.MinValue;
                for (int i = 1, j = commandParameters.Length; i < j; i++)
                {
                    if (parameterValues[i - 1] != null && (!parameterValues[i - 1].GetType().Namespace.Equals("System") || parameterValues[i - 1].GetType().Namespace.Equals("System.Collections.Generic")))
                    {
                        commandParameters[i].Value = Serializer(parameterValues[i - 1]);
                    }
                    else
                    {
                        commandParameters[i].Value = parameterValues[i - 1];
                    }
                }
            }
            else
            {   
                if (commandParameters.Length != parameterValues.Length)
                {
                    throw new ArgumentException("La cantidad de valores no coinciden con la cantidad de parametros.");
                }
                for (int i = 0, j = commandParameters.Length; i < j; i++)
                {
                    if (parameterValues[i - 1] != null && (!parameterValues[i - 1].GetType().Namespace.Equals("System") || parameterValues[i - 1].GetType().Namespace.Equals("System.Collections.Generic")))
                    {
                        commandParameters[i].Value = Serializer(parameterValues[i]);
                    }
                    else
                    {
                        commandParameters[i].Value = parameterValues[i];
                    }
                }
            }
        }
        private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters)
        {
            //if the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //associate the connection with the command
            command.Connection = connection;

            //set the command text (stored procedure name or SQL statement)
            command.CommandText = commandText;

            //if we were provided a transaction, assign it.
            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            //set the command type
            command.CommandType = commandType;

            //attach the command parameters if they are provided
            if (commandParameters != null)
            {
                Controladora.AttachParameters(command, commandParameters);
            }

            return;
        }
        private int ExecuteNonQuery(string spName, SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, sqlConnection, sqlTransaction, CommandType.StoredProcedure, spName, commandParameters);
            int retval = cmd.ExecuteNonQuery();
            if (cmd.Parameters != null)
            {
                this.sqlParameterCollection = new SqlParameter[cmd.Parameters.Count];
                cmd.Parameters.CopyTo(this.sqlParameterCollection, 0);
            }
            cmd.Parameters.Clear();
            if (cmd.Transaction == null)
            {
                CloseConnection();
            }
            return retval;
        }
        private T ExecuteXmlReader<T>(SqlCommand cmd)
        {
            MemoryStream memoryStream = new MemoryStream();
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Encoding = new UTF8Encoding(false);
            xmlWriterSettings.ConformanceLevel = ConformanceLevel.Document;
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.OmitXmlDeclaration = true;
            XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
            XmlSerializer x = new XmlSerializer(typeof(T));
            object obj =x.Deserialize(cmd.ExecuteXmlReader());
            if (cmd.Transaction == null)
            {
                CloseConnection();
            }
            return (T)obj;
        }
        private List<T> ExecuteXmlReaderList<T>(SqlCommand cmd)
        {
            MemoryStream memoryStream = new MemoryStream();
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Encoding = new UTF8Encoding(false);
            xmlWriterSettings.ConformanceLevel = ConformanceLevel.Document;
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.OmitXmlDeclaration = true;
            XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
            XmlSerializer x = new XmlSerializer(typeof(List<T>));
            object obj = null;
            try
            {
                obj = x.Deserialize(cmd.ExecuteXmlReader());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd.Transaction == null)
                {
                    CloseConnection();
                }
            }
            return (List<T>)obj;
        }
        private DataSet ExecuteDataSet(SqlCommand cmd)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (cmd.Parameters != null)
            {
                this.sqlParameterCollection = new SqlParameter[cmd.Parameters.Count];
                cmd.Parameters.CopyTo(this.sqlParameterCollection, 0);
            }
            cmd.Parameters.Clear();
            if (cmd.Transaction == null)
            {
                CloseConnection();
            }
            return ds;
        }
        private T ExecuteEscalar<T>(SqlCommand cmd)
        {
            object obj = cmd.ExecuteScalar();
            if (cmd.Parameters != null)
            {
                this.sqlParameterCollection = new SqlParameter[cmd.Parameters.Count];
                cmd.Parameters.CopyTo(this.sqlParameterCollection, 0);
            }
            cmd.Parameters.Clear();
            if (cmd.Transaction == null)
            {
                CloseConnection();
            }
            return (T)obj;
        }
        private string GetConnectionForChangeDatabase()
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(sqlConnection.ConnectionString);
            sqlConnectionStringBuilder.InitialCatalog = sqlConnection.Database;
            return sqlConnectionStringBuilder.ToString();

        }
        internal static string Serializer(object obj)
        {
            //esaa
            MemoryStream memoryStream = new MemoryStream();
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Encoding = new UTF8Encoding(false);
            xmlWriterSettings.ConformanceLevel = ConformanceLevel.Document;
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.OmitXmlDeclaration = true;
            XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
            XmlSerializer x = new XmlSerializer(obj.GetType());
            x.Serialize(xmlWriter, obj);
            return Encoding.UTF8.GetString(memoryStream.ToArray()).Replace(">true<", ">1<").Replace(">false<", ">0<"); ;
        }
        #endregion Metodos Privados
        #region Metodos controladores de cache
        private static SqlParameter[] DiscoverSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(spName, cn))
            {
                cn.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlCommandBuilder.DeriveParameters(cmd);

                if (!includeReturnValueParameter)
                {
                    cmd.Parameters.RemoveAt(0);
                }

                SqlParameter[] discoveredParameters = new SqlParameter[cmd.Parameters.Count];

                cmd.Parameters.CopyTo(discoveredParameters, 0);
                cn.Close();
                return discoveredParameters;
            }
        }
        private static SqlParameter[] CloneParameters(SqlParameter[] originalParameters)
        {
            SqlParameter[] clonedParameters = new SqlParameter[originalParameters.Length];

            for (int i = 0, j = originalParameters.Length; i < j; i++)
            {
                clonedParameters[i] = (SqlParameter)((ICloneable)originalParameters[i]).Clone();
            }

            return clonedParameters;
        }
        protected static void CacheParameterSet(string connectionString, string commandText, params SqlParameter[] commandParameters)
        {
            string hashKey = connectionString + ":" + commandText;

            paramCache[hashKey] = commandParameters;
        }
        protected static SqlParameter[] GetCachedParameterSet(string connectionString, string commandText)
        {
            string hashKey = connectionString + ":" + commandText;

            SqlParameter[] cachedParameters = (SqlParameter[])paramCache[hashKey];

            if (cachedParameters == null)
            {
                return null;
            }
            else
            {
                return CloneParameters(cachedParameters);
            }
        }
        protected static SqlParameter[] GetSpParameterSet(string connectionString, string spName)
        {
            return GetSpParameterSet(connectionString, spName, false);
        }
        private static SqlParameter[] GetSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
        {
            string hashKey = connectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");

            SqlParameter[] cachedParameters;

            cachedParameters = (SqlParameter[])paramCache[hashKey];

            if (cachedParameters == null)
            {
                cachedParameters = (SqlParameter[])(paramCache[hashKey] = DiscoverSpParameterSet(connectionString, spName, includeReturnValueParameter));
            }

            return CloneParameters(cachedParameters);
        }
        #endregion
        #region Miembros de IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    sqlConnection.Dispose();
                    sqlTransaction.Dispose();
                }
            }
            disposed = true;
        }
        #endregion

        
    }
}
