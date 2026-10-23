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

        var result = new UserController(context).GetAllEmployees();

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

        var result = new UserController(context).GetAllEmployees();

        Assert.Empty(result);
    }

    [Fact]
    public void UpdateEmployeeSalary_UpdatesOnlyProvidedFields()
    {
        var options = new DbContextOptionsBuilder<UserDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new UserDbContext(options);
        context.Employee.Add(new Information
        {
            ID = 1,
            Name = "Alice",
            Department = "Engineering",
            Salary = 100000,
            JoinedOn = new DateTime(2020, 1, 1)
        });
        context.SaveChanges();

        var result = new UserController(context).UpdateEmployeeSalary(new Information
        {
            ID = 1,
            Department = "Research",
            Salary = 110000
        });

        var updated = context.Employee.Find(1);
        Assert.True(result);
        Assert.NotNull(updated);
        Assert.Equal("Alice", updated.Name);
        Assert.Equal("Research", updated.Department);
        Assert.Equal(110000, updated.Salary);
        Assert.Equal(new DateTime(2020, 1, 1), updated.JoinedOn);
    }

    [Fact]
    public void UpdateEmployeeSalary_ReturnsFalseWhenEmployeeDoesNotExist()
    {
        var options = new DbContextOptionsBuilder<UserDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        using var context = new UserDbContext(options);

        var result = new UserController(context).UpdateEmployeeSalary(new Information { ID = 404 });

        Assert.False(result);
    }

    [Fact]
    public void RemoveEmployees_DeletesEmployeeWithMatchingId()
    {
        var options = new DbContextOptionsBuilder<UserDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new UserDbContext(options);
        context.Employee.AddRange(
            new Information { ID = 1, Name = "Alice", Department = "Engineering", Salary = 100000 },
            new Information { ID = 2, Name = "Bob", Department = "Support", Salary = 80000 });
        context.SaveChanges();

        var result = new UserController(context).RemoveEmployees(1);

        Assert.True(result);
        Assert.Null(context.Employee.Find(1));
        Assert.NotNull(context.Employee.Find(2));
    }

    [Fact]
    public void RemoveEmployees_ReturnsFalseWhenEmployeeDoesNotExist()
    {
        var options = new DbContextOptionsBuilder<UserDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        using var context = new UserDbContext(options);

        var result = new UserController(context).RemoveEmployees(404);

        Assert.False(result);
    }
}
