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

        [HttpPost("AddEmployees")]
        public bool SaveEmployees(List<Information> information)
        {
            _userDbContext.Employee.AddRange(information);
            int count = _userDbContext.SaveChanges();
            return count == information.Count;
        }

        [HttpPatch("UpdateEmployee")]
        public bool UpdateEmployeeSalary(Information update)
        {
            var employee = _userDbContext.Employee.Find(update.ID);
            if (employee is null)
                return false;

            if (update.Name is not null)
                employee.Name = update.Name;

            if (update.Department is not null)
                employee.Department = update.Department;

            if (update.Salary > 0)
                employee.Salary = update.Salary;

            int count = _userDbContext.SaveChanges();
            return count > 0;
        }

        [HttpDelete("RemoveEmployees")]
        public bool RemoveEmployees(int id)
        {
            var employee = _userDbContext.Employee.Find(id);
            if (employee is null)
                return false;

            _userDbContext.Employee.Remove(employee);
            int count = _userDbContext.SaveChanges();
            return count == 1;
        }
    }
}
