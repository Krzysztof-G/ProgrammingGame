using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProgrammingGame.Api.Models;
using ProgrammingGame.Common.Enums;

namespace ProgrammingGame.Api.Controllers
{
    public class SystemController : BaseController
    {
        public SystemController(IMapper mapper) : base(mapper)
        {
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
