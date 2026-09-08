using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BOL.Entidades;
using System.Data.Common;
using Utilities;
using System.Data;

namespace BOL.Controladoras
{
    internal static class DAO
    {
        internal static EDM Current
        {
            get
            {
                if (Contexto.Items != null)
                {
                    if (Contexto.Items["sqlDAOEntities"] == null)
                    {
                        string IPCliente = Contexto.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (IPCliente == null) IPCliente = Contexto.ServerVariables["REMOTE_ADDR"];
                        Contexto.Items.Add("sqlDAOEntities", CreateEDM(IPCliente));
                    }
                    return (EDM)Contexto.Items["sqlDAOEntities"];
                }
                else
                {
                    return CreateEDM();
                }
            }
        }

        private static EDM CreateEDM(string workstationID = "")
        {
            EDM _bd = new EDM();

            _bd.Database.CommandTimeout = _bd.Database.Connection.ConnectionTimeout;
            _bd.Configuration.ProxyCreationEnabled = false;
            _bd.Configuration.LazyLoadingEnabled = false;
            _bd.Configuration.EnsureTransactionsForFunctionsAndCommands = false;

            return _bd;
        }

    }
    public static class EDMExtensions
    {
        public static void OpenConnection(this EDM edm)
        {
            if (edm.Database.Connection.State.Equals(ConnectionState.Open))
            {
                if (edm.Database.CurrentTransaction == null)
                {
                    edm.Database.Connection.Close();
                    edm.Database.Connection.Open();
                }
            }
            else
            {
                edm.Database.Connection.Open();
            }
        }

        public static void BeginTransaction(this EDM edm)
        {
            if (edm.Database.CurrentTransaction == null)
            {
                edm.Database.BeginTransaction();
            }
        }
        public static bool ExistCurrentTransaction(this EDM edm)
        {
            return (edm.Database.CurrentTransaction != null);
        }
        public static void CommitTransaction(this EDM edm)
        {
            if (edm.Database.CurrentTransaction != null)
            {
                edm.Database.CurrentTransaction.Commit();
            }
        }

        public static void RollbackTransaction(this EDM edm)
        {
            if (edm.Database.CurrentTransaction != null)
            {
                edm.Database.CurrentTransaction.Rollback();
            }
        }

        public static void CloseConnection(this EDM edm)
        {
            if (edm.Database.Connection.State.Equals(ConnectionState.Open))
            {
                if (edm.Database.CurrentTransaction != null)
                {
                    edm.Database.CurrentTransaction.Rollback();
                }
                edm.Database.Connection.Close();
            }
        }

    }
}