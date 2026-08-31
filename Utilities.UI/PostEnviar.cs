using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Utilities.UI
{
    /// <summary>
    /// Envia datos via post a una URL.
    /// </summary>
    public class PostEnviar
    {

        // private string m_url = string.Empty;
        private Uri m_url;
        private NameValueCollection m_values = new NameValueCollection();
        private PostTypeEnum m_type = PostTypeEnum.Get;

        /// <summary>
        /// Constructor.
        /// </summary>
        public PostEnviar()
        {
        }

        ///// <summary>
        ///// Constructor que acepta una url como parametro
        ///// </summary>
        //public PostEnviar(string url): this()
        //{
        //    Accesos.PostEnviar(new Uri(url));
        //    //m_url = url;
        //}
        /// <summary>
        /// Constructor que acepta una url como parametro
        /// </summary>
        public PostEnviar(Uri url)
            : this()
        {
            m_url = url;
        }

        ///// <summary>
        ///// Constructor que aceta la url e items para postear.
        ///// </summary>
        //public PostEnviar(string url, NameValueCollection values)
        //    : this(url)
        //{
        //    m_values = values;
        //}
        /// <summary>
        /// Constructor que aceta la url e items para postear.
        /// </summary>
        public PostEnviar(Uri url, NameValueCollection values)
            : this(url)
        {
            m_values = values;
        }
        ///// <summary>
        ///// Gets / sets la url a enviar el post.
        ///// </summary>
        //public string Url
        //{
        //    get { return m_url; }
        //    set { m_url = value; }
        //}
        /// <summary>
        /// Gets / sets la url a enviar el post.
        /// </summary>
        public Uri Url
        {
            get { return m_url; }
            set { m_url = value; }
        }
        /// <summary>
        /// Gets / sets el valor de los nombres de la coleccion de items a postear.
        /// </summary>
        public NameValueCollection PostItems
        {
            get { return m_values; }
        }
        /// <summary>
        /// Gets / sets el tipo de accion a realizar por la url.
        /// </summary>
        public PostTypeEnum PostType
        {
            get { return m_type; }
            set { m_type = value; }
        }
        /// <summary>
        /// Envia la informacion a la url.
        /// </summary>
        /// <returns>Un string que contiene el resultado del post.</returns>
        public string Post()
        {
            StringBuilder parameters = new StringBuilder();
            for (int i = 0; i < m_values.Count; i++)
            {
                EncodeAndAddItem(ref parameters, m_values.GetKey(i), m_values[i]);
            }
            string result = PostData(m_url, parameters.ToString());
            return result;
        }
        ///// <summary>
        ///// Envia la informacion a la url.
        ///// </summary>
        ///// <param name="url">La donde postear.</param>
        ///// <returns>un string conteniendo el resultado del post.</returns>
        //public string Post(string url)
        //{
        //    m_url = url;
        //    return this.Post();
        //}

        //public string Post(string url, NameValueCollection values)
        //{
        //    m_values = values;
        //    return this.Post(url);
        //}
        /// <summary>
        /// Envia la informacion a la url.
        /// </summary>
        /// <param name="url">La donde postear.</param>
        /// <returns>un string conteniendo el resultado del post.</returns>
        public string Post(Uri url)
        {
            m_url = url;
            return this.Post();
        }

        public string Post(Uri url, NameValueCollection values)
        {
            m_values = values;
            return this.Post(url);
        }

        private string PostData(Uri url, string postData)
        {
            HttpWebRequest request = null;
            if (m_type == PostTypeEnum.Post)
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = postData.Length;
                request.Proxy = null;
                using (Stream writeStream = request.GetRequestStream())
                {
                    UTF8Encoding encoding = new UTF8Encoding();
                    byte[] bytes = encoding.GetBytes(postData);
                    writeStream.Write(bytes, 0, bytes.Length);
                }
            }
            else
            {
                Uri uri = new Uri(url.ToString() + "?" + postData);
                request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "GET";
                request.Proxy = null;
            }
            string result = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        result = readStream.ReadToEnd();
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// codifica un item y lo agrega al string.
        /// </summary>
        /// <param name="baseRequest">Datos previamente codificados.</param>
        /// <param name="dataItem">Datos a codificar.</param>
        /// <returns>Un string que contiene los datos anteriores codificados y los nuevos.</returns>
        private void EncodeAndAddItem(ref StringBuilder baseRequest, string key, string dataItem)
        {
            if (baseRequest == null)
            {
                baseRequest = new StringBuilder();
            }
            if (baseRequest.Length != 0)
            {
                baseRequest.Append("&");
            }
            baseRequest.Append(key);
            baseRequest.Append("=");
            baseRequest.Append(System.Web.HttpUtility.UrlEncode(dataItem));
        }
    }
    /// <summary>
    /// Determina que tipo de post realiza.
    /// </summary>
    public enum PostTypeEnum
    { Get, Post }

}
