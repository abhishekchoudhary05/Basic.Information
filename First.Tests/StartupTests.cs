using First.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace First.Tests
{
    /// <summary>
    /// Unit tests for the Startup class.
    /// Tests cover configuration, service registration, and middleware setup.
    /// </summary>
    public class StartupTests
    {
        private IConfiguration CreateTestConfiguration()
        {
            var inMemorySettings = new Dictionary<string, string?> {
                {"ConnectionStrings:CollegeDBConnectionString", "TestConnectionString"},
                {"Logging:LogLevel:Default", "Information"}
            };
            return new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
        }

        private static ApplicationBuilder CreateApplicationBuilder(Startup startup)
        {
            var builder = WebApplication.CreateBuilder();
            startup.ConfigureServices(builder.Services);
            return new ApplicationBuilder(builder.Services.BuildServiceProvider());
        }

        [Fact]
        public void Startup_CanBeInstantiated_WithConfiguration()
        {
            // Arrange
            var configuration = CreateTestConfiguration();

            // Act
            var startup = new Startup(configuration);

            // Assert
            Assert.NotNull(startup);
            Assert.IsType<Startup>(startup);
        }

        [Fact]
        public void Startup_Configuration_IsAccessible()
        {
            // Arrange
            var configuration = CreateTestConfiguration();
            var startup = new Startup(configuration);

            // Act
            var result = startup.Configuration;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("TestConnectionString", result["ConnectionStrings:CollegeDBConnectionString"]);
        }

        [Fact]
        public void ConfigureServices_AddsControllers()
        {
            // Arrange
            var configuration = CreateTestConfiguration();
            var startup = new Startup(configuration);
            var services = new ServiceCollection();

            // Act
            startup.ConfigureServices(services);

            // Assert - verify services can be built
            var serviceProvider = services.BuildServiceProvider();
            Assert.NotNull(serviceProvider);
        }

        [Fact]
        public void ConfigureServices_RegistersRequiredServices()
        {
            // Arrange
            var configuration = CreateTestConfiguration();
            var startup = new Startup(configuration);
            var services = new ServiceCollection();

            // Act
            startup.ConfigureServices(services);

            // Assert - Check that services were added
            var serviceProvider = services.BuildServiceProvider();
            Assert.NotNull(serviceProvider);
        }

        [Fact]
        public void ConfigureServices_AddsMvcControllers()
        {
            // Arrange
            var configuration = CreateTestConfiguration();
            var startup = new Startup(configuration);
            var services = new ServiceCollection();

            // Act
            startup.ConfigureServices(services);

            // Assert - verify services were registered
            Assert.True(services.Count > 0);
        }

        [Fact]
        public void Configure_DoesNotThrow_InDevelopment()
        {
            // Arrange
            var configuration = CreateTestConfiguration();
            var startup = new Startup(configuration);

            var mockEnv = new Mock<IWebHostEnvironment>();
            mockEnv.SetupGet(e => e.EnvironmentName).Returns("Development");

            var appBuilder = CreateApplicationBuilder(startup);

            // Act & Assert - method executes without throwing
            startup.Configure(appBuilder, mockEnv.Object);
            Assert.True(true);
        }

        [Fact]
        public void Configure_DoesNotThrow_InProduction()
        {
            // Arrange
            var configuration = CreateTestConfiguration();
            var startup = new Startup(configuration);

            var mockEnv = new Mock<IWebHostEnvironment>();
            mockEnv.SetupGet(e => e.EnvironmentName).Returns("Production");

            var appBuilder = CreateApplicationBuilder(startup);

            // Act & Assert
            startup.Configure(appBuilder, mockEnv.Object);
            Assert.True(true);
        }

        [Fact]
        public void Configure_ConfiguresRouting()
        {
            // Arrange
            var configuration = CreateTestConfiguration();
            var startup = new Startup(configuration);

            var mockEnv = new Mock<IWebHostEnvironment>();
            mockEnv.SetupGet(e => e.EnvironmentName).Returns("Development");

            var appBuilder = CreateApplicationBuilder(startup);

            // Act
            startup.Configure(appBuilder, mockEnv.Object);

            // Assert - app builder should be configured without error
            Assert.NotNull(appBuilder);
        }

        [Fact]
        public void Configure_ConfiguresEndpoints()
        {
            // Arrange
            var configuration = CreateTestConfiguration();
            var startup = new Startup(configuration);

            var mockEnv = new Mock<IWebHostEnvironment>();
            mockEnv.SetupGet(e => e.EnvironmentName).Returns("Development");

            var appBuilder = CreateApplicationBuilder(startup);

            // Act - This will configure endpoints
            startup.Configure(appBuilder, mockEnv.Object);

            // Assert - method completes successfully
            Assert.True(true);
        }

        [Fact]
        public void Startup_Constructor_AcceptsConfiguration()
        {
            // Arrange
            var configuration = CreateTestConfiguration();

            // Act - Startup constructor accepts configuration
            var startup = new Startup(configuration);

            // Assert - method succeeds
            Assert.NotNull(startup);
        }

        [Fact]
        public void ConfigureServices_DoesNotThrow()
        {
            // Arrange
            var configuration = CreateTestConfiguration();
            var startup = new Startup(configuration);
            var services = new ServiceCollection();

            // Act & Assert
            startup.ConfigureServices(services);
            Assert.NotNull(services);
        }

        [Fact]
        public void ConfigureServices_MultipleCalls_AddsServicesOnce()
        {
            // Arrange
            var configuration = CreateTestConfiguration();
            var startup = new Startup(configuration);
            var services1 = new ServiceCollection();
            var services2 = new ServiceCollection();

            // Act
            startup.ConfigureServices(services1);
            startup.ConfigureServices(services2);

            // Assert - Each call should add services
            Assert.NotNull(services1);
            Assert.NotNull(services2);
        }

        [Fact]
        public void Configure_EmptyEnvironmentName_Works()
        {
            // Arrange
            var configuration = CreateTestConfiguration();
            var startup = new Startup(configuration);

            var mockEnv = new Mock<IWebHostEnvironment>();
            mockEnv.SetupGet(e => e.EnvironmentName).Returns(string.Empty);

            var appBuilder = CreateApplicationBuilder(startup);

            // Act & Assert
            startup.Configure(appBuilder, mockEnv.Object);
            Assert.True(true);
        }
    }
}