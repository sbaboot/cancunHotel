using CancunHotel.Domain.Service.Contract.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancunHotel.Api.Controller.User
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService UserService { get; set; }
        public UserController(IUserService userService)
        {
            UserService = userService;
        }
        [HttpGet()]
        public string GetAll()
        {
            return UserService.GetAll();
        }
    }
}
