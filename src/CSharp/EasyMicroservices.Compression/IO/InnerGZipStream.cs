using EasyMicroservices.Utilities.IO.Interfaces;
using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace EasyMicroservices.Compression.IO
{
    internal class InnerGZipStream : Stream
    {
        GZipStream Reader { get; }
        GZipStream Writer { get; }
        Stream BaseStream;
        public InnerGZipStream(Stream stream)
        {
            BaseStream = stream;
            Reader = new GZipStream(stream, CompressionMode.Decompress);
            Writer = new GZipStream(stream, CompressionMode.Compress);
        }

        public override bool CanRead => Reader.CanRead;

        public override bool CanSeek => false;

        public override bool CanWrite => Writer.CanWrite;

        public override long Length => BaseStream.Length;

        public override long Position
        {
            get => BaseStream.Position;
            set => BaseStream.Position = value;
        }

        public override void Flush()
        {
            Writer.Flush();
            BaseStream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return Reader.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            Writer.Write(buffer, offset, count);
        }

        public override void Close()
        {
            if (Writer.CanWrite)
                Writer.Close();
            if (Reader.CanRead)
                Reader.Close();
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (Writer.CanWrite)
                Writer.Dispose();
            if (Reader.CanRead)
                Reader.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="innerStreamMiddleware"></param>
        /// <returns></returns>
        public static Task<Stream> GetStream(Stream stream, IStreamMiddleware innerStreamMiddleware)
        {
            if (innerStreamMiddleware != null)
                return innerStreamMiddleware.GetStream(stream);
            return Task.FromResult((Stream)new InnerGZipStream(stream));
        }
    }
}
