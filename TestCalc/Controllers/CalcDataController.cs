using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestCalc.Filters;

namespace TestCalc.Controllers
{
    [ApiController]
    [ApiKeyAuth]
    [Route("[controller]")]
    public class CalcDataController : ControllerBase
    {
        private readonly ILogger<CalcDataController> _logger;
        public CalcDataController(ILogger<CalcDataController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get([FromQuery] string a, [FromQuery] string b, [FromQuery] string del)
        {
            try
            {
                double _a = Convert.ToDouble(a);
                double _b = Convert.ToDouble(b);
                switch (del)
                {
                    case "+": return Ok(_a + _b);
                    case "-": return Ok(_a - _b);
                    case "*": return Ok(_a * _b);
                    case "div": return Ok(_a / _b);
                    default: return Ok(_a + _b);
                }
            }catch(Exception e)
            {
                return BadRequest(e);
            }           
        }
    }
}
