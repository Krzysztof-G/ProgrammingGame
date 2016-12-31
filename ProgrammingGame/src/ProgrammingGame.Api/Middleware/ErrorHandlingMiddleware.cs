using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ProgrammingGame.Api.Exceptions;
using ProgrammingGame.Api.Models;
using ProgrammingGame.Common.Enums;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProgrammingGame.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception == null)
                return;

            var code = HttpStatusCode.InternalServerError;

            if (exception is ActionNotAvailableException)
            {
                code = HttpStatusCode.Forbidden;
            }
            else if (exception is ApiException)
            {
                code = HttpStatusCode.BadRequest;
            }

            await WriteExceptionAsync(context, exception, code).ConfigureAwait(false);
        }

        private static async Task WriteExceptionAsync(HttpContext context, Exception exception, HttpStatusCode code)
        {
            var response = context.Response;
            response.StatusCode = (int)code;

            string message;
            var error = new ErrorDto
            {
                Code = (int)((exception as ApiException)?.ErrorCode ?? ErrorCodes.OtherProblem),
                Description = exception.Message,
            };

            if (context.Request.ContentType == "application/xml")
            {
                response.ContentType = "application/xml";
                var serializer = new XmlSerializer(error.GetType());
                using (var textWriter = new StringWriter())
                {
                    var namespaces = new XmlSerializerNamespaces();
                    namespaces.Add("", "");

                    serializer.Serialize(textWriter, error, namespaces);
                    message = textWriter.ToString();
                }
            }
            else
            {
                response.ContentType = "application/json";
                message = JsonConvert.SerializeObject(error);
            }

            await response.WriteAsync(message).ConfigureAwait(false);
        }
    }
}