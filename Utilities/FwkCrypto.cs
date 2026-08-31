using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Web;


namespace Utilities
{
    public class FwkCrypto
    {
        private static string GetDefaultPass()
        {
            return "6aWPa4B9A*z%Jk2=[:yo";
        }

        public static string NewGUID
        {
            get
            {
                return System.Guid.NewGuid().ToString();
            }
        }

        public static string ComputeHash(string s)
        {
            byte[] b = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(s));
            return Convert.ToBase64String(b, 0, b.Length);

        }

        public static string HashWithSalt(string pwd, string salt)
        {

            // Concatenate pwd + salt
            string pwdAndSalt = string.Format("{0}+{1}", pwd, salt);

            // Hash 
            string hwsPwd = ComputeHash(pwdAndSalt);
            return hwsPwd;
        }

        public static string HashWithSalt(string pwd)
        {
            return HashWithSalt(pwd, GetDefaultPass());
        }

        public static string SaltCreate(int size)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public static string EncryptString(string s, string passPhrase, string IV)
        {
            RijndaelManaged r = GetCryptor(passPhrase, IV);

            byte[] byteIn = Encoding.UTF8.GetBytes(s);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, r.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(byteIn, 0, byteIn.Length);
            cs.FlushFinalBlock();
            Byte[] bo = ms.ToArray();
            return Convert.ToBase64String(bo, 0, bo.Length);
        }

        /// <summary>
        /// Para desencriptar desde otro lenguaje seguir los siguiente pasos:
        /// - El parametro s correspondiente al dato encriptado, decodificarlo en base 64
        /// - El parametro passPhrase correspondiente a la clave, hacerle ComputeHash con MD5
        /// - El parametro IV tiene que ser igual a la clave
        /// - Por ultimo desencriptar el dato con rijndael-128 en mode CBC.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="passPhrase"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        public static string DecryptString(string s, string passPhrase, string IV)
        {
            RijndaelManaged r = GetCryptor(passPhrase, IV);

            byte[] bytein = Convert.FromBase64String(s);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, r.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(bytein, 0, bytein.Length);
            cs.FlushFinalBlock();

            Byte[] bo = ms.ToArray();
            return Encoding.UTF8.GetString(bo, 0, bo.Length);
        }

        public static string EncryptQueryString(string s)
        {
            return EncryptQueryString(s, GetDefaultPass(), GetDefaultPass());
        }

        public static string EncryptQueryString(string s, string passPhrase, string IV)
        {
            return HttpUtility.UrlEncode(EncryptString(s, passPhrase, IV));
        }
        public static string DecryptQueryString(string s)
        {
            return DecryptQueryString(s, GetDefaultPass(), GetDefaultPass());
        }
        /// <summary>
        /// Para desencriptar desde otro lenguaje seguir los siguiente pasos:
        /// - El parametro s correspondiente al dato encriptado, decodificarlo en base 64
        /// - El parametro passPhrase correspondiente a la clave, hacerle ComputeHash con MD5
        /// - El parametro IV tiene que ser igual a la clave
        /// - Por ultimo desencriptar el dato con rijndael-128 en mode CBC.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="passPhrase"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        public static string DecryptQueryString(string s, string passPhrase, string IV)
        {
            return HttpUtility.UrlDecode(DecryptString(s, passPhrase, IV));
        }

        static RijndaelManaged GetCryptor(string passPhrase, string IV)
        {
            RijndaelManaged r = new RijndaelManaged();
            MD5CryptoServiceProvider m = new MD5CryptoServiceProvider();
            SHA1CryptoServiceProvider h = new SHA1CryptoServiceProvider();
            r.Key = m.ComputeHash(ASCIIEncoding.ASCII.GetBytes(passPhrase));
            r.IV = m.ComputeHash(ASCIIEncoding.ASCII.GetBytes(IV));
            r.Mode = CipherMode.CBC;
            return r;
        }
    }
}
