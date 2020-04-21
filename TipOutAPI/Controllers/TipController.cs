using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TipoutProject;

namespace TipOutAPI.Controllers
{
    [ApiController]
    [Route("api/tip")]
    public class TipController : ControllerBase
    {
        private readonly ILogger<CrudController> _logger;
        private IConfiguration _config;
        public TipController(ILogger<CrudController> logger, IConfiguration config)
        {
            _config = config;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public List<TipModel> GetUser(string id)
        {
            try
            {
                return Program.userController.GetUser(id).TipList;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return null;
            }
        }

        [HttpPost]
        public bool AddTip([FromBody] TipModel tip)
        {
            return Program.userController.AddTip(tip.ownerID, tip);
        }
    }
}
