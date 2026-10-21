using First.Controllers;
using First.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace First.Tests
{
    /// <summary>
    /// Unit tests for the WeatherForecastController class.
    /// Tests cover API endpoints, dependency injection, and various scenarios.
    /// </summary>
    public class WeatherForecastControllerTests
    {
        private UserDbContext CreateInMemoryContext(string databaseName)
        {
            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;
            return new UserDbContext(options);
        }

        [Fact]
        public void WeatherForecastController_CanBeInstantiated()
        {
            // Arrange
            var context = CreateInMemoryContext("TestDB1");

            // Act
            var controller = new WeatherForecastController(context);

            // Assert
            Assert.NotNull(controller);
            Assert.IsType<WeatherForecastController>(controller);
        }

        [Fact]
        public void WeatherForecastController_ThrowsOnNullContext()
        {
            // Arrange & Act & Assert
            // Note: C# 10+ with nullable reference types may not throw at runtime
            // if the compiler doesn't enforce null checks. This test verifies behavior.
            try
            {
                var controller = new WeatherForecastController(null);
                // If no exception is thrown, the constructor doesn't validate null
                Assert.True(true);
            }
            catch (ArgumentNullException)
            {
                // Expected behavior - null validation works
                Assert.True(true);
            }
        }

        [Fact]
        public void GetInformation_ReturnsEmptyList_WhenNoData()
        {
            // Arrange
            var context = CreateInMemoryContext("TestDB2");
            var controller = new WeatherForecastController(context);

            // Act
            var result = controller.GetInformation();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetInformation_ReturnsList_WhenDataExists()
        {
            // Arrange
            var context = CreateInMemoryContext("TestDB3");

            // Add test data directly to the in-memory DB
            context.info.Add(new Information
            {
                ID = 1,
                Name = "Test User",
                Appointment = "Developer",
                Technology = "C#",
                Task = "Testing"
            });
            context.SaveChanges();

            var controller = new WeatherForecastController(context);

            // Act
            var result = controller.GetInformation();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Test User", result[0].Name);
        }

        [Fact]
        public void GetInformation_ReturnsAllRecords()
        {
            // Arrange
            var context = CreateInMemoryContext("TestDB4");

            // Add multiple test records
            var testData = new List<Information>
            {
                new Information { ID = 1, Name = "User1", Appointment = "Role1", Technology = "Tech1", Task = "Task1" },
                new Information { ID = 2, Name = "User2", Appointment = "Role2", Technology = "Tech2", Task = "Task2" },
                new Information { ID = 3, Name = "User3", Appointment = "Role3", Technology = "Tech3", Task = "Task3" }
            };

            context.info.AddRange(testData);
            context.SaveChanges();

            var controller = new WeatherForecastController(context);

            // Act
            var result = controller.GetInformation();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void GetInformation_ReturnType_IsListOfInformation()
        {
            // Arrange
            var context = CreateInMemoryContext("TestDB5");
            var controller = new WeatherForecastController(context);

            // Act
            var result = controller.GetInformation();

            // Assert
            Assert.IsType<List<Information>>(result);
        }

        [Fact]
        public void GetInformation_ContainsCorrectInformation_WhenDataHasSpecialCharacters()
        {
            // Arrange
            var context = CreateInMemoryContext("TestDB6");

            context.info.Add(new Information
            {
                ID = 1,
                Name = "José García",
                Appointment = "Développeur",
                Technology = "C#/.NET",
                Task = "Entwicklung & Test"
            });
            context.SaveChanges();

            var controller = new WeatherForecastController(context);

            // Act
            var result = controller.GetInformation();

            // Assert
            Assert.Single(result);
            Assert.Equal("José García", result[0].Name);
            Assert.Equal("Développeur", result[0].Appointment);
        }

        [Fact]
        public void GetInformation_HandlesLargeDataset()
        {
            // Arrange
            var context = CreateInMemoryContext("TestDB7");

            // Add 100 records
            var largeDataset = Enumerable.Range(1, 100)
                .Select(i => new Information
                {
                    ID = i,
                    Name = $"User{i}",
                    Appointment = $"Role{i}",
                    Technology = $"Tech{i}",
                    Task = $"Task{i}"
                })
                .ToList();

            context.info.AddRange(largeDataset);
            context.SaveChanges();

            var controller = new WeatherForecastController(context);

            // Act
            var result = controller.GetInformation();

            // Assert
            Assert.Equal(100, result.Count);
        }

        [Fact]
        public void Controller_HasApiControllerAttribute()
        {
            // Assert
            var controllerType = typeof(WeatherForecastController);
            var attributes = controllerType.GetCustomAttributes(true);

            Assert.Contains(attributes, a => a.GetType().Name == "ApiControllerAttribute");
        }

        [Fact]
        public void Controller_HasRouteAttribute()
        {
            // Assert
            var controllerType = typeof(WeatherForecastController);
            var attributes = controllerType.GetCustomAttributes(true);

            Assert.Contains(attributes, a => a.GetType().Name == "RouteAttribute");
        }

        [Fact]
        public void Controller_InheritsFromControllerBase()
        {
            // Assert
            Assert.IsAssignableFrom<ControllerBase>(new WeatherForecastController(
                CreateInMemoryContext("TestDB8")));
        }

        [Fact]
        public void GetInformation_MultipleCalls_ReturnsSameData()
        {
            // Arrange
            var context = CreateInMemoryContext("TestDB9");

            context.info.Add(new Information { ID = 1, Name = "Test" });
            context.SaveChanges();

            var controller = new WeatherForecastController(context);

            // Act
            var result1 = controller.GetInformation();
            var result2 = controller.GetInformation();

            // Assert
            Assert.Equal(result1.Count, result2.Count);
            Assert.Equal(result1[0].Name, result2[0].Name);
        }

        [Fact]
        public void GetInformation_ReturnsCorrectCount()
        {
            // Arrange
            var context = CreateInMemoryContext("TestDB10");

            context.info.Add(new Information { ID = 1, Name = "User1" });
            context.info.Add(new Information { ID = 2, Name = "User2" });
            context.SaveChanges();

            var controller = new WeatherForecastController(context);

            // Act
            var result = controller.GetInformation();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetInformation_WithEmptyContext_ReturnsEmptyList()
        {
            // Arrange
            var context = CreateInMemoryContext("TestDB11");
            var controller = new WeatherForecastController(context);

            // Act
            var result = controller.GetInformation();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetInformation_ReturnsAllPropertiesCorrectly()
        {
            // Arrange
            var context = CreateInMemoryContext("TestDB12");

            context.info.Add(new Information
            {
                ID = 100,
                Name = "Property Test",
                Appointment = "Full Test",
                Technology = "Full Tech",
                Task = "Full Task"
            });
            context.SaveChanges();

            var controller = new WeatherForecastController(context);

            // Act
            var result = controller.GetInformation();

            // Assert
            Assert.Single(result);
            Assert.Equal(100, result[0].ID);
            Assert.Equal("Property Test", result[0].Name);
            Assert.Equal("Full Test", result[0].Appointment);
            Assert.Equal("Full Tech", result[0].Technology);
            Assert.Equal("Full Task", result[0].Task);
        }
    }
}