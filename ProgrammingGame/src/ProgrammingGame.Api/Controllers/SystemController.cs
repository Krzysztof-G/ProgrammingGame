using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProgrammingGame.Api.Models;
using ProgrammingGame.Common.Enums;
using System;
using System.Collections.Generic;

namespace ProgrammingGame.Api.Controllers
{
    public class SystemController : BaseController
    {
        private readonly ILogger<SystemController> _logger;

        public SystemController(IMapper mapper, ILogger<SystemController> logger) : base(mapper)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Ping()
        {
            return Ok("Pong");
        }

        [HttpGet]
        public IActionResult GetErrorCodes()
        {
            var errorCodes = (int[])Enum.GetValues(typeof(ErrorCodes));
            var errorCodeNames = Enum.GetNames(typeof(ErrorCodes));

            var errorCodeDictionary = new List<DictionaryItem>();
            for (int i = 0; i < errorCodes.Length; i++)
            {
                errorCodeDictionary.Add(new DictionaryItem { Code = errorCodes[i], Name = errorCodeNames[i] });
            }

            return Ok(errorCodeDictionary.ToArray());
        }
    }
}
