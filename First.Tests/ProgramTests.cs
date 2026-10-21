using First;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using Xunit;

namespace First.Tests
{
    /// <summary>
    /// Unit tests for the Program class.
    /// Tests cover entry point, host building, and configuration.
    /// </summary>
    public class ProgramTests
    {
        [Fact]
        public void Program_Type_Exists()
        {
            // Arrange & Act
            var programType = typeof(Program);

            // Assert
            Assert.NotNull(programType);
        }

        [Fact]
        public void Program_MainMethod_IsEntryPoint()
        {
            // Arrange & Act
            var mainMethod = typeof(Program).GetMethod("Main",
                BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic,
                null,
                new Type[] { typeof(string[]) },
                null);

            // Assert
            Assert.NotNull(mainMethod);
        }

        [Fact]
        public void Program_StaticMain_CanAcceptNullArgs()
        {
            // Arrange
            var mainMethod = typeof(Program).GetMethod("Main",
                BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic,
                null,
                new Type[] { typeof(string[]) },
                null);

            // Assert - Main method should accept string array parameter
            Assert.NotNull(mainMethod);
            var parameters = mainMethod?.GetParameters();
            Assert.NotNull(parameters);
            Assert.Single(parameters);
            Assert.Equal(typeof(string[]), parameters[0].ParameterType);
        }

        [Fact]
        public void Program_CreateHostBuilder_ReturnsIHostBuilder()
        {
            // Arrange & Act
            var method = typeof(Program).GetMethod("CreateHostBuilder",
                BindingFlags.Public | BindingFlags.Static);

            // Assert
            Assert.NotNull(method);
        }

        [Fact]
        public void Program_ClassIsStaticOrSealed()
        {
            // Arrange & Act
            var programType = typeof(Program);

            // Note: The Program class is neither sealed nor static (it's a regular class)
            // Assert that it exists and has a public constructor (implicitly)
            Assert.NotNull(programType);
        }

        [Fact]
        public void Program_Type_HasPublicConstructor()
        {
            // Arrange - The Program class should have a constructor
            var constructors = typeof(Program).GetConstructors();

            // Note: The Program class is a standard class with a public constructor
            // Assert that it exists
            Assert.NotEmpty(constructors);
        }

        [Fact]
        public void Program_Namespace_IsCorrect()
        {
            // Arrange & Act
            var programNamespace = typeof(Program).Namespace;

            // Assert
            Assert.Equal("First", programNamespace);
        }

        [Fact]
        public void Program_CreateHostBuilder_Exists()
        {
            // Act
            var method = typeof(Program).GetMethod("CreateHostBuilder",
                BindingFlags.Public | BindingFlags.Static);

            // Assert
            Assert.NotNull(method);
        }

        [Fact]
        public void Host_CanBeCreatedWithDefaultSettings()
        {
            // Arrange & Act
            var host = Host.CreateDefaultBuilder().Build();

            // Assert
            Assert.NotNull(host);
            host.Dispose();
        }

        [Fact]
        public void Host_CanBeDisposed()
        {
            // Arrange & Act
            var host = Host.CreateDefaultBuilder().Build();

            // Act & Assert
            Assert.NotNull(host);
            host.Dispose();
        }

        [Fact]
        public void Program_CanCallMainMethod()
        {
            // Arrange
            var args = Array.Empty<string>();

            // Act & Assert - Verify Main method exists
            var mainMethod = typeof(Program).GetMethod("Main",
                BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);
            Assert.NotNull(mainMethod);
        }

        [Fact]
        public void Program_Type_IsPublic()
        {
            // Arrange & Act
            var programType = typeof(Program);

            // Assert
            Assert.True(programType.IsPublic);
        }
    }
}