using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TipoutProject;

namespace TipOutAPI.Controllers
{
    [ApiController]
    [Route("api/crud")]
    public class CrudController : ControllerBase
    {
        private readonly ILogger<CrudController> _logger;
        private IConfiguration _config;
        public CrudController(ILogger<CrudController> logger, IConfiguration config)
        {
            _config = config;
            _logger = logger;
        }

        //get user 
        [HttpGet("{id}")]
        public UserModel GetUser(string id)
        {
            try
            {
                return Program.userController.GetUser(id);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return null;
            }
        }

        //add user
        [HttpPost]
        public bool CreateUser([FromBody]AuthorizationModel newuser)
        {
            try
            {
                return Program.userController.AddUser(newuser);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return false;
            }
        }

        //delete user
        [HttpDelete("{id}")]
        public bool DeleteUser(string id)
        {
            try
            {
                return Program.userController.DeleteUser(id);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return false;
            }
        }

        //update user - several options.
        [HttpPut]
        public bool UpdateUser([FromBody] UpdateUserModel update)
        {
            try
            {
                if (update.dateToUpdate == "username")
                {
                    return Program.userController.UpdateUsername(update.userID, update.updateData);
                }
                if (update.dateToUpdate == "password")
                {
                    return Program.userController.UpdatePassword(update.userID, update.updateData);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return false;
            }
            return false;
        }
    }
}
