using First.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace First.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserDbContext context) : ControllerBase
    {
        private readonly UserDbContext _userDbContext = context;

        [HttpGet("GetAllEmployees")]
        public List<Information> GetAllEmployees()
        {
            return _userDbContext.Employee.ToList();
        }

        [HttpPost("AddEmployee")]
        public bool SaveEmployee(Information information)
        {
            _userDbContext.Employee.Add(information);
            return _userDbContext.SaveChanges() == 1;
        }
    }
}
