using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PruebaPWC.Controllers
{
    [Route("/")]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DefaultController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Content("Initial....");
        }
    }
}
