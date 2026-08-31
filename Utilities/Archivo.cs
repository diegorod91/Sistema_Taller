using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Xml.Serialization;
using System.Security.Cryptography;

namespace Utilities
{
    public class Archivo
    {
        private string fileName;
        private byte[] inputStream;
        public int Id { get; set; }
        public string Guid { get; set; }
        public int ContentLength { get; set; }
        public string ContentType { get; set; }
        public string FileName
        {
            get { return fileName.Split('\\')[fileName.Split('\\').Length - 1]; }
            set { fileName = value; }
        }
        public string MD5Checksum { get; set; }

        [XmlIgnore]
        public byte[] InputStream
        {
            get { return inputStream == null ? null : inputStream.Length.Equals(0) ? null : inputStream; }
            set { inputStream = value; }
        }

        public Archivo()
        {
            this.ContentType = string.Empty;
            this.FileName = string.Empty;
            this.Guid = System.Guid.NewGuid().ToString().Replace("-", string.Empty);
        }

        public Archivo(HttpPostedFile file)
            : this()
        {
            this.ContentLength = file.ContentLength;
            this.ContentType = file.ContentType;
            this.FileName = file.FileName;
            this.InputStream = file.InputStream.ReadFully();
            this.Guid = System.Guid.NewGuid().ToString().Replace("-", string.Empty);
            // this.MD5Checksum = this.InputStream.GetMD5Hash();

            using (var md5 = MD5.Create())
            {
                this.MD5Checksum = BitConverter.ToString(md5.ComputeHash(this.InputStream)).Replace("-", string.Empty);
            }

        }
    }
}
