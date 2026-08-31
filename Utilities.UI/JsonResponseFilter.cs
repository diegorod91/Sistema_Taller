using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Utilities.UI
{
    public class JsonResponseFilter : Stream
    {
        private Stream stream;
        private StreamWriter streamWriter;
        
        public JsonResponseFilter(Stream stm)
        {
            stream = stm;
            streamWriter = new StreamWriter(stream, System.Text.Encoding.UTF8);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            MemoryStream ms = new MemoryStream(buffer, offset, count, false);
            StreamReader sr = new StreamReader(ms, System.Text.Encoding.UTF8);
            bool bNewLine = false;
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                s = s.Trim();

                if (s != "" )
                {
                    if (s.StartsWith("{\"d\":["))
                    {
                        s = s.Remove(0,5);
                    }
                    if (s.EndsWith("]}"))
                    {
                        s = s.Remove(s.Length-1,1);
                    }
                    if (bNewLine)
                    {
                        streamWriter.WriteLine();
                        bNewLine = false;
                    }
                    streamWriter.Write(s);
                    if (s[s.Length - 1] != '>')
                        bNewLine = true;
                }
            }
            streamWriter.Flush();
        }
        
        public override int Read(byte[] buffer, int offset, int count)
        {
            return stream.Read(buffer, offset, count);
        }

        public override bool CanRead
        { get { return false; } }

        public override bool CanSeek
        { get { return false; } }

        public override bool CanWrite
        { get { return true; } }

        public override long Length
        { get { return stream.Length; } }

        public override long Position
        {
            get { return stream.Position; }
            set { stream.Position = value; }
        }

        public override void Flush()
        {
            stream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            stream.SetLength(value);
        }
    }
}
