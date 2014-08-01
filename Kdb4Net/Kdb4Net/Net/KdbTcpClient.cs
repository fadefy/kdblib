using System;
using System.Net.Sockets;
using System.Text;

namespace Kdb4Net.Net
{
    public class KdbTcpClient : TcpClient
    {
        private int _bufferSize = 2048;

        public int BufferSize
        {
            get { return _bufferSize; }
            set
            {
                if (_bufferSize <= 0)
                    throw new ArgumentOutOfRangeException("BufferSize", "Must be a positive value.");

                _bufferSize = value;
            }
        }

        public KdbTcpClient(string host, int port, string userName)
        {
            Connect(host, port);

            var response = SendRequest(userName);
            if (response.Length != 1)
            {
                throw new UnauthorizedAccessException();
            }
        }

        private byte[] SendRequest(string data)
        {
            WriteToStream(ConverToBytes(data));

            return ReadAllFromStream();
        }

        private byte[] ReadAllFromStream()
        {
            var bufferSize = _bufferSize;
            var buffer = new byte[bufferSize];
            var readedCount = 0;
            var sizeToRead = bufferSize;
            // Do remember the Stream cannot be seek.
            while ((readedCount += GetStream().Read(buffer, bufferSize - sizeToRead, sizeToRead)) == bufferSize)
            {
                sizeToRead = bufferSize;
                Array.Resize(ref buffer, bufferSize >>= 1);
            }
            Array.Resize(ref buffer, readedCount);

            return buffer;
        }

        private byte[] ConverToBytes(string data)
        {
            var bufferSize = Encoding.Default.GetByteCount(data) + 1;
            // Initialize request buffer for proper size.
            var buffer = new byte[bufferSize];
            var written = 0;
            // Convert data string into buffer.
            while ((written = Encoding.Default.GetBytes(data, 0, data.Length, buffer, 0)) != (bufferSize - 1))
            {
                Array.Resize(ref buffer, written + 1);
            }
            // Finish the bytes buffer.
            buffer[written] = 0;

            return buffer;
        }

        private void WriteToStream(byte[] buffer)
        {
            // Write buffer to TCP connection.
            this.GetStream().Write(buffer, 0, buffer.Length);
        }
    }
}
