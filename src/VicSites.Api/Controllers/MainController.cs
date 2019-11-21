using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VicSites.Business.Definition;

namespace VicSites.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {

        private readonly ILogger<MainController> _logger;
        private readonly IMainComponent _mainComponent;

        public MainController(ILogger<MainController> logger, IMainComponent mainComponent)
        {
            _logger = logger;
            _mainComponent = mainComponent;
        }


        [HttpGet("", Name = "Index")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogWarning("Calling Index()");
                await Task.Delay(1);
                return Ok("Hello World");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Index()");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }


        [HttpGet("visits", Name = "GetNumberOfVisits")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetNumberOfVisits()
        {
            try
            {
                _logger.LogWarning("Calling GetNumberOfVisits()");

                var numberOfVisits = await _mainComponent.GetNumberOfVisits();
                return Ok(new { numberOfVisits });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetNumberOfVisits()");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}