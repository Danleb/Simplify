using System;

namespace Simplifing
{
    public class SyntaxException : Exception
    {
        public SyntaxException(string message) : base(message)
        {
        }

        public SyntaxException(string message, int position) : base(message)
        {

        }
    }
}
