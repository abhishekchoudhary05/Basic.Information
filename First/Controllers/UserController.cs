using First.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace First.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserDbContext context) : ControllerBase
    {
        private readonly UserDbContext _userDbContext = context;

        [HttpGet]
        public List<Information> GetInformation()
        {
            return _userDbContext.Employee.ToList();
        }
    }
}
