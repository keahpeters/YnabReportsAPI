using System;

namespace YnabReportsAPI.YnabAPI.Exceptions
{
    public class YnabResponseException : Exception
    {
        public YnabResponseException(string? message)
            : base(message)
        {
        }
    }
}
