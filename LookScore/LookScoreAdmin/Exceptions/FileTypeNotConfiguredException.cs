using System;

namespace LookScoreAdmin.Exceptions
{
    public class FileTypeNotConfiguredException : Exception
    {
        public FileTypeNotConfiguredException() { }

        public FileTypeNotConfiguredException(string message) : base(message) { }
    }
}
