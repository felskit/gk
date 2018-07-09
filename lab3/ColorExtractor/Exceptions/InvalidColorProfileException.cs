using System;

namespace ColorExtractor.Exceptions
{
    public class InvalidColorProfileException : Exception
    {
        public InvalidColorProfileException() { }

        public InvalidColorProfileException(string message) : base(message) { }
    }
}
