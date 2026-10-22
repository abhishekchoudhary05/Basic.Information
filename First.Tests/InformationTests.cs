using First.Models;
using Xunit;

namespace First.Tests;

public class InformationTests
{
    [Fact]
    public void Information_StoresItsProperties()
    {
        var joinedOn = new DateTime(2024, 1, 15);
        var information = new Information
        {
            ID = 7,
            Name = "Jane Doe",
            Department = "Engineering",
            Salary = (decimal)125000.5f,
            JoinedOn = joinedOn
        };

        Assert.Equal(7, information.ID);
        Assert.Equal("Jane Doe", information.Name);
        Assert.Equal("Engineering", information.Department);
        Assert.Equal((decimal)125000.5f, information.Salary);
        Assert.Equal(joinedOn, information.JoinedOn);
    }

    [Fact]
    public void Information_DefaultsAreUsable()
    {
        var information = new Information();

        Assert.Equal(0, information.ID);
        Assert.Null(information.Name);
        Assert.Null(information.Department);
        Assert.Equal((decimal)0f, information.Salary);
        Assert.Equal(default, information.JoinedOn);
    }
}
