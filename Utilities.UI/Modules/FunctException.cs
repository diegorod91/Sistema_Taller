using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data.SqlClient;

namespace Utilities.UI
{
    public class FunctException : Exception
    {
        public static string codigoError;
        public static string nombreArchivo;
        public static string msg;

        public FunctException(string messageID)
            : base(messageID)
        {
        }
        public static void ProcessException(ref Exception ex)
        {
            if (ex is SqlException) CheckSQLException(ref ex);
        }
        private static void CheckSQLException(ref Exception ex)
        {
            CheckSQLError(ref ex, ((SqlException)ex).Number);
        }
        private static void CheckSQLError(ref Exception ex, int nativeErr)
        {
            switch (nativeErr)
            {
                case (int)SQLErrors.UQ:
                case (int)SQLErrors.UQQ: ex = new FunctException(Messages.SQL_UQViolation.ToDescriptionString()); break;
                case (int)SQLErrors.FK: ex = new FunctException(Messages.SQL_FKViolation.ToDescriptionString()); break;
                default: break;
            }
        }
    }
    public enum SQLErrors { UQ = 52627, UQQ = 52601, FK = 50547 }
    public enum Messages
    {
        [Description("Registro duplicado")]
        SQL_UQViolation,
        [Description("Registro no se puede eliminar (Posee dependencias)")]
        SQL_FKViolation
    }
    public static class MessagesExtensions
    {
        public static string ToDescriptionString(this Messages val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
