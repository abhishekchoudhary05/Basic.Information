using First.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace First.Tests;

public class UserDbContextTests
{
    private static UserDbContext CreateContext() => new(
        new DbContextOptionsBuilder<UserDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options);

    [Fact]
    public void InfoSet_CanCreateReadUpdateAndDeleteInformation()
    {
        using var context = CreateContext();
        var information = new Information
        {
            ID = 1,
            Name = "Initial Name",
            Department = "Engineering",
            Salary = 95000,
            JoinedOn = new DateTime(2023, 5, 1)
        };

        context.Employee.Add(information);
        context.SaveChanges();
        Assert.Equal("Initial Name", context.Employee.Single().Name);

        information.Name = "Updated Name";
        context.SaveChanges();
        Assert.Equal("Updated Name", context.Employee.Single().Name);

        context.Employee.Remove(information);
        context.SaveChanges();
        Assert.Empty(context.Employee);
    }

    [Fact]
    public void InfoSet_CanFilterByDepartment()
    {
        using var context = CreateContext();
        context.Employee.AddRange(
            new Information { ID = 1, Name = "Alice", Department = "Engineering" },
            new Information { ID = 2, Name = "Bob", Department = "Support" },
            new Information { ID = 3, Name = "Charlie", Department = "Engineering" });
        context.SaveChanges();

        var engineeringStaff = context.Employee.Where(item => item.Department == "Engineering").ToList();

        Assert.Equal(2, engineeringStaff.Count);
        Assert.All(engineeringStaff, item => Assert.Equal("Engineering", item.Department));
    }
}
