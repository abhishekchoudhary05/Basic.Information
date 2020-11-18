using First.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace First.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly UserDbContext _userDbContext;

        public WeatherForecastController(IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>();

            var connectionString = configuration["ConnectionStrings:CollegeDBConnectionString"];
            optionsBuilder.UseSqlServer(connectionString);

            _userDbContext = new UserDbContext(optionsBuilder.Options);
        }

        [HttpGet]
        public List<Information> GetInformation()
        {
            return _userDbContext.info.ToList();
        }
    }
}
