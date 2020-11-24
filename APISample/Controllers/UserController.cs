using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APISample.APIModels;
using APISample.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace APISample.Controllers
{
    [Route("api/user")]
    [ApiController]
    [EnableCors("AllowLocalhost")]
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet]
        [Route("GetUsers")]
        public IList<UserApiModel> GetUsers()
        {
            return _userService.GetAllUsers();
        }
      
        [HttpPost]
        [Route("AddUser")]
        public UserApiModel AddUser(UserApiModel model)
        {
            return _userService.AddUser(model);

        }

        [HttpPost]
        [Route("EditUser")]
        public UserApiModel EditUser(UserApiModel model)
        {
            return _userService.EditUser(model);

        }

      
        [HttpDelete]
        [Route("Delete")]
        public bool Delete(long id)
        {
            return _userService.DeleteUser(id);
        }
    }
}
