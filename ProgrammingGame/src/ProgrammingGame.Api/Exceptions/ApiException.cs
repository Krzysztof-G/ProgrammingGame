using System;
using ProgrammingGame.Common.Enums;

namespace ProgrammingGame.Api.Exceptions
{
    public class ApiException : Exception
    {
        public ErrorCodes ErrorCode { get; private set; }

        public ApiException(string message, ErrorCodes errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public ApiException(string message, ErrorCodes errorCode, Exception innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }
}
