using System;
namespace HessQLParser.Errors
{
    public class ParsingException : Exception
    {
        public ParsingException(string message) : base(message) { }
    }
}