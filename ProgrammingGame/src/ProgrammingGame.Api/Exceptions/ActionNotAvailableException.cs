using System;
using ProgrammingGame.Common.Enums;

namespace ProgrammingGame.Api.Exceptions
{
    public class ActionNotAvailableException : ApiException
    {
        public ActionNotAvailableException(string message, ErrorCodes errorCode) : base(message, errorCode)
        {
        }

        public ActionNotAvailableException(string message, ErrorCodes errorCode, Exception innerException) : base(message, errorCode, innerException)
        {
        }
    }
}
