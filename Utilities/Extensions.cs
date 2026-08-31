using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Utilities
{
    public static class Extensions
    {
        #region Stream
        public static byte[] ReadFully(this Stream input)
        {
            byte[] buffer = new byte[input.Length];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        #endregion Stream

        #region String
        public static int WordCount(this String str)
        {
            return str.Split(new char[] { ' ', '.', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }
        public static string SuperTrim(this String str)
        {
            return Regex.Replace(str.Trim(), @"\s+", " ");
        }
        public static bool ToBool(this String value)
        {
            return bool.Parse(value);
        }
        public static bool ToBool(this String value, bool defaultValue)
        {
            bool d;
            if (!bool.TryParse(value, out d)) return defaultValue;
            return d;
        }
        public static byte ToByte(this String value)
        {
            return byte.Parse(value);
        }
        public static byte ToByte(this String value, byte defaultValue)
        {
            byte d;
            if (!byte.TryParse(value, out d)) return defaultValue;
            return d;
        }
        public static short ToShort(this String value)
        {
            return short.Parse(value);
        }
        public static short ToShort(this String value, short defaultValue)
        {
            short d;
            if (!short.TryParse(value, out d)) return defaultValue;
            return d;
        }
        public static int ToInt(this String value)
        {
            return int.Parse(value);
        }
        public static int ToInt(this String value,int defaultValue)
        {
            int d;
            if (!int.TryParse(value, out d)) return defaultValue;
            return d;
        }
        public static long ToLong(this String value)
        {
            return long.Parse(value);
        }
        public static long ToLong(this String value, long defaultValue)
        {
            long d;
            if (!long.TryParse(value, out d)) return defaultValue;
            return d;
        }
        public static decimal ToDecimal(this String value)
        {
            return decimal.Parse(value.Replace(',', '.'), NumberStyles.Float, new CultureInfo("en-US"));
        }
        public static decimal ToDecimal(this String value, decimal defaultValue)
        {
            decimal n;
            if (!decimal.TryParse(value.Replace(',', '.'), NumberStyles.Float, new CultureInfo("en-US"), out n)) return defaultValue;
            return n;
        }
        public static double ToDouble(this String value)
        {
            return double.Parse(value.Replace(',', '.'), NumberStyles.Float, new CultureInfo("en-US"));
        }
        public static double ToDouble(this String value, double defaultValue)
        {
            double n;
            if (!double.TryParse(value.Replace(',', '.'), NumberStyles.Float, new CultureInfo("en-US"), out n)) return defaultValue;
            return n;
        }
        public static float ToFloat(this String value)
        {
            return float.Parse(value.Replace(',', '.'), NumberStyles.Float, new CultureInfo("en-US"));
        }
        public static float ToFloat(this String value, float defaultValue)
        {
            float n;
            if (!float.TryParse(value.Replace(',', '.'), NumberStyles.Float, new CultureInfo("en-US"), out n)) return defaultValue;
            return n;
        }
        public static DateTime ToDateTime(this String value)
        {
            return DateTime.Parse(value);
        }
        public static DateTime ToDateTime(this String value, DateTime defaultValue)
        {
            DateTime d;
            if (!DateTime.TryParse(value, out d)) return defaultValue;
            return d;
        }
        #endregion String

        #region Double
        static Array arUnidad, arQuinces, arDecena, arCentena;
        static double Numero;
        static string Cadena;
        static int J, K, A, B, C, D, E, F, G, H, SinDecimales, CE, DE, UN, VE;

        public static string ToLetters(this double n)
        {
            int millons = 0;
            string txtmillon = string.Empty;
            string txtmenos = string.Empty;
            if (n > 9999999.99)
            {
                millons = (int)n / 1000000;
                n = n - (millons * 1000000);
            }
            switch (millons)
            {
                case 0: break;
                case 1: txtmillon = DoIt2(millons) + " MILLÓN "; break;
                default:
                    txtmillon = DoIt2(millons) + " MILLONES ";
                    break;
            }
            if (n < 0)
            {
                txtmenos = "MENOS ";
                n = Math.Abs(n);
            }
            return txtmenos + txtmillon + DoIt2(n) + ".--";
        }

        static string DoIt2(double n)
        {
            Numero = n;
            //if (Numero > 9999999.99)
            //    return "El valor es mayor que 9999999.99 - No se puede procesar";

            arUnidad = Array.CreateInstance(typeof(string), 10);
            arUnidad.SetValue("UN ", 1);
            arUnidad.SetValue("DOS ", 2);
            arUnidad.SetValue("TRES ", 3);
            arUnidad.SetValue("CUATRO ", 4);
            arUnidad.SetValue("CINCO ", 5);
            arUnidad.SetValue("SEIS ", 6);
            arUnidad.SetValue("SIETE ", 7);
            arUnidad.SetValue("OCHO ", 8);
            arUnidad.SetValue("NUEVE ", 9);
            arQuinces = Array.CreateInstance(typeof(string), 6);
            arQuinces.SetValue("ONCE ", 1);
            arQuinces.SetValue("DOCE ", 2);
            arQuinces.SetValue("TRECE ", 3);
            arQuinces.SetValue("CATORCE ", 4);
            arQuinces.SetValue("QUINCE ", 5);
            arDecena = Array.CreateInstance(typeof(string), 10);
            arDecena.SetValue("DIECI", 1);
            arDecena.SetValue("VEINTI", 2);
            arDecena.SetValue("TREINTA ", 3);
            arDecena.SetValue("CUARENTA ", 4);
            arDecena.SetValue("CINCUENTA ", 5);
            arDecena.SetValue("SESENTA ", 6);
            arDecena.SetValue("SETENTA ", 7);
            arDecena.SetValue("OCHENTA ", 8);
            arDecena.SetValue("NOVENTA ", 9);
            arCentena = Array.CreateInstance(typeof(string), 10);
            arCentena.SetValue("CIEN ", 1);
            arCentena.SetValue("DOSCIENTOS ", 2);
            arCentena.SetValue("TRESCIENTOS ", 3);
            arCentena.SetValue("CUATROCIENTOS ", 4);
            arCentena.SetValue("QUINIENTOS ", 5);
            arCentena.SetValue("SEISCIENTOS ", 6);
            arCentena.SetValue("SETECIENTOS ", 7);
            arCentena.SetValue("OCHOCIENTOS ", 8);
            arCentena.SetValue("NOVECIENTOS ", 9);
            // Separo el Nro. en enteros // 
            SinDecimales = System.Convert.ToInt32(Numero * 100);
            J = (SinDecimales / 1000000000);
            SinDecimales = SinDecimales - (J * 1000000000);
            K = (SinDecimales / 100000000);
            SinDecimales = SinDecimales - (K * 100000000);
            A = (SinDecimales / 10000000);
            SinDecimales = SinDecimales - (A * 10000000);
            B = (SinDecimales / 1000000);
            SinDecimales = SinDecimales - (B * 1000000);
            C = (SinDecimales / 100000);
            SinDecimales = SinDecimales - (C * 100000);
            D = (SinDecimales / 10000);
            SinDecimales = SinDecimales - (D * 10000);
            E = (SinDecimales / 1000);
            SinDecimales = SinDecimales - (E * 1000);
            F = (SinDecimales / 100);
            SinDecimales = SinDecimales - (F * 100);
            G = (SinDecimales / 10);
            SinDecimales = SinDecimales - (G * 10);
            H = SinDecimales;
            VE = 0;
            Cadena = "";
            if (J != 0 || K != 0)
            {
                DE = J;
                UN = K;
                VE = 0;
                Miles();
                if (J == 0 && K == 1)
                {
                    Cadena = Cadena + "MILLON ";
                }
                else
                {
                    Cadena = Cadena + "MILLONES ";
                }
            }
            if (A != 0 || B != 0 || C != 0)
            {
                CE = A;
                DE = B;
                UN = C;
                VE = 0;
                Miles();
                Cadena = Cadena + "MIL ";
            }
            //if (D != 0 || E != 0 || F != 0)
            //{
            CE = D;
            DE = E;
            UN = F;
            VE = 0;
            Miles();
            if (F == 1 && E != 1)
            {
                Cadena = Cadena.Trim() + "O ";
            }
            // }
            //if (G != 0 || H != 0)
            //{
            if (Cadena != "")
            {
                Cadena = Cadena + "PESOS CON ";
            }
            CE = 0;
            DE = G;
            UN = H;
            VE = 0;
            Miles();
            if (G == 0 && H == 1)
            {
                Cadena = Cadena + "CENTAVO";
            }
            else
            {
                Cadena = Cadena + "CENTAVOS";
            }
            //}
            //else
            //{
            //    Cadena = Cadena + ".--";
            //}
            return Cadena;
        }
        // ======================== // 
        static void Miles()
        {
            while (VE == 0)
            {
                if (CE != 0)
                {
                    Cadena = Cadena + arCentena.GetValue(CE);
                    if ((DE != 0 || UN != 0) && CE == 1)
                    {
                        Cadena = Cadena.Trim() + "TO ";
                    }
                }
                if (DE != 0)
                {
                    if (DE == 2)
                    {
                        if (UN == 0)
                        {
                            Cadena = Cadena + "VEINTE ";
                            break;
                        }
                        else
                        {
                            Cadena = Cadena + arDecena.GetValue(2);
                            Cadena = Cadena + arUnidad.GetValue(UN);
                            break;
                        }
                    }
                    if (DE == 1)
                    {
                        if (UN == 0)
                        {
                            Cadena = Cadena + "DIEZ ";
                            break;
                        }
                        if (UN > 0 && UN < 6)
                        {
                            Cadena = Cadena + arQuinces.GetValue(UN);
                            break;
                        }
                        else
                        {
                            Cadena = Cadena + arDecena.GetValue(DE);
                            //Cadena = Cadena + "Y "; 
                        }

                    }
                    else // Para decena distinta de 1 o 2
                    {
                        Cadena = Cadena + arDecena.GetValue(DE);
                        if (UN != 0)
                        {
                            Cadena = Cadena + "Y ";
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                if (UN != 0)
                {
                    Cadena = Cadena + arUnidad.GetValue(UN);
                }
                else
                    Cadena += " CERO ";
                VE = 1;
            } // Fin del while 

            // ======================== // 
        }
        #endregion Double

        #region BetweenExtensions
        /// <summary>
        /// an extension class for the between operation
        /// name pattern IsBetweenXX where X = I -> Inclusive, X = E -> Exclusive
        /// </summary>

        /// <summary>
        /// between check <![CDATA[min <= value <= max]]> 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">the value to check</param>
        /// <param name="min">Inclusive minimum border</param>
        /// <param name="max">Inclusive maximum border</param>
        /// <returns>return true if the value is between the min and max else false</returns>
        public static bool IsBetweenII<T>(this T value, T min, T max) where T : IComparable
        {
            return (min.CompareTo(value) <= 0) && (value.CompareTo(max) <= 0);
        }

        /// <summary>
        /// between check <![CDATA[min <= value <= max]]>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">the value to check</param>
        /// <param name="min">Exclusive minimum border</param>
        /// <param name="max">Inclusive maximum border</param>
        /// <returns>return true if the value is between the min and max else false</returns>
        public static bool IsBetweenEI<T>(this T value, T min, T max) where T : IComparable
        {
            return (min.CompareTo(value) < 0) && (value.CompareTo(max) <= 0);
        }

        /// <summary>
        /// between check <![CDATA[min <= value <= max]]>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">the value to check</param>
        /// <param name="min">Inclusive minimum border</param>
        /// <param name="max">Exclusive maximum border</param>
        /// <returns>return true if the value is between the min and max else false</returns>
        public static bool IsBetweenIE<T>(this T value, T min, T max) where T : IComparable
        {
            return (min.CompareTo(value) <= 0) && (value.CompareTo(max) < 0);
        }

        /// <summary>
        /// between check <![CDATA[min <= value <= max]]>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">the value to check</param>
        /// <param name="min">Exclusive minimum border</param>
        /// <param name="max">Exclusive maximum border</param>
        /// <returns>return true if the value is between the min and max else false</returns>

        public static bool IsBetweenEE<T>(this T value, T min, T max) where T : IComparable
        {
            return (min.CompareTo(value) < 0) && (value.CompareTo(max) < 0);
        }
        #endregion BetweenExtensions

        #region Object

        public static string Serialize(this object obj)
        {
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

        public static string ToJSON(this object obj)
        {
            JsonSerializerSettings jsSettings = new JsonSerializerSettings();
            jsSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            jsSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string json = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None, jsSettings);
            return json;

        }

        #endregion Object
    }
}
