using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Utilities.UI
{
    public class MessageFilterStream : Stream
    // This filter changes all characters passed through it to uppercase.
    {
        private Stream strSink;
        private long lngPosition;
        private string message;
        public MessageFilterStream(Stream sink)
        {
            strSink = sink;
        }
        public MessageFilterStream(Stream sink, string message)
            : this(sink)
        {
            this.message = message;
        }
        // The following members of Stream must be overriden.
        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override long Length
        {
            get { return 0; }
        }

        public override long Position
        {
            get { return lngPosition; }
            set { lngPosition = value; }
        }

        public override long Seek(long offset, System.IO.SeekOrigin direction)
        {
            return strSink.Seek(offset, direction);
        }

        public override void SetLength(long length)
        {
            strSink.SetLength(length);
        }

        public override void Close()
        {
            strSink.Close();
        }

        public override void Flush()
        {
            strSink.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return strSink.Read(buffer, offset, count);
        }

        // The Write method actually does the filtering.
        public override void Write(byte[] buffer, int offset, int count)
        {
            byte[] data = new byte[count];
            Buffer.BlockCopy(buffer, offset, data, 0, count);
            string inputstring = Encoding.UTF8.GetString(data);
            //string script = "<script type='text/javascript'>alert('" + message + "'); </script>";
            data = Encoding.UTF8.GetBytes(inputstring);
            strSink.Write(data, 0, count);

        }
    }
}
