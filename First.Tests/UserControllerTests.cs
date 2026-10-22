using First.Controllers;
using First.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace First.Tests;

public class UserControllerTests
{
    [Fact]
    public void GetInformation_ReturnsAllStoredInformation()
    {
        var options = new DbContextOptionsBuilder<UserDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new UserDbContext(options);
        context.Employee.AddRange(
            new Information { ID = 1, Name = "Alice", Department = "Engineering", Salary = 100000 },
            new Information { ID = 2, Name = "Bob", Department = "Support", Salary = 80000 });
        context.SaveChanges();

        var result = new UserController(context).GetInformation();

        Assert.Equal(2, result.Count);
        Assert.Contains(result, item => item.ID == 1 && item.Name == "Alice" && item.Department == "Engineering");
        Assert.Contains(result, item => item.ID == 2 && item.Name == "Bob" && item.Salary == 80000);
    }

    [Fact]
    public void GetInformation_ReturnsEmptyListWhenThereAreNoRecords()
    {
        var options = new DbContextOptionsBuilder<UserDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        using var context = new UserDbContext(options);

        var result = new UserController(context).GetInformation();

        Assert.Empty(result);
    }
}
