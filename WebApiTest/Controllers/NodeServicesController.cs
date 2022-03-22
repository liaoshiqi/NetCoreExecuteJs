using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NodeServicesController : ControllerBase
    {
        private readonly INodeServices _nodeServices;
        public NodeServicesController(INodeServices nodeServices)
        {
            _nodeServices = nodeServices;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _nodeServices.InvokeAsync<string>("./jsfiles/hello.js", "nodeServices");
            return Ok(result);
        }
    }
}
