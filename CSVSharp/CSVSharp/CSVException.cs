using System;

namespace CSVSharp
{
    public class CSVSharpException : Exception
    {
        public CSVSharpException()
        {
        }

        public CSVSharpException(string message) : base(message)
        {
        }

        public CSVSharpException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
