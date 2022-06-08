using System;

namespace LookScoreCommon.Exceptions
{
    public class UnsupportedFileTypeException : Exception
    {
        public UnsupportedFileTypeException() { }

        public UnsupportedFileTypeException(string message) : base(message) { }
    }
}
