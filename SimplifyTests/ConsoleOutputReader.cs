using System;
using System.IO;
using System.Text;

namespace SimplifyTests
{
    public class ConsoleOutputReader : IDisposable
    {
        private readonly MemoryStream _memoryStream;
        private readonly StreamReader _streamReader;
        private readonly StreamWriter _streamWriter;
        private long _writePosition = 0;
        private long _readPosition = 0;

        public ConsoleOutputReader()
        {
            _memoryStream = new MemoryStream();
            _memoryStream.Seek(0, SeekOrigin.Begin);
            _streamWriter = new StreamWriter(_memoryStream);
            _streamReader = new StreamReader(_memoryStream);
            Console.SetOut(_streamWriter);
        }

        public void Dispose()
        {
            var standardOutput = new StreamWriter(Console.OpenStandardOutput())
            {
                AutoFlush = true
            };
            Console.SetOut(standardOutput);

            _streamWriter.Dispose();
            _streamReader.Dispose();
            _memoryStream.Dispose();
        }

        public string ReadLine()
        {
            _streamWriter.Flush();
            _writePosition = _memoryStream.Position;
            _memoryStream.Position = _readPosition;
            var line = _streamReader.ReadLine();
            _readPosition = _memoryStream.Position;
            _memoryStream.Position = _writePosition;
            return line;
        }
    }
}
